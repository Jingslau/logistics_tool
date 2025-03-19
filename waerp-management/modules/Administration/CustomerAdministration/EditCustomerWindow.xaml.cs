using System.Windows;
using System.Windows.Controls;
using waerp_management.errorHandling;
using waerp_management.sql;
using waerp_management.store.Administration;

namespace waerp_management.application.Administration.CustomerAdministration
{
    /// <summary>
    /// Interaction logic for EditCustomerWindow.xaml
    /// </summary>
    public partial class EditCustomerWindow : Window
    {
        public EditCustomerWindow()
        {
            InitializeComponent();
            VendorNumber.Text = CurrentCustomerModel.CustomerIDNumber;
            CustomerName.Text = CurrentCustomerModel.CustomerName;
            CustomerAdress.Text = CurrentCustomerModel.CustomerAdress;
            CustomerPostcode.Text = CurrentCustomerModel.CustomerPostcode;
            CustomerCity.Text = CurrentCustomerModel.CustomerCity;
            CustomerCountry.Text = CurrentCustomerModel.CustomerCountry;
            CustomerWebsite.Text = CurrentCustomerModel.CustomerWebsite;
            CustomerPhone.Text = CurrentCustomerModel.CustomerPhone;
            CustomerMail.Text = CurrentCustomerModel.CustomerMail;
            CustomerContact.Text = CurrentCustomerModel.CustomerContact;
        }

        private void CloseDialog_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            CurrentCustomerModel.CustomerIDNumber = VendorNumber.Text;
            CurrentCustomerModel.CustomerName = CustomerName.Text;
            CurrentCustomerModel.CustomerAdress = CustomerAdress.Text;
            CurrentCustomerModel.CustomerPostcode = CustomerPostcode.Text;
            CurrentCustomerModel.CustomerCity = CustomerCity.Text;
            CurrentCustomerModel.CustomerCountry = CustomerCountry.Text;
            CurrentCustomerModel.CustomerWebsite = CustomerWebsite.Text;
            CurrentCustomerModel.CustomerPhone = CustomerPhone.Text;
            CurrentCustomerModel.CustomerMail = CustomerMail.Text;
            CurrentCustomerModel.CustomerContact = CustomerContact.Text;
            AdministrationQueries.UpdateCustomer();
            ErrorHandlerModel.ErrorText = $"Die Änderungen für den Kunden {CustomerName.Text} wurden erfolgreich übernommen!";
            ErrorHandlerModel.ErrorType = "SUCCESS";

            ErrorWindow showSuccess = new ErrorWindow();
            showSuccess.ShowDialog();
            DialogResult = false;
        }

        private void OnlyNumbers_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string input = e.Text;
            e.Handled = TextFieldRules.OnlyNumbers(input, e);
        }

        private void EMail_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string input = e.Text;
            e.Handled = TextFieldRules.VerifyEmail(input, textBox);
        }
    }
}
