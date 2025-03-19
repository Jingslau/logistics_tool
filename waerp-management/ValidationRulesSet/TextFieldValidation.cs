using System.Globalization;
using System.Windows.Controls;

namespace waerp_management.ValidationRulesSet
{
    public class TextFieldValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((value ?? "").ToString())
                ? new ValidationResult(false, "Das Feld darf nicht leer sein!")
                : ValidationResult.ValidResult;
        }
    }
}
