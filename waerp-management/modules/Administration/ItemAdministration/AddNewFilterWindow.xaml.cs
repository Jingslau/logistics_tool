using System.Data;
using System.Windows;
using System.Windows.Controls;
using waerp_management.errorHandling;
using waerp_management.sql;

namespace waerp_management.modules.Administration.ItemAdministration
{
    /// <summary>
    /// Interaction logic for AddNewFilterWindow.xaml
    /// </summary>
    public partial class AddNewFilterWindow : Window
    {
        public string filterNo;
        public AddNewFilterWindow()
        {
            InitializeComponent();
            FilterIDSelector.Items.Add("1");
            FilterIDSelector.Items.Add("2");
            FilterIDSelector.Items.Add("3");
            FilterIDSelector.Items.Add("4");
            FilterIDSelector.Items.Add("5");
        }

        private void FilterIDSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilterIDSelector.SelectedItem != null)
            {
                filterNo = FilterIDSelector.SelectedItem.ToString();
                newFiltername.IsEnabled = true;
            }
        }

        private void CloseDialog_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void EditFilter_Click(object sender, RoutedEventArgs e)
        {
            DataSet ds = AdministrationQueries.RunSql($"SELECT * FROM filter{filterNo}_names WHERE name = '{newFiltername.Text}'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ErrorHandlerModel.ErrorText = "Es besteht bereits ein Filter mit diesem Namen!";
                ErrorHandlerModel.ErrorType = "NOTALLOWED";
                ErrorWindow showError = new ErrorWindow();
                showError.ShowDialog();
            }
            else if (newFiltername.Text.Length <= 0)
            {
                ErrorHandlerModel.ErrorText = "Bitte geben Sie einen neuen Filternamen ein!";
                ErrorHandlerModel.ErrorType = "NOTALLOWED";
                ErrorWindow showError = new ErrorWindow();
                showError.ShowDialog();
            }
            else
            {
                AdministrationQueries.RunSql($"INSERT INTO filter{filterNo}_names (filter_id, name) VALUES ({AdministrationQueries.GetMaxId(AdministrationQueries.GetAllInfo($"filter{filterNo}_names"), "filter_id")}, '{newFiltername.Text}')");
                ErrorHandlerModel.ErrorText = "Der Filter wurde erfolgreich angelegt!";
                ErrorHandlerModel.ErrorType = "SUCCESS";
                ErrorWindow showSuccess = new ErrorWindow();
                showSuccess.ShowDialog();
                DialogResult = false;
            }
        }
    }
}
