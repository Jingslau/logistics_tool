using System.Data;
using System.Windows.Controls;
using waerp_management.sql;
using waerp_management.store.Administration;

namespace waerp_management.application.Administration.VendorAdministration
{
    /// <summary>
    /// Interaction logic for VendorAdministrationView.xaml
    /// </summary>
    public partial class VendorAdministrationView : UserControl
    {
        public VendorAdministrationView()
        {
            InitializeComponent();
            VendorDataItems.DataContext = AdministrationQueries.GetAllInfo("vendor_objects");
            VendorDataItems.ItemsSource = new DataView(AdministrationQueries.GetAllInfo("vendor_objects").Tables[0]);
        }

        private void VendorDataItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                CurrentCustomerModel.CustomerID = row_selected["vendor_id"].ToString();

                CurrentCustomerModel.CustomerName = row_selected["vendor_name"].ToString();
                CurrentCustomerModel.CustomerAdress = row_selected["vendor_adress"].ToString();
                CurrentCustomerModel.CustomerPostcode = row_selected["vendor_postcode"].ToString();
                CurrentCustomerModel.CustomerCity = row_selected["vendor_city"].ToString();
                CurrentCustomerModel.CustomerCountry = row_selected["vendor_country"].ToString();
                CurrentCustomerModel.CustomerWebsite = row_selected["vendor_website"].ToString();
                CurrentCustomerModel.CustomerPhone = row_selected["vendor_phone"].ToString();
                CurrentCustomerModel.CustomerMail = row_selected["vendor_mail"].ToString();
                CurrentCustomerModel.CustomerContact = row_selected["vendor_contact"].ToString();
            }
        }

        private void EditVendor_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            EditVendorWindow openEdit = new EditVendorWindow();
            openEdit.ShowDialog();
            VendorDataItems.DataContext = AdministrationQueries.GetAllInfo("vendor_objects");
            VendorDataItems.ItemsSource = new DataView(AdministrationQueries.GetAllInfo("vendor_objects").Tables[0]);

        }

        private void DeleteVendor_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ConfirmDeleteWindow openConfirm = new ConfirmDeleteWindow();
            openConfirm.ShowDialog();
            VendorDataItems.DataContext = AdministrationQueries.GetAllInfo("vendor_objects");
            VendorDataItems.ItemsSource = new DataView(AdministrationQueries.GetAllInfo("vendor_objects").Tables[0]);
        }

        private void AddVendorBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddNewVendorWindow openAdd = new AddNewVendorWindow();
            openAdd.ShowDialog();
            VendorDataItems.DataContext = AdministrationQueries.GetAllInfo("vendor_objects");
            VendorDataItems.ItemsSource = new DataView(AdministrationQueries.GetAllInfo("vendor_objects").Tables[0]);

        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchBox.Text != "")
            {
                DataSet ds = AdministrationQueries.GetAllInfo("vendor_objects");
                DataSet output = AdministrationQueries.GetAllInfo("vendor_objects");
                output.Tables[0].Rows.Clear();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["vendor_name"].ToString().Contains(searchBox.Text)
                        | row["vendor_adress"].ToString().Contains(searchBox.Text)
                        | row["vendor_city"].ToString().Contains(searchBox.Text)
                        | row["vendor_country"].ToString().Contains(searchBox.Text)
                        | row["vendor_contact"].ToString().Contains(searchBox.Text)
                        | row["vendor_mail"].ToString().Contains(searchBox.Text)
                        | row["vendor_website"].ToString().Contains(searchBox.Text)
                        | row["vendor_phone"].ToString().Contains(searchBox.Text)
                         | row["vendor_postcode"].ToString().Contains(searchBox.Text))
                    {
                        output.Tables[0].ImportRow(row);
                    }
                }
                VendorDataItems.DataContext = output;
                VendorDataItems.ItemsSource = new DataView(output.Tables[0]);
            }
            else
            {
                VendorDataItems.DataContext = AdministrationQueries.GetAllInfo("vendor_objects");
                VendorDataItems.ItemsSource = new DataView(AdministrationQueries.GetAllInfo("vendor_objects").Tables[0]);
            }
        }
    }
}
