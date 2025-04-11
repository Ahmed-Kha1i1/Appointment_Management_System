using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Appointments.Queries.GetAppointments
{
    public class GetAppointmentsQueryValidator : AbstractValidator<GetAppointmentsQuery>
    {
        public GetAppointmentsQueryValidator()
        {
            RuleFor(a => a.StartDate).LessThanOrEqualTo(a => a.EndDate)
                .WithMessage("Start date must be less than end date.");

        }
    }
}
