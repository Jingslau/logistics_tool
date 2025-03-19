using System.Windows;
using waerp_management.sql;

namespace waerp_management.application.Administration.TempLocationAdministration
{
    /// <summary>
    /// Interaction logic for ConfirmDeleteTempLocationWindow.xaml
    /// </summary>
    public partial class ConfirmDeleteTempLocationWindow : Window
    {
        public ConfirmDeleteTempLocationWindow()
        {
            InitializeComponent();
        }

        private void DeleteTempLocation_Click(object sender, RoutedEventArgs e)
        {
            AdministrationQueries.DeleteTempLocation();

            DialogResult = false;
        }

        private void CancleBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
