using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using waerp_management.errorHandling;
using waerp_management.sql;
using waerp_management.store.Administration;

namespace waerp_management.application.Administration.ItemAdministration
{
    /// <summary>
    /// Interaction logic for EditSelectedItemWindow.xaml
    /// </summary>
    public partial class EditSelectedItemWindow : Window
    {
        public string ImagePathStr;
        public EditSelectedItemWindow()
        {

            InitializeComponent();
            ItemIdent.Text = CurrentItemAdministrationModel.SelectedItem["item_ident"].ToString();
            ImagePathStr = CurrentItemAdministrationModel.SelectedItem["item_image_path"].ToString();

            string[] ImagePathArr = ImagePathStr.Split('/');
            if (ImagePathArr[ImagePathArr.Length - 1] == "default.jpg")
            {
                Uri imageUri = new Uri(ImagePathStr, UriKind.Relative);

                ItemImage.Source = new BitmapImage(imageUri);
            }
            else
            {
                Uri imageUri = new Uri(ImagePathStr);

                ItemImage.Source = new BitmapImage(imageUri);
                ImagePath.Text = ImagePathStr;
            }


            for (int i = 1; i <= 5; i++)
            {
                DataSet ds = AdministrationQueries.GetFilterList(i);
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    if (i == 1)
                    {
                        Filter1.Items.Add(ds.Tables[0].Rows[j]["name"].ToString());
                    }
                    else if (i == 2)
                    {
                        Filter2.Items.Add(ds.Tables[0].Rows[j]["name"].ToString());
                    }
                    else if (i == 3)
                    {
                        Filter3.Items.Add(ds.Tables[0].Rows[j]["name"].ToString());

                    }
                    else if (i == 4)
                    {
                        Filter4.Items.Add(ds.Tables[0].Rows[j]["name"].ToString());
                    }
                    else if (i == 5)
                    {
                        Filter5.Items.Add(ds.Tables[0].Rows[j]["name"].ToString());
                    }

                }

                if (CurrentItemAdministrationModel.SelectedItem["item_orderable"].ToString() == "True")
                {
                    MinQuant_Text.Text = CurrentItemAdministrationModel.SelectedItem["item_quantity_min"].ToString();
                    MinOrder_Text.Text = CurrentItemAdministrationModel.SelectedItem["item_orderquant_min"].ToString();
                    OrderCheckbox.IsChecked = true;
                    ds = ItemAdministrationQueries.RunSql($"SELECT * FROM item_vendor_relations WHERE item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"].ToString()}");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        CurrencyCombobox.SelectedIndex = int.Parse(ds.Tables[0].Rows[0]["currency_id"].ToString());
                        price_Text.Text = ds.Tables[0].Rows[0]["item_price"].ToString();
                        VendorCombobox.SelectedIndex = int.Parse(ds.Tables[0].Rows[0]["vendor_id"].ToString());

                    }
                }




