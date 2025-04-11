using AMS.Application.Common.Response;
using AMS.Application.Contracts;
using AMS.Application.Contracts.Persistence;
using MediatR;

namespace AMS.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository, IAuthRespository authRespository, IRoleRepository roleRepository)
        : ResponseHandler, IRequestHandler<RefreshTokenCommand, Response<AuthDTO>>
    {
        public async Task<Response<AuthDTO>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Token))
            {
                return BadRequest<AuthDTO>("Invalid Token");
            }
            var refreshToken = await refreshTokenRepository.GetWithUserAsync(request.Token);

            if (refreshToken is null || !refreshToken.IsActive)
            {
                return BadRequest<AuthDTO>("Invalid Token");
            }
            var role = await roleRepository.GetByIdAsync((int)request.RoleId);

            if (role is null)
            {
                return BadRequest<AuthDTO>("Invalid Role");
            }

            refreshToken.RevokedOn = DateTime.UtcNow;
            await refreshTokenRepository.SaveChangesAsync();

            var newRefreshToken = refreshTokenRepository.GenerateRefreshToken(refreshToken.userId);
            await refreshTokenRepository.AddAsync(newRefreshToken);

            var tokenResult = authRespository.GenerateAccessToken(refreshToken.User, role.Name);

            AuthDTO authDto = new AuthDTO
            {
                AccessToken = tokenResult.AccessToken,
                UserId = refreshToken.userId,
                ExpiresOn = tokenResult.ExpiresOn,
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiration = newRefreshToken.ExpiresOn,
                RoleId = request.RoleId,
            };

            return Success(authDto);
        }
    }
}
