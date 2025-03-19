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
using waerp_management.dbtools;
using waerp_management.errorHandling;
using waerp_management.models;
using waerp_management.sql;
using waerp_management.store;

namespace waerp_management.application.rentItem
{
    /// <summary>
    /// Interaction logic for RentSelectedItemView.xaml
    /// </summary>
    public partial class RentSelectedItemView : Window
    {
        public string locationID;
        public string locationName;
        public string locationQuantity;
        public string groupID;
        public string zoneName;
        public DataSet AllLocation;
        public DataSet CurrentLocations;
        public string groupName;
        public string groupQuantity;
        public bool IsGroup = false;
        MySqlConnection conn = new MySqlConnection(SqlConn.GetConnectionString());
        public RentSelectedItemView()
        {
            InitializeComponent();
            GetItemContent();
            GetLocations();
            ReportWrongLocationModel.ItemIdent = CurrentRentModel.ItemIdentStr;
            RentBtn.IsEnabled = false;
            Boolean check = false;
            for (int i = 0; i < CurrentRentModel.ItemIdentStr.Length; i++)
            {
                if (CurrentRentModel.ItemIdentStr[i].ToString() == "ä" ||
                    CurrentRentModel.ItemIdentStr[i].ToString() == "ü" ||
                    CurrentRentModel.ItemIdentStr[i].ToString() == "ö" ||
                    CurrentRentModel.ItemIdentStr[i].ToString() == "Ä" ||
                    CurrentRentModel.ItemIdentStr[i].ToString() == "Ü" ||
                    CurrentRentModel.ItemIdentStr[i].ToString() == "Ö" ||
                    CurrentRentModel.ItemIdentStr[i].ToString() == "ß" ||
                    CurrentRentModel.ItemIdentStr[i].ToString() == "%" ||
                    CurrentRentModel.ItemIdentStr[i].ToString() == "&")
                {
                    check = true;
                }
            }
            if (check == false)
            {

                Barcode.Text = "*" + CurrentRentModel.ItemIdentStr + "*";
            }
            else
            {
                Barcode.Text = "";
            }

        }

        private void GetItemContent()
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from item_objects WHERE item_id = " + CurrentRentModel.ItemIdent, conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);



            CurrentRentModel.ItemIdentStr = ds.Tables[0].Rows[0]["item_ident"].ToString();
            CurrentRentModel.ItemTotalQuantity = ds.Tables[0].Rows[0]["item_quantity_total"].ToString();
            CurrentRentModel.ItemDescription = ds.Tables[0].Rows[0]["item_description"].ToString();
            CurrentRentModel.ItemImagePath = ds.Tables[0].Rows[0]["item_image_path"].ToString();


            ItemDescription2.Text = ds.Tables[0].Rows[0]["item_description_2"].ToString();
            ItemIdent.Text = CurrentRentModel.ItemIdentStr;
            ItemDescription.Text = CurrentRentModel.ItemDescription;
            ItemTotalQuantity.Text = CurrentRentModel.ItemTotalQuantity;
            ItemImage.Source = new BitmapImage(new Uri(@CurrentRentModel.ItemImagePath, UriKind.Relative));

            conn.Close();
        }

        private void GetLocations()
        {
            DataSet ds = RentItemQueries.GetLocations();
            locationData.DataContext = ds;
            if (ds.Tables.Count > 0)
            {
                locationData.ItemsSource = new DataView(ds.Tables[0]);
                AllLocation = ds;
            }

            DataSet ds2 = RentItemQueries.GetGroups();
            if (ds2.Tables.Count > 0)
            {
                ZoneGroupData.DataContext = ds2;
                ZoneGroupData.ItemsSource = new DataView(ds2.Tables[0]);
            }



        }
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public static DateTime Now { get; }

