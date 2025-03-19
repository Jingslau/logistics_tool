using System.Windows;
using waerp_management.store;

namespace waerp_management.application.returnItem
{
    /// <summary>
    /// Interaction logic for SuccessReturnView.xaml
    /// </summary>
    public partial class SuccessReturnView : Window
    {
        public SuccessReturnView()
        {
            InitializeComponent();
            ItemIdent.Text = $"Bitte lagern Sie \n den Artikel mit der Artikelnummer {CurrentRentModel.ItemIdentStr}     \n in das Fach ein:";
            LocationName.Text = CurrentReturnModel.ReturnLocation;
        }
        private void CloseCurrentDialog(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
