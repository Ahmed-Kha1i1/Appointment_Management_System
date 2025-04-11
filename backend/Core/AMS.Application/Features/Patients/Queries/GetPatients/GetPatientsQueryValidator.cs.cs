using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Patients.Queries.GetPatients
{
    public class GetPatientsQueryValidator : AbstractValidator<GetPatientsQuery>
    {
        public GetPatientsQueryValidator()
        {
            RuleFor(x => x.SearchQuery)
           .MaximumLength(100).WithMessage("Search query cannot exceed 100 characters")
           .When(x => !string.IsNullOrEmpty(x.SearchQuery));

            // Validate OrderDirection
            RuleFor(x => x.OrderDirection)
                .NotEmpty().WithMessage("Order direction is required")
                .Must(x => x.ToLower() == "asc" || x.ToLower() == "desc")
                .WithMessage("Order direction must be either 'asc' or 'desc'");

            RuleFor(x => x.Gender)
                .IsInEnum().
                When(x => x.Gender != null);


            // Validate OrderBy
            RuleFor(x => x.OrderBy)
                .NotEmpty().WithMessage("Order by field is required")
                .Must(x => x.ToLower() == "id" || x.ToLower() == "name")
                .WithMessage("Order by must be either 'id' or 'name'");
        }
    }
}
