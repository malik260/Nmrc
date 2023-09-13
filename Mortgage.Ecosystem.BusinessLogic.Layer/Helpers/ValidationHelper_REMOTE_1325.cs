using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Helpers
{
    public class ValidationHelper
    {
        public static bool ValidateNin(string nin)
        {
            nin = nin.Replace(" ", "");

            if (nin.Length != 11)
            {
                return false;
            }

            var chars = nin.ToCharArray();

            var checkDigit = GetNinCheckDigit(chars[new Range(1, 12)]);

            return chars[0] == checkDigit;
        }

        public static char GetNinCheckDigit(string baseNin)
        {
            return GetNinCheckDigit(baseNin.ToCharArray());
        }

        public static char GetNinCheckDigit(char[] baseNin)
        {
            if (baseNin.Length != 11)
            {
                throw new ArgumentException("Please enter the base Nin only", nameof(baseNin));
            }

            var alpha = new[]
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'T', 'U', 'V', 'W', 'X',
                'Y', 'Z'
            };

            var check = 0;

            for (var i = 1; i < baseNin.Length; i++)
            {
                if (int.TryParse(baseNin[i].ToString(), out var x))
                {
                    var n = x * (i + 1);
                    check += n;
                }
            }

            var alphaIndex = check % 23;

            return alpha[alphaIndex];
        }

        public static bool ValidateNhsNumber(string nhsNumber)
        {
            nhsNumber = nhsNumber.Replace(" ", "");

            if (nhsNumber.Length != 10)
            {
                return false;
            }

            if (!int.TryParse(nhsNumber[9].ToString(), out var checkDigit))
            {
                return false;
            }

            var result = 0;

            for (var i = 0; i < 9; i++)
            {
                if (int.TryParse(nhsNumber[i].ToString(), out var digitValue))
                {
                    result += digitValue * (10 - i);
                }
                else
                {
                    return false;
                }
            }

            var validationResult = 11 - result % 11;

            if (validationResult == 10) return false;

            return validationResult == 11 ? checkDigit == 0 : checkDigit == validationResult;
        }

        public static void ValidateModel<T>(T model)
        {
            var validationContext = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(model, validationContext, results, true))
            {
                var errors = results.Select(x => x.ErrorMessage).ToArray();
                var message = string.Join(Environment.NewLine, errors);
                throw new InvalidDataException(message);
            }
        }

        public static bool ValidateBvn(string bvnNumber)
        {
            bvnNumber = bvnNumber.Replace(" ", "");

            if (bvnNumber.Length != 11)
            {
                return false;
            }

            if (!ValidatorHelper.IsNumeric(bvnNumber))
            {
                return false;
            }

            return true;
        }

        public static bool ValidateAccountNumber(string accountNumber)
        {
            accountNumber = accountNumber.Replace(" ", "");

            if (accountNumber.Length != 10)
            {
                return false;
            }

            if (!ValidatorHelper.IsNumeric(accountNumber))
            {
                return false;
            }

            return true;
        }
    }
}
