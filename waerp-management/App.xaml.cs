using Microsoft.Win32;
using MySqlConnector;
using System;
using System.Threading;
using System.Windows;
using waerp_management.dbtools;
using waerp_management.errorHandling;
using waerp_management.FirstTimeStartup;
using waerp_management.loginHandling;
using waerp_management.ViewModels;

namespace waerp_management
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);



            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\wærp-stockpilot", true);
            if (key != null)
            {
                Thread databaseThread = new Thread(CheckDatabaseReachability);

                // Set the thread as a background thread
                databaseThread.IsBackground = true;

                // Start the thread
                databaseThread.Start();
                loginView mainWindow = new loginView
                {
                    DataContext = new MainViewModel()
                };
            }
            else
            {
                FirstTimeStartUpWindow openInstallation = new FirstTimeStartUpWindow();
                openInstallation.Show();
            }












            // Create an instance of the StartupWindow and show it
            // The MainWindow will be automatically created and displayed



        }
        private void CheckDatabaseReachability()
        {
            while (true)
            {
                // Perform the database reachability check here
                // You can use a try-catch block to catch any exceptions

                try
                {
                    // Attempt to connect to the MySQL database
                    using (var connection = new MySqlConnection(SqlConn.GetConnectionString()))
                    {
                        connection.Open();
                    }
                }
                catch (Exception ex)
                {

                    ErrorHandlerModel.ErrorText = "Die Datenbank ist nicht erreichbar. Bitte prüfen Sie Ihre Netzwerkverbindung oder wenden Sie sich an den Systemadministrator!";
                    ErrorHandlerModel.ErrorType = "NOTALLOWED";
                    ErrorWindow openError = new ErrorWindow();
                    openError.ShowDialog();
                    ErrorLogger.LogSqlError(ex);
                }

                // Wait for 10 seconds before performing the next reachability check
                Thread.Sleep(10000);
            }
        }
    }
}
