using System.Windows;
using waerp_management.errorHandling;
using waerp_management.sql;

namespace waerp_management.modules.returnItem
{
    /// <summary>
    /// Interaction logic for ConfirmDeleteReturn.xaml
    /// </summary>
    public partial class ConfirmDeleteReturn : Window
    {
        public ConfirmDeleteReturn()
        {
            InitializeComponent();
        }

        private void CancleBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            ReturnItemQueries.DeleteRent();
            ErrorHandlerModel.ErrorText = "Die Entnahme wurde erfolgreich gelöscht!";
            ErrorHandlerModel.ErrorType = "SUCCESS";
            ErrorWindow openSuccess = new ErrorWindow();
            openSuccess.ShowDialog();
            DialogResult = false;
        }
    }
}
