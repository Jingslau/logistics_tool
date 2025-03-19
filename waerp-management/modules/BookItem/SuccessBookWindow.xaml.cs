using System.Windows;
using waerp_management.store;

namespace waerp_management.application.BookItem
{
    /// <summary>
    /// Interaction logic for SuccessBookWindow.xaml
    /// </summary>
    public partial class SuccessBookWindow : Window
    {
        public SuccessBookWindow()
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
