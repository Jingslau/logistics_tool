using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace waerp_management.ValidationRulesSet
{
    public class PostcodeValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string postalCode = value as string;

            if (string.IsNullOrWhiteSpace(postalCode))
                return new ValidationResult(false, "Postleitzahl wird benötigt.");

            // Regex pattern for validating postal codes
            string postalPattern = @"^\d{5}$";

            Regex regex = new Regex(postalPattern);

            if (!regex.IsMatch(postalCode))
                return new ValidationResult(false, "Ungültige Postleitzahl.");

            return ValidationResult.ValidResult;
        }
    }
}
