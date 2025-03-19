using System.Data;
using System.Windows.Controls;
using waerp_management.sql;
using waerp_management.store.Administration;

namespace waerp_management.application.Administration.UserAdministration
{
    /// <summary>
    /// Interaction logic for UserAdministrationView.xaml
    /// </summary>
    public partial class UserAdministrationView : UserControl
    {
        public static DataSet AllUsers = new DataSet();
        public static DataSet AllRoles = new DataSet();

        public UserAdministrationView()
        {
            InitializeComponent();
            DataSet Users = AdministrationQueries.GetAllInfo("users");
            DataSet Roles = AdministrationQueries.GetAllInfo("user_roles");
            Users.Tables[0].Columns.Add("role_name");

            if (Users.Tables[0].Rows.Count > 0 && Roles.Tables[0].Rows.Count > 0 && Users.Tables.Count > 0 && Roles.Tables.Count > 0)
            {
                for (int i = 0; i < Users.Tables[0].Rows.Count; i++)
                {
                    Users.Tables[0].Rows[i]["role_name"] = Roles.Tables[0].Rows[int.Parse(Users.Tables[0].Rows[i]["role_id"].ToString())]["name"].ToString();
                }

                AllUsers = Users;
                AllRoles = Roles;

                UserDataItems.DataContext = Users;

                UserDataItems.ItemsSource = new DataView(Users.Tables[0]);
            }


        }

        public void GetAllUsers()
        {
            DataSet Users = AdministrationQueries.GetAllInfo("users");
            DataSet Roles = AdministrationQueries.GetAllInfo("user_roles");
            Users.Tables[0].Columns.Add("role_name");
            for (int i = 0; i < Users.Tables[0].Rows.Count; i++)
            {
                Users.Tables[0].Rows[i]["role_name"] = Roles.Tables[0].Rows[int.Parse(Users.Tables[0].Rows[i]["role_id"].ToString())]["name"].ToString();
            }

            AllUsers = Users;
            AllRoles = Roles;
            UserDataItems.DataContext = Users;

            UserDataItems.ItemsSource = new DataView(Users.Tables[0]);
        }

        private void UserDataItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                CurrentUserAdministrationModel.UserID = int.Parse(row_selected["user_id"].ToString());
            }
        }

        private void EditUser_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            EditUserWindow openEdit = new EditUserWindow();
            openEdit.ShowDialog();
            GetAllUsers();

        }

        private void DeleteUser_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ConfirmDeleteWindow openConfirm = new ConfirmDeleteWindow();
            openConfirm.ShowDialog();
            GetAllUsers();
        }

        private void AddUserBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddNewUserWindow openAdd = new AddNewUserWindow();
            openAdd.ShowDialog();
            GetAllUsers();

        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchBox.Text != "")
            {

                DataSet ds = AllUsers.Copy();
                DataSet output = AllUsers.Copy();
                output.Tables[0].Rows.Clear();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["user_ident"].ToString().Contains(searchBox.Text)
                        | row["username"].ToString().Contains(searchBox.Text)
                        | row["name"].ToString().Contains(searchBox.Text)
                        | row["surname"].ToString().Contains(searchBox.Text)
                        | row["email"].ToString().Contains(searchBox.Text)
                        | row["role_name"].ToString().Contains(searchBox.Text)
                      )
                    {
                        output.Tables[0].ImportRow(row);
                    }
                }
                UserDataItems.DataContext = output;
                UserDataItems.ItemsSource = new DataView(output.Tables[0]);
            }
            else
            {
                UserDataItems.DataContext = AllUsers;
                UserDataItems.ItemsSource = new DataView(AllUsers.Tables[0]);
            }
        }
    }
}
