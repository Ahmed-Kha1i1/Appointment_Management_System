using AMS.Application.Common.Validators;
using FluentValidation;

namespace AMS.Application.Features.Appointments.Commands.DeleteAppointment
{
    public class DeleteAppointmentCommandValidator : AbstractValidator<DeleteAppointmentCommand>
    {
        public DeleteAppointmentCommandValidator()
        {
            RuleFor(x => x.AppointmentId)
                .GreaterThanZero();
        }
    }
}