using Microsoft.Win32;
using MySqlConnector;
using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using waerp_management.dbtools;
using waerp_management.errorHandling;

namespace waerp_management.application.History
{
    /// <summary>
    /// Interaktionslogik für HistoryView.xaml
    /// </summary>
    public partial class HistoryView : UserControl
    {
        public class HistoryParams
        {
            public static DataSet CurrentDB = new DataSet();
            public static DataSet HistoryDB = new DataSet();
            public static string SelectedID = "";

        }
        MySqlConnection conn = new MySqlConnection(SqlConn.GetConnectionString());
        public HistoryView()
        {
            InitializeComponent();


            MySqlCommand cmd = new MySqlCommand("Select * from history_log", conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            HistoryParams.HistoryDB = new DataSet();
            HistoryParams.CurrentDB = new DataSet();
            adp.Fill(HistoryParams.HistoryDB);
            if (HistoryParams.HistoryDB.Tables[0].Rows.Count > 0)
            {


                cmd = new MySqlCommand("Select * FROM log_action_names", conn);
                adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataSet ds2 = new DataSet();
                DataSet ds3 = new DataSet();
                DataSet ds4 = new DataSet();
                DataSet ds5 = new DataSet();
                adp.Fill(ds);



                HistoryParams.HistoryDB.Tables[0].Columns.Add("item_ident", typeof(string));
                HistoryParams.HistoryDB.Tables[0].Columns.Add("name_DE", typeof(string));
                HistoryParams.HistoryDB.Tables[0].Columns.Add("item_location_old_name", typeof(string));
                HistoryParams.HistoryDB.Tables[0].Columns.Add("item_location_new_name", typeof(string));
                HistoryParams.HistoryDB.Tables[0].Columns.Add("username", typeof(string));
                HistoryParams.HistoryDB.Tables[0].Columns.Add("date", typeof(string));
                HistoryParams.HistoryDB.Tables[0].Columns.Add("old_zone_name", typeof(string));
                HistoryParams.HistoryDB.Tables[0].Columns.Add("new_zone_name", typeof(string));

                foreach (DataRow row in HistoryParams.HistoryDB.Tables[0].Rows)
                {

                    DateTime oldFormat = DateTime.Parse(row["createdAt"].ToString());
                    string newFormat = oldFormat.ToString("G");
                    row["date"] = newFormat;

                    cmd = new MySqlCommand("Select * FROM item_objects WHERE item_id = " + row["item_id"], conn);
                    adp = new MySqlDataAdapter(cmd);
                    adp.Fill(ds2);
                    cmd = new MySqlCommand("Select * FROM users WHERE user_id = " + row["user_id"], conn);
                    adp = new MySqlDataAdapter(cmd);
                    adp.Fill(ds3);
                    cmd = new MySqlCommand("Select * FROM floor_objects WHERE floor_id = " + row["old_zone"], conn);
                    adp = new MySqlDataAdapter(cmd);
                    adp.Fill(ds4);
                    cmd = new MySqlCommand("Select * FROM floor_objects WHERE floor_id = " + row["new_zone"], conn);
                    adp = new MySqlDataAdapter(cmd);
                    adp.Fill(ds5);
                    foreach (DataRow row3 in ds2.Tables[0].Rows)
                    {
                        row["item_ident"] = row3["item_ident"];
                    }
                    foreach (DataRow row4 in ds3.Tables[0].Rows)
                    {
                        row["username"] = row4["username"];
                    }
                    foreach (DataRow row2 in ds.Tables[0].Rows)
                    {
                        if (row["action_id"].ToString() == row2["action_id"].ToString())
                        {
                            row["name_DE"] = row2["name_DE"];
                        }
                    }


                    if (row["item_location_old"].ToString() != "0")
                    {
                        cmd = new MySqlCommand("Select * FROM location_objects WHERE location_id = " + row["item_location_old"], conn);
                        adp = new MySqlDataAdapter(cmd);
                        adp.Fill(ds2);
                        foreach (DataRow row3 in ds2.Tables[0].Rows)
                        {
                            row["item_location_old_name"] = row3["location_name"];
                        }
                    }
                    if (row["item_location_new"].ToString() != "0")
                    {
                        cmd = new MySqlCommand("Select * FROM location_objects WHERE location_id = " + row["item_location_new"], conn);
                        adp = new MySqlDataAdapter(cmd);
                        adp.Fill(ds2);
                        foreach (DataRow row3 in ds2.Tables[0].Rows)
                        {
                            row["item_location_new_name"] = row3["location_name"];
                        }
                    }
                    if (row["old_zone"].ToString() != "0")
                    {
                        cmd = new MySqlCommand("Select * FROM floor_objects WHERE floor_id = " + row["old_zone"], conn);
                        adp = new MySqlDataAdapter(cmd);
                        adp.Fill(ds2);
                        foreach (DataRow row3 in ds2.Tables[0].Rows)
                        {
                            row["old_zone_name"] = row3["floor_name"];
                        }
                    }
                    if (row["new_zone"].ToString() != "0")
                    {
                        cmd = new MySqlCommand("Select * FROM floor_objects WHERE floor_id = " + row["new_zone"], conn);
                        adp = new MySqlDataAdapter(cmd);
                        adp.Fill(ds2);
                        foreach (DataRow row3 in ds2.Tables[0].Rows)
                        {
                            row["new_zone_name"] = row3["floor_name"];
                        }
                    }

                }

                adp.Fill(HistoryParams.HistoryDB);
                HistoryParams.CurrentDB = HistoryParams.HistoryDB;
                HistoryParams.HistoryDB.Tables[0].Rows.RemoveAt(HistoryParams.HistoryDB.Tables[0].Rows.Count - 1);
                dataGridItems.DataContext = HistoryParams.HistoryDB;
                dataGridItems.ItemsSource = new DataView(HistoryParams.HistoryDB.Tables[0]);
                conn.Close();
                dataGridItems.Items.SortDescriptions.Clear();
                dataGridItems.Items.SortDescriptions.Add(new SortDescription("history_id", ListSortDirection.Descending));
                dataGridItems.Items.Refresh();
                ExportDataBtn.IsEnabled = true;
                searchBox.IsEnabled = true;
            }
            else
            {
                DataSet ds = new DataSet();
                ds.Tables.Add("empty");
                dataGridItems.DataContext = ds;
                dataGridItems.ItemsSource = new DataView(ds.Tables[0]);
                ExportDataBtn.IsEnabled = false;
                searchBox.IsEnabled = false;
            }
        }

        private void dataGridItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                HistoryParams.SelectedID = row_selected["history_id"].ToString();
            }
        }



