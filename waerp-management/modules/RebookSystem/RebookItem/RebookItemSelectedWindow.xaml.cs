using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using waerp_management.application.returnItem;
using waerp_management.dbtools;
using waerp_management.errorHandling;
using waerp_management.sql;
using waerp_management.store;

namespace waerp_management.application.rebookItem
{
    /// <summary>
    /// Interaction logic for RefMinusQuantity_ClickbookItemSelectedWindow.xaml
    /// </summary>
    public partial class RebookItemSelectedWindow : Window
    {
        MySqlConnection conn = new MySqlConnection(SqlConn.GetConnectionString());
        static class LocationData
        {
            public static DataSet EmptyLocations = new DataSet();
            public static DataSet ItemLocations = new DataSet();
            public static DataSet AllLocation;
            public static DataSet CurrentLocations;
            public static string OldLocationSelected;
            public static string OldID;
            public static string NewID;
            public static string OldLocationQuantity;
            public static Boolean NewLocation = false;
            public static string NewLocationSelected;
            public static string NewLocationQuantity;
        }
        public RebookItemSelectedWindow()
        {
            InitializeComponent();
            InitVars();
            ItemIdent.Text = CurrentRebookModel.ItemIdentStr;
            ItemDescription.Text = CurrentRebookModel.ItemDescription;
            ItemTotalQuantity.Text = CurrentRebookModel.ItemTotalQuantity;
            for (int i = 0; i < CurrentRentModel.ItemIdentStr.Length; i++)
            {
                if (CurrentRebookModel.ItemIdentStr[i].ToString() == "ä" ||
                    CurrentRebookModel.ItemIdentStr[i].ToString() == "ü" ||
                    CurrentRebookModel.ItemIdentStr[i].ToString() == "ö" ||
                    CurrentRebookModel.ItemIdentStr[i].ToString() == "Ä" ||
                    CurrentRebookModel.ItemIdentStr[i].ToString() == "Ü" ||
                    CurrentRebookModel.ItemIdentStr[i].ToString() == "Ö" ||
                    CurrentRebookModel.ItemIdentStr[i].ToString() == "ß" ||
                    CurrentRebookModel.ItemIdentStr[i].ToString() == "%" ||
                    CurrentRebookModel.ItemIdentStr[i].ToString() == "&")
                {
                }
            }
            //if (check == false)
            //{

            //    Barcode.Text = "*" + CurrentRebookModel.ItemIdentStr + "*";
            //}
            //else
            //{
            //    Barcode.Text = "";
            //}
            GetEmptyLocaitons();
            GetUsedLocations();


        }

        private void InitVars()
        {
            LocationData.EmptyLocations = new DataSet();
            LocationData.ItemLocations = new DataSet();
            LocationData.OldLocationSelected = "";
            LocationData.OldID = "";
            LocationData.NewID = "";
            LocationData.OldLocationQuantity = "";
            LocationData.NewLocation = false;
            LocationData.NewLocationSelected = "";
            LocationData.NewLocationQuantity = "";
        }
        private void GetEmptyLocaitons()
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from location_objects", conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);

            adp.Fill(LocationData.EmptyLocations, "locationDataSec");
            conn.Close();
        }

        private void GetUsedLocations()
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand($"Select * from item_location_relations WHERE item_id = {CurrentRebookModel.ItemIdent}", conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            List<string> strDetailIDList = new List<string>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                strDetailIDList.Add(row["location_id"].ToString());
            }

