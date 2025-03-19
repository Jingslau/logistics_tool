using Microsoft.Win32;
using System.Configuration;

namespace waerp_management.dbtools
{
    internal class SqlConn
    {
        public Configuration config;
        public SqlConn()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }


        public static string GetConnectionString()
        {

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\wærp-stockpilot", true);

            string connDetails = $"Server={key.GetValue("DBServer").ToString()};userid={key.GetValue("DBUser").ToString()};password={key.GetValue("DBPassword").ToString()};Database={key.GetValue("DBSchema").ToString()}; Port={key.GetValue("DBPort")}";
            return connDetails;
        }

        public static string GetLicenseServerConnectionString()
        {
            return $"Server=localhost;userid=root;password=050582496031dDp!;Database=opentools";
        }

        public static bool SetConnectionString(string dbServer, string dbUser, string dbPassword, string dbSchema, string dbPort)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\wærp-stockpilot", true);
            key.SetValue("DBServer", dbServer);
            key.SetValue("DBUser", dbUser);
            key.SetValue("DBPassword", dbPassword);
            key.SetValue("DBSchema", dbSchema);
            key.SetValue("DBPort", dbPort);



            return true;
        }




    }
}
