using MySqlConnector;
using System.Data;
using System.Reflection;
using System.Windows;


using waerp_management.config.DatabaseSettingsView;
using waerp_management.dbtools;
using waerp_management.main;
using waerp_management.sql;

namespace waerp_management.loginHandling
{
    /// <summary>
    /// Interaction logic for loginView.xaml
    /// </summary>
    /// 

    public partial class loginView : Window
    {
        MySqlConnection conn = new MySqlConnection(SqlConn.GetConnectionString());
        public loginView()
        {
            InitializeComponent();
            CurrentVersion.Text = "Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString();

        }
        public string username { get; set; }
        public string password { get; set; }

        private void loginHandler(object sender, RoutedEventArgs e)
        {


            conn.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM users WHERE username = '{usernameP.Text}'", conn);
            DataSet ds = new DataSet();
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            adp.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count == 1)
            {
                if (ds.Tables[0].Rows[0]["user_password"].ToString() == PasswordInput.Password)
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
            MainPasswordWindow openMasterlogin = new MainPasswordWindow();
            openMasterlogin.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        public void LoginDev(object sender, RoutedEventArgs e)
        {
            usernameP.Text = "Bayer_D";
            PasswordInput.Password = "bayer";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM users WHERE username = '{usernameP.Text}'", conn);
            DataSet ds = new DataSet();
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            adp.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count == 1)
            {
                if (ds.Tables[0].Rows[0]["user_password"].ToString() == PasswordInput.Password)
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
    }
}
