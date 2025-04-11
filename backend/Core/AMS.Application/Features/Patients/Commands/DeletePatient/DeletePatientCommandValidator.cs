using AMS.Application.Common.Validators;
using FluentValidation;

namespace AMS.Application.Features.Patients.Commands.DeletePatient
{
    public class DeletePatientCommandValidator : AbstractValidator<DeletePatientCommand>
    {
        public DeletePatientCommandValidator()
        {
            RuleFor(x => x.PatientId).GreaterThanZero();
        }
    }
}