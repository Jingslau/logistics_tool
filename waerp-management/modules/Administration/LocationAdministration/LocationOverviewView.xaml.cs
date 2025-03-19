using Microsoft.Win32;
using MySqlConnector;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using waerp_management.dbtools;
using waerp_management.errorHandling;
using waerp_management.sql;
using waerp_management.store.Administration;

namespace waerp_management.application.Administration.LocationAdministration
{
    /// <summary>
    /// Interaction logic for LocationOverviewView.xaml
    /// </summary>
    public partial class LocationOverviewView : UserControl
    {
        static class LocationParams
        {
            public static DataSet CurrentLocations = new DataSet();
            public static DataSet LocationsDB = new DataSet();
        }
        MySqlConnection conn = new MySqlConnection(SqlConn.GetConnectionString());
        public LocationOverviewView()
        {
            InitializeComponent();

            GetLocations();
            //     try
            //    {

            //conn.Open();



            //MySqlCommand cmd = new MySqlCommand("Select * from location_objects", conn);
            //MySqlDataAdapter adp = new MySqlDataAdapter(cmd);

            //DataSet ds = new DataSet();
            //DataSet ds2 = new DataSet();

            //adp.Fill(LocationParams.LocationsDB, "itemData");

            //LocationParams.LocationsDB.Tables[0].Columns.Add("item_ident", typeof(string));





            //foreach (DataRow row in LocationParams.LocationsDB.Tables[0].Rows)
            //{
            //    if (row["location_quantity"].ToString() != "0")
            //    {
            //        cmd = new MySqlCommand("Select * FROM item_location_relations WHERE location_id = " + row["location_id"].ToString(), conn);
            //        adp = new MySqlDataAdapter(cmd);
            //        adp.Fill(ds);
            //        foreach (DataRow row2 in ds.Tables[0].Rows)
            //        {
            //            cmd = new MySqlCommand("SELECT * FROM item_objects WHERE item_id =" + row2["item_id"].ToString(), conn);
            //            adp = new MySqlDataAdapter(cmd);
            //            adp.Fill(ds2);
            //            foreach (DataRow row3 in ds2.Tables[0].Rows)
            //            {
            //                row["item_ident"] = row3["item_ident"];
            //            }
            //            ds2 = new DataSet();

            //        }
            //        ds = new DataSet();
            //    }

            //}



            //DataLocationItems.DataContext = LocationParams.LocationsDB;
            //conn.Close();




            // }
            //catch (MySqlException ex)
            //{
            //    MessageBox.Show(ex.ToString());

            //}
            //finally
            //{
            //    conn.Close();
            //}
        }

