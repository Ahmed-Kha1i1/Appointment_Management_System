using AMS.Application.Common.Validators;
using AMS.Application.Features.Doctors.Commands.AddDoctor;
using FluentValidation;

namespace AMS.Application.Features.Doctors.Commands.UpdateDoctor
{
    public class UpdateDoctorCommandValidator : AbstractValidator<UpdateDoctorCommand>
    {
        public UpdateDoctorCommandValidator()
        {
            RuleFor(x => x.DoctorId).GreaterThan(0);
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50).WithMessage("FirstName cannot exceed 50 characters."); ;
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50).WithMessage("LastName cannot exceed 50 characters."); ;
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.SpecializationId).GreaterThanZero();
            
        }
    }
}