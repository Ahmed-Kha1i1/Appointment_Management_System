using AMS.Application.Common.Enums;
using AMS.Application.Common.Response;
using AMS.Doman.Entities;
using MediatR;

namespace AMS.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<Response<AuthDTO>>
    {
        public string Token { get; set; }
        public enRole RoleId { get; set; }

        public RefreshTokenCommand(string token, enRole role)
        {
            Token = token;
            RoleId = role;
        }
    }
}
