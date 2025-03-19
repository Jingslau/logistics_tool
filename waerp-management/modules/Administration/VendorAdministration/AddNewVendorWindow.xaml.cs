using System.Windows;
using waerp_management.errorHandling;
using waerp_management.sql;
using waerp_management.store.Administration;

namespace waerp_management.application.Administration.VendorAdministration
{
    /// <summary>
    /// Interaction logic for AddNewVendorWindow.xaml
    /// </summary>
    public partial class AddNewVendorWindow : Window
    {
        public AddNewVendorWindow()
        {
            InitializeComponent();
        }

        private void CloseDialog_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void CreateVendor_Click(object sender, RoutedEventArgs e)
        {
            if (VendorName.Text == "")
            {
                ErrorHandlerModel.ErrorText = "Es muss mindest ein Kundenname angegeben werden!";
                ErrorHandlerModel.ErrorType = "NOTALLOWED";
                ErrorWindow showError = new ErrorWindow();
                showError.ShowDialog();
            }
            else
            {
                CurrentCustomerModel.CustomerName = VendorName.Text;
                CurrentCustomerModel.CustomerAdress = VendorAdress.Text;
                CurrentCustomerModel.CustomerPostcode = VendorPostcode.Text;
                CurrentCustomerModel.CustomerCity = VendorCity.Text;
                CurrentCustomerModel.CustomerCountry = VendorCountry.Text;
                CurrentCustomerModel.CustomerWebsite = VendorWebsite.Text;
                CurrentCustomerModel.CustomerPhone = VendorPhone.Text;
                CurrentCustomerModel.CustomerMail = VendorMail.Text;
                CurrentCustomerModel.CustomerContact = VendorContact.Text;
                AdministrationQueries.CreateVendor();
                ErrorHandlerModel.ErrorText = $"Die Änderungen für den Kunden {VendorName.Text} wurden erfolgreich übernommen!";
                ErrorHandlerModel.ErrorType = "SUCCESS";

                ErrorWindow showSuccess = new ErrorWindow();
                showSuccess.ShowDialog();
                DialogResult = false;
            }
        }
    }
}
