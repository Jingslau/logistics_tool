using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace waerp_management.store
{
    internal class CurrentReturnModel
    {
        static CurrentReturnModel()
        {
            ItemIdent = "1";
            ItemDescription = "asd";
            ItemIdentStr = "";
            ItemTotalQuantity = "";
            ReturnLocation = "";
            ReturnQuantity = "";
            SearchIdent = "";
            MachineID = "";
            RentID = "";
            ReturnLocationID = "";
            ItemImagePath = "";
            RentedQuantity = "";

        }


        public static string ItemImagePath { get; set; }
        public static string ItemIdentStr { get; set; }
        public static string ReturnLocationID { get; set; }
        public static string SearchIdent { get; set; }
        public static string ReturnLocation { get; set; }
        public static string RentID { get; set; }
        public static string ReturnQuantity { get; set; }
        public static string ItemTotalQuantity { get; set; }
        public static string MachineID { get; set; }
        public static string RentedQuantity { get; set; }

        public static string ItemIdent { get; set; }
        public static string ItemDescription { get; set; }

        public static event PropertyChangedEventHandler PropertyChanged;
        private static void RaisePorpertyChanged([CallerMemberName] string propertyName = "")
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
