using AMS.Application.Common.Validators;
using AMS.Application.Features.Doctors.Commands.AddDoctor;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace AMS.Application.Features.Doctors.Commands.CreateDoctor
{
    public class CreateDoctorCommandValidator : AbstractValidator<CreateDoctorCommand>
    {
        public CreateDoctorCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50).WithMessage("FirstName cannot exceed 50 characters."); ;
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50).WithMessage("LastName cannot exceed 50 characters."); ;
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().SetAsyncValidator(new Common.Validators.PasswordValidator<CreateDoctorCommand>());
            RuleFor(x => x.SpecializationId).GreaterThanZero();
        }
    }
}
