using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace waerp_management.ValidationRulesSet
{
    public class MoneyValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string price = value as string;

            if (string.IsNullOrWhiteSpace(price))
                return new ValidationResult(false, "Es wird ein Preis benötigt (falls nicht vorhanden dann 0,00).");

            // Regex pattern for validating prices with comma or point and 2 decimal places
            string pricePattern = @"^\d+(\.|,)\d{2}$";

            Regex regex = new Regex(pricePattern);

            if (!regex.IsMatch(price))
                return new ValidationResult(false, "Ungültiges Format (XX,XX).");

            return ValidationResult.ValidResult;
        }
    }
}
