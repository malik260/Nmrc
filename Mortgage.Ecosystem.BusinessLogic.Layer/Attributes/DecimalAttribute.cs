using Mortgage.Ecosystem.BusinessLogic.Layer.Constants;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Attributes
{
    internal class DecimalAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var stringValue = value?.ToString() ?? string.Empty;

            if (Regex.IsMatch(stringValue, RegularExpressions.Decimal))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Value must be a valid decimal.");
        }
    }
}
