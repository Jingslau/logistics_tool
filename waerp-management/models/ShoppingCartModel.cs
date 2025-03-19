using System.Data;

namespace waerp_management.store
{
    internal class ShoppingCartModel
    {
        static ShoppingCartModel()
        {
            ItemQuantity = "";
            check = false;
            ShoppingCartInput = new DataSet();

        }
        public static string ItemQuantity { get; set; }
        public static bool check { get; set; }
        public static DataSet ShoppingCartInput { get; set; }
    }
}
