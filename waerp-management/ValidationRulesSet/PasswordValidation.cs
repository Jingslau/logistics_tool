using System.Globalization;
using System.Windows.Controls;

namespace waerp_management.ValidationRulesSet
{
    public class PasswordValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((value ?? "").ToString())
                ? new ValidationResult(false, "Passwort muss eingegeben werden!")
                : ValidationResult.ValidResult;
        }
    }
}
