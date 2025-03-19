using System.Windows;
using waerp_management.errorHandling;
using waerp_management.sql;
using waerp_management.store.Administration;

namespace waerp_management.application.Administration.TempLocationAdministration
{
    /// <summary>
    /// Interaction logic for EditTempLocationWindow.xaml
    /// </summary>
    public partial class EditTempLocationWindow : Window
    {
        public EditTempLocationWindow()
        {
            InitializeComponent();
            LocationValA.Text = CurrentLocationAdministrationModel.SelectedLocationName;
        }

        private void EditTempLocaiton_Click(object sender, RoutedEventArgs e)
        {
            if (LocationValA.Text != "")
            {
                CurrentLocationAdministrationModel.LocationName = LocationValA.Text;
                if (AdministrationQueries.EditTempLocation())
                {
                    DialogResult = false;
                }
            }
            else
            {
                ErrorHandlerModel.ErrorText = "Die Zwischenlagerbezeichnung darf nicht leer sein!";
                ErrorHandlerModel.ErrorType = "NOTALLOWED";
                ErrorWindow showError = new ErrorWindow();
                showError.ShowDialog();
            }

        }

        private void CloseDialog_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
