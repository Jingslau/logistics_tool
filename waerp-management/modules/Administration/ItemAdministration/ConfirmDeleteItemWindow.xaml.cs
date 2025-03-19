using System.Data;
using System.Windows;
using waerp_management.errorHandling;
using waerp_management.sql;
using waerp_management.store.Administration;

namespace waerp_management.modules.Administration.ItemAdministration
{
    /// <summary>
    /// Interaction logic for ConfirmDeleteItemWindow.xaml
    /// </summary>
    public partial class ConfirmDeleteItemWindow : Window
    {
        public ConfirmDeleteItemWindow()
        {
            InitializeComponent();
        }

        private void CancleBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {



            AdministrationQueries.RunSqlExec($"DELETE FROM item_objects WHERE item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]}");
            AdministrationQueries.RunSqlExec($"DELETE FROM item_location_relations WHERE item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]}");
            AdministrationQueries.RunSqlExec($"DELETE FROM floor_group_item_relations WHERE item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]}");
            AdministrationQueries.RunSqlExec($"DELETE FROM item_filter_relations WHERE item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]}");
            AdministrationQueries.RunSqlExec($"DELETE FROM item_rents WHERE item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]}");
            AdministrationQueries.RunSqlExec($"DELETE FROM item_subitem_relations WHERE item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]}");
            AdministrationQueries.RunSqlExec($"DELETE FROM item_vendor_relations WHERE item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]}");
            AdministrationQueries.RunSqlExec($"DELETE FROM order_item_relations WHERE item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]}");

            ErrorHandlerModel.ErrorText = "Der Artikel wurde erfolgreich gelöscht!";
            ErrorHandlerModel.ErrorType = "SUCCESS";
            ErrorWindow showSuccess = new ErrorWindow();
            showSuccess.ShowDialog();
            DialogResult = false;



        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            AdministrationQueries.RunSqlExec($"DELETE FROM item_objects WHERE item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]}");

            DataSet ds = AdministrationQueries.RunSql($"SELECT * FROM item_location_relations WHERE item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]}");

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    AdministrationQueries.RunSqlExec($"UPDATE location_objects SET location_quantity = location_quantity - {int.Parse(ds.Tables[0].Rows[i]["location_item_quantity"].ToString())}");
                }
            }

            AdministrationQueries.RunSqlExec($"DELETE FROM item_location_relations WHERE item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]}");
            AdministrationQueries.RunSqlExec($"DELETE FROM floor_group_item_relations WHERE item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]}");
            AdministrationQueries.RunSqlExec($"DELETE FROM item_filter_relations WHERE item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]}");
            AdministrationQueries.RunSqlExec($"DELETE FROM item_rents WHERE item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]}");
            AdministrationQueries.RunSqlExec($"DELETE FROM item_subitem_relations WHERE item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]}");
            AdministrationQueries.RunSqlExec($"DELETE FROM item_vendor_relations WHERE item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]}");
            AdministrationQueries.RunSqlExec($"DELETE FROM order_item_relations WHERE item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]}");

            ErrorHandlerModel.ErrorText = "Artikel wurde erfolreich gelöscht!";
            ErrorHandlerModel.ErrorType = "SUCCESS";
            ErrorWindow openSuccess = new ErrorWindow();
            openSuccess.ShowDialog();
            DialogResult = false;
        }
    }
}