        private void RentItem(object sender, RoutedEventArgs e)
        {
            CurrentRentModel.IsGroup = IsGroup;
            if (IsGroup)
            {
                int NewQuant = int.Parse(CurrentRentModel.ItemTotalQuantity) - int.Parse(QuantityInput.Text);
                if (QuantityInput.Text == "")
                {
                    ErrorHandlerModel.ErrorText = "Sie müssen einen Wert für die Entnahme wählen!";
                    ErrorHandlerModel.ErrorType = "NOTALLOWED";
                    ErrorWindow notAllowed = new ErrorWindow();
                    notAllowed.ShowDialog();
                }
                else
                {
                    if (int.Parse(groupQuantity) < int.Parse(QuantityInput.Text))
                    {
                        ErrorHandlerModel.ErrorText = "Sie können nicht mehr entnahmen als in der Palette aktuell verfügbar ist!";
                        ErrorHandlerModel.ErrorType = "NOTALLOWED";
                        ErrorWindow notAllowed = new ErrorWindow();
                        notAllowed.ShowDialog();
                    }
                    else
                    {
                        if (int.Parse(QuantityInput.Text) > 0)
                        {
                            if (RentItemQueries.RentItemExec(NewQuant.ToString(), QuantityInput.Text, groupID, groupQuantity, zoneName))
                            {
                                //CurrentRentModel.RentLocation = locationName;
                                CurrentRentModel.RentQuantity = QuantityInput.Text.ToString();


                                SuccessRentView successDialog = new SuccessRentView();
                                Nullable<bool> dialogResult = successDialog.ShowDialog();
                                DialogResult = false;
                            }
                            else
                            {
                                ErrorHandlerModel.ErrorText = "Es ist ein Fehler aufgetreten!";
                                ErrorHandlerModel.ErrorType = "NOTALLOWED";
                                ErrorWindow notAllowed = new ErrorWindow();
                                notAllowed.ShowDialog();
                            }
                        }
                        else if (int.Parse(QuantityInput.Text) == 0)
                        {
                            ErrorHandlerModel.ErrorText = "Sie können keine Menge von 0 entnehmen!";
                            ErrorHandlerModel.ErrorType = "NOTALLOWED";
                            ErrorWindow notAllowed = new ErrorWindow();
                            notAllowed.ShowDialog();
                        }
                        else if (int.Parse(groupQuantity) - int.Parse(QuantityInput.Text) < 0)
                        {
                            ErrorHandlerModel.ErrorText = "Sie können nicht mehr entnahmen als im Lagerort aktuell verfügbar ist!";
                            ErrorHandlerModel.ErrorType = "NOTALLOWED";
                            ErrorWindow notAllowed = new ErrorWindow();
                            notAllowed.ShowDialog();
                        }
                        else
                        {
                            ErrorHandlerModel.ErrorText = "Sie müssen einen Wert für die Entnahme wählen!";
                            ErrorHandlerModel.ErrorType = "NOTALLOWED";
                            ErrorWindow notAllowed = new ErrorWindow();
                            notAllowed.ShowDialog();
                        }

                    }
                }
            }
            else
            {
                int NewQuant = int.Parse(CurrentRentModel.ItemTotalQuantity) - int.Parse(QuantityInput.Text);

                if (QuantityInput.Text == "")
                {
                    ErrorHandlerModel.ErrorText = "Sie müssen einen Wert für die Entnahme wählen!";
                    ErrorHandlerModel.ErrorType = "NOTALLOWED";
                    ErrorWindow notAllowed = new ErrorWindow();
                    notAllowed.ShowDialog();
                }
                else
                {
                    if (int.Parse(locationQuantity) < int.Parse(QuantityInput.Text))
                    {
                        ErrorHandlerModel.ErrorText = "Sie können nicht mehr entnahmen als im Lagerort aktuell verfügbar ist!";
                        ErrorHandlerModel.ErrorType = "NOTALLOWED";
                        ErrorWindow notAllowed = new ErrorWindow();
                        notAllowed.ShowDialog();
                    }
                    else
                    {
                        if (int.Parse(QuantityInput.Text) > 0)
                        {
                            if (RentItemQueries.RentItemExec(NewQuant.ToString(), QuantityInput.Text, locationID, locationQuantity, zoneName))
                            {
                                // CurrentRentModel.RentLocation = locationName;
                                CurrentRentModel.RentQuantity = QuantityInput.Text.ToString();


                                SuccessRentView successDialog = new SuccessRentView();
                                Nullable<bool> dialogResult = successDialog.ShowDialog();
                                DialogResult = false;
                            }
                            else
                            {
                                ErrorHandlerModel.ErrorText = "Es ist ein Fehler aufgetreten!";
                                ErrorHandlerModel.ErrorType = "NOTALLOWED";
                                ErrorWindow notAllowed = new ErrorWindow();
                                notAllowed.ShowDialog();

                            }
                        }
                        else if (int.Parse(QuantityInput.Text) == 0)
                        {
                            ErrorHandlerModel.ErrorText = "Sie können keine Menge von 0 entnehmen!";
                            ErrorHandlerModel.ErrorType = "NOTALLOWED";
                            ErrorWindow notAllowed = new ErrorWindow();
                            notAllowed.ShowDialog();
                        }
                        else if (int.Parse(locationQuantity) - int.Parse(QuantityInput.Text) < 0)
                        {
                            ErrorHandlerModel.ErrorText = "Sie können nicht mehr entnahmen als im Lagerort aktuell verfügbar ist!";
                            ErrorHandlerModel.ErrorType = "NOTALLOWED";
                            ErrorWindow notAllowed = new ErrorWindow();
                            notAllowed.ShowDialog();
                        }
                        else
                        {
                            ErrorHandlerModel.ErrorText = "Sie müssen einen Wert für die Entnahme wählen!";
                            ErrorHandlerModel.ErrorType = "NOTALLOWED";
                            ErrorWindow notAllowed = new ErrorWindow();
                            notAllowed.ShowDialog();
                        }

                    }



                }
            }


        }

