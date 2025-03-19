using System.Windows;
using waerp_management.errorHandling;
using waerp_management.sql;

namespace waerp_management.modules.TempLocations
{
    /// <summary>
    /// Interaction logic for ConfirmDeleteGroupWindow.xaml
    /// </summary>
    public partial class ConfirmDeleteGroupWindow : Window
    {
        public ConfirmDeleteGroupWindow()
        {
            InitializeComponent();
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (TempLocationsQueries.DeleteGroup())
            {

                ErrorHandlerModel.ErrorText = "Die Palette wurde erfolgreich aus dem Lager gelöscht!";
                ErrorHandlerModel.ErrorType = "SUCCESS";
                ErrorWindow openSuccess = new ErrorWindow();
                openSuccess.ShowDialog(); DialogResult = false;
            }

        }

        private void CancleBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
