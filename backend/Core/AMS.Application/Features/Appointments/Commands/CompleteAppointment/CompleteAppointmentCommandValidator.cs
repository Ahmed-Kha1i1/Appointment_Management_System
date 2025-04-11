using AMS.Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Appointments.Commands.CompleteAppointment
{
    public class CompleteAppointmentCommandValidator : AbstractValidator<CompleteAppointmentCommand>
    {
        public CompleteAppointmentCommandValidator()
        {
            RuleFor(x => x.AppointmentId)
                .GreaterThanZero();
        }
    }
}
