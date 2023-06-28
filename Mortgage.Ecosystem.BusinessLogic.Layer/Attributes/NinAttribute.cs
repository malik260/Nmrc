using Mortgage.Ecosystem.BusinessLogic.Layer.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Attributes
{
    internal class NinAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (ValidationHelper.ValidateNin((string?)value))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("NIN is invalid.");
        }
    }
}
