using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace waerp_management.errorHandling
{
    internal class TextFieldRules
    {
        public static bool OnlyNumbers(string input, System.Windows.Input.TextCompositionEventArgs e)
        {

            // Use regular expression to check if input is numeric
            Regex regex = new Regex("[^0-9]+");
            bool isNumeric = !regex.IsMatch(input);

            return !isNumeric;
        }

        public static bool VerifyEmail(string input, TextBox textBox)
        {
            // Check if input is "@" and if "@" already exists in the TextBox text
            if (input == "@" && textBox.Text.Contains("@"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool VerifyMoneyInput(TextCompositionEventArgs e, TextBox textBox)
        {
            if (!IsValidNumericInput(e.Text) && !IsValidDecimalSeparatorInput(e.Text, textBox))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static bool IsValidNumericInput(string input)
        {
            // Check if the input is a numeric character (0-9)
            return Regex.IsMatch(input, @"^[0-9]$");
        }

        private static bool IsValidDecimalSeparatorInput(string input, TextBox textBox)
        {
            // Check if the input is a valid decimal separator (either a point or comma)
            bool isDecimalSeparator = input == "." || input == ",";

            // Validate that there is at most one decimal separator in the text box
            bool hasDecimalSeparator = textBox.Text.Contains(".") || textBox.Text.Contains(",");
            if (isDecimalSeparator && hasDecimalSeparator)
            {
                return false;
            }

            // Validate that the input is placed after any numeric characters and that there are at most two decimal places
            int caretIndex = textBox.CaretIndex;
            string newText = textBox.Text.Insert(caretIndex, input);

            return Regex.IsMatch(newText, @"^\d+(\.|,)?\d{0,2}$");
        }
    }
}
