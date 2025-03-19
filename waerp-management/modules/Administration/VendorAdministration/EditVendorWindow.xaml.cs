using System.Windows;
using waerp_management.errorHandling;
using waerp_management.sql;
using waerp_management.store.Administration;

namespace waerp_management.application.Administration.VendorAdministration
{
    /// <summary>
    /// Interaction logic for EditVendorWindow.xaml
    /// </summary>
    public partial class EditVendorWindow : Window
    {
        public EditVendorWindow()
        {
            InitializeComponent();

            VendorName.Text = CurrentCustomerModel.CustomerName;
            VendorAdress.Text = CurrentCustomerModel.CustomerAdress;
            VendorPostcode.Text = CurrentCustomerModel.CustomerPostcode;
            VendorCity.Text = CurrentCustomerModel.CustomerCity;
            VendorCountry.Text = CurrentCustomerModel.CustomerCountry;
            VendorWebsite.Text = CurrentCustomerModel.CustomerWebsite;
            VendorPhone.Text = CurrentCustomerModel.CustomerPhone;
            VendorMail.Text = CurrentCustomerModel.CustomerMail;
            VendorContact.Text = CurrentCustomerModel.CustomerContact;
        }

        private void CloseDialog_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
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
            AdministrationQueries.UpdateVendor();
            ErrorHandlerModel.ErrorText = $"Die Änderungen für den Kunden {VendorName.Text} wurden erfolgreich übernommen!";
            ErrorHandlerModel.ErrorType = "SUCCESS";

            ErrorWindow showSuccess = new ErrorWindow();
            showSuccess.ShowDialog();
            DialogResult = false;
        }
    }
}
