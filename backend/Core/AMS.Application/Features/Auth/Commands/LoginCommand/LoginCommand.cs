using AMS.Application.Common.Enums;
using AMS.Application.Common.Response;
using MediatR;

namespace AMS.Application.Features.Auth.Commands.LoginCommand
{
    public class LoginCommand : IRequest<Response<AuthDTO>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Recaptcha { get; set; }
        public enRole RoleId { get; set; }
    }
}


