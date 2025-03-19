using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using waerp_management.dbtools;
using waerp_management.errorHandling;
using waerp_management.sql;
using waerp_management.store.Administration;

namespace waerp_management.application.Administration.ItemAdministration
{
    /// <summary>
    /// Interaction logic for AddNewItemView.xaml
    /// </summary>
    public partial class AddNewItemView : Window
    {

        MySqlConnection conn = new MySqlConnection(SqlConn.GetConnectionString());

        static class ItemValues
        {
            public static string[] ItemFilterIDs = { "", "", "", "", "" };
            public static string[] ItemFilterNames = { "", "", "", "", "" };



        }

        public AddNewItemView()
        {
            InitializeComponent();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from item_objects", conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);


            foreach (DataRow row in ds.Tables[0].Rows)
            {
                ItemIdents.Items.Add(row["item_ident"].ToString());
            }
            cmd = new MySqlCommand("Select * from vendor_objects", conn);
            adp = new MySqlDataAdapter(cmd);
            ds = new DataSet();
            adp.Fill(ds);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                VendorCombobox.Items.Add(row["vendor_name"].ToString());
            }

            cmd = new MySqlCommand("SELECT * FROM currency", conn);
            adp = new MySqlDataAdapter(cmd);
            ds = new DataSet();
            adp.Fill(ds);
            int defaultIndexCurrency = -1;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                CurrencyCombobox.Items.Add(ds.Tables[0].Rows[i]["currency_code"].ToString());
                if (ds.Tables[0].Rows[i]["currency_isDefault"].ToString() == "True")
                {
                    defaultIndexCurrency = i;
                }
            }


            CurrencyCombobox.SelectedIndex = defaultIndexCurrency;

            conn.Close();





            Filter2.Items.Add("");
            Filter3.Items.Add("");
            Filter4.Items.Add("");
            Filter5.Items.Add("");

            bool stop1 = false;

            for (int i = 1; i <= 5; i++)
            {
                ds = AdministrationQueries.GetFilterList(i);

                if (ds.Tables[0].Rows.Count > 0)
                {
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
                }
                else
                {
                    stop1 = true;
                }
            }
            int FilterCounter = 1;
            bool stop = false;
            DataSet allItems = AdministrationQueries.RunSql("SELECT * FROM item_objects");
            if (allItems.Tables[0].Rows.Count > 0)
            {
                if (!stop1)
                {
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
                }
            }
            Filter1.SelectedIndex = -1;
            Filter2.SelectedIndex = -1;
            Filter3.SelectedIndex = -1;

