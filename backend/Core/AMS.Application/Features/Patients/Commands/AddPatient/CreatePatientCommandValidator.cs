using AMS.Application.Features.Doctors.Commands.AddDoctor;
using FluentValidation;

namespace AMS.Application.Features.Patients.Commands.CreatePatient
{
    public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50).WithMessage("FirstName cannot exceed 50 characters."); ;
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50).WithMessage("LastName cannot exceed 50 characters."); ;
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.BirthDate).NotEmpty().LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("BirthDate cannot be in the future.");
            RuleFor(x => x.Gender).IsInEnum();
            RuleFor(x => x.Password).NotEmpty().SetAsyncValidator(new Common.Validators.PasswordValidator<CreatePatientCommand>());
        }
    }
}