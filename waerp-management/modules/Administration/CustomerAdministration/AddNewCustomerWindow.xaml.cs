using System.Windows;
using System.Windows.Controls;
using waerp_management.errorHandling;
using waerp_management.sql;
using waerp_management.store.Administration;

namespace waerp_management.application.Administration.CustomerAdministration
{
    /// <summary>
    /// Interaction logic for AddNewCustomerWindow.xaml
    /// </summary>
    public partial class AddNewCustomerWindow : Window
    {
        public AddNewCustomerWindow()
        {
            InitializeComponent();
        }
        public string customer_id { get; set; }

        private void CloseDialog_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void CreateCustomer_Click(object sender, RoutedEventArgs e)
        {



            if (CustomerName.Text == "" | VendorNumber.Text == "" | CustomerPhone.Text == "" | CustomerWebsite.Text == "" | CustomerContact.Text == "" | CustomerAdress.Text == "" | CustomerMail.Text == "" | CustomerCity.Text == "" | CustomerCountry.Text == "" | CustomerPostcode.Text == "")
            {
                ErrorHandlerModel.ErrorText = "Es müssen alle Felder ausgefüllt sein!";
                ErrorHandlerModel.ErrorType = "NOTALLOWED";
                ErrorWindow showError = new ErrorWindow();
                showError.ShowDialog();
            }
            else
            {
                if (!AdministrationQueries.CheckCustomerID(VendorNumber.Text))
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
                    AdministrationQueries.CreateCustomer();
                    ErrorHandlerModel.ErrorText = $"Der Kunden {CustomerName.Text} wurden erfolgreich angelegt!";
                    ErrorHandlerModel.ErrorType = "SUCCESS";

                    ErrorWindow showSuccess = new ErrorWindow();
                    showSuccess.ShowDialog();
                    DialogResult = false;
                }
                else
                {
                    ErrorHandlerModel.ErrorText = "Es besteht bereits ein Kunde mit der selben Kundennummer!";
                    ErrorHandlerModel.ErrorType = "NOTALLOWED";
                    ErrorWindow showError = new ErrorWindow();
                    showError.ShowDialog();
                }

            }
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