                ds = ItemAdministrationQueries.RunSql("SELECT * FROM currency");
                int defaultIndexCurrency = -1;
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    CurrencyCombobox.Items.Add(ds.Tables[0].Rows[j]["currency_code"].ToString());
                    if (ds.Tables[0].Rows[j]["currency_isDefault"].ToString() == "True")
                    {
                        defaultIndexCurrency = j;
                    }
                }


                CurrencyCombobox.SelectedIndex = defaultIndexCurrency;

            }
            DataSet ds4 = ItemAdministrationQueries.RunSql("SELECT * FROM vendor_objects");
            for (int k = 0; k < ds4.Tables[0].Rows.Count; k++)
            {
                VendorCombobox.Items.Add(ds4.Tables[0].Rows[k]["vendor_name"].ToString());
            }


            int FilterCounter = 1;
            bool stop = false;
            string FilterName = ItemAdministrationQueries.GetFilterName(FilterCounter, CurrentItemAdministrationModel.SelectedItem["item_id"].ToString());

            if (FilterName != "")
            {
                for (int i = 0; i < Filter1.Items.Count; i++)
                {
                    if (Filter1.Items[i].ToString() == FilterName)
                    {
                        Filter1.SelectedIndex = i;
                        FilterCounter++;

                        break;
                    }
                }
                Filter2.IsEnabled = true;
            }
            else
            {

                stop = true;
            }

            FilterName = ItemAdministrationQueries.GetFilterName(FilterCounter, CurrentItemAdministrationModel.SelectedItem["item_id"].ToString());

            if (FilterName != "" && !stop)
            {
                for (int i = 0; i < Filter2.Items.Count; i++)
                {
                    if (Filter2.Items[i].ToString() == FilterName)
                    {
                        Filter2.SelectedIndex = i;
                        FilterCounter++;
                        Filter3.IsEnabled = true;
                        break;
                    }
                }
            }

            FilterName = ItemAdministrationQueries.GetFilterName(FilterCounter, CurrentItemAdministrationModel.SelectedItem["item_id"].ToString());

            if (FilterName != "" && !stop)
            {
                for (int i = 0; i < Filter3.Items.Count; i++)
                {
                    if (Filter3.Items[i].ToString() == FilterName)
                    {
                        Filter3.SelectedIndex = i;
                        FilterCounter++;
                        Filter4.IsEnabled = true;
                        break;
                    }
                }
            }


            FilterName = ItemAdministrationQueries.GetFilterName(FilterCounter, CurrentItemAdministrationModel.SelectedItem["item_id"].ToString());

            if (FilterName != "" && !stop)
            {
                for (int i = 0; i < Filter4.Items.Count; i++)
                {
                    if (Filter4.Items[i].ToString() == FilterName)
                    {
                        Filter4.SelectedIndex = i;
                        FilterCounter++;
                        Filter5.IsEnabled = true;
                        break;
                    }
                }
            }

            FilterName = ItemAdministrationQueries.GetFilterName(FilterCounter, CurrentItemAdministrationModel.SelectedItem["item_id"].ToString());

            if (FilterName != "" && !stop)
            {
                for (int i = 0; i < Filter5.Items.Count; i++)
                {
                    if (Filter5.Items[i].ToString() == FilterName)
                    {
                        Filter5.SelectedIndex = i;
                        FilterCounter++;
                        break;
                    }
                }
            }


            ItemDescription2.Text = CurrentItemAdministrationModel.SelectedItem["item_description_2"].ToString();
            ItemDescription.Text = CurrentItemAdministrationModel.SelectedItem["item_description"].ToString();
        }



        private void CloseDialog_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            bool EditImage = false;
            string destinationFilePath = "";
            //if (ImagePath.Text != ImagePathStr | ImagePath.Text != "")
            //{
            //    ItemImage.Source = null;
            //    ItemImage.InvalidateVisual();

            //    EditImage = true;
            //    DataSet globalImagePath = AdministrationQueries.RunSql($"SELECT * FROM company_settings WHERE settings_name = 'global_image_path'");
            //    string sourcePath = $@"{ImagePath.Text}";
            //    string destinationFolderPath = $@"{globalImagePath.Tables[0].Rows[0][2]}";
            //    string fileName = Path.GetFileName(sourcePath);
            //    string newFileName = ItemIdent.Text + Path.GetExtension(sourcePath);
            //    destinationFilePath = Path.Combine(destinationFolderPath, newFileName);

            //    if (File.Exists(destinationFilePath))
            //    {
            //        File.Delete(destinationFilePath);
            //    }


            //    File.Copy(sourcePath, destinationFilePath);

            //}


            if (ItemIdent.Text != "" | ItemDescription.Text != "" | Filter1.Text != "")
            {
                int selectedVendorID = VendorCombobox.SelectedIndex;
                selectedVendorID++;
                CurrentItemAdministrationModel.NewItemIdentStr = ItemIdent.Text;
                CurrentItemAdministrationModel.NewItemDescription = ItemDescription.Text;

                CurrentItemAdministrationModel.NewItemDescription2 = ItemDescription2.Text;
                CurrentItemAdministrationModel.NewItemFilter1 = Filter1.Text;
                CurrentItemAdministrationModel.NewItemFilter2 = Filter2.Text;
                CurrentItemAdministrationModel.NewItemFilter3 = Filter3.Text;
                CurrentItemAdministrationModel.NewItemFilter4 = Filter4.Text;
                CurrentItemAdministrationModel.NewItemFilter5 = Filter5.Text;




                CurrentItemAdministrationModel.NewItemPrice = price_Text.Text;
                CurrentItemAdministrationModel.NewItemCurrencyID = CurrencyCombobox.SelectedIndex;
                CurrentItemAdministrationModel.NewItemVendorID = selectedVendorID;

                if (OrderCheckbox.IsChecked == true)
                {
                    CurrentItemAdministrationModel.NewItemIsOrderable = true;
                    if (VendorCombobox.Text == "" | MinQuant_Text.Text == "" | MinOrder_Text.Text == "" | price_Text.Text == "" | CurrencyCombobox.Text == "")
                    {
                        ErrorHandlerModel.ErrorText = "Die Felder Lieferant, Min.-Bestand, Min.-Bestellmenge und EK-Preis müssen ausgefüllt sein!";
                        ErrorHandlerModel.ErrorType = "NOTALLOWED";
                        ErrorWindow openError = new ErrorWindow();
                        openError.ShowDialog();

                    }
                    else
                    {
                        CurrentItemAdministrationModel.NewItemMinQuant = MinQuant_Text.Text;
                        CurrentItemAdministrationModel.NewItemMinOrder = MinOrder_Text.Text;
                        ItemAdministrationQueries.SaveEditedItem(destinationFilePath, EditImage);

                        DialogResult = false;
                    }
                }
                else
                {
                    ItemAdministrationQueries.SaveEditedItem(destinationFilePath, EditImage);

                    DialogResult = false;
                }
            }
            else
            {
                CurrentItemAdministrationModel.NewItemIsOrderable = false;
                ErrorHandlerModel.ErrorText = "Die Felder Artikelnummer, Kunde und Beschreibung müssen ausgefüllt sein!";
                ErrorHandlerModel.ErrorType = "NOTALLOWED";
                ErrorWindow openError = new ErrorWindow();
                openError.ShowDialog();
            }
        }


        private void Filter1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Filter1.SelectedItem != null)
            {
                Filter2.IsEnabled = true;
            }
            else
            {
                Filter2.IsEnabled = false;
                Filter2.Text = "";
                Filter2.SelectedIndex = -1;
                Filter3.SelectedIndex = -1;
                Filter3.Text = "";
                Filter3.IsEnabled = false;
                Filter4.SelectedIndex = -1;
                Filter4.Text = "";
                Filter4.IsEnabled = false;
                Filter5.SelectedIndex = -1;
                Filter5.Text = "";
                Filter5.IsEnabled = false;
            }
        }


        private void Filter2_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Filter2.SelectedItem != null && Filter2.SelectedIndex != 0)
            {
                Filter3.IsEnabled = true;
            }
            else
            {
                Filter3.SelectedIndex = -1;
                Filter3.Text = "";
                Filter3.IsEnabled = false;
                Filter4.SelectedIndex = -1;
                Filter4.Text = "";
                Filter4.IsEnabled = false;
                Filter5.SelectedIndex = -1;
                Filter5.Text = "";
                Filter5.IsEnabled = false;
            }
        }



        private void Filter3_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Filter3.SelectedItem != null && Filter3.SelectedIndex != 0)
            {
                Filter4.IsEnabled = true;
            }
            else
            {
                Filter4.SelectedIndex = -1;
                Filter4.Text = "";
                Filter4.IsEnabled = false;
                Filter5.SelectedIndex = -1;
                Filter5.Text = "";
                Filter5.IsEnabled = false;
            }
        }
        private void Filter4_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Filter4.SelectedItem != null && Filter4.SelectedIndex != 0)
            {
                Filter5.IsEnabled = true;
            }
            else
            {
                Filter5.SelectedIndex = -1;
                Filter5.Text = "";
                Filter5.IsEnabled = false;
            }
        }


        private void OrderCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            if (OrderCheckbox.IsChecked == true)
            {
                VendorCombobox.IsEnabled = true;
                MinQuant_Text.IsEnabled = true;
                MinOrder_Text.IsEnabled = true;
                price_Text.IsEnabled = true;
                CurrencyCombobox.IsEnabled = true;
            }

        }

        private void OrderCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (OrderCheckbox.IsChecked == false)
            {
                VendorCombobox.IsEnabled = false;
                MinQuant_Text.IsEnabled = false;
                MinOrder_Text.IsEnabled = false;
                CurrencyCombobox.IsEnabled = false;
                price_Text.IsEnabled = false;
                VendorCombobox.SelectedIndex = -1;
            }
        }

        private void MinQuant_Text_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Get the TextBox reference
            TextBox textBox = (TextBox)sender;

            // Get the current caret position
            int caretIndex = textBox.CaretIndex;

            // Remove non-numeric characters
            string text = new string(textBox.Text.Where(c => char.IsDigit(c)).ToArray());

            // Handle leading zeros
            if (text.Length > 1 && text[0] == '0')
            {
                text = text.TrimStart('0');
            }

            // Set the modified text back to the TextBox
            textBox.Text = text;

            // Ensure the value is greater than or equal to 0
            if (!string.IsNullOrEmpty(text) && int.Parse(text) < 0)
            {
                textBox.Text = "0";
            }

            // Adjust the caret position if necessary
            if (caretIndex > textBox.Text.Length)
            {
                caretIndex = textBox.Text.Length;
            }

            // Set the new caret position
            textBox.CaretIndex = caretIndex;
        }

        private void MinOrder_Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Get the TextBox reference
            TextBox textBox = (TextBox)sender;

            // Get the current caret position
            int caretIndex = textBox.CaretIndex;

            // Remove non-numeric characters
            string text = new string(textBox.Text.Where(c => char.IsDigit(c)).ToArray());

            // Handle leading zeros
            if (text.Length > 1 && text[0] == '0')
            {
                text = text.TrimStart('0');
            }

            // Set the modified text back to the TextBox
            textBox.Text = text;

            // Ensure the value is greater than or equal to 0
            if (!string.IsNullOrEmpty(text) && int.Parse(text) < 0)
            {
                textBox.Text = "0";
            }

            // Adjust the caret position if necessary
            if (caretIndex > textBox.Text.Length)
            {
                caretIndex = textBox.Text.Length;
            }

            // Set the new caret position
            textBox.CaretIndex = caretIndex;
        }


        private void CurrencyTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string decimalString = ItemAdministrationQueries.RunSql("SELECT * FROM settings WHERE settings_name = 'decimal'").Tables[0].Rows[0]["settings_input"].ToString();
            char decimalChar = char.Parse(ItemAdministrationQueries.RunSql("SELECT * FROM settings WHERE settings_name = 'decimal'").Tables[0].Rows[0]["settings_input"].ToString());

            // Allow only numeric and decimal input
            Regex regex = new Regex(@"^[0-9]*(?:\.[0-9]{0,2})?$");

            string newText = GetNewText(price_Text, e.Text);
            string updatedText = FormatCurrencyValue(newText);

            bool isValidInput = regex.IsMatch(updatedText);

            e.Handled = !isValidInput || updatedText != newText;

            if (isValidInput)
            {
                int caretIndex = price_Text.CaretIndex;
                string text = price_Text.Text;

                // If a decimal point is added, adjust the caret position
                if (e.Text == decimalString && text.Contains(decimalString))
                {
                    int dotIndex = text.IndexOf(decimalString);
                    price_Text.CaretIndex = dotIndex + 1;
                    e.Handled = true;
                }
                else if (caretIndex < text.Length && text[caretIndex] == decimalChar)
                {
                    price_Text.CaretIndex = caretIndex + 1;
                }
            }
        }

        private void price_Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Update the text format
            price_Text.Text = FormatCurrencyValue(price_Text.Text);
        }

        private string GetNewText(TextBox textBox, string input)
        {
            int caretIndex = textBox.CaretIndex;
            string text = textBox.Text;
            return text.Substring(0, caretIndex) + input + text.Substring(caretIndex + textBox.SelectionLength);
        }

        private string FormatCurrencyValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            string decimalString = ItemAdministrationQueries.RunSql("SELECT * FROM settings WHERE settings_name = 'decimal'").Tables[0].Rows[0]["settings_input"].ToString();
            char decimalChar = char.Parse(ItemAdministrationQueries.RunSql("SELECT * FROM settings WHERE settings_name = 'decimal'").Tables[0].Rows[0]["settings_input"].ToString());

            // Remove non-numeric and non-decimal characters
            string formattedValue = new string(value.Where(c => char.IsDigit(c) || c == decimalChar).ToArray());

            // Split into whole number and decimal parts
            string[] parts = formattedValue.Split(decimalChar);

            if (parts.Length > 1)
            {
                // Truncate decimal part to two digits
                parts[1] = parts[1].PadRight(2, '0');
            }
            parts[0] = RemoveLeadingZeros(parts[0]);
            // Reconstruct the formatted value
            formattedValue = string.Join(decimalString, parts);

            return formattedValue;
        }
        private string RemoveLeadingZeros(string input)
        {
            if (input.Length > 1 && input[0] == '0')
            {
                int firstNonZeroIndex = input.IndexOfAny(new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' });
                if (firstNonZeroIndex != -1)
                {
                    return input.Substring(firstNonZeroIndex);
                }
                return "0";
            }
            return input;
        }

        private void ImagePathBtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog(); openFileDialog.Filter = "JPEG|*.jpg|PNG|*.png";
            if (openFileDialog.ShowDialog() == true)
            {

                ImagePath.Text = Path.GetDirectoryName(openFileDialog.FileName) + "\\" + Path.GetFileName(openFileDialog.FileName);

                string selectedFilePath = openFileDialog.FileName;
                Uri imageUri = new Uri(selectedFilePath);
                ItemImage.Source = new BitmapImage(imageUri);
            }
        }

        private void price_Text_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = TextFieldRules.VerifyMoneyInput(e, (TextBox)sender);
        }
    }
}
