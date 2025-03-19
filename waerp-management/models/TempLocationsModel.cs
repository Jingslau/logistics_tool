namespace waerp_management.store
{
    internal class TempLocationsModel
    {
        static TempLocationsModel()
        {
            FloorID = "";
            GroupID = "";
            ItemQuant = "";
            ItemName = "";
        }
        public static string FloorID { get; set; }
        public static string ItemName { get; set; }
        public static string ItemQuant { get; set; }
        public static string GroupID { get; set; }
    }
}
