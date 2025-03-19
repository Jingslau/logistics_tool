using System.Windows;
using waerp_management.errorHandling;
using waerp_management.sql;
using waerp_management.store.Administration;

namespace waerp_management.application.Administration.LocationAdministration
{
    /// <summary>
    /// Interaction logic for AddNewLocationView.xaml
    /// </summary>
    public partial class AddNewLocationView : Window
    {

        public AddNewLocationView()
        {
            InitializeComponent();


        }
        private void CloseDialog_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

        }



        private void CreateLocation_Click(object sender, RoutedEventArgs e)
        {
            if (LocationValA.Text != "" | LocationValB.Text != "" | LocationValC.Text != "" | LocationValD.Text != "")
            {
                CurrentLocationAdministrationModel.LocationName = LocationValA.Text.Replace(" ", "") + ";" + LocationValB.Text.Replace(" ", "") + ";" + LocationValC.Text.Replace(" ", "") + ";" + LocationValD.Text.Replace(" ", "");
                if (AdministrationQueries.CreateLocation())
                {
                    DialogResult = false;
                }

            }
            else
            {
                ErrorHandlerModel.ErrorText = "Bitte geben Sie einen Schrank, Ebene und Fach an, damit der Lagerort angelegt werden kann!";
                ErrorHandlerModel.ErrorType = "NOTALLOWED";
                ErrorWindow openNotallowed = new ErrorWindow();
                openNotallowed.ShowDialog();
            }


        }

    }
}
