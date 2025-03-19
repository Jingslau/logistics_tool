using MySqlConnector;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using waerp_management.application.Administration.ItemAdministration;
using waerp_management.dbtools;
using waerp_management.modules.Administration.ItemAdministration;
using waerp_management.store.Administration;

namespace waerp_management.application.ItemAdministration
{
    /// <summary>
    /// Interaction logic for ItemOverviewView.xaml
    /// </summary>
    public partial class ItemOverviewView : UserControl
    {
        MySqlConnection conn = new MySqlConnection(SqlConn.GetConnectionString());
        DataRow CurrentSelectedItem;
        static class LocationParams
        {
            public static DataSet AllItems = new DataSet();
        }

        public ItemOverviewView()
        {
            InitializeComponent();

            try
            {

                conn.Open();



                MySqlCommand cmd2 = new MySqlCommand("Select * from item_objects", conn);
                MySqlDataAdapter adp2 = new MySqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                adp2.Fill(ds2);
                LocationParams.AllItems = ds2;
                dataGridItems.DataContext = ds2;
                dataGridItems.ItemsSource = new DataView(ds2.Tables[0]);

                conn.Close();
                dataGridItems.SelectedIndex = 0;




            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                conn.Close();
            }
        }

        private void dataGridItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                CurrentSelectedItem = row_selected.Row;
                CurrentItemAdministrationModel.SelectedItem = row_selected.Row;
            }
        }
        private void OpenNewItemDialog_Click(object sender, RoutedEventArgs e)
        {
            AddNewItemView test = new AddNewItemView();
            Nullable<bool> DialogResult = test.ShowDialog();
            DialogResult = false;
            LoadItemData();

        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            dataGridItems.DataContext = null;
            dataGridItems.ItemsSource = null;
            CurrentItemAdministrationModel.currentSelectedTableIndex = dataGridItems.SelectedIndex;
            EditSelectedItemWindow openEdit = new EditSelectedItemWindow();
            openEdit.ShowDialog();
            LoadItemData();
            dataGridItems.SelectedIndex = CurrentItemAdministrationModel.currentSelectedTableIndex;


        }

        private void LoadItemData()
        {
            try
            {

                conn.Open();



                MySqlCommand cmd2 = new MySqlCommand("Select * from item_objects", conn);
                MySqlDataAdapter adp2 = new MySqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                adp2.Fill(ds2);
                LocationParams.AllItems = ds2;
                dataGridItems.DataContext = ds2;
                dataGridItems.ItemsSource = new DataView(ds2.Tables[0]);

                conn.Close();
                dataGridItems.SelectedIndex = 0;




            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                conn.Close();
            }
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchBox.Text != "")
            {
                DataSet output = LocationParams.AllItems.Copy();
                output.Tables[0].Rows.Clear();

                foreach (DataRow row in LocationParams.AllItems.Tables[0].Rows)
                {
                    if (row["item_ident"].ToString().Contains(searchBox.Text) | row["item_description"].ToString().Contains(searchBox.Text))
                    {
                        output.Tables[0].ImportRow(row);
                    }
                }
                dataGridItems.DataContext = output;
                dataGridItems.ItemsSource = new DataView(output.Tables[0]);
            }
            else
            {
                dataGridItems.DataContext = LocationParams.AllItems;
                dataGridItems.ItemsSource = new DataView(LocationParams.AllItems.Tables[0]);

                dataGridItems.SelectedIndex = 0;
            }


        }

        private void AddItemFilter_Click(object sender, RoutedEventArgs e)
        {
            AddNewFilterWindow openAddFilter = new AddNewFilterWindow();
            openAddFilter.ShowDialog();
            LoadItemData();
        }

        private void EditItemFilter_Click(object sender, RoutedEventArgs e)
        {
            EditFilterWindow openFilterEdit = new EditFilterWindow();
            openFilterEdit.ShowDialog();
            LoadItemData();
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            ConfirmDeleteItemWindow openConfirm = new ConfirmDeleteItemWindow();
            openConfirm.ShowDialog();
            LoadItemData();
        }


        private void CopyItemIdent(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(CurrentItemAdministrationModel.SelectedItem["item_ident"].ToString());
        }

        private void CopyDescription(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(CurrentItemAdministrationModel.SelectedItem["item_description"].ToString());
        }

        private void CopyAll(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(CurrentItemAdministrationModel.SelectedItem["item_ident"].ToString() + "; " + CurrentItemAdministrationModel.SelectedItem["item_description"].ToString());
        }
    }
}