        private void locationData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            IsGroup = false;
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                ZoneGroupData.SelectedIndex = -1;
                ReportWrongLocationModel.LocationIdent = row_selected["location_name"].ToString();
                locationID = row_selected["location_id"].ToString();
                locationName = row_selected["location_name"].ToString();
                locationQuantity = row_selected["location_quantity"].ToString();
                CurrentRentModel.RentLocation = row_selected["location_name"].ToString();
                if (int.Parse(locationQuantity) > 0)
                {
                    RentBtn.IsEnabled = true;
                }
            }

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

            }

        }

        private void QuantityNumInput_Click(object sender, RoutedEventArgs e)
        {
            CurrentRentModel.RentQuantity = QuantityInput.Text.ToString();
            RentNumberInputView numberInput = new RentNumberInputView();
            Nullable<bool> dialogResult = numberInput.ShowDialog();
            QuantityInput.Text = CurrentRentModel.RentQuantity.ToString();
        }

        private void ZoneGroupData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsGroup = true;

            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                locationData.SelectedIndex = -1;
                ReportWrongLocationModel.LocationIdent = row_selected["group_name"].ToString();
                zoneName = row_selected["floor_name"].ToString();
                groupID = row_selected["group_id"].ToString();
                groupName = row_selected["group_name"].ToString();
                groupQuantity = row_selected["item_quantity"].ToString();
                CurrentRentModel.RentLocation = row_selected["floor_name"].ToString() + " - " + row_selected["group_name"].ToString();

                if (int.Parse(groupQuantity) > 0)
                {
                    RentBtn.IsEnabled = true;
                }
            }

        }

        private void resetSearch(object sender, RoutedEventArgs e)
        {

        }

        private void ReportErrorLocationBtn_Click(object sender, RoutedEventArgs e)
        {

            ReportLocationView openReport = new ReportLocationView();
            openReport.ShowDialog();
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            locationData.DataContext = null;
            locationData.ItemsSource = null;
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

                locationData.DataContext = output;
                locationData.ItemsSource = new DataView(output.Tables[0]);
                CurrentLocations = output;
            }
            else
            {
                CurrentLocations = AllLocation;
                locationData.DataContext = AllLocation;
                locationData.ItemsSource = new DataView(AllLocation.Tables[0]);
                locationData.SelectedIndex = 0;
            }
        }
    }
}
