using MySqlConnector;
using System;
using System.Data;
using System.Reflection;
using System.Windows;
using waerp_management.config.DatabaseSettingsView;
using waerp_management.dbtools;
using waerp_management.main;
using waerp_management.sql;
using waerp_management.store;

namespace waerp_management.LoginTouch
{
    /// <summary>
    /// Interaction logic for LoginTouchWindow.xaml
    /// </summary>
    public partial class LoginTouchWindow : Window
    {


        MySqlConnection conn = new MySqlConnection(SqlConn.GetConnectionString());
        public LoginTouchWindow()
        {
            this.WindowState = WindowState.Maximized;
            InitializeComponent();
            CurrentVersion.Text = "Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " [IMPACT GmbH]";
        }
        public string username { get; set; }
        public string password { get; set; }

        private void loginHandler(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM users WHERE user_ident = '{NumberInput.Text}'", conn);
            DataSet ds = new DataSet();
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            adp.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count == 1)
            {
                if (ds.Tables[0].Rows[0]["user_ident"].ToString() == NumberInput.Text)
                {
                    MainWindowViewModel.Firstname = ds.Tables[0].Rows[0]["name"].ToString();
                    MainWindowViewModel.Lastname = ds.Tables[0].Rows[0]["surname"].ToString();
                    MainWindowViewModel.UserID = ds.Tables[0].Rows[0]["user_id"].ToString();
                    MainWindowViewModel.username = ds.Tables[0].Rows[0]["username"].ToString();
                    MainWindowViewModel.RoleID = int.Parse(ds.Tables[0].Rows[0]["role_id"].ToString());


                    DataSet ds2 = AdministrationQueries.RunSql($"SELECT * FROM user_roles WHERE role_id = {MainWindowViewModel.RoleID}");
                    MainWindowViewModel.RoleStr = ds2.Tables[0].Rows[0]["name"].ToString();
                    ds = new DataSet();
                    ds = AdministrationQueries.RunSql($"SELECT * FROM user_privilege_relations WHERE user_id = {MainWindowViewModel.UserID}");

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["privilege_id"].ToString() == "1")
                        {
                            MainWindowViewModel.showAdministration = true;
                            MainWindowViewModel.showSettings = true;
                        }
                        else if (ds.Tables[0].Rows[i]["privilege_id"].ToString() == "2")
                        {
                            MainWindowViewModel.showRebook = true;
                        }
                        else if (ds.Tables[0].Rows[i]["privilege_id"].ToString() == "3")
                        {
                            MainWindowViewModel.showOrdersystem = true;
                        }
                    }
                    conn.Close();
                    MainWindow win2 = new MainWindow();
                    win2.Show();
                    Close();

                }
                else
                {
                    SnackbarError.IsActive = true;
                }

            }
            else
            {
                SnackbarError.IsActive = true;
            }

        }

        private void CloseButton(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ShowSettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            DatabaseSettingsView openSettings = new DatabaseSettingsView();
            Nullable<bool> dialogResult = openSettings.ShowDialog();
        }

        private void Button_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
        private void NumberNine_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (NumberInput.Text.Length < 8)
            {
                NumberInput.Text += "9";
            }
        }
        private void NumberEight_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (NumberInput.Text.Length < 8)
            {
                NumberInput.Text += "8";
            }
        }
        private void NumberSeven_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (NumberInput.Text.Length < 8)
            {
                NumberInput.Text += "7";
            }
        }
        private void NumberSix_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (NumberInput.Text.Length < 8)
            {
                NumberInput.Text += "6";
            }
        }
        private void NumberFive_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (NumberInput.Text.Length < 8)
            {
                NumberInput.Text += "5";
            }
        }
        private void NumberFour_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (NumberInput.Text.Length < 8)
            {
                NumberInput.Text += "4";
            }
        }
        private void NumberThree_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (NumberInput.Text.Length < 8)
            {

                NumberInput.Text += "3";

            }
        }
        private void NumberTwo_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (NumberInput.Text.Length < 8)
            {

                NumberInput.Text += "2";

            }
        }
        private void NumberOne_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (NumberInput.Text.Length < 8)
            {

                NumberInput.Text += "1";

            }
        }
        private void NumberZero_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (NumberInput.Text.Length < 8)
            {

                NumberInput.Text += "0";

            }
        }

        private void DeleteLastDigit_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (NumberInput.Text.Length == 1 && NumberInput.Text != "0")
            {
                NumberInput.Text = "";
            }
            else if (NumberInput.Text.Length > 1)
            {
                NumberInput.Text = NumberInput.Text.Remove(NumberInput.Text.Length - 1);
            }
        }
        private void SaveNumberInput_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            CurrentRentModel.RentQuantity = NumberInput.Text;
            DialogResult = false;
        }


    }
}