            Filter4.SelectedIndex = -1;
            Filter5.SelectedIndex = -1;
        }


        private void CloseDialog_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

        }
        private void CreateItem_Click(object sender, RoutedEventArgs e)
        {
            string destinationFilePath = "";
            if (ItemIdents.Text != "" | ItemDescription1.Text != "" | Filter1.Text != "")
            {
                string[] Filters = { Filter1.Text, Filter2.Text, Filter3.Text, Filter4.Text, Filter5.Text };
                string[] FilterIDs = { "", "", "", "", "" };

                for (int i = 1; i < 6; i++)
                {
                    if (Filters[i - 1] == "")
                    {
                        FilterIDs[i - 1] = "0";
                    }
                    else
                    {
                        FilterIDs[i - 1] = ItemAdministrationQueries.GetFilterID(i, Filters[i - 1]);
                    }

                }

                if (ImagePath.Text != "")
                {


                    DataSet globalImagePath = AdministrationQueries.RunSql($"SELECT * FROM company_settings WHERE settings_name = 'global_image_path'");
                    string sourcePath = $@"{ImagePath.Text}";
                    string destinationFolderPath = $@"{globalImagePath.Tables[0].Rows[0][2]}";
                    string fileName = Path.GetFileName(sourcePath);
                    string newFileName = ItemIdents.Text + Path.GetExtension(sourcePath);
                    destinationFilePath = Path.Combine(destinationFolderPath, newFileName);

                    if (File.Exists(destinationFilePath))
                    {
                        File.Delete(destinationFilePath);
                    }

                    File.Copy(sourcePath, destinationFilePath);
                }

                if (OrderCheckbox.IsChecked == true)
                {
                    if (VendorCombobox.Text == "" | MinQuant_Text.Text == "" | MinOrder_Text.Text == "" | price_Text.Text == "" | CurrencyCombobox.Text == "")
                    {
                        ErrorHandlerModel.ErrorText = "Wenn ein Artikel bestellbar ist, dann muss ein Lieferant, Mindestbestand und Mindestbestellmenge angegeben werden!";
                        ErrorHandlerModel.ErrorType = "NOTALLOWED";
                        ErrorWindow showNotallowed = new ErrorWindow();
                        showNotallowed.ShowDialog();
                    }
                    else
                    {
                        if (ItemAdministrationQueries.CreateNewItem(ItemIdents.Text, ItemDescription1.Text, ItemDescription2.Text, FilterIDs, int.Parse(MinQuant_Text.Text), int.Parse(MinOrder_Text.Text), 1, VendorCombobox.SelectedIndex, CurrencyCombobox.SelectedIndex, price_Text.Text, destinationFilePath))
                        {
                            ErrorHandlerModel.ErrorText = "Der Artikel wurde erfolgreich angelegt!";
                            ErrorHandlerModel.ErrorType = "SUCCESS";
                            ErrorWindow showSuccess = new ErrorWindow();
                            showSuccess.ShowDialog();
                            DialogResult = false;
                        }
                    }
                }
                else
                {
                    if (ItemAdministrationQueries.CreateNewItem(ItemIdents.Text, ItemDescription1.Text, ItemDescription2.Text, FilterIDs, 0, 0, 0, -1, 0, "", destinationFilePath))
                    {
                        ErrorHandlerModel.ErrorText = "Der Artikel wurde erfolgreich angelegt!";
                        ErrorHandlerModel.ErrorType = "SUCCESS";
                        ErrorWindow showSuccess = new ErrorWindow();
                        showSuccess.ShowDialog();
                        DialogResult = false;
                    }
                }
            }
            else
            {
                ErrorHandlerModel.ErrorText = "Die Felder Artikelnummer, Beschreibung und Kunde sind nicht ausgefüllt!";
                ErrorHandlerModel.ErrorType = "NOTALLOWED";
                ErrorWindow showNotallowed = new ErrorWindow();
                showNotallowed.ShowDialog();
            }

        }

        private void SelectFilter_Click(object sender, RoutedEventArgs e)
        {
            if (ItemIdents.Text != "")
            {
                conn.Open();
                try
                {
                    MySqlCommand cmd = new MySqlCommand($"Select * from item_objects WHERE item_ident = {ItemIdents.Text}", conn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adp.Fill(ds); if (ds.Tables[0].Rows.Count > 0)
                    {


                        string SelectedItemID = "";

                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            SelectedItemID = row["item_id"].ToString();
                        }
                        cmd = new MySqlCommand($"Select * from item_filter_relations WHERE item_id = {SelectedItemID}", conn);
                        adp = new MySqlDataAdapter(cmd);
                        ds = new DataSet();
                        adp.Fill(ds);

                        List<string> strDetailIDList = new List<string>();

                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            ItemValues.ItemFilterIDs[0] = row["filter1_id"].ToString();

                            ItemValues.ItemFilterIDs[1] = row["filter2_id"].ToString();

                            ItemValues.ItemFilterIDs[2] = row["filter3_id"].ToString();
                            ItemValues.ItemFilterIDs[3] = row["filter4_id"].ToString();

                            ItemValues.ItemFilterIDs[4] = row["filter5_id"].ToString();
                        }
                        for (int i = 0; ItemValues.ItemFilterIDs.Length > i; i++)
                        {
                            int IND = i + 1;
                            string IndexID = IND.ToString();
                            cmd = new MySqlCommand($"Select * from filter{IndexID}_names WHERE filter_id = {ItemValues.ItemFilterIDs[i]}", conn);
                            adp = new MySqlDataAdapter(cmd);
                            ds = new DataSet();
                            adp.Fill(ds);


                            if (i == 1)
                            {
                                foreach (DataRow row in ds.Tables[0].Rows)
                                {
                                    Filter2.Text = row["name"].ToString();
                                }

                            }
                            else if (i == 2)
                            {
                                foreach (DataRow row in ds.Tables[0].Rows)
                                {
                                    Filter3.Text = row["name"].ToString();
                                }

                            }
                            else if (i == 3)
                            {
                                foreach (DataRow row in ds.Tables[0].Rows)
                                {
                                    Filter4.Text = row["name"].ToString();
                                }

                            }
                            else
                            {
                                foreach (DataRow row in ds.Tables[0].Rows)
                                {
                                    Filter5.Text = row["name"].ToString();
                                }

                            }

                        }
                    }
                    conn.Close();
                }
                catch (MySqlException)
                {
                    ErrorHandlerModel.ErrorText = "Angegebene Artikelnummer besitzt keine Filter!";
                    ErrorHandlerModel.ErrorType = "NOTALLOWED";
                    ErrorWindow openNotallowed = new ErrorWindow();
                    openNotallowed.ShowDialog(); conn.Close();

                }
                finally
                {
                    conn.Close();
                }





            }



            conn.Close();

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
            //  price_Text.Text = FormatCurrencyValue(price_Text.Text);
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
            //   ItemImage.Source = new BitmapImage(new Uri($@"{ImagePath.Text}", UriKind.Relative));
        }

        private void price_Text_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = TextFieldRules.VerifyMoneyInput(e, (TextBox)sender);
        }
    }
}
