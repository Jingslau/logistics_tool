using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace waerp_management.ValidationRulesSet
{
    public class PhonenumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string phoneNumber = value as string;

            if (string.IsNullOrWhiteSpace(phoneNumber))
                return new ValidationResult(false, "Telefonnummer wird benötigt");

            // Replace "+" with "00" in the phone number
            phoneNumber = phoneNumber.Replace("+", "00");

            // Regex pattern for validating phone numbers
            string phonePattern = @"^00\d{2,}$";

            Regex regex = new Regex(phonePattern);

            if (!regex.IsMatch(phoneNumber))
                return new ValidationResult(false, "Ungültige Rufnummer");

            return ValidationResult.ValidResult;
        }
    }
}
