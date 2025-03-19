using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using waerp_management.dbtools;
using waerp_management.errorHandling;
using waerp_management.modules.returnItem;
using waerp_management.sql;
using waerp_management.store;

namespace waerp_management.application.returnItem
{
    /// <summary>
    /// Interaction logic for ReturnItemSelect.xaml
    /// </summary>
    static class SearchParams
    {
        public static int counter = 1;
        public static string machine;
        public static string selectedUserID;
        public static string filter_1;
        public static string currentSelectedFilter;
        public static string filter_1_id;
        public static string selectedItem;
        public static string[] breadcrumbs = new string[5];
        public static string[] SearchParamArr = new string[5];
        public static DataSet currentDataItems = new DataSet();

        public static List<string> breadcrumbList = new List<string> { "", "", "", "", "", "" };
        public static DataSet allRents = new DataSet();

        public static DataSet CurrentSelectedUserRents = new DataSet();
    }

    public partial class ReturnItemSelect : UserControl
    {

        MySqlConnection conn = new MySqlConnection(SqlConn.GetConnectionString());
        public ReturnItemSelect()
        {
            InitializeComponent();

            SearchParams.counter = 1;
            SearchParams.filter_1 = "";
            SearchParams.currentSelectedFilter = "";
            SearchParams.filter_1_id = "";
            SearchParams.selectedItem = "";
            SearchParams.breadcrumbList = new List<string> { "", "", "", "", "", "" };
            SearchParams.breadcrumbs = new string[5];
            SearchParams.SearchParamArr = new string[5];

            GetAllRents();



        }
        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dataGridItems.Items.Count != 0)
            {
                if (toggleUsers.IsChecked == false)
                {

                    if (searchBox.Text != "")
                    {
                        DataSet output = SearchParams.allRents.Copy();
                        output.Tables[0].Rows.Clear();

                        foreach (DataRow row in SearchParams.allRents.Tables[0].Rows)
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
                        dataGridItems.DataContext = SearchParams.allRents;
                        dataGridItems.ItemsSource = new DataView(SearchParams.allRents.Tables[0]);
                    }
                }
                else
                {
                    if (searchBox.Text != "")
                    {
                        DataSet output = SearchParams.allRents.Copy();
                        output.Tables[0].Rows.Clear();

                        foreach (DataRow row in SearchParams.allRents.Tables[0].Rows)
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
                        dataGridItems.DataContext = SearchParams.allRents;
                        dataGridItems.ItemsSource = new DataView(SearchParams.allRents.Tables[0]);
                    }
                }
            }
        }
        private void GetAllFilterForSelection()
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM item_rents WHERE user_id = {SearchParams.selectedUserID}", conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            SearchParams.allRents = new DataSet();
            adp.Fill(SearchParams.allRents, "itemData");

            List<string> AllRentsItem = new List<string>();
            for (int i = 0; i < SearchParams.allRents.Tables[0].Rows.Count; i++)
            {
                AllRentsItem.Add(SearchParams.allRents.Tables[0].Rows[i]["item_id"].ToString());
            }
            string[] RentItemsArr = new string[AllRentsItem.Count];
            for (int i = 0; i < AllRentsItem.Count; i++)
            {
                RentItemsArr[i] = AllRentsItem[i].ToString();
            }
            var sql = string.Format("SELECT * FROM item_filter_relations WHERE item_id IN ({0})", string.Join(", ", RentItemsArr));
            cmd = new MySqlCommand(sql, conn);
            adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();


            adp.Fill(ds);

            bool checkFilter = false;
            List<string> FilterIDs = new List<string>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                checkFilter = false;
                for (int j = 0; j < FilterIDs.Count; j++)
                {
                    if (FilterIDs[j] == ds.Tables[0].Rows[i]["filter1_id"].ToString())
                    {
                        checkFilter = true;
                    }
                }
                if (!checkFilter)
                {
                    FilterIDs.Add(ds.Tables[0].Rows[i]["filter1_id"].ToString());
                }
            }

            String[] FilterIDArr = new String[FilterIDs.Count];
            for (int i = 0; i < FilterIDs.Count; i++)
            {
                FilterIDArr[i] = FilterIDs[i].ToString();
            }



            sql = string.Format("SELECT * FROM filter1_names WHERE filter_id IN ({0})", string.Join(", ", FilterIDArr));
            cmd = new MySqlCommand(sql, conn);
            adp = new MySqlDataAdapter(cmd);
            ds = new DataSet();
            adp.Fill(ds, "filterData");
            conn.Close();
            dataGridFilter.DataContext = ds;
        }

        private void GetAllRents()
        {
            SearchParams.counter = 1;
            SearchParams.filter_1 = "";
            SearchParams.currentSelectedFilter = "";
            SearchParams.filter_1_id = "";
            SearchParams.selectedItem = "";
            SearchParams.breadcrumbList = new List<string> { "", "", "", "", "", "" };
            SearchParams.breadcrumbs = new string[5];
            SearchParams.SearchParamArr = new string[5];

            List<int> UserListIDs = new List<int>();
            List<string> ItemListIDs = new List<string>();

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
                    ItemListIDs.Add(allRents.Tables[0].Rows[i]["item_id"].ToString());

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
                    bool isCopy = false;
                    for (int j = 0; j < UserListIDs.Count; j++)
                    {
                        if (UserListIDs[j] == int.Parse(allRents.Tables[0].Rows[i]["user_id"].ToString()))
                        {
                            isCopy = true;
                        }
                    }
                    if (!isCopy)
                    {
                        UserListIDs.Add(int.Parse(allRents.Tables[0].Rows[i]["user_id"].ToString()));
                    }
                }

                string[] tmpIds = new string[UserListIDs.Count];
                for (int i = 0; i < UserListIDs.Count; i++)
                {
                    tmpIds[i] = UserListIDs[i].ToString();
                }
                string[] tmpItemIDs = new string[ItemListIDs.Count];
                for (int i = 0; i < ItemListIDs.Count; i++)
                {
                    tmpItemIDs[i] = ItemListIDs[i].ToString();
                }


                DataSet users = AdministrationQueries.RunSql(string.Format("SELECT * FROM users WHERE user_id IN ({0})", string.Join(", ", tmpIds)));
                userData.DataContext = users;
                userData.ItemsSource = new DataView(users.Tables[0]);

                SearchParams.allRents = allRents;
                dataGridItems.DataContext = allRents;
                dataGridItems.ItemsSource = new DataView(allRents.Tables[0]);
                dataGridItems.SelectedIndex = 0;

                SearchParams.currentDataItems = allRents;
                OpenRentBtn.IsEnabled = true;
                DelteRentBtn.IsEnabled = true;


                //Get ItemFilters
                DataSet ItemFilters = AdministrationQueries.RunSql(string.Format("SELECT * FROM item_filter_relations WHERE item_id IN ({0})", string.Join(", ", tmpItemIDs)));
                bool checkFilter = false;
                List<string> FilterIDs = new List<string>();
                for (int i = 0; i < ItemFilters.Tables[0].Rows.Count; i++)
                {
                    checkFilter = false;
                    for (int j = 0; j < FilterIDs.Count; j++)
                    {
                        if (FilterIDs[j] == ItemFilters.Tables[0].Rows[i]["filter1_id"].ToString())
                        {
                            checkFilter = true;
                        }
                    }
                    if (!checkFilter)
                    {
                        FilterIDs.Add(ItemFilters.Tables[0].Rows[i]["filter1_id"].ToString());
                    }
                }

                String[] FilterIDArr = new String[FilterIDs.Count];
                for (int i = 0; i < FilterIDs.Count; i++)
                {
                    FilterIDArr[i] = FilterIDs[i].ToString();
                }

                DataSet Filter1Names = AdministrationQueries.RunSql(string.Format("SELECT * FROM filter1_names WHERE filter_id IN ({0})", string.Join(", ", FilterIDArr)));
                dataGridFilter.DataContext = Filter1Names;
                dataGridFilter.ItemsSource = new DataView(Filter1Names.Tables[0]);
            }
            else
            {
                OpenRentBtn.IsEnabled = false;
                DelteRentBtn.IsEnabled = false;
            }

        }

        private void toggleAllUsers(object sender, RoutedEventArgs e)
        {
            ResetSearchPrompt();
            if (toggleUsers.IsChecked == true)
            {
                searchBox.Text = "";
                searchBox.IsEnabled = true;
                GetAllRents();
                userData.IsEnabled = false;
                nextBtn.IsEnabled = false;
                backBtn.IsEnabled = false;
                breadcrumbData.Text = "";
            }
            else
            {
                searchBox.IsEnabled = false;
                searchBox.Text = "";
                userData.IsEnabled = true;
                SearchParams.counter = 1;
                SearchParams.filter_1 = "";
                SearchParams.currentSelectedFilter = "";
                SearchParams.filter_1_id = "";
                SearchParams.selectedItem = "";
                SearchParams.breadcrumbs = new string[5];
                SearchParams.SearchParamArr = new string[5];

                breadcrumbData.Text = string.Empty;
                dataGridFilter.DataContext = null;
                dataGridItems.DataContext = null;
                dataGridItems.ItemsSource = null;
                dataGridFilter.ItemsSource = null;
                conn.Open();
                MySqlCommand cmd2 = new MySqlCommand("Select * from filter1_names", conn);
                MySqlDataAdapter adp2 = new MySqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                adp2.Fill(ds2, "filterData");
                SearchParams.currentDataItems = ds2;
                dataGridFilter.DataContext = ds2;
                conn.Close();
                nextBtn.IsEnabled = false;
                backBtn.IsEnabled = false;
                breadcrumbData.Text = "";
                DelteRentBtn.IsEnabled = false;
                OpenRentBtn.IsEnabled = false;
            }
        }

        private void userData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            SearchParams.counter = 1;
            SearchParams.currentSelectedFilter = "";
            SearchParams.selectedItem = "";
            SearchParams.breadcrumbs = new string[5];
            SearchParams.SearchParamArr = new string[5];
            OpenRentBtn.IsEnabled = false;
            DelteRentBtn.IsEnabled = false;


            dataGridItems.DataContext = null;

            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                SearchParams.selectedUserID = row_selected["user_id"].ToString();


                List<string> ItemListIDs = new List<string>();
                DataSet allRents = AdministrationQueries.RunSql($"SELECT * FROM item_rents WHERE user_id = {row_selected["user_id"]}");
                allRents.Tables[0].Columns.Add("item_ident");
                allRents.Tables[0].Columns.Add("item_description");
                allRents.Tables[0].Columns.Add("item_description_2");
                allRents.Tables[0].Columns.Add("item_image_path");
                allRents.Tables[0].Columns.Add("employee");


                if (allRents.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < allRents.Tables[0].Rows.Count; i++)
                    {
                        ItemListIDs.Add(allRents.Tables[0].Rows[i]["item_id"].ToString());

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

                    SearchParams.CurrentSelectedUserRents = allRents;
                    SearchParams.allRents = allRents;

                    dataGridItems.DataContext = allRents;
                    dataGridItems.ItemsSource = new DataView(allRents.Tables[0]);
                    SearchParams.currentDataItems = allRents;
                    dataGridItems.SelectedIndex = 0;
                    OpenRentBtn.IsEnabled = true;
                    DelteRentBtn.IsEnabled = true;
                    string[] tmpItemIDs = new string[ItemListIDs.Count];
                    for (int i = 0; i < ItemListIDs.Count; i++)
                    {
                        tmpItemIDs[i] = ItemListIDs[i].ToString();
                    }
                    DataSet ItemFilters = AdministrationQueries.RunSql(string.Format("SELECT * FROM item_filter_relations WHERE item_id IN ({0})", string.Join(", ", tmpItemIDs)));
                    bool checkFilter = false;
                    List<string> FilterIDs = new List<string>();
                    for (int i = 0; i < ItemFilters.Tables[0].Rows.Count; i++)
                    {
                        checkFilter = false;
                        for (int j = 0; j < FilterIDs.Count; j++)
                        {
                            if (FilterIDs[j] == ItemFilters.Tables[0].Rows[i]["filter1_id"].ToString())
                            {
                                checkFilter = true;
                            }
                        }
                        if (!checkFilter)
                        {
                            FilterIDs.Add(ItemFilters.Tables[0].Rows[i]["filter1_id"].ToString());
                        }
                    }

                    String[] FilterIDArr = new String[FilterIDs.Count];
                    for (int i = 0; i < FilterIDs.Count; i++)
                    {
                        FilterIDArr[i] = FilterIDs[i].ToString();
                    }

                    DataSet Filter1Names = AdministrationQueries.RunSql(string.Format("SELECT * FROM filter1_names WHERE filter_id IN ({0})", string.Join(", ", FilterIDArr)));
                    dataGridFilter.DataContext = Filter1Names;
                    dataGridFilter.ItemsSource = new DataView(Filter1Names.Tables[0]);
                }
                else
                {
                    OpenRentBtn.IsEnabled = false;
                    DelteRentBtn.IsEnabled = false;
                }
            }
            conn.Close();
        }
        private void filterData_SelectionChanged(object sender, RoutedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                OpenRentBtn.IsEnabled = false;
                DelteRentBtn.IsEnabled = false;
                SearchParams.currentSelectedFilter = row_selected["filter_id"].ToString();
                SearchParams.SearchParamArr[SearchParams.counter - 1] = row_selected["filter_id"].ToString();
                List<string> itemsReturnList = new List<string>();
                int index = SearchParams.counter - 1;
                SearchParams.breadcrumbs[SearchParams.counter - 1] = row_selected["name"].ToString();
                SearchParams.breadcrumbList[index] = row_selected["name"].ToString();
                breadcrumbBuild(false);

                conn.Open();


                String stageSearchStr = "";

                for (int i = 1; i <= SearchParams.counter; i++)
                {
                    stageSearchStr += "filter" + i.ToString() + "_id = '" + SearchParams.SearchParamArr[i - 1] + "'";
                    if (i != SearchParams.counter)
                    {
                        stageSearchStr += " AND ";
                    }
                }


                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM item_filter_relations WHERE " + stageSearchStr, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);



                dataGridItems.DataContext = ds;
                dataGridItems.ItemsSource = new DataView(ds.Tables[0]);
                SearchParams.currentDataItems = ds;
                List<string> strDetailIDList = new List<string>();

                if (toggleUsers.IsChecked == true)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        foreach (DataRow row2 in SearchParams.allRents.Tables[0].Rows)
                        {
                            if (row["item_id"].ToString() == row2["item_id"].ToString())
                            {
                                strDetailIDList.Add(row["item_id"].ToString());
                            }
                        }

                    }

                    String[] tmpArr = new string[strDetailIDList.Count];
                    for (int i = 0; i < strDetailIDList.Count; i++)
                    {
                        tmpArr[i] = strDetailIDList[i].ToString();
                    }


                    if (tmpArr.Length > 0)
                    {
                        List<string> ItemListIDs = new List<string>();
                        DataSet allRents = AdministrationQueries.RunSql(string.Format("SELECT * FROM item_rents WHERE item_id IN ({0})", string.Join(", ", tmpArr)));
                        allRents.Tables[0].Columns.Add("item_ident");
                        allRents.Tables[0].Columns.Add("item_description");
                        allRents.Tables[0].Columns.Add("item_description_2");
                        allRents.Tables[0].Columns.Add("item_image_path");
                        allRents.Tables[0].Columns.Add("employee");


                        if (allRents.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < allRents.Tables[0].Rows.Count; i++)
                            {
                                ItemListIDs.Add(allRents.Tables[0].Rows[i]["item_id"].ToString());

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

                            SearchParams.currentDataItems = allRents;
                            SearchParams.allRents = allRents;

                            dataGridItems.DataContext = allRents;
                            dataGridItems.ItemsSource = new DataView(allRents.Tables[0]);
                            dataGridItems.SelectedIndex = 0;
                            SearchParams.currentDataItems = allRents;
                            OpenRentBtn.IsEnabled = true;
                            DelteRentBtn.IsEnabled = true;
                        }


                        checkButtons(allRents);
                    }
                    else
                    {
                        dataGridItems.DataContext = null;
                        dataGridItems.ItemsSource = null;
                    }
                    conn.Close();
                }
                else
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        foreach (DataRow row2 in SearchParams.CurrentSelectedUserRents.Tables[0].Rows)
                        {
                            if (row["item_id"].ToString() == row2["item_id"].ToString())
                            {
                                strDetailIDList.Add(row["item_id"].ToString());
                            }
                        }

                    }

                    String[] tmpArr = new string[strDetailIDList.Count];
                    for (int i = 0; i < strDetailIDList.Count; i++)
                    {
                        tmpArr[i] = strDetailIDList[i].ToString();
                    }


                    if (tmpArr.Length > 0)
                    {
                        List<string> ItemListIDs = new List<string>();
                        DataSet allRents = AdministrationQueries.RunSql(string.Format("SELECT * FROM item_rents WHERE " + $"user_id = {SearchParams.selectedUserID} " + "AND item_id IN ({0})", string.Join(", ", tmpArr)));
                        allRents.Tables[0].Columns.Add("item_ident");
                        allRents.Tables[0].Columns.Add("item_description");
                        allRents.Tables[0].Columns.Add("item_description_2");
                        allRents.Tables[0].Columns.Add("item_image_path");
                        allRents.Tables[0].Columns.Add("employee");


                        if (allRents.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < allRents.Tables[0].Rows.Count; i++)
                            {
                                ItemListIDs.Add(allRents.Tables[0].Rows[i]["item_id"].ToString());

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

                            SearchParams.currentDataItems = allRents;
                            SearchParams.allRents = allRents;

                            dataGridItems.DataContext = allRents;
                            dataGridItems.ItemsSource = new DataView(allRents.Tables[0]);
                            dataGridItems.SelectedIndex = 0;
                            SearchParams.currentDataItems = allRents;
                            OpenRentBtn.IsEnabled = true;
                            DelteRentBtn.IsEnabled = true;
                        }


                        checkButtons(allRents);
                    }
                    else
                    {
                        dataGridItems.DataContext = null;
                        dataGridItems.ItemsSource = null;
                    }
                    conn.Close();

                }

                conn.Close();

            }

            conn.Close();

        }

        private void machineData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                SearchParams.machine = row_selected["name"].ToString();
            }
        }

        private void breadcrumbBuild(Boolean itemSelected)
        {
            breadcrumbData.Text = "";
            if (itemSelected == true)
            {
                breadcrumbData.Text = "..:: " + SearchParams.breadcrumbList.ElementAt(0);
                if (SearchParams.counter > 1)
                {
                    for (var i = 1; i < SearchParams.counter; i++)
                    {
                        breadcrumbData.Text += " : " + SearchParams.breadcrumbList.ElementAt(i);
                    }
                }
                breadcrumbData.Text = breadcrumbData.Text + " :: " + CurrentReturnModel.ItemIdentStr;

            }
            else
            {
                if (SearchParams.counter == 1)
                {
                    breadcrumbData.Text = "..:: " + SearchParams.breadcrumbList.ElementAt(0);
                }
                else
                {
                    breadcrumbData.Text = "..:: " + SearchParams.breadcrumbList.ElementAt(0);
                    for (var i = 1; i < SearchParams.counter; i++)
                    {
                        breadcrumbData.Text += " : " + SearchParams.breadcrumbList.ElementAt(i);
                    }
                }
            }
        }

        private void getNextStage(object sender, RoutedEventArgs e)
        {
            if (SearchParams.counter == 0)
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM item_objects", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                adp.Fill(ds);
                dataGridItems.DataContext = ds;
                dataGridItems.ItemsSource = new DataView(ds.Tables[0]);
                conn.Close();
            }
            else
            {
                if (SearchParams.counter < 5)
                {
                    resetSearchBtn.IsEnabled = true;
                    string[] itemArr = new String[SearchParams.currentDataItems.Tables[0].Rows.Count];
                    int counter = 0;
                    for (int i = 0; i < SearchParams.currentDataItems.Tables[0].Rows.Count; i++)
                    {
                        itemArr[counter] = SearchParams.currentDataItems.Tables[0].Rows[i]["item_id"].ToString();
                        counter++;
                    }

                    DataSet ds = new DataSet();
                    ds = ReturnItemQueries.GetItemFilterRelationsSQL(SearchParams.counter, SearchParams.SearchParamArr, itemArr);
                    ds = RentItemQueries.GetFilterNames(SearchParams.counter + 1, FilterIDBuilder(ds, SearchParams.counter + 1));
                    dataGridFilter.DataContext = ds;
                    dataGridFilter.ItemsSource = new DataView(ds.Tables[0]);
                    SearchParams.counter++;
                    backBtn.IsEnabled = true;
                    nextBtn.IsEnabled = false;
                }
                else
                {
                    ErrorHandlerModel.ErrorText = "Es ist ein Fehler bei der Suche aufgetreten!";
                    ErrorHandlerModel.ErrorType = "NOTALLOWED";
                    ErrorWindow showError = new ErrorWindow();
                    showError.ShowDialog();
                }

            }

        }
        private string[] FilterIDBuilder(DataSet ds, int id)
        {
            List<string> strDetailIDList = new List<string>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                strDetailIDList.Add(row[$"filter{id}_id"].ToString());
            }

            String[] tmpArr = new string[strDetailIDList.Count];
            for (int i = 0; i < strDetailIDList.Count; i++)
            {
                tmpArr[i] = strDetailIDList[i].ToString();
            }
            return tmpArr;

        }

        private String BobTheSQLBuilder()
        {
            String returnSQL = "SELECT * FROM item_filter_relations WHERE ";
            for (int i = 0; i < SearchParams.counter; i++)
            {
                returnSQL += $"filter{i + 1}_id = '{SearchParams.SearchParamArr[i]}'";
                if (i < SearchParams.counter - 1)
                {
                    returnSQL += " AND ";
                }
            }

            instructionText.Text = returnSQL;


            return returnSQL;
        }

        private void checkButtons(DataSet ds)
        {
            List<string> strDetailIDList = new List<string>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                strDetailIDList.Add(row["item_id"].ToString());
            }

            String[] tmpArr = new string[strDetailIDList.Count];
            for (int i = 0; i < strDetailIDList.Count; i++)
            {
                tmpArr[i] = strDetailIDList[i].ToString();
            }
            if (tmpArr.Length == 1)
            {
                nextBtn.IsEnabled = false;
            }
            else
            {
                nextBtn.IsEnabled = true;
            }


        }

        private void getLastStage(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            if (SearchParams.counter == 2)
            {
                backBtn.IsEnabled = false;
                resetSearchBtn.IsEnabled = false;
                nextBtn.IsEnabled = false;
                breadcrumbData.Text = "";
                if (toggleUsers.IsChecked == true)
                {
                    GetAllRents();
                }
                else
                {

                    LoadUserDataItems();
                }
            }
            else
            {


                if (SearchParams.counter < 4)
                {
                    string[] itemArr = new String[SearchParams.currentDataItems.Tables[0].Rows.Count];
                    int counter = 0;
                    for (int i = 0; i < SearchParams.currentDataItems.Tables[0].Rows.Count; i++)
                    {
                        itemArr[counter] = SearchParams.currentDataItems.Tables[0].Rows[i]["item_id"].ToString();
                        counter++;
                    }

                    SearchParams.SearchParamArr[SearchParams.counter - 2] = "";
                    ds = ReturnItemQueries.GetItemFilterRelationsSQL(SearchParams.counter - 2, SearchParams.SearchParamArr, itemArr);
                    ds = RentItemQueries.GetFilterNames(SearchParams.counter - 1, FilterIDBuilder(ds, SearchParams.counter - 1));
                    dataGridFilter.DataContext = ds;
                    SearchParams.currentDataItems = ds;
                    dataGridFilter.ItemsSource = new DataView(ds.Tables[0]);
                    SearchParams.counter--;
                    breadcrumbBuild(false);
                    nextBtn.IsEnabled = false;
                }
            }
            if (SearchParams.counter == 1)
            {
                breadcrumbData.Text = "";
            }

        }



        private void resetSearch(object sender, EventArgs e)
        {
            SearchParams.counter = 1;
            SearchParams.filter_1 = "";
            SearchParams.currentSelectedFilter = "";
            SearchParams.filter_1_id = "";
            SearchParams.selectedItem = "";
            SearchParams.breadcrumbs = new string[5];
            SearchParams.SearchParamArr = new string[5];
            int index = userData.SelectedIndex;
            backBtn.IsEnabled = false;
            resetSearchBtn.IsEnabled = false;
            nextBtn.IsEnabled = false;
            breadcrumbData.Text = "";
            OpenRentBtn.IsEnabled = false;
            DelteRentBtn.IsEnabled = false;
            if (toggleUsers.IsChecked == true)
            {
                GetAllRents();
            }
            else
            {
                LoadUserDataItems();
                dataGridFilter.DataContext = null;
                dataGridFilter.ItemsSource = null;
                dataGridItems.DataContext = null;
                dataGridItems.ItemsSource = null;
            }






        }
        private void ResetSearchPrompt()
        {

            SearchParams.counter = 1;
            SearchParams.currentSelectedFilter = "";
            SearchParams.selectedItem = "";
            SearchParams.breadcrumbs = new string[5];
            SearchParams.SearchParamArr = new string[5];
            OpenRentBtn.IsEnabled = false;
            DelteRentBtn.IsEnabled = false;


            dataGridItems.DataContext = null;
            conn.Open();
            MySqlCommand cmd2 = new MySqlCommand("Select * from filter1_names", conn);
            MySqlDataAdapter adp2 = new MySqlDataAdapter(cmd2);
            DataSet ds2 = new DataSet();
            adp2.Fill(ds2, "filterData");

            breadcrumbData.Text = "";
            dataGridFilter.DataContext = ds2;
            conn.Close();
        }
        private void ItemData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OpenRentBtn.IsEnabled = true;
            DelteRentBtn.IsEnabled = true;
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                if (CurrentReturnModel.MachineID != "")
                {
                    OpenRentBtn.IsEnabled = true;
                    DelteRentBtn.IsEnabled = true;
                }
                CurrentReturnModel.ItemIdent = row_selected["item_id"].ToString();
                CurrentReturnModel.ItemIdentStr = row_selected["item_ident"].ToString();
                CurrentReturnModel.ItemDescription = row_selected["item_description"].ToString();
                CurrentReturnModel.ItemTotalQuantity = row_selected["rent_quantity"].ToString();
                CurrentReturnModel.ItemImagePath = row_selected["item_image_path"].ToString();
                breadcrumbBuild(true);
            }
        }

        private void openRentWindow(object sender, RoutedEventArgs e)
        {
            if (CurrentReturnModel.MachineID != null || CurrentReturnModel.MachineID != "")
            {
                ReturnSelectionView test = new ReturnSelectionView();
                Nullable<bool> dialogResult = test.ShowDialog();
                GetAllRents();

            }
            else
            {
                ErrorHandlerModel.ErrorText = "Bitte wählen Sie eine Maschine!";
                ErrorHandlerModel.ErrorType = "NOTALLOWED";
                ErrorWindow showError = new ErrorWindow();
                showError.ShowDialog();
            }


        }

        private void DelteRentBtn_Click(object sender, RoutedEventArgs e)
        {
            ConfirmDeleteReturn openConfirm = new ConfirmDeleteReturn();
            openConfirm.ShowDialog();
            OpenRentBtn.IsEnabled = false;
            DelteRentBtn.IsEnabled = false;
            SearchParams.counter = 1;
            SearchParams.filter_1 = "";
            SearchParams.currentSelectedFilter = "";
            SearchParams.filter_1_id = "";
            SearchParams.selectedItem = "";
            SearchParams.breadcrumbList = new List<string> { "", "", "", "", "", "" };
            SearchParams.breadcrumbs = new string[5];
            SearchParams.SearchParamArr = new string[5];

            GetAllRents();
        }

        private void resetSearch(object sender, RoutedEventArgs e)
        {
            OpenRentBtn.IsEnabled = false;
            DelteRentBtn.IsEnabled = false;

            SearchParams.counter = 1;
            SearchParams.filter_1 = "";
            SearchParams.currentSelectedFilter = "";
            SearchParams.filter_1_id = "";
            SearchParams.selectedItem = "";
            SearchParams.breadcrumbList = new List<string> { "", "", "", "", "", "" };
            SearchParams.breadcrumbs = new string[5];
            SearchParams.SearchParamArr = new string[5];

            int index = userData.SelectedIndex;
            backBtn.IsEnabled = false;
            resetSearchBtn.IsEnabled = true;
            nextBtn.IsEnabled = false;
            breadcrumbData.Text = "";
            OpenRentBtn.IsEnabled = false;
            DelteRentBtn.IsEnabled = false;
            GetAllRents();
            toggleUsers.IsChecked = true;


        }

        private void LoadUserDataItems()
        {
            SearchParams.counter = 1;
            SearchParams.filter_1 = "";
            SearchParams.currentSelectedFilter = "";
            SearchParams.filter_1_id = "";
            SearchParams.selectedItem = "";
            SearchParams.breadcrumbList = new List<string> { "", "", "", "", "", "" };
            SearchParams.breadcrumbs = new string[5];
            SearchParams.SearchParamArr = new string[5];
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from item_rents WHERE user_id = '" + SearchParams.selectedUserID + "'", conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            SearchParams.CurrentSelectedUserRents = ds;
            List<string> strDetailIDList = new List<string>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                strDetailIDList.Add(row["item_id"].ToString());
            }

            String[] tmpArr = new string[strDetailIDList.Count];
            for (int i = 0; i < strDetailIDList.Count; i++)
            {
                tmpArr[i] = strDetailIDList[i].ToString();
            }

            if (tmpArr.Length == 0)
            {
                ds = new DataSet();
                adp.Fill(ds, "itemData");
                dataGridItems.DataContext = ds;
                SearchParams.currentDataItems = ds;
            }
            else
            {
                var sql = string.Format("SELECT * FROM item_objects WHERE item_id IN ({0})", string.Join(", ", tmpArr));
                cmd = new MySqlCommand(sql, conn);
                adp = new MySqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                adp.Fill(ds2, "itemData");

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    foreach (DataRow row2 in ds2.Tables[0].Rows)
                    {
                        if (row["item_id"].ToString() == row2["item_id"].ToString())
                        {
                            row2["item_quantity_total"] = row["rent_quantity"];
                        }
                    }
                }

                dataGridItems.DataContext = ds2;

                List<string> AllRentsItem = new List<string>();
                for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                {
                    AllRentsItem.Add(ds2.Tables[0].Rows[i]["item_id"].ToString());
                }
                string[] RentItemsArr = new string[AllRentsItem.Count];
                for (int i = 0; i < AllRentsItem.Count; i++)
                {
                    RentItemsArr[i] = AllRentsItem[i].ToString();
                }
                sql = string.Format("SELECT * FROM item_filter_relations WHERE item_id IN ({0})", string.Join(", ", RentItemsArr));
                cmd = new MySqlCommand(sql, conn);
                adp = new MySqlDataAdapter(cmd);
                ds = new DataSet();


                adp.Fill(ds);

                bool checkFilter = false;
                List<string> FilterIDs = new List<string>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    checkFilter = false;
                    for (int j = 0; j < FilterIDs.Count; j++)
                    {
                        if (FilterIDs[j] == ds.Tables[0].Rows[i]["filter1_id"].ToString())
                        {
                            checkFilter = true;
                        }
                    }
                    if (!checkFilter)
                    {
                        FilterIDs.Add(ds.Tables[0].Rows[i]["filter1_id"].ToString());
                    }
                }

                String[] FilterIDArr = new String[FilterIDs.Count];
                for (int i = 0; i < FilterIDs.Count; i++)
                {
                    FilterIDArr[i] = FilterIDs[i].ToString();
                }



                sql = string.Format("SELECT * FROM filter1_names WHERE filter_id IN ({0})", string.Join(", ", FilterIDArr));
                cmd = new MySqlCommand(sql, conn);
                adp = new MySqlDataAdapter(cmd);
                ds = new DataSet();
                adp.Fill(ds, "filterData");
                dataGridFilter.DataContext = ds;
                dataGridFilter.ItemsSource = new DataView(ds.Tables[0]);
            }
            conn.Close();

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


