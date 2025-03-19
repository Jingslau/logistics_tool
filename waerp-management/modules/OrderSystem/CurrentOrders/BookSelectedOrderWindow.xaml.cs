using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using waerp_management.application.returnItem;
using waerp_management.dbtools;
using waerp_management.errorHandling;
using waerp_management.sql;
using waerp_management.store;

namespace waerp_management.application.OrderSystem.CurrentOrders
{
    /// <summary>
    /// Interaction logic for BookSelectedOrderWindow.xaml
    /// </summary>
    public partial class BookSelectedOrderWindow : Window
    {
        public string locationID;
        public string locationName;
        public string locationQuantity;
        public DataSet AllLocation;
        public DataSet CurrentLocations;
        MySqlConnection conn = new MySqlConnection(SqlConn.GetConnectionString());


        public BookSelectedOrderWindow()
        {
            InitializeComponent();
            GetItemContent();
            GetLocations();
            ReturnBtn.IsEnabled = false;
            Boolean check = false;
            for (int i = 0; i < ActiveOrderModel.ItemIdentStr.Length; i++)
            {
                if (ActiveOrderModel.ItemIdentStr[i].ToString() == "ä" ||
                    ActiveOrderModel.ItemIdentStr[i].ToString() == "ü" ||
                    ActiveOrderModel.ItemIdentStr[i].ToString() == "ö" ||
                    ActiveOrderModel.ItemIdentStr[i].ToString() == "Ä" ||
                    ActiveOrderModel.ItemIdentStr[i].ToString() == "Ü" ||
                    ActiveOrderModel.ItemIdentStr[i].ToString() == "Ö" ||
                    ActiveOrderModel.ItemIdentStr[i].ToString() == "ß" ||
                    ActiveOrderModel.ItemIdentStr[i].ToString() == "%" ||
                    ActiveOrderModel.ItemIdentStr[i].ToString() == "&")
                {
                    check = true;
                }
            }
            if (check == false)
            {

                Barcode.Text = "*" + ActiveOrderModel.ItemIdentStr + "*";
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
            MySqlCommand cmd = new MySqlCommand("Select * from item_objects WHERE item_id = '" + ActiveOrderModel.CurrentItemId + "'", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ActiveOrderModel.ItemIdentStr = reader.GetString("item_ident");
                ActiveOrderModel.ItemDescription = reader.GetString("item_description");
                ActiveOrderModel.ItemImagePath = reader.GetString("item_image_path");
            }

            ItemIdent.Text = ActiveOrderModel.ItemIdentStr;
            ItemDescription.Text = ActiveOrderModel.ItemDescription;
            ItemTotalQuantity.Text = ActiveOrderModel.ItemQuantity;
            OrderIdent.Text = ActiveOrderModel.Order_Ident;
            ItemImage.Source = new BitmapImage(new Uri(ActiveOrderModel.ItemImagePath, UriKind.Relative));

            conn.Close();
        }

        private void GetLocations()
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from item_location_relations WHERE item_id = '" + ActiveOrderModel.CurrentItemId + "'", conn);
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
                MySqlCommand cmd2 = new MySqlCommand(sql, conn);
                MySqlDataAdapter adp2 = new MySqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                adp2.Fill(ds2, "locationData");
                locationData.DataContext = ds2;


            }

