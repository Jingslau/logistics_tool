using System.Globalization;
using System.Windows.Controls;

namespace waerp_management.errorHandling
{

    public class ValidationRules : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((value ?? "").ToString())
                ? new ValidationResult(false, "Benutzername muss eingegeben werden")
                : ValidationResult.ValidResult;
        }
    }
}
