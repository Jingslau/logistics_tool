using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace waerp_management.ValidationRulesSet
{
    public class EMailValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string email = value as string;

            if (string.IsNullOrWhiteSpace(email))
                return new ValidationResult(false, "E-Mail Adresse wird benötigt.");

            // Regex pattern for validating email addresses
            string emailPattern = @"^[\w\.-]+@([\w-]+\.)+[\w-]{2,4}$";

            Regex regex = new Regex(emailPattern);

            if (!regex.IsMatch(email))
                return new ValidationResult(false, "Ungültige E-Mail Adresse.");

            return ValidationResult.ValidResult;
        }
    }
}
