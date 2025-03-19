using System.Windows;
using waerp_management.sql;

namespace waerp_management.modules.TempLocations
{
    /// <summary>
    /// Interaction logic for ConfirmDeleteFromGroupWindow.xaml
    /// </summary>
    public partial class ConfirmDeleteFromGroupWindow : Window
    {
        public ConfirmDeleteFromGroupWindow()
        {
            InitializeComponent();
        }

        private void CancleBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            TempLocationsQueries.DeleteItemFromGroup();

            DialogResult = false;
        }
    }
}