        public void GetLocations()
        {
            conn.Open();

            LocationParams.LocationsDB = new DataSet();
            MySqlCommand cmd = new MySqlCommand("Select * from location_objects", conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            adp.Fill(ds, "itemData");
            LocationParams.CurrentLocations = ds;

            LocationParams.LocationsDB = ds;



            DataLocationItems.DataContext = ds;

            DataLocationItems.SelectedIndex = 0;
            conn.Close();
        }


        private void OpenNewItemDialog_Click(object sender, RoutedEventArgs e)
        {
            AddNewLocationView test = new AddNewLocationView();
            Nullable<bool> DialogResult = test.ShowDialog(); conn.Close();
            GetLocations();
            DataLocationItems.SelectedIndex = 0;

        }

        private void DeleteLocation_Click(object sender, RoutedEventArgs e)
        {
            ConfirmDeleteLocationWindow openConfirm = new ConfirmDeleteLocationWindow();
            openConfirm.ShowDialog();
            GetLocations();
            DataLocationItems.SelectedIndex = 0;
        }

        private void DataLocationItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                if (row_selected["location_quantity"].ToString() == "0")
                {
                    GroupItemData.DataContext = null;
                    GroupItemData.ItemsSource = null;
                }
                else
                {


                    CurrentLocationAdministrationModel.SelectedLocationName = row_selected["location_name"].ToString();
                    CurrentLocationAdministrationModel.SelectedLocationId = row_selected["location_id"].ToString();

                    conn.Close();
                    conn.Open();


                    MySqlCommand cmd = new MySqlCommand($"Select * from item_location_relations WHERE location_id = {row_selected["location_id"]}", conn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adp.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string[] tmpArr = new string[ds.Tables[0].Rows.Count];
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            tmpArr[i] = ds.Tables[0].Rows[i]["item_id"].ToString();
                        }

                        cmd = new MySqlCommand(string.Format("SELECT * FROM item_objects WHERE item_id IN ({0})", string.Join(", ", tmpArr)), conn);
                        adp = new MySqlDataAdapter(cmd);
                        DataSet ds2 = new DataSet();
                        adp.Fill(ds2);

                        ds2.Tables[0].Columns.Add("location_quantity");

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            for (int j = 0; j < ds2.Tables[0].Rows.Count; j++)
                            {
                                if (ds.Tables[0].Rows[i]["item_id"].ToString() == ds2.Tables[0].Rows[j]["item_id"].ToString())
                                {
                                    ds2.Tables[0].Rows[j]["location_quantity"] = ds.Tables[0].Rows[i]["location_item_quantity"];
                                }
                            }
                        }

                        GroupItemData.DataContext = ds2;
                        GroupItemData.ItemsSource = new DataView(ds2.Tables[0]);

                    }
                }
                conn.Close();
            }
            conn.Close();
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            GroupItemData.DataContext = null;
            GroupItemData.ItemsSource = null;
            if (searchBox.Text != "")
            {
                DataSet output = LocationParams.LocationsDB.Copy();
                output.Tables[0].Rows.Clear();

                foreach (DataRow row in LocationParams.LocationsDB.Tables[0].Rows)
                {
                    if (row["location_name"].ToString().Contains(searchBox.Text))
                    {
                        output.Tables[0].ImportRow(row);
                    }
                }
                if (output.Tables[0].Rows.Count == 0)
                {
                    ExportAsCSV.IsEnabled = false;
                }
                else
                {
                    ExportAsCSV.IsEnabled = true;
                }
                DataLocationItems.DataContext = output;
                LocationParams.CurrentLocations = output;
            }
            else
            {
                ExportAsCSV.IsEnabled = true;
                LocationParams.CurrentLocations = LocationParams.LocationsDB;
                DataLocationItems.DataContext = LocationParams.LocationsDB;
                DataLocationItems.SelectedIndex = 0;
            }
        }

        private void EditLocation_Click(object sender, RoutedEventArgs e)
        {
            EditLocationWindow openEdit = new EditLocationWindow();
            openEdit.ShowDialog();
            GetLocations();
            DataLocationItems.SelectedIndex = 0;
        }

        private void ExportAsCSV_Click(object sender, RoutedEventArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add("locationInfo");
            ds.Tables[0].Columns.Add("location_ident");
            ds.Tables[0].Columns.Add("item_ident");
            ds.Tables[0].Columns.Add("item_description1");
            ds.Tables[0].Columns.Add("item_description2");
            ds.Tables[0].Columns.Add("item_quantity");

            for (int i = 0; i < LocationParams.CurrentLocations.Tables[0].Rows.Count; i++)
            {
                DataSet tmp = AdministrationQueries.RunSql($"SELECT * FROM item_location_relations WHERE location_id = {LocationParams.CurrentLocations.Tables[0].Rows[i]["location_id"]}");
                for (int j = 0; j < tmp.Tables[0].Rows.Count; j++)
                {
                    int currentIndex = ds.Tables[0].Rows.Count;
                    ds.Tables[0].Rows.Add();
                    ds.Tables[0].Rows[currentIndex]["location_ident"] = LocationParams.CurrentLocations.Tables[0].Rows[i]["location_name"];
                    ds.Tables[0].Rows[currentIndex]["item_quantity"] = tmp.Tables[0].Rows[j]["location_item_quantity"];

                    DataSet tmpItem = AdministrationQueries.RunSql($"SELECT * FROM item_objects where item_id = {tmp.Tables[0].Rows[j]["item_id"]}");
                    ds.Tables[0].Rows[currentIndex]["item_ident"] = tmpItem.Tables[0].Rows[0]["item_ident"];
                    ds.Tables[0].Rows[currentIndex]["item_description1"] = tmpItem.Tables[0].Rows[0]["item_description"];
                    ds.Tables[0].Rows[currentIndex]["item_description2"] = tmpItem.Tables[0].Rows[0]["item_description_2"];
                }
            }
            string currentTime = DateTime.Now.ToString("d") + "_" + DateTime.Now.ToString("T");
            currentTime = currentTime.Replace(":", ".");
            currentTime = currentTime.Replace("/", "-");

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\wærp-stockpilot", true);
            string path = key.GetValue("HistoryLogsPath").ToString() + "\\location_overview_" + currentTime + ".csv";

            DataTable dt = ds.Tables[0].Copy();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j].ToString() == "")
                    {
                        dt.Rows[i][j] = dt.Rows[i][j] = "0";
                    }


                    if (dt.Rows[i][j].ToString().Contains(";") | dt.Rows[i][j].ToString().Contains(",") | dt.Rows[i][j].ToString().Contains(":"))
                    {
                        dt.Rows[i][j] = dt.Rows[i][j].ToString().Replace(";", " ");
                        dt.Rows[i][j] = dt.Rows[i][j].ToString().Replace(",", " ");
                    }


                    if (dt.Rows[i][j].ToString().Contains("Ö"))
                    {
                        dt.Rows[i][j] = dt.Rows[i][j].ToString().Replace("Ö", "OE");
                    }
                    if (dt.Rows[i][j].ToString().Contains("Ü"))
                    {
                        dt.Rows[i][j] = dt.Rows[i][j].ToString().Replace("Ü", "UE");
                    }
                    if (dt.Rows[i][j].ToString().Contains("Ö"))
                    {
                        dt.Rows[i][j] = dt.Rows[i][j].ToString().Replace("Ä", "AE");
                    }
                    if (dt.Rows[i][j].ToString().Contains("ß"))
                    {
                        dt.Rows[i][j] = dt.Rows[i][j].ToString().Replace("ß", "ss");
                    }


                }

            }

            ToCSV(dt, path);
            ErrorHandlerModel.ErrorText = "Eine aktuelle Kopie der Historie wurde erfolgreich im Ordner gespeichter.";
            ErrorHandlerModel.ErrorType = "SUCCESS";
            ErrorWindow openSuccess = new ErrorWindow();
            openSuccess.ShowDialog();
        }
        private void ToCSV(DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers    
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(", ");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(", ");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
    }
}
