using System.ComponentModel.DataAnnotations;
using Mortgage.Ecosystem.BusinessLogic.Layer.Helpers;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Attributes
{
    internal class NhsNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (ValidationHelper.ValidateNhsNumber((string)value))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("NHS number is invalid.");
        }
    }
}