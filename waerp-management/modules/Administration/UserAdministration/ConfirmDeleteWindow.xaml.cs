using System.Windows;
using waerp_management.errorHandling;
using waerp_management.sql;

namespace waerp_management.application.Administration.UserAdministration
{
    /// <summary>
    /// Interaction logic for ConfirmDeleteWindow.xaml
    /// </summary>
    public partial class ConfirmDeleteWindow : Window
    {
        public ConfirmDeleteWindow()
        {
            InitializeComponent();
        }

        private void CancleBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            AdministrationQueries.DeleteUser();
            ErrorHandlerModel.ErrorText = $"Der Benutzer wurde erfolgreich aus der Datenbank gelöscht!";
            ErrorHandlerModel.ErrorType = "SUCCESS";
            ErrorWindow openSuccess = new ErrorWindow();
            openSuccess.ShowDialog();
            DialogResult = false;
        }
    }
}
