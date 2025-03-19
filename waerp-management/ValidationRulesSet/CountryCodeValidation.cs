using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace waerp_management.ValidationRulesSet
{
    internal class CountryCodeValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string countryCode = value as string;

            if (string.IsNullOrWhiteSpace(countryCode))
                return new ValidationResult(false, "Ländercode wird benötigt.");

            // Regex pattern for validating three-letter country codes
            string countryCodePattern = @"^[A-Za-z]{3}$";

            Regex regex = new Regex(countryCodePattern);

            if (!regex.IsMatch(countryCode))
                return new ValidationResult(false, "Ungültiger Ländercode.");

            // You can further validate the country code against a list of known ISO codes if needed

            return ValidationResult.ValidResult;
        }
    }
}
