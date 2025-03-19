using System.Windows;
using waerp_management.store;

namespace waerp_management.application.Global
{
    /// <summary>
    /// Interaction logic for ContactCardWindow.xaml
    /// </summary>
    public partial class ContactCardWindow : Window
    {
        public ContactCardWindow()
        {
            InitializeComponent();

            CompanyAdress.Text = ContactCardModel.CompanyAdress;
            CompanyCity.Text = ContactCardModel.CompanyCity;
            CompanyCountry.Text = ContactCardModel.CompanyCountry;
            CompanyMail.Text = ContactCardModel.CompanyMail;
            CompanyName.Text = ContactCardModel.CompanyName;
            CompanyPhone.Text = ContactCardModel.CompanyPhone;
            CompanyPostcode.Text = ContactCardModel.CompanyPostcode;
            CompanyWebsite.Text = ContactCardModel.CompanyWebsite;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
