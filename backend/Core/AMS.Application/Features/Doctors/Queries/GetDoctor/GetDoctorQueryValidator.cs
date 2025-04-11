using AMS.Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Doctors.Queries.GetDoctor
{
    public class GetDoctorQueryValidator : AbstractValidator<GetDoctorQuery>
    {
        public GetDoctorQueryValidator()
        {
            RuleFor(x => x.DoctorId)
                .GreaterThanZero();
        }
    }
    
}
