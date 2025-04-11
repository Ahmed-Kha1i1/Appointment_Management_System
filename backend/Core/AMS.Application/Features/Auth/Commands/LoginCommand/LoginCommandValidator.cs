using FluentValidation;

namespace AMS.Application.Features.Auth.Commands.LoginCommand
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(l => l.Email).NotEmpty().EmailAddress();
            RuleFor(l => l.Password).NotEmpty();
            RuleFor(l => l.RoleId).NotEmpty().IsInEnum();

            RuleFor(l => l.Recaptcha)
            .NotEmpty()
            .When(l => (int)l.RoleId != 1) 
            .WithMessage("CAPTCHA verification is required.");
        }
    }
}