            String[] tmpArr = new string[strDetailIDList.Count];
            for (int i = 0; i < strDetailIDList.Count; i++)
            {
                tmpArr[i] = strDetailIDList[i].ToString();
            }
            if (tmpArr.Length > 0)
            {
                var sql = string.Format("SELECT * FROM location_objects WHERE location_id IN ({0})", string.Join(", ", tmpArr));

                cmd = new MySqlCommand(sql, conn);
                adp = new MySqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                adp.Fill(ds2);

                for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        if (ds.Tables[0].Rows[j]["location_id"].ToString() == ds2.Tables[0].Rows[i]["location_id"].ToString())
                        {
                            ds2.Tables[0].Rows[i]["location_quantity"] = ds.Tables[0].Rows[j]["location_item_quantity"].ToString();
                        }
                    }
                }

                LocationData.ItemLocations = ds2;

                currentLocations.ItemsSource = new DataView(ds2.Tables[0]);
                SelectLocationData.ItemsSource = new DataView(ds2.Tables[0]);

            }

            SelectLocationData.DataContext = LocationData.ItemLocations;
            LocationData.AllLocation = LocationData.ItemLocations;
            currentLocations.DataContext = LocationData.ItemLocations;
            conn.Close();
        }

        private void ShowEmptyLocations_Click(object sender, EventArgs e)
        {
            LocationData.NewLocation = true;
            SelectLocationData.DataContext = LocationData.EmptyLocations;
        }
        private void ShowUsedLocations_Click(object sender, EventArgs e)
        {
            LocationData.NewLocation = false;
            SelectLocationData.DataContext = LocationData.ItemLocations;
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
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void QuantityNumInput_Click(object sender, RoutedEventArgs e)
        {
            CurrentReturnModel.ReturnQuantity = QuantityInput.Text.ToString();
            ReturnNumberInputView numberInput = new ReturnNumberInputView();
            numberInput.ShowDialog();
            QuantityInput.Text = CurrentReturnModel.ReturnQuantity.ToString();
        }
        private void RebookItem_Click(object sender, RoutedEventArgs e)
        {


            int QuantityInputVal = int.Parse(QuantityInput.Text);
            if (LocationData.OldLocationSelected == LocationData.NewLocationSelected)
            {
                ErrorHandlerModel.ErrorType = "NOTALLOWED";
                ErrorHandlerModel.ErrorText = "Der neue und alte Lagerort sind identisch! Bitte wählen Sie einen anderen Lagerort aus!";
                ErrorWindow ErrorDialog = new ErrorWindow();
                ErrorDialog.ShowDialog();

            }
            else if (QuantityInputVal == 0 |
                QuantityInputVal < 0)
            {
                ErrorHandlerModel.ErrorType = "NOTALLOWED";
                ErrorHandlerModel.ErrorText = "Es wurde eine ungültige Eingabe für den Wert der umzubuchende Menge gewählt!";
                ErrorWindow ErrorDialog = new ErrorWindow();
                ErrorDialog.ShowDialog();
            }


            else if (QuantityInputVal > int.Parse(LocationData.OldLocationQuantity))
            {
                ErrorHandlerModel.ErrorType = "NOTALLOWED";
                ErrorHandlerModel.ErrorText = "Es wurde eine ungültige Eingabe für den Wert der umzubuchende Menge gewählt!";
                ErrorWindow ErrorDialog = new ErrorWindow();
                ErrorDialog.ShowDialog();
            }
            else if (int.Parse(LocationData.OldLocationQuantity) == 0)
            {
                ErrorHandlerModel.ErrorType = "NOTALLOWED";
                ErrorHandlerModel.ErrorText = "Es wurde eine ungültige Eingabe für den Wert der umzubuchende Menge gewählt!";
                ErrorWindow ErrorDialog = new ErrorWindow();
                ErrorDialog.ShowDialog();
            }


            else
            {
                MySqlCommand cmd = new MySqlCommand();
                int newQuantOldLocation = int.Parse(LocationData.OldLocationQuantity) - int.Parse(QuantityInput.Text);
                int newQuantNewLocation = int.Parse(LocationData.NewLocationQuantity) + int.Parse(QuantityInput.Text);
                cmd = new MySqlCommand("SELECT * FROM item_location_relations", conn);
                MySqlDataAdapter adp2 = new MySqlDataAdapter(cmd);
                DataSet ds3 = new DataSet();
                adp2.Fill(ds3);

                int quantyLoc = int.Parse(AdministrationQueries.RunSql($"SELECT * FROM location_objects WHERE location_id = {LocationData.NewID}").Tables[0].Rows[0]["location_quantity"].ToString()) + int.Parse(QuantityInput.Text);
                int quantOldLoc = int.Parse(AdministrationQueries.RunSql($"SELECT * FROM location_objects WHERE location_id = {LocationData.OldID}").Tables[0].Rows[0]["location_quantity"].ToString()) - int.Parse(QuantityInput.Text);

                int maxID = 0;
                foreach (DataRow row in ds3.Tables[0].Rows)
                {
                    if (int.Parse(row["id"].ToString()) > maxID)
                    {
                        maxID = int.Parse(row["id"].ToString());
                    }
                }
                maxID++;
                string maxIDStr = maxID.ToString();
                if (newQuantOldLocation == 0)
                {
                    conn.Open();
                    cmd = new MySqlCommand($"DELETE FROM item_location_relations WHERE location_id = {LocationData.OldID} AND item_id = {CurrentRebookModel.ItemIdent}", conn);
                    cmd.ExecuteNonQuery();
                    if (LocationData.NewLocation == true)
                    {

                        cmd = new MySqlCommand($"UPDATE location_objects SET location_quantity= {quantOldLoc} WHERE location_id={LocationData.OldID}", conn);
                        cmd.ExecuteNonQuery();
                        cmd = new MySqlCommand($"UPDATE location_objects SET location_quantity= {quantyLoc} WHERE location_id={LocationData.NewID}", conn);
                        cmd.ExecuteNonQuery();
                        cmd = new MySqlCommand($"INSERT INTO item_location_relations (id, item_id, location_id, location_item_quantity) VALUES ( {maxIDStr},{CurrentRebookModel.ItemIdent}, {LocationData.NewID},  {QuantityInput.Text})", conn);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd = new MySqlCommand($"UPDATE location_objects SET location_quantity= {quantOldLoc} WHERE location_id={LocationData.OldID}", conn);
                        cmd.ExecuteNonQuery();
                        cmd = new MySqlCommand($"UPDATE item_location_relations SET location_item_quantity = location_item_quantity + {QuantityInput.Text} WHERE item_id = {CurrentRebookModel.ItemIdent} AND location_id = {LocationData.NewID}", conn);
                        cmd.ExecuteNonQuery();
                        cmd = new MySqlCommand($"UPDATE location_objects SET location_quantity=  {quantyLoc} WHERE location_id = {LocationData.NewID}", conn);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {

                    conn.Open();
                    cmd = new MySqlCommand($"UPDATE location_objects SET location_quantity= {quantOldLoc} WHERE location_id = {LocationData.OldID}", conn);

                    cmd.ExecuteNonQuery();
                    cmd = new MySqlCommand($"UPDATE item_location_relations SET location_item_quantity = location_item_quantity - {QuantityInput.Text} WHERE location_id = {LocationData.OldID} AND item_id = {CurrentRebookModel.ItemIdent}", conn);
                    cmd.ExecuteNonQuery();
                    if (LocationData.NewLocation == true)
                    {
                        cmd = new MySqlCommand($"UPDATE location_objects SET location_quantity = {quantyLoc} WHERE location_id={LocationData.NewID}", conn);
                        cmd.ExecuteNonQuery();
                        cmd = new MySqlCommand($"INSERT INTO item_location_relations (id, item_id, location_id, location_item_quantity) VALUES ({maxIDStr}, {CurrentRebookModel.ItemIdent}, {LocationData.NewID},  {QuantityInput.Text})", conn);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd = new MySqlCommand($"UPDATE location_objects SET location_quantity = {quantyLoc} WHERE location_id = {LocationData.NewID}", conn);
                        cmd.ExecuteNonQuery();
                        cmd = new MySqlCommand($"UPDATE item_location_relations SET location_item_quantity=location_item_quantity + {QuantityInput.Text} WHERE location_id = {LocationData.NewID} AND item_id = {CurrentRebookModel.ItemIdent}", conn);
                        cmd.ExecuteNonQuery();
                    }
                }
                CurrentRebookModel.OldLocationName = LocationData.OldLocationSelected;
                CurrentRebookModel.NewLocationName = LocationData.NewLocationSelected;
                RebookSuccessWindow RebookSuccessOpen = new RebookSuccessWindow();
                Nullable<bool> dialogResult = RebookSuccessOpen.ShowDialog();
                DialogResult = false;
                conn.Close();
            }
        }

        private void currentLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                LocationData.OldLocationSelected = row_selected["location_name"].ToString();
                LocationData.OldLocationQuantity = row_selected["location_quantity"].ToString();
                LocationData.OldID = row_selected["location_id"].ToString();
            }
        }

        private void SelectLocationData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                LocationData.NewLocationSelected = row_selected["location_name"].ToString();
                LocationData.NewLocationQuantity = row_selected["location_quantity"].ToString();
                LocationData.NewID = row_selected["location_id"].ToString();
            }
        }

        private void OpenNumberInput_Click(object sender, RoutedEventArgs e)
        {
            CurrentRebookModel.RebookQuantity = QuantityInput.Text;
            RebookNumberInputWindow NumberPad = new RebookNumberInputWindow();
            Nullable<bool> dialogResult = NumberPad.ShowDialog();
            QuantityInput.Text = CurrentRebookModel.RebookQuantity;
        }

        private void ShowUsedLocations_Click(object sender, RoutedEventArgs e)
        {
            LocationData.NewLocation = false;
            LocationData.AllLocation = RebookGroupQueries.GetItemLocations();
            SelectLocationData.DataContext = LocationData.AllLocation;
            SelectLocationData.ItemsSource = new DataView(LocationData.AllLocation.Tables[0]);

        }

        private void ShowEmptyLocations_Click(object sender, RoutedEventArgs e)
        {
            LocationData.NewLocation = true;
            LocationData.AllLocation = RebookGroupQueries.GetAllLocations();
            SelectLocationData.DataContext = LocationData.AllLocation;
            SelectLocationData.ItemsSource = new DataView(LocationData.AllLocation.Tables[0]);


        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SelectLocationData.DataContext = null;
            SelectLocationData.ItemsSource = null;
            if (searchBox.Text != "")
            {
                DataSet output = LocationData.AllLocation.Copy();
                output.Tables[0].Rows.Clear();

                foreach (DataRow row in LocationData.AllLocation.Tables[0].Rows)
                {
                    if (row["location_name"].ToString().Contains(searchBox.Text))
                    {
                        output.Tables[0].ImportRow(row);
                    }
                }

                SelectLocationData.DataContext = output;
                SelectLocationData.ItemsSource = new DataView(output.Tables[0]);
                LocationData.CurrentLocations = output;
            }
            else
            {
                LocationData.CurrentLocations = LocationData.AllLocation;
                SelectLocationData.DataContext = LocationData.AllLocation;
                SelectLocationData.ItemsSource = new DataView(LocationData.AllLocation.Tables[0]);
                SelectLocationData.SelectedIndex = 0;
            }
        }
    }
}
