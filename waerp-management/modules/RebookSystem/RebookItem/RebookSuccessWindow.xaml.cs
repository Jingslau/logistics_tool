using System.Windows;
using waerp_management.store;

namespace waerp_management.application.rebookItem
{
    /// <summary>
    /// Interaction logic for RebookSuccessWindow.xaml
    /// </summary>
    public partial class RebookSuccessWindow : Window
    {
        public RebookSuccessWindow()
        {
            InitializeComponent();
            OldLocationName.Text = CurrentRebookModel.OldLocationName;
            ItemIdent.Text = $"Bitte Lagern Sie \n den Artikel mit der Artikelnummer {CurrentRebookModel.ItemIdentStr} \n aus dem Fach:";
            NewLocationName.Text = CurrentRebookModel.NewLocationName;
        }

        private void CloseCurrentDialog(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
