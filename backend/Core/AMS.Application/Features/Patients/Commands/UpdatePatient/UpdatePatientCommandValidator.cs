using AMS.Application.Features.Patients.Commands.CreatePatient;
using FluentValidation;

namespace AMS.Application.Features.Patients.Commands.UpdatePatient
{
    public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
    {
        public UpdatePatientCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50).WithMessage("FirstName cannot exceed 50 characters.");
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50).WithMessage("LastName cannot exceed 50 characters.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.BirthDate).NotEmpty().LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("BirthDate cannot be in the future.");
            RuleFor(x => x.Gender).IsInEnum();
        }
    }
}