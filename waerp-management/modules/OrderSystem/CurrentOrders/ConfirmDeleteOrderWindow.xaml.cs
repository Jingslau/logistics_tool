using System.Windows;
using waerp_management.errorHandling;
using waerp_management.sql;

namespace waerp_management.modules.OrderSystem.CurrentOrders
{
    /// <summary>
    /// Interaction logic for ConfirmDeleteOrderWindow.xaml
    /// </summary>
    public partial class ConfirmDeleteOrderWindow : Window
    {
        public ConfirmDeleteOrderWindow()
        {
            InitializeComponent();
        }

        private void CancleBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void DeleteLocation_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentOrdersQueries.DeleteOrder())
            {
                ErrorHandlerModel.ErrorText = "Die Bestellung wurde erfolgreich gelöscht!";
                ErrorHandlerModel.ErrorType = "SUCCESS";
                ErrorWindow showSuccess = new ErrorWindow();
                showSuccess.ShowDialog();
                DialogResult = false;
            }
        }
    }
}
