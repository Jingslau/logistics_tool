
using MySqlConnector;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using waerp_management.application.RentItem;
using waerp_management.application.ReportLocation;
using waerp_management.application.returnItem;
using waerp_management.dbtools;
using waerp_management.errorHandling;
using waerp_management.models;
using waerp_management.sql;
using waerp_management.store;

namespace waerp_management.application.BookItem
{
    /// <summary>
    /// Interaction logic for BookItemSelectionView.xaml
    /// </summary>
    public partial class BookItemSelectionView : Window
    {
        public string locationID;
        public string locationName;
        public bool isNewLocation = false;
        public string locationQuantity;
        public DataSet AllLocation = new DataSet();
        public DataSet CurrentLocations = new DataSet();
        MySqlConnection conn = new MySqlConnection(SqlConn.GetConnectionString());


        public BookItemSelectionView()
        {
            InitializeComponent();
            GetItemContent();
            GetLocations();
            ReportWrongLocationModel.ItemIdent = CurrentReturnModel.ItemIdentStr;
            BookBtn.IsEnabled = false;
            Boolean check = false;
            for (int i = 0; i < CurrentReturnModel.ItemIdentStr.Length; i++)
            {
                if (CurrentReturnModel.ItemIdentStr[i].ToString() == "ä" ||
                    CurrentReturnModel.ItemIdentStr[i].ToString() == "ü" ||
                    CurrentReturnModel.ItemIdentStr[i].ToString() == "ö" ||
                    CurrentReturnModel.ItemIdentStr[i].ToString() == "Ä" ||
                    CurrentReturnModel.ItemIdentStr[i].ToString() == "Ü" ||
                    CurrentReturnModel.ItemIdentStr[i].ToString() == "Ö" ||
                    CurrentReturnModel.ItemIdentStr[i].ToString() == "ß" ||
                    CurrentReturnModel.ItemIdentStr[i].ToString() == "%" ||
                    CurrentReturnModel.ItemIdentStr[i].ToString() == "&")
                {
                    check = true;
                }
            }
            if (check == false)
            {

                Barcode.Text = "*" + CurrentReturnModel.ItemIdentStr + "*";
            }
            else
            {
                Barcode.Text = "";
            }
        }

        private void GetItemContent()
        {
            conn.Open();
            MySqlDataReader reader = null;
            MySqlCommand cmd = new MySqlCommand("Select * from item_objects WHERE item_id = '" + CurrentReturnModel.ItemIdent + "'", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CurrentReturnModel.ItemIdentStr = reader.GetString("item_ident");
                CurrentReturnModel.ItemDescription = reader.GetString("item_description");
            }

            ItemIdent.Text = CurrentReturnModel.ItemIdentStr;
            ItemDescription.Text = CurrentReturnModel.ItemDescription;
            ItemTotalQuantity.Text = CurrentRentModel.ItemTotalQuantity;

            ItemImage.Source = new BitmapImage(new Uri(CurrentReturnModel.ItemImagePath, UriKind.Relative));

            conn.Close();
        }

        private void GetLocations()
        {
            CurrentReturnModel.ItemIdent = CurrentRentModel.ItemIdent;
            if (ReturnItemQueries.GetItemLocations() != null)
            {
                locationData.DataContext = ReturnItemQueries.GetItemLocations();
                locationData.ItemsSource = new DataView(ReturnItemQueries.GetItemLocations().Tables[0]);
            }
            locationEmptyData.DataContext = ReturnItemQueries.GetAllLocations();
            locationEmptyData.ItemsSource = new DataView(ReturnItemQueries.GetAllLocations().Tables[0]);
            AllLocation = ReturnItemQueries.GetAllLocations();

        }
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }



