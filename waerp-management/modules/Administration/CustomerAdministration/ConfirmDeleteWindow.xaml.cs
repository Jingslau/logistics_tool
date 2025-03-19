using System.Windows;
using waerp_management.errorHandling;
using waerp_management.sql;
using waerp_management.store.Administration;

namespace waerp_management.application.Administration.CustomerAdministration
{
    /// <summary>
    /// Interaction logic for ConfirmDeleteWindow.xaml
    /// </summary>
    public partial class ConfirmDeleteWindow : Window
    {
        public ConfirmDeleteWindow()
        {
            InitializeComponent();
            ConfirmText.Text = $"Sind Sie sich sicher, dass Sie den Kunden {CurrentCustomerModel.CustomerName} löschen möchten?";
        }

        private void CancleBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

        }

        private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            AdministrationQueries.DeleteCustomer();
            ErrorHandlerModel.ErrorText = $"Der Kunde {CurrentCustomerModel.CustomerName} wurde erfolgreich aus der Datenbank gelöscht";
            ErrorHandlerModel.ErrorType = "SUCCESS";
            ErrorWindow openSuccess = new ErrorWindow();
            openSuccess.ShowDialog();
            DialogResult = false;
        }
    }
}
