using AMS.Application.Common.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Appointments.Commands.AddAppointment
{


    public class AddAppointmentCommandValidator : AbstractValidator<AddAppointmentCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddAppointmentCommandValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            RuleFor(x => x.DoctorId)
                .GreaterThanZero();

            RuleFor(x => x.PatientId)
                .GreaterThanZero()
                .Must((command, patientId) => PatientIdMatchesClaim(patientId))
                .WithMessage("You can only create appointments for yourself");

            RuleFor(x => x.AppointmentDate)
                .NotEmpty().WithMessage("Appointment date is required")
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
                .WithMessage("Appointment date must be today or in the future.");

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("Start time is required")
                .Must((command, startTime) => BeValidStartTime(command.AppointmentDate, startTime))
                .WithMessage("Start time cannot be in the past for today's appointments");
        }

        private bool PatientIdMatchesClaim(int patientId)
        {
            // Get the NameIdentifier claim (user ID) from the JWT
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var RoleClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;

            //if user is admin so he book appointment to any patient.
            if(!string.IsNullOrEmpty(userIdClaim) && RoleClaim == Roles.Roles.Admin)
            {
                return true;
            }

            if (string.IsNullOrEmpty(userIdClaim) || !long.TryParse(userIdClaim, out var userId))
            {
                return false;
            }

            return patientId == userId;
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
