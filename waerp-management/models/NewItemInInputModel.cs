namespace waerp_management.models
{
    internal class NewItemInInputModel
    {

        static NewItemInInputModel()
        {
            ItemID = "";
            ItemDescription = "";
            ItemIdent = "";
            QuantityInput = "0";

        }
        public static string ItemID { get; set; }
        public static string ItemDescription { get; set; }
        public static string ItemIdent { get; set; }
        public static string QuantityInput { get; set; }
    }
}
