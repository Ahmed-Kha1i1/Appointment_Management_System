using AMS.Application.Common.Extensions;
using AMS.Application.Common.Models;
using AMS.Application.Common.Response;
using AMS.Application.Contracts;
using AMS.Application.Contracts.Persistence;
using AMS.Doman.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AMS.Application.Features.Auth.Commands.LoginCommand
{
    public class LoginCommandHandler
        (IUserRepository userRepository, IAuthRespository authRespository, 
        IRefreshTokenRepository refreshTokenRepository, IRoleRepository roleRepository, IOptionsSnapshot<RecaptchaSettings> recaptchaSettingsOptions) 
        : ResponseHandler, IRequestHandler<LoginCommand, Response<AuthDTO>>
    {
        public async Task<Response<AuthDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var recaptchaSettings = recaptchaSettingsOptions.Value;
            if (!string.IsNullOrEmpty(request.Recaptcha))
            {
                var client = new HttpClient();
                var result = await client.PostAsync($"{recaptchaSettings.URL}?secret={recaptchaSettings.SecretKey}&response={request.Recaptcha}", null);
                var responseBody = await result.Content.ReadAsStringAsync();

                var recaptchaResult = JsonConvert.DeserializeObject<CaptchaResponse>(responseBody);
                if (recaptchaResult == null || !recaptchaResult.Success)
                    return BadRequest<AuthDTO>("Invalid captcha");
            }

            var hashedPassword = request.Password.ComputeHash();

            var user = await userRepository.GetAsync(request.Email, hashedPassword, (int)request.RoleId);

            if (user == null)
            {
                return BadRequest<AuthDTO>("Email or password is incorrect!");
            }

            var role = await roleRepository.GetByIdAsync((int)request.RoleId);
            if (role is null)
            {
                return BadRequest<AuthDTO>("Invalid role");
            }
            var tokenResult = authRespository.GenerateAccessToken(user, role.Name);
            

            AuthDTO AuthInfo = new AuthDTO
            {
                AccessToken = tokenResult.AccessToken,
                UserId = user.Id,
                ExpiresOn = tokenResult.ExpiresOn,
                RoleId = request.RoleId
            };


            var ActiveRefreshToken = await refreshTokenRepository.GetActiveRefreshToken(user.Id);

            if (ActiveRefreshToken != null)
            {
                AuthInfo.RefreshToken = ActiveRefreshToken.Token;
                AuthInfo.RefreshTokenExpiration = ActiveRefreshToken.ExpiresOn;
            }
            else
            {
                var refreshToken = refreshTokenRepository.GenerateRefreshToken(user.Id);

                await refreshTokenRepository.AddAsync(refreshToken);

                AuthInfo.RefreshToken = refreshToken.Token;
                AuthInfo.RefreshTokenExpiration = refreshToken.ExpiresOn;

                await refreshTokenRepository.SaveChangesAsync();
            }
            
            return Success(AuthInfo);
        }
    }
}

public class CaptchaResponse
{
    public bool Success { get; set; }
}