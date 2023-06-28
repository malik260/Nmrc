using Mortgage.Ecosystem.BusinessLogic.Layer.Constants;
using System.ComponentModel.DataAnnotations;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Attributes
{
    internal class GenderAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string stringValue)
            {
                var validValues = new string[] { Gender.Male, Gender.Female, Gender.Other, Gender.Unknown };

                if (validValues.Contains(stringValue))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("Gender is invalid.");
        }
    }
}