        private void ExportDataBtn_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = HistoryParams.CurrentDB.Tables[0].Copy();
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

                }

            }
            string currentTime = DateTime.Now.ToString("d") + "_" + DateTime.Now.ToString("T");
            currentTime = currentTime.Replace(":", ".");
            currentTime = currentTime.Replace("/", "-");

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\wærp-stockpilot", true);
            string path = key.GetValue("HistoryLogsPath").ToString() + "\\history_overview_" + currentTime + ".csv";



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

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchBox.Text != "")
            {
                DataSet ds = HistoryParams.HistoryDB.Copy();
                DataSet output = HistoryParams.HistoryDB.Copy();
                output.Tables[0].Rows.Clear();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["item_ident"].ToString().Contains(searchBox.Text)
                        | row["username"].ToString().Contains(searchBox.Text)
                        | row["item_location_old_name"].ToString().Contains(searchBox.Text)
                        | row["item_location_new_name"].ToString().Contains(searchBox.Text)
                        | row["old_zone_name"].ToString().Contains(searchBox.Text)
                        | row["new_zone_name"].ToString().Contains(searchBox.Text)
                        | row["name_DE"].ToString().Contains(searchBox.Text))

                    {
                        output.Tables[0].ImportRow(row);
                    }
                }
                HistoryParams.CurrentDB = output;
                dataGridItems.DataContext = output;
                dataGridItems.ItemsSource = new DataView(output.Tables[0]);
                dataGridItems.Items.SortDescriptions.Clear();
                dataGridItems.Items.SortDescriptions.Add(new SortDescription("history_id", ListSortDirection.Descending));
                dataGridItems.Items.Refresh();
                if (output.Tables[0].Rows.Count == 0)
                {
                    ExportDataBtn.IsEnabled = false;
                }
            }
            else
            {
                dataGridItems.DataContext = HistoryParams.HistoryDB;
                dataGridItems.ItemsSource = new DataView(HistoryParams.HistoryDB.Tables[0]);
                dataGridItems.Items.SortDescriptions.Clear();
                dataGridItems.Items.SortDescriptions.Add(new SortDescription("history_id", ListSortDirection.Descending));
                dataGridItems.Items.Refresh();
                searchBox.IsEnabled = true;
                ExportDataBtn.IsEnabled = true;
            }
        }
    }
}
