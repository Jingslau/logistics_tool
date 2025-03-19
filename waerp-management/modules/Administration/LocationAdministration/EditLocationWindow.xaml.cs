using System.Windows;
using waerp_management.errorHandling;
using waerp_management.sql;
using waerp_management.store.Administration;

namespace waerp_management.application.Administration.LocationAdministration
{
    /// <summary>
    /// Interaction logic for EditLocationWindow.xaml
    /// </summary>
    public partial class EditLocationWindow : Window
    {
        public EditLocationWindow()
        {
            InitializeComponent();
            string[] selectedLocation = new string[4];
            selectedLocation = CurrentLocationAdministrationModel.SelectedLocationName.Split(';');
            LocationValA.Text = selectedLocation[0];
            LocationValB.Text = selectedLocation[1];
            LocationValC.Text = selectedLocation[2];
            LocationValD.Text = selectedLocation[3];
        }

        private void EditLocation_Click(object sender, RoutedEventArgs e)
        {
            if (LocationValA.Text != "" | LocationValB.Text != "" | LocationValC.Text != "" | LocationValD.Text != "")
            {
                CurrentLocationAdministrationModel.LocationName = LocationValA.Text.Replace(" ", "") + ";" + LocationValB.Text.Replace(" ", "") + ";" + LocationValC.Text.Replace(" ", "") + ";" + LocationValD.Text.Replace(" ", "");
                if (AdministrationQueries.EditLocation())
                {
                    DialogResult = false;
                }


            }
            else
            {
                ErrorHandlerModel.ErrorText = "Bitte geben Sie Lagerbezeichnung, Regal, Ebene und Platz an, damit der Lagerort bearbeitet werden kann!";
                ErrorHandlerModel.ErrorType = "NOTALLOWED";
                ErrorWindow openNotallowed = new ErrorWindow();
                openNotallowed.ShowDialog();
            }
        }

        private void CloseDialog_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
