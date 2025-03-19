namespace waerp_management.main
{
    internal class MainWindowViewModel
    {

        static MainWindowViewModel()
        {
            Firstname = "Max";
            Lastname = "Mustermann";
            UserID = "";
            username = "";
            RoleID = 0;
            RoleStr = "";
            CurrentBreadcumb = "";
            showOrdersystem = false;
            showRebook = false;
            showAdministration = false;
            showSettings = false;
            CurrentMainIndex = -1;
            openApplication = false;
            loginSuccesful = false;
        }

        public static string Firstname { get; set; }
        public static string CurrentBreadcumb { get; set; }
        public static string password { get; set; }
        public static string RoleStr { get; set; }
        public static bool showOrdersystem { get; set; }
        public static bool loginSuccesful { get; set; }
        public static bool openApplication { get; set; }
        public static bool showRebook { get; set; }
        public static bool showAdministration { get; set; }
        public static bool showSettings { get; set; }

        public static string UserID { get; set; }
        public static string Lastname { get; set; }
        public static string username { get; set; }
        public static int RoleID { get; set; }
        public static int CurrentMainIndex { get; set; }

        public static string Fullname => $"{Firstname} {Lastname}";



    }
}
