
using AMS.Application.Common.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;
namespace AMS.Application.Common.Validators
{
    public class ValidationResultFactory : ResponseHandler, IFluentValidationAutoValidationResultFactory
    {

        public IActionResult CreateActionResult(ActionExecutingContext context, ValidationProblemDetails? validationProblemDetails)
        {
            var firstErrors = new Dictionary<string, string>();

            foreach (var error in validationProblemDetails.Errors)
            {
                // Take the first error message for each key
                if (error.Value != null && error.Value.Length > 0)
                {
                    firstErrors[error.Key] = error.Value.First();
                }
            }
            var response = ValidationError(firstErrors, "One or more Valodation error occurred.");
            var result = new ObjectResult(response)
            {
                StatusCode = (int)response.StatusCode
            };

            return result;
        }
    }
}

