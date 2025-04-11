using AMS.Application.Common.Response;
using MediatR;

namespace AMS.Application.Features.Auth.Commands.RevokeToken
{
    public class RevokeTokenCommand : IRequest<Response<bool>>
    {
        public string Token { get; set; }

        public RevokeTokenCommand(string token)
        {
            Token = token;
        }
    }
}
