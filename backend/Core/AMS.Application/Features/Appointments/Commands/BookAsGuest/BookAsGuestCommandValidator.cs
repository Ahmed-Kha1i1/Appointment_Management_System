using AMS.Application.Common.Validators;
using FluentValidation;

namespace AMS.Application.Features.Appointments.Commands.BookAsGuest
{
    public class BookAsGuestCommandValidator  :AbstractValidator<BookAsGuestCommand>
    {
        public BookAsGuestCommandValidator()
        {
            RuleFor(x => x.DoctorId)
               .GreaterThanZero();

            RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.AppointmentDate)
                .NotEmpty().WithMessage("Appointment date is required")
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
                .WithMessage("Appointment date must be today or in the future.");

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("Start time is required")
                .Must((command, startTime) => BeValidStartTime(command.AppointmentDate, startTime))
                .WithMessage("Start time cannot be in the past for today's appointments");
        }
        private bool BeValidStartTime(DateOnly appointmentDate, TimeOnly startTime)
        {
            // If appointment is not today, the time is automatically valid
            if (appointmentDate != DateOnly.FromDateTime(DateTime.Now))
            {
                return true;
            }
            // For today's appointments, check if time is in the future
            var currentTime = TimeOnly.FromDateTime(DateTime.Now);
            return startTime >= currentTime;
        }
    }
}
