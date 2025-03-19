using Microsoft.Win32;
using System;
using System.Data;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using waerp_management.config;
using waerp_management.config.AccountSettingsView;
using waerp_management.loginHandling;
using waerp_management.LoginTouch;
using waerp_management.main;
using waerp_management.sql;

namespace waerp_management
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\wærp-stockpilot", true);
            if (key != null)
            {

                DataSet settings = new DataSet();
                settings = AdministrationQueries.RunSql("SELECT * FROM company_settings");
                LicenseText.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString() + " | " + "Licensed for " + settings.Tables[0].Rows[0]["settings_value"];


                key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\wærp-stockpilot", true);
                if (bool.Parse(key.GetValue("IsTouch").ToString()))
                {
                    DashboardTouch.Visibility = Visibility.Visible;
                    DashboardMKß.Visibility = Visibility.Collapsed;
                    this.WindowState = WindowState.Maximized;
                    navframe.Navigate(new System.Uri("/mainGUI/DashboardTouchView.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    DashboardTouch.Visibility = Visibility.Collapsed;
                    DashboardMKß.Visibility = Visibility.Visible;
                    navframe.Navigate(new System.Uri("/mainGUI/DashboardView.xaml", UriKind.RelativeOrAbsolute));
                }
            }

            RoleNameUser.Text = MainWindowViewModel.RoleStr;



            Breadcrumbs.Text = "Dashboard";
            if (!MainWindowViewModel.showAdministration)
            {
                Settings.Visibility = Visibility.Collapsed;
                ManagementExpander.Visibility = Visibility.Collapsed;
            }
            else
            {
                Settings.Visibility = Visibility.Visible;
                ManagementExpander.Visibility = Visibility.Visible;
            }
            if (!MainWindowViewModel.showRebook)
            {
                RebookExpander.Visibility = Visibility.Collapsed;
            }
            else
            {
                RebookExpander.Visibility = Visibility.Visible;
            }
            if (!MainWindowViewModel.showOrdersystem)
            {
                OrdersystemExpander.Visibility = Visibility.Collapsed;
            }
            else
            {
                OrdersystemExpander.Visibility = Visibility.Visible;
            }
            //Breadcrumbs.Text = "Dashboard";
            //if (MainWindowViewModel.RoleID != 1)
            //{
            //    RoleNameUser.Text = "Mitarbeiter";
            //    Settings.Visibility = Visibility.Collapsed;
            //    ManagementExpander.Visibility = Visibility.Collapsed;
            //    OrdersystemExpander.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    RoleNameUser.Text = "Administrator";
            //    Settings.Visibility = Visibility.Visible;
            //    ManagementExpander.Visibility = Visibility.Visible;
            //    //       OrdersystemExpander.Visibility = Visibility.Visible;
            //}
        }

        private void sidebar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selected = SidebarMain.SelectedItem as NavButton;


        }




        private void NavButton_Selected_Dashboard(object sender, RoutedEventArgs e)
        {
            SidebarRebook.SelectedIndex = -1;
            SidebarMngt.SelectedIndex = -1;
            SidebarShopping.SelectedIndex = -1;
            Breadcrumbs.Text = "Dashboard";
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\wærp-stockpilot", true);


            if (key != null)
            {
                {
                    if (bool.Parse(key.GetValue("IsTouch").ToString()))
                    {
                        this.WindowState = WindowState.Maximized;
                        navframe.Navigate(new Uri("/mainGUI/DashboardTouchView.xaml", UriKind.Relative));
                    }
                    else
                    {
                        navframe.Navigate(new Uri("/mainGUI/DashboardView.xaml", UriKind.Relative));
                    }
                }
            }
        }
        private void NavButton_Selected_RentItem(object sender, RoutedEventArgs e)
        {
            SidebarRebook.SelectedIndex = -1;
            SidebarMngt.SelectedIndex = -1;
            SidebarShopping.SelectedIndex = -1;
            Breadcrumbs.Text = "Entnahme";
            navframe.Navigate(new Uri("/modules/RentItem/RentItemView.xaml", UriKind.Relative));
        }
        private void NavButton_Selected_OutsideLocation(object sender, RoutedEventArgs e)
        {
            SidebarRebook.SelectedIndex = -1;
            SidebarMngt.SelectedIndex = -1;
            SidebarShopping.SelectedIndex = -1;
            Breadcrumbs.Text = "Zwischenlager";
            navframe.Navigate(new Uri("/modules/TempLocations/TempLocationsView.xaml", UriKind.Relative));
        }


        private void NavButton_Selected_ReturnItem(object sender, RoutedEventArgs e)
        {
            SidebarRebook.SelectedIndex = -1;
            SidebarMngt.SelectedIndex = -1;
            SidebarShopping.SelectedIndex = -1;
            Breadcrumbs.Text = "Rückgabe";
            navframe.Navigate(new Uri("/modules/returnItem/ReturnItemSelect.xaml", UriKind.Relative));

        }

        private void NavButton_Selected_History(object sender, RoutedEventArgs e)
        {
            SidebarRebook.SelectedIndex = -1;
            SidebarMngt.SelectedIndex = -1;
            SidebarShopping.SelectedIndex = -1;
            Breadcrumbs.Text = "Historie";
            navframe.Navigate(new Uri("/modules/History/HistoryView.xaml", UriKind.Relative));

        }
        private void NavButton_Selected_ItemOverview(object sender, RoutedEventArgs e)
        {
            SidebarRebook.SelectedIndex = -1;
            SidebarMain.SelectedIndex = -1;
            SidebarShopping.SelectedIndex = -1;
            Breadcrumbs.Text = "Verwaltung : Artikelverwaltung";
            navframe.Navigate(new Uri("/modules/Administration/ItemAdministration/ItemOverviewView.xaml", UriKind.Relative));


        }
        private void NavButton_Selected_RebookItem(object sender, RoutedEventArgs e)
        {
            SidebarMngt.SelectedIndex = -1;
            SidebarMain.SelectedIndex = -1;
            SidebarShopping.SelectedIndex = -1;
            Breadcrumbs.Text = "Umbuchen : Artikel";
            navframe.Navigate(new Uri("/modules/RebookSystem/RebookItem/RebookViewSelection.xaml", UriKind.Relative));
        }
        private void NavButton_Selected_RebookGroup(object sender, RoutedEventArgs e)
        {
            SidebarMngt.SelectedIndex = -1;
            SidebarMain.SelectedIndex = -1;
            SidebarShopping.SelectedIndex = -1;
            Breadcrumbs.Text = "Umbuchen : Palette";
            navframe.Navigate(new Uri("/modules/RebookSystem/RebookGroup/RebookGroupView.xaml", UriKind.Relative));
        }
        private void NavButton_Selected_StockOverview(object sender, RoutedEventArgs e)
        {
            SidebarRebook.SelectedIndex = -1;
            SidebarMain.SelectedIndex = -1;
            SidebarMngt.SelectedIndex = -1;
            Breadcrumbs.Text = "Verwaltung : Lagerübersicht";
            navframe.Navigate(new Uri("/modules/OrderSystem/StorageOverview/StorageOverviewView.xaml", UriKind.Relative));

        }
        private void NavButton_Selected_ItemOverviewOrder(object sender, RoutedEventArgs e)
        {
            SidebarRebook.SelectedIndex = -1;
            SidebarMain.SelectedIndex = -1;
            SidebarMngt.SelectedIndex = -1;
            Breadcrumbs.Text = "Bestellsystem : Artikelübersicht";
            navframe.Navigate(new Uri("/modules/OrderSystem/ItemOverviewShop/ItemOverviewShopView.xaml", UriKind.Relative));

        }
        private void NavButton_Selected_ActiveOrderOverview(object sender, RoutedEventArgs e)
        {
            SidebarRebook.SelectedIndex = -1;
            SidebarMain.SelectedIndex = -1;
            SidebarMngt.SelectedIndex = -1;
            Breadcrumbs.Text = "Bestellsystem : Aktive Bestellungen";
            navframe.Navigate(new Uri("/modules/OrderSystem/CurrentOrders/CurrentOrdersView.xaml", UriKind.Relative));

        }

        private void NavButton_Selected_LocationOverview(object sender, RoutedEventArgs e)
        {
            SidebarRebook.SelectedIndex = -1;
            SidebarMain.SelectedIndex = -1;
            SidebarShopping.SelectedIndex = -1;
            Breadcrumbs.Text = "Verwaltung : Lagerortverwaltung";
            navframe.Navigate(new Uri("/modules/Administration/LocationAdministration/LocationOverviewView.xaml", UriKind.Relative));
        }
        private void NavButton_Selected_TempLocationOverview(object sender, RoutedEventArgs e)
        {
            SidebarRebook.SelectedIndex = -1;
            SidebarMain.SelectedIndex = -1;
            SidebarShopping.SelectedIndex = -1;
            Breadcrumbs.Text = "Verwaltung : Zwischenlagerverwaltung";
            navframe.Navigate(new Uri("/modules/Administration/TempLocationAdministration/TempLocatioOverviewView.xaml", UriKind.Relative));
        }
        private void NavButton_Selected_CustomerOverview(object sender, RoutedEventArgs e)
        {
            SidebarRebook.SelectedIndex = -1;
            SidebarMain.SelectedIndex = -1;
            SidebarShopping.SelectedIndex = -1;
            Breadcrumbs.Text = "Verwaltung : Kundenverwaltung";
            navframe.Navigate(new Uri("/modules/Administration/CustomerAdministration/CustomerAdministrationView.xaml", UriKind.Relative));
        }
        private void NavButton_Selected_VendorOverview(object sender, RoutedEventArgs e)
        {
            SidebarRebook.SelectedIndex = -1;
            SidebarMain.SelectedIndex = -1;
            SidebarShopping.SelectedIndex = -1;
            Breadcrumbs.Text = "Verwaltung : Lieferantenverwaltung";
            navframe.Navigate(new Uri("/modules/Administration/VendorAdministration/VendorAdministrationView.xaml", UriKind.Relative));
        }
        private void NavButton_Selected_UserOverview(object sender, RoutedEventArgs e)
        {
            SidebarRebook.SelectedIndex = -1;
            SidebarMain.SelectedIndex = -1;
            SidebarShopping.SelectedIndex = -1;
            Breadcrumbs.Text = "Verwaltung : Mitarbeiterverwaltung";
            navframe.Navigate(new Uri("/modules/Administration/UserAdministration/UserAdministrationView.xaml", UriKind.Relative));
        }

        private void NavButton_Selected_OldOrderOverview(object sender, RoutedEventArgs e)
        {
            SidebarRebook.SelectedIndex = -1;
            SidebarMain.SelectedIndex = -1;
            SidebarShopping.SelectedIndex = -1;
            Breadcrumbs.Text = "Bestellsystem : Vergangene Bestellungen";
            navframe.Navigate(new Uri("/modules/OrderSystem/ManageOldOrders/OldOrdersOverviewView.xaml", UriKind.Relative));

        }
        public void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.BorderThickness = new System.Windows.Thickness(4);
            }
            else
            {
                this.BorderThickness = new System.Windows.Thickness(0);
            }
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (e.ClickCount == 2)
                {
                    AdjustWindowSize();
                }
                else
                {
                    base.OnMouseLeftButtonDown(e);
                    this.WindowState = WindowState.Normal;
                    this.DragMove();
                }
            }

        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            AdjustWindowSize();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void AdjustWindowSize()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                MaxButton.Content = FindResource("maxBtn");
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                MaxButton.Content = FindResource("minBtn");
            }
        }

        private void NavButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            loginView loginWindow = new loginView();
            this.Close();
            loginWindow.Show();
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\wærp-stockpilot", true);

            MainWindowViewModel.Firstname = "Max";
            MainWindowViewModel.Lastname = "Mustermann";
            MainWindowViewModel.UserID = "";
            MainWindowViewModel.username = "";
            MainWindowViewModel.RoleID = 0;
            MainWindowViewModel.RoleStr = "";
            MainWindowViewModel.CurrentBreadcumb = "";
            MainWindowViewModel.showOrdersystem = false;
            MainWindowViewModel.showRebook = false;
            MainWindowViewModel.showAdministration = false;
            MainWindowViewModel.showSettings = false;
            MainWindowViewModel.openApplication = false;
            MainWindowViewModel.loginSuccesful = false;

            if (bool.Parse(key.GetValue("IsTouch").ToString()))
            {
                LoginTouchWindow LoginWindowTouch = new LoginTouchWindow();
                this.Close();
                LoginWindowTouch.Show();
            }
            else
            {
                loginView LoginScreen = new loginView();
                this.Close();
                LoginScreen.Show();
            }
        }
        private void OpenAdmin_Click(object sender, RoutedEventArgs e)
        {
            Breadcrumbs.Text = "Verwaltung";
            navframe.Navigate(new System.Uri("/modules/Administration/AdministrationOverviewView.xaml", UriKind.RelativeOrAbsolute));
        }



        private void SidebarMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OpenSettings_Click(object sender, RoutedEventArgs e)
        {
            UserSettingsWindow openSettings = new UserSettingsWindow();
            Nullable<bool> dialogResult = openSettings.ShowDialog();
        }

        private void SidebarRebook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AccountSettingsWindow openAccSettings = new AccountSettingsWindow();
            openAccSettings.ShowDialog();
        }

        private void ShutdownTop_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LogoutTop_Click(object sender, RoutedEventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\wærp-stockpilot", true);

            MainWindowViewModel.Firstname = "Max";
            MainWindowViewModel.Lastname = "Mustermann";
            MainWindowViewModel.UserID = "";
            MainWindowViewModel.username = "";
            MainWindowViewModel.RoleID = 0;
            MainWindowViewModel.RoleStr = "";
            MainWindowViewModel.CurrentBreadcumb = "";
            MainWindowViewModel.showOrdersystem = false;
            MainWindowViewModel.showRebook = false;
            MainWindowViewModel.showAdministration = false;
            MainWindowViewModel.showSettings = false;
            MainWindowViewModel.openApplication = false;
            MainWindowViewModel.loginSuccesful = false;

            if (bool.Parse(key.GetValue("IsTouch").ToString()))
            {
                LoginTouchWindow LoginWindowTouch = new LoginTouchWindow();
                this.Close();
                LoginWindowTouch.Show();
            }
            else
            {
                loginView LoginScreen = new loginView();
                this.Close();
                LoginScreen.Show();
            }
        }

        private void TouchDashboardBtn_Click(object sender, RoutedEventArgs e)
        {
            SidebarRebook.SelectedIndex = -1;
            SidebarMngt.SelectedIndex = -1;
            SidebarShopping.SelectedIndex = -1;
            SidebarMain.SelectedIndex = -1;
            navframe.Navigate(new Uri("/mainGUI/DashboardTouchView.xaml", UriKind.Relative));
        }
    }

}
