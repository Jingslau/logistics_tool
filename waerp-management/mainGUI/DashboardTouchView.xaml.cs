using System;
using System.Windows;
using System.Windows.Controls;
using waerp_management.application;

namespace waerp_management.main
{
    /// <summary>
    /// Interaction logic for DashboardTouchView.xaml
    /// </summary>
    public partial class DashboardTouchView : Page
    {
        public DashboardTouchView()
        {
            InitializeComponent();
        }

        private void FloorLocation_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/modules/TempLocations/TempLocationsView.xaml", UriKind.Relative));
        }

        private void ScanItem_Click(object sender, RoutedEventArgs e)
        {
            ScanBarcodeWindow openScanner = new ScanBarcodeWindow();
            openScanner.ShowDialog();
        }

        private void ReturnItem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/modules/returnItem/ReturnItemSelect.xaml", UriKind.Relative));
        }

        private void ItemIncoming_Click(object sender, RoutedEventArgs e)
        {
            ScanBarcodeWindow openScanner = new ScanBarcodeWindow();
            openScanner.ShowDialog();
        }
    }
}
