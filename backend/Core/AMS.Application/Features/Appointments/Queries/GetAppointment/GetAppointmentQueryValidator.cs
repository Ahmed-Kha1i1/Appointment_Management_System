using AMS.Application.Common.Response;
using AMS.Application.Common.Validators;
using AMS.Application.Features.Appointments.Commands.UpdateAppointment;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Appointments.Queries.GetAppointment
{
    public class GetAppointmentQueryValidator : AbstractValidator<GetAppointmentQuery>
    {
        public GetAppointmentQueryValidator()
        {
            RuleFor(x => x.AppointmentId)
                .GreaterThanZero();
        }
    }
}