        private void BookItem(object sender, RoutedEventArgs e)
        {
            if (QuantityInput.Text == "")
            {
                ErrorHandlerModel.ErrorText = "Sie müssen einen Wert für die Entnahme wählen!";
                ErrorHandlerModel.ErrorType = "NOTALLOWED";
                ErrorWindow showError = new ErrorWindow();
                showError.ShowDialog();
            }
            else
            {
                if (int.Parse(QuantityInput.Text) > 0)
                {
                    if (isNewLocation)
                    {
                        CurrentReturnModel.ReturnQuantity = QuantityInput.Text;
                        BookItemQueries.BookItemNewLocation();
                    }
                    else
                    {
                        CurrentReturnModel.ReturnQuantity = QuantityInput.Text;
                        BookItemQueries.BookItemLocation();
                    }




                    CurrentReturnModel.ReturnLocation = locationName;
                    CurrentReturnModel.ReturnQuantity = QuantityInput.Text.ToString();


                    SuccessReturnView successDialog = new SuccessReturnView();
                    successDialog.ShowDialog();
                    DialogResult = false;

                }
                else if (int.Parse(QuantityInput.Text) == 0)
                {
                    ErrorHandlerModel.ErrorText = "Sie können keine Menge von 0 buchen!";
                    ErrorHandlerModel.ErrorType = "NOTALLOWED";
                    ErrorWindow showError = new ErrorWindow();
                    showError.ShowDialog();
                }
                else
                {
                    ErrorHandlerModel.ErrorText = "Sie müssen einen Wert für die Entnahme wählen!";
                    ErrorHandlerModel.ErrorType = "NOTALLOWED";
                    ErrorWindow showError = new ErrorWindow();
                    showError.ShowDialog();
                }

            }

        }

        private void locationData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            isNewLocation = false;
            locationEmptyData.SelectedIndex = -1;
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                CurrentReturnModel.ReturnLocationID = row_selected["location_id"].ToString();
                ReportWrongLocationModel.LocationIdent = row_selected["location_name"].ToString();
                locationID = row_selected["location_id"].ToString();
                locationName = row_selected["location_name"].ToString();
                locationQuantity = row_selected["location_quantity"].ToString();
            }

            BookBtn.IsEnabled = true;

        }
        private void locationEmptyData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            isNewLocation = true;
            locationData.SelectedIndex = -1;
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                CurrentReturnModel.ReturnLocationID = row_selected["location_id"].ToString();
                ReportWrongLocationModel.LocationIdent = row_selected["location_name"].ToString();
                locationID = row_selected["location_id"].ToString();
                locationName = row_selected["location_name"].ToString();
                locationQuantity = row_selected["location_quantity"].ToString();
            }
            BookBtn.IsEnabled = true;

        }

        private void CloseCurrentDialog(object sender, RoutedEventArgs e)
        {

            DialogResult = false;
        }
        private void PlusQuantity_Click(object sender, RoutedEventArgs e)
        {
            int quant = int.Parse(QuantityInput.Text);
            quant++;
            QuantityInput.Text = quant.ToString();
        }

        private void MinusQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(QuantityInput.Text) > 0)
            {
                int quant = int.Parse(QuantityInput.Text);
                quant--;
                QuantityInput.Text = quant.ToString();
                RentNumberInputView test = new RentNumberInputView();
                Nullable<bool> dialogResult = test.ShowDialog();
            }

        }

        private void QuantityNumInput_Click(object sender, RoutedEventArgs e)
        {
            CurrentReturnModel.ReturnQuantity = QuantityInput.Text.ToString();
            ReturnNumberInputView numberInput = new ReturnNumberInputView();
            Nullable<bool> dialogResult = numberInput.ShowDialog();
            QuantityInput.Text = CurrentReturnModel.ReturnQuantity.ToString();
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            locationEmptyData.DataContext = null;
            locationEmptyData.ItemsSource = null;
            if (searchBox.Text != "")
            {
                DataSet output = AllLocation.Copy();
                output.Tables[0].Rows.Clear();

                foreach (DataRow row in AllLocation.Tables[0].Rows)
                {
                    if (row["location_name"].ToString().Contains(searchBox.Text))
                    {
                        output.Tables[0].ImportRow(row);
                    }
                }

                locationEmptyData.DataContext = output;
                locationEmptyData.ItemsSource = new DataView(output.Tables[0]);
                CurrentLocations = output;
            }
            else
            {
                CurrentLocations = AllLocation;
                locationEmptyData.DataContext = AllLocation;
                locationEmptyData.ItemsSource = new DataView(AllLocation.Tables[0]);
                locationEmptyData.SelectedIndex = 0;
            }
        }

        private void resetSearch(object sender, RoutedEventArgs e)
        {
            ReportLocationView openReport = new ReportLocationView();
            openReport.ShowDialog();
        }
    }
}
