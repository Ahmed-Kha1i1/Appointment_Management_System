using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AMS.Application.Common.Validators
{
    public class PasswordValidator<T> : AsyncPropertyValidator<T, string> where T : class
    {
        public override string Name => "PasswordValidator";

        public override Task<bool> IsValidAsync(ValidationContext<T> context, string value, CancellationToken cancellation)
        {
            var passwordRegex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,20}$");
            
            // Validate the password against the regex
            var isValid = passwordRegex.IsMatch(value);

            return Task.FromResult(isValid);
        }
    }
}
