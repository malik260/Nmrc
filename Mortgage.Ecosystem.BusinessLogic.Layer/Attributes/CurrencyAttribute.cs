using System.ComponentModel.DataAnnotations;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Attributes
{
    internal class CurrencyAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if ((decimal?)value >= 0m)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Amount cannot be less than 0.00.");
        }
    }
}
