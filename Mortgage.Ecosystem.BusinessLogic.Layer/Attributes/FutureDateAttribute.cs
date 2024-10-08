﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Attributes
{
    internal class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var date = (DateTime?)value;

            if (date != null && date < DateTime.Today)
            {
                return new ValidationResult("Date cannot be in the past.");
            }
            return ValidationResult.Success;
        }
    }
}