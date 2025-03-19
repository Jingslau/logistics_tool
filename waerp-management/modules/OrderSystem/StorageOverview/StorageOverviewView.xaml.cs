using System.Windows.Controls;
using waerp_management.sql;

namespace waerp_management.application.OrderSystem.StorageOverview
{
    /// <summary>
    /// Interaction logic for StorageOverviewView.xaml
    /// </summary>
    public partial class StorageOverviewView : UserControl
    {
        public StorageOverviewView()
        {
            InitializeComponent();
            CurrentStock.Text = OrderItemOverviewQueries.GetCurrentStock().ToString();
            CurrentRent.Text = OrderItemOverviewQueries.GetCurrentRent().ToString();
            CurrentNew.Text = OrderItemOverviewQueries.GetCurrentNew().ToString();
        }
    }
}
