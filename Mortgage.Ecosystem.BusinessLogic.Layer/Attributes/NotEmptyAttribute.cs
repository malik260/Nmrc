using System.ComponentModel.DataAnnotations;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Attributes
{
    internal class NotEmptyAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is Guid guid)
            {
                if (guid != Guid.Empty)
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult("Value cannot be empty.");
            }
            return ValidationResult.Success;
        }
    }
}
