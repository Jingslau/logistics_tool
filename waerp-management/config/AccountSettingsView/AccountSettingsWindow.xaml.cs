using System.Data;
using System.Windows;
using waerp_management.errorHandling;
using waerp_management.main;
using waerp_management.sql;

namespace waerp_management.config.AccountSettingsView
{
    /// <summary>
    /// Interaction logic for AccountSettingsWindow.xaml
    /// </summary>
    public partial class AccountSettingsWindow : Window
    {
        public static string password = "";
        public static string user_id = "";
        public AccountSettingsWindow()
        {
            InitializeComponent();
            DataSet user = AdministrationQueries.RunSql($"SELECT * FROM users WHERE user_id = {MainWindowViewModel.UserID}");
            UserID.Text = user.Tables[0].Rows[0]["user_ident"].ToString();
            vname.Text = user.Tables[0].Rows[0]["name"].ToString();
            Surname.Text = user.Tables[0].Rows[0]["surname"].ToString();
            mail.Text = user.Tables[0].Rows[0]["email"].ToString();
            Username.Text = user.Tables[0].Rows[0]["username"].ToString();

            password = user.Tables[0].Rows[0]["user_password"].ToString();
            user_id = user.Tables[0].Rows[0]["user_id"].ToString();

        }

        private void SaveNewPassword_Click(object sender, RoutedEventArgs e)
        {
            if (oldPW.Password != password)
            {
                ErrorHandlerModel.ErrorText = "Das bisherige Kennwort ist nicht korrekt!";
                ErrorHandlerModel.ErrorType = "NOTALLOWED";
                ErrorWindow showError = new ErrorWindow();
                showError.ShowDialog();
            }
            else if (newPW.Password != newPW2.Password)
            {
                ErrorHandlerModel.ErrorText = "Die Kennwörter müssen übereinstimmen!";
                ErrorHandlerModel.ErrorType = "NOTALLOWED";
                ErrorWindow showError = new ErrorWindow();
                showError.ShowDialog();
            }
            else
            {
                AdministrationQueries.EditPassword(newPW.Password, user_id);
                ErrorHandlerModel.ErrorText = "Das Kennwort wurde erfolgreich geändert!";
                ErrorHandlerModel.ErrorType = "SUCCESS";
                ErrorWindow showError = new ErrorWindow();
                showError.ShowDialog();
            }
        }

        private void CloseAccountSettings_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
