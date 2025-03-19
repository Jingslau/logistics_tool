namespace waerp_management.store
{
    internal class CurrentRentModel
    {
        static CurrentRentModel()
        {
            ItemIdent = "";
            ItemDescription = "asd";
            ItemIdentStr = "";
            ItemTotalQuantity = "";
            RentLocation = "";
            IsGroup = false;
            RentQuantity = "";
            MachineID = "";
            ItemImagePath = "";
        }


        public static string ItemImagePath { get; set; }
        public static string ItemIdentStr { get; set; }
        public static string RentLocation { get; set; }
        public static string RentQuantity { get; set; }
        public static string ItemTotalQuantity { get; set; }
        public static string MachineID { get; set; }
        public static bool IsGroup { get; set; }

        public static string ItemIdent { get; set; }
        public static string ItemDescription { get; set; }


        public static void ResetParams()
        {
            ItemIdent = "";
            ItemDescription = "asd";
            ItemIdentStr = "";
            ItemTotalQuantity = "";
            RentLocation = "";
            RentQuantity = "";
            ItemImagePath = "";
        }
    }





}
