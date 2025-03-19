using MaterialDesignThemes.Wpf;
using MySqlConnector;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using waerp_management.application;
using waerp_management.application.returnItem;
using waerp_management.dbtools;
using waerp_management.sql;
using waerp_management.store;

namespace waerp_management.main
{
    /// <summary>
    /// Interaction logic for mainDashboard.xaml
    /// </summary>
    public partial class mainDashboard : Page
    {

        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
        MySqlConnection conn = new MySqlConnection(SqlConn.GetConnectionString());
        public mainDashboard()
        {
            InitializeComponent();
            fullname.Text = MainWindowViewModel.Fullname;

            DataSet allRents = AdministrationQueries.RunSql("SELECT * FROM item_rents");
            allRents.Tables[0].Columns.Add("item_ident");
            allRents.Tables[0].Columns.Add("item_description");
            allRents.Tables[0].Columns.Add("item_description_2");
            allRents.Tables[0].Columns.Add("item_image_path");
            allRents.Tables[0].Columns.Add("employee");


            if (allRents.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < allRents.Tables[0].Rows.Count; i++)
                {
                    DataSet tmp = AdministrationQueries.RunSql($"SELECT * FROM item_objects WHERE item_id = {allRents.Tables[0].Rows[i]["item_id"]}");
                    if (tmp.Tables[0].Rows.Count != 0)
                    {
                        allRents.Tables[0].Rows[i]["item_ident"] = tmp.Tables[0].Rows[0]["item_ident"];
                        allRents.Tables[0].Rows[i]["item_description"] = tmp.Tables[0].Rows[0]["item_description"];
                        allRents.Tables[0].Rows[i]["item_description_2"] = tmp.Tables[0].Rows[0]["item_description_2"];
                        allRents.Tables[0].Rows[i]["item_image_path"] = tmp.Tables[0].Rows[0]["item_image_path"];

                        DataSet tmp2 = AdministrationQueries.RunSql($"SELECT * FROM users WHERE user_id = {allRents.Tables[0].Rows[i]["user_id"]}");
                        if (tmp2.Tables[0].Rows.Count > 0)
                        {
                            allRents.Tables[0].Rows[i]["employee"] = tmp2.Tables[0].Rows[0]["username"];
                        }

                    }
                }
                dashboardRents.DataContext = allRents;
                dashboardRents.ItemsSource = new DataView(allRents.Tables[0]);
                dashboardRents.SelectedIndex = 0;
                ReturnSelectedItemBtn.IsEnabled = true;
            }
            else
            {
                dashboardRents.DataContext = new DataSet();
                ReturnSelectedItemBtn.IsEnabled = false;
            }

            // Initialize the timer
            //DispatcherTimer timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromSeconds(10);
            //timer.Tick += Timer_Tick;

            //// Start the timer
            //timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Refresh the data
            RefreshData();
        }

        public void RefreshData()
        {
            DataSet allRents = AdministrationQueries.RunSql("SELECT * FROM item_rents");
            allRents.Tables[0].Columns.Add("item_ident");
            allRents.Tables[0].Columns.Add("item_description");
            allRents.Tables[0].Columns.Add("item_description_2");
            allRents.Tables[0].Columns.Add("item_image_path");
            allRents.Tables[0].Columns.Add("employee");


            if (allRents.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < allRents.Tables[0].Rows.Count; i++)
                {
                    DataSet tmp = AdministrationQueries.RunSql($"SELECT * FROM item_objects WHERE item_id = {allRents.Tables[0].Rows[i]["item_id"]}");
                    if (tmp.Tables[0].Rows.Count != 0)
                    {
                        allRents.Tables[0].Rows[i]["item_ident"] = tmp.Tables[0].Rows[0]["item_ident"];
                        allRents.Tables[0].Rows[i]["item_description"] = tmp.Tables[0].Rows[0]["item_description"];
                        allRents.Tables[0].Rows[i]["item_description_2"] = tmp.Tables[0].Rows[0]["item_description_2"];
                        allRents.Tables[0].Rows[i]["item_image_path"] = tmp.Tables[0].Rows[0]["item_image_path"];

                        DataSet tmp2 = AdministrationQueries.RunSql($"SELECT * FROM users WHERE user_id = {allRents.Tables[0].Rows[i]["user_id"]}");
                        if (tmp2.Tables[0].Rows.Count > 0)
                        {
                            allRents.Tables[0].Rows[i]["employee"] = tmp2.Tables[0].Rows[0]["username"];
                        }

                    }
                }
                dashboardRents.DataContext = allRents;
                dashboardRents.ItemsSource = new DataView(allRents.Tables[0]);
                dashboardRents.SelectedIndex = 0;
                ReturnSelectedItemBtn.IsEnabled = true;
            }
            else
            {
                dashboardRents.DataContext = new DataSet();
                ReturnSelectedItemBtn.IsEnabled = false;
            }
        }



        public bool MultiSelect { get; set; }
        public class Item
        {
            public string Artikelnummer { get; set; }
            public string Bezeichnung { get; set; }
            public int Menge { get; set; }
            public PackIconKind IconKind { get; set; }
        }

        private void dataGridCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                CurrentReturnModel.ItemIdent = row_selected["item_id"].ToString();
                CurrentReturnModel.ItemIdentStr = row_selected["item_ident"].ToString();
                CurrentReturnModel.ItemDescription = row_selected["item_description"].ToString();
                CurrentReturnModel.ItemTotalQuantity = row_selected["rent_quantity"].ToString();
                CurrentReturnModel.ItemImagePath = row_selected["item_image_path"].ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/application/RentItem/RentItemView.xaml", UriKind.Relative));

        }
        private void ReturnItem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/application/returnItem/ReturnItemSelect.xaml", UriKind.Relative));

        }
        private void RebookItem(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/application/rebookItem/RebookViewSelection.xaml", UriKind.Relative));

        }
        private void History(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/application/History/HistoryView.xaml", UriKind.Relative));

        }

        private void OpenBarcodeScan_Click(object sender, RoutedEventArgs e)
        {
            ScanBarcodeWindow BarcodeScan = new ScanBarcodeWindow();
            Nullable<bool> dialogResult = BarcodeScan.ShowDialog();
        }

        private void OpenRentItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReturnSelectedItemBtn_Click(object sender, RoutedEventArgs e)
        {
            ReturnSelectionView openReturn = new ReturnSelectionView();
            openReturn.ShowDialog(); RefreshData();


        }

        private void CopyItemIdent(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(CurrentReturnModel.ItemIdentStr);
        }

        private void CopyDescription(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(CurrentReturnModel.ItemDescription);
        }

        private void CopyAll(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(CurrentReturnModel.ItemIdentStr + "; " + CurrentReturnModel.ItemDescription + "; Bestand:" + CurrentReturnModel.ItemTotalQuantity);
        }
    }
}
