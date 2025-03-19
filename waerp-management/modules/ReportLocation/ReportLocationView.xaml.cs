using System.Windows;
using waerp_management.errorHandling;
using waerp_management.models;

namespace waerp_management.application.ReportLocation
{
    /// <summary>
    /// Interaction logic for ReportLocationView.xaml
    /// </summary>
    public partial class ReportLocationView : Window
    {
        public ReportLocationView()
        {
            InitializeComponent();
            ItemIdent.Text = ReportWrongLocationModel.ItemIdent;
            LocationIdent.Text = ReportWrongLocationModel.LocationIdent;

        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            ReportWrongLocationModel.Description = Description.Text;
            if (ErrorReporter.SendErrorReportLocation())
            {
                ErrorHandlerModel.ErrorType = "SUCCESS";
                ErrorHandlerModel.ErrorText = "Der Fehlbestand wurde erfolgreich gemeldet!";
                ErrorWindow openSuccess = new ErrorWindow();
                openSuccess.ShowDialog();
                DialogResult = false;
            }
            else
            {
                ErrorHandlerModel.ErrorType = "NOTALLOWED";
                ErrorHandlerModel.ErrorText = "Es ist ein Fehleraufgetreten. Bitte wenden Sie sich an Ihren Administrator! <br> (SMTP Exception ERROR)";
                ErrorWindow openFailure = new ErrorWindow();
                openFailure.ShowDialog();
                DialogResult = false;
            }

        }

        private void CloseDialog_Click(object sender, RoutedEventArgs e)
        {

            DialogResult = false;
        }
    }
}
