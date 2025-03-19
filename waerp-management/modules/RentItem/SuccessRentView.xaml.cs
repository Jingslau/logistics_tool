using System.Windows;
using waerp_management.store;

namespace waerp_management.application.rentItem
{
    /// <summary>
    /// Interaction logic for SuccessRentView.xaml
    /// </summary>
    public partial class SuccessRentView : Window
    {
        public SuccessRentView()
        {
            InitializeComponent();
            ItemIdent.Text = $"Bitte Entnehmen Sie \n den Artikel mit der Artikelnummer {CurrentRentModel.ItemIdentStr}     \n aus dem Fach:";
            LocationName.Text = CurrentRentModel.RentLocation;
        }

        private void CloseCurrentDialog(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
