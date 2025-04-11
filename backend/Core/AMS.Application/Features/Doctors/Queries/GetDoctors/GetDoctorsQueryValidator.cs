using AMS.Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Doctors.Queries.GetDoctors
{
    public class GetDoctorsQueryValidator  :AbstractValidator<GetDoctorsQuery>
    {
        public GetDoctorsQueryValidator()
        {
            RuleFor(x => x.SearchQuery)
            .MaximumLength(100).WithMessage("Search query cannot exceed 100 characters")
           .When(x => !string.IsNullOrEmpty(x.SearchQuery));

            RuleFor(x => x.SpecializationId)
                .GreaterThanZero()
                .When(s => s.SpecializationId is not null);

            // Validate OrderDirection
            RuleFor(x => x.OrderDirection)
                .NotEmpty().WithMessage("Order direction is required")
                .Must(x => x.ToLower() == "asc" || x.ToLower() == "desc")
                .WithMessage("Order direction must be either 'asc' or 'desc'");

            // Validate OrderBy
            RuleFor(x => x.OrderBy)
                .NotEmpty().WithMessage("Order by field is required")
                .Must(x => x.ToLower() == "id" || x.ToLower() == "name")
                .WithMessage("Order by must be either 'id' or 'name'");
        }

    }
}
