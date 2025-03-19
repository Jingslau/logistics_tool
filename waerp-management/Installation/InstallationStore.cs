namespace waerp_management.Installation
{
    internal class InstallationStore
    {
        static InstallationStore()
        {
            ServerAdress = "";
            DatabaseUser = "";
            DatabasePassword = "";
            ServerSchema = "";
            DatabasePort = "";
        }
        public static string ServerAdress { get; set; }
        public static string DatabaseUser { get; set; }
        public static string DatabasePassword { get; set; }
        public static string ServerSchema { get; set; }

        public static string DatabasePort { get; set; }
    }
}