            cmd = new MySqlCommand("SELECT * FROM location_objects WHERE location_quantity = 0", conn);
            adp = new MySqlDataAdapter(cmd);
            ds = new DataSet();
            adp.Fill(ds, "locationEmptyData");
            locationEmptyData.DataContext = ds;
            AllLocation = ds;
            conn.Close();
        }
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }



        private void ReturnItem(object sender, RoutedEventArgs e)
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
                if (int.Parse(QuantityInput.Text) > 0 && int.Parse(QuantityInput.Text) <= int.Parse(ActiveOrderModel.ItemQuantity))
                {
                    if (CurrentOrdersQueries.CheckIfUsed(locationID))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("UPDATE item_objects SET item_quantity_total = item_quantity_total + '" + QuantityInput.Text.ToString() + "' WHERE item_id = '" + ActiveOrderModel.CurrentItemId + "'", conn);
                        cmd.ExecuteNonQuery();

                        cmd = new MySqlCommand("UPDATE location_objects SET location_quantity = location_quantity + '" + QuantityInput.Text.ToString() + "' WHERE location_id = '" + locationID + "'", conn);
                        cmd.ExecuteNonQuery();

                        if (int.Parse(ItemTotalQuantity.Text) == int.Parse(QuantityInput.Text))
                        {
                            cmd = new MySqlCommand($"UPDATE order_item_relations SET isOpen = 0 WHERE order_ident = '{ActiveOrderModel.Order_Ident}' AND item_id = {ActiveOrderModel.CurrentItemId}", conn);
                            cmd.ExecuteNonQuery();

                            DataSet CheckOpenOrders = AdministrationQueries.RunSql($"SELECT * FROM order_item_relations WHERE order_ident = '{ActiveOrderModel.Order_Ident}' AND isOpen = 1");

                            if (CheckOpenOrders.Tables[0].Rows.Count <= 0)
                            {
                                AdministrationQueries.RunSqlExec($"UPDATE order_objects SET order_status = 0 WHERE order_ident = '{ActiveOrderModel.Order_Ident}'");
                            }


                            cmd = new MySqlCommand($"UPDATE item_objects SET item_onorder = 0 WHERE item_id = {ActiveOrderModel.CurrentItemId}", conn);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            int newQuant = int.Parse(ItemTotalQuantity.Text) - int.Parse(QuantityInput.Text);
                            cmd = new MySqlCommand($"UPDATE order_item_relations SET order_quantity = {newQuant} WHERE item_id = {ActiveOrderModel.CurrentItemId} AND order_ident = '{ActiveOrderModel.Order_Ident}'", conn);
                            cmd.ExecuteNonQuery();
                        }


                        conn.Close();
                    }
                    else
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("UPDATE item_objects SET item_quantity_total = item_quantity_total + '" + QuantityInput.Text.ToString() + "' WHERE item_id = '" + ActiveOrderModel.CurrentItemId + "'", conn);
                        cmd.ExecuteNonQuery();

                        cmd = new MySqlCommand("UPDATE location_objects SET location_quantity = location_quantity + '" + QuantityInput.Text.ToString() + "' WHERE location_id = '" + locationID + "'", conn);
                        cmd.ExecuteNonQuery();

                        cmd = new MySqlCommand($"INSERT INTO item_location_relations (id, item_id, location_id) VALUES({CurrentOrdersQueries.GetMaxIdLocation()},{ActiveOrderModel.CurrentItemId},{locationID})", conn);
                        cmd.ExecuteNonQuery();

                        if (int.Parse(ItemTotalQuantity.Text) == int.Parse(QuantityInput.Text))
                        {
                            cmd = new MySqlCommand($"UPDATE order_item_relations SET isOpen = 0 WHERE order_ident = '{ActiveOrderModel.Order_Ident}' AND item_id = {ActiveOrderModel.CurrentItemId}", conn);
                            cmd.ExecuteNonQuery();

                            DataSet CheckOpenOrders = AdministrationQueries.RunSql($"SELECT * FROM order_item_relations WHERE order_ident = '{ActiveOrderModel.Order_Ident}' AND isOpen = 1");

                            if (CheckOpenOrders.Tables[0].Rows.Count <= 0)
                            {
                                AdministrationQueries.RunSqlExec($"UPDATE order_objects SET order_status = 0 WHERE order_ident = '{ActiveOrderModel.Order_Ident}'");
                            }


                            cmd = new MySqlCommand($"UPDATE item_objects SET item_onorder = 0 WHERE item_id = {ActiveOrderModel.CurrentItemId}", conn);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            int newQuant = int.Parse(ItemTotalQuantity.Text) - int.Parse(QuantityInput.Text);
                            cmd = new MySqlCommand($"UPDATE order_item_relations SET order_quantity = {newQuant} WHERE item_id = {ActiveOrderModel.CurrentItemId} AND order_ident = '{ActiveOrderModel.Order_Ident}'", conn);
                            cmd.ExecuteNonQuery();
                        }


                        conn.Close();
                    }

                    CurrentReturnModel.ReturnLocation = locationName;
                    CurrentReturnModel.ReturnQuantity = QuantityInput.Text.ToString();


                    SuccessReturnView successDialog = new SuccessReturnView();
                    Nullable<bool> dialogResult = successDialog.ShowDialog();
                    DialogResult = false;

                }
                else if (int.Parse(QuantityInput.Text) == 0)
                {
                    ErrorHandlerModel.ErrorText = "Sie können keine Menge von 0 entnehmen!";
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
            locationEmptyData.UnselectAll();
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                locationID = row_selected["location_id"].ToString();
                locationName = row_selected["location_name"].ToString();
                locationQuantity = row_selected["location_quantity"].ToString();
            }

            ReturnBtn.IsEnabled = true;

        }
        private void locationEmptyData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            locationData.UnselectAll();
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                locationID = row_selected["location_id"].ToString();
                locationName = row_selected["location_name"].ToString();
                locationQuantity = row_selected["location_quantity"].ToString();
            }
            ReturnBtn.IsEnabled = true;

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
            ActiveOrderModel.BookQuantity = QuantityInput.Text.ToString();
            BookOrderNumberInputWindow numberInput = new BookOrderNumberInputWindow();
            Nullable<bool> dialogResult = numberInput.ShowDialog();
            QuantityInput.Text = ActiveOrderModel.BookQuantity.ToString();
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
    }
}
