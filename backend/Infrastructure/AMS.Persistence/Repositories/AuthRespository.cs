using AMS.Application.Contracts;
using AMS.Application.Features.Auth;
using AMS.Doman.Entities;
using AMS.Persistence.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AMS.Persistence.Repositories
{
    public class AuthRespository : IAuthRespository
    {
        private readonly JwtSettings _jwtOptions;
        public AuthRespository(IOptionsSnapshot<JwtSettings> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public TokenResult GenerateAccessToken(User user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, roleName)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.Lifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SigningKey)), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)

            };

            var SecurityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(SecurityToken);

            var tokenResult = new TokenResult
            {
                AccessToken = accessToken,
                ExpiresOn = DateTime.UtcNow.AddMinutes(_jwtOptions.Lifetime),
            };

            return tokenResult;
        }

    }
}
