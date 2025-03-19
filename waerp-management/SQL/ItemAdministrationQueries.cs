using MySqlConnector;
using System.Data;
using waerp_management.dbtools;
using waerp_management.errorHandling;
using waerp_management.store.Administration;

namespace waerp_management.sql
{
    internal class ItemAdministrationQueries
    {
        public static MySqlConnection conn = new MySqlConnection(SqlConn.GetConnectionString());

        public static string GetFilterID(int filterNo, string filterName)
        {
            DataSet ds = RunSql($"SELECT * FROM filter{filterNo}_names WHERE name = '{filterName}'");
            if (ds.Tables[0].Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return ds.Tables[0].Rows[0]["filter_id"].ToString();
            }
        }

        public static string GetSelectedFilterID(int filterNo, string filterName)
        {
            if (filterName.Length != 0)
            {
                return RunSql($"SELECT * FROM filter{filterNo}_names WHERE name = '{filterName}'").Tables[0].Rows[0]["filter_id"].ToString();
            }
            else
            {
                return "null";
            }
        }

        public static string GetFilterName(int filterNo, string itemId)
        {
            DataSet tmp = RunSql($"SELECT * FROM item_filter_relations WHERE item_id = {itemId}");
            if (tmp.Tables[0].Rows.Count > 0)
            {
                DataSet ds = RunSql($"SELECT * FROM filter{filterNo}_names WHERE filter_id = '{tmp.Tables[0].Rows[0][$"filter{filterNo}_id"]}'");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return "";
                }
                else
                {
                    return ds.Tables[0].Rows[0]["name"].ToString();
                }
            }
            else
            {
                return "";
            }


        }

        public static bool SaveEditedItem(string newImagePath, bool editImage)
        {


            if (CurrentItemAdministrationModel.NewItemIsOrderable)
            {
                if (editImage)
                {
                    RunSqlExec($"UPDATE item_objects SET " +
           $"item_ident = '{CurrentItemAdministrationModel.NewItemIdentStr}'" +
           $", " +
           $"item_description = '{CurrentItemAdministrationModel.NewItemDescription}' " +
           $", " +
           $"item_quantity_min = {CurrentItemAdministrationModel.NewItemMinQuant}" +
           $", " +
           $"item_orderquant_min = {CurrentItemAdministrationModel.NewItemMinOrder}" +
           $", " +
           $"item_orderable = 1 " +
           $", " +
           $"item_image_path = '{newImagePath.Replace("\\", "\\\\")}'" +
           $" WHERE " +
           $"item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]}");
                }
                else
                {
                    RunSqlExec($"UPDATE item_objects SET " +
           $"item_ident = '{CurrentItemAdministrationModel.NewItemIdentStr}'" +
           $", " +
           $"item_description = '{CurrentItemAdministrationModel.NewItemDescription}' " +
                 $", " +
                 $"item_description_2 = '{CurrentItemAdministrationModel.NewItemDescription2}' " +
           $", " +
           $"item_quantity_min = {CurrentItemAdministrationModel.NewItemMinQuant}" +
           $", " +
           $"item_orderquant_min = {CurrentItemAdministrationModel.NewItemMinOrder}" +
           $", " +
           $"item_orderable = 1 " +
           $" WHERE " +
           $"item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]}");
                }



                if (RunSql($"SELECT * FROM item_vendor_relations WHERE item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]}").Tables[0].Rows.Count == 0)
                {
                    RunSqlExec($"INSERT INTO item_vendor_relations (item_id, vendor_id, item_price, currency_id) VALUES" +
                                               $" " +
                                               $"(" +
                                               $"{CurrentItemAdministrationModel.SelectedItem["item_id"]}," +
                                               $"{CurrentItemAdministrationModel.NewItemVendorID}," +
                                               $"'{CurrentItemAdministrationModel.NewItemPrice}'," +
                                               $"{CurrentItemAdministrationModel.NewItemCurrencyID}" +
                                               $")");
                }
                else
                {
                    RunSqlExec($"UPDATE item_vendor_relations SET vendor_id = {CurrentItemAdministrationModel.NewItemVendorID}, item_price = '{CurrentItemAdministrationModel.NewItemPrice}', currency_id = {CurrentItemAdministrationModel.NewItemCurrencyID} WHERE item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]}");
                }





            }
            else
            {
                RunSqlExec($"UPDATE item_objects SET " +
                 $"item_ident = '{CurrentItemAdministrationModel.NewItemIdentStr}'" +
                 $", " +
                 $"item_description = '{CurrentItemAdministrationModel.NewItemDescription}' " +
                 $", " +
                 $"item_description_2 = '{CurrentItemAdministrationModel.NewItemDescription2}' " +
                 $", " +
                 $"item_quantity_min = 0" +
                 $", " +
                 $"item_orderquant_min = 0" +
                 $", " +
                 $"item_orderable = 0" +
                 $", " +
                 $"item_onorder = 0" +
                 $" WHERE " +
                 $"item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]}");
                RunSqlExec($"DELETE FROM item_vendor_relations WHERE item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]}");


            }



            RunSqlExec($"UPDATE item_filter_relations SET " +
                 $"filter1_id = {GetSelectedFilterID(1, CurrentItemAdministrationModel.NewItemFilter1)}" +
                 $", " +
                 $"filter2_id = {GetSelectedFilterID(2, CurrentItemAdministrationModel.NewItemFilter2)}" +
                 $", " +
                 $"filter3_id= {GetSelectedFilterID(3, CurrentItemAdministrationModel.NewItemFilter3)}" +
                 $", " +
                 $"filter4_id={GetSelectedFilterID(4, CurrentItemAdministrationModel.NewItemFilter4)}" +
                 $", " +
                 $"filter5_id = {GetSelectedFilterID(5, CurrentItemAdministrationModel.NewItemFilter5)} " +
                 $" WHERE " +
                 $"item_id = {CurrentItemAdministrationModel.SelectedItem["item_id"]} ");

            ErrorHandlerModel.ErrorText = "Der Artikel wurde erfolgreich bearbeitet!";
            ErrorHandlerModel.ErrorType = "SUCCESS";
            ErrorWindow openSuccess = new ErrorWindow();
            openSuccess.ShowDialog();
            return true;
        }

        public static string GetFilterIDName(string filterNo, string filterID)
        {
            return RunSql($"SELECT * FROM filter{filterNo}_names WHERE filter_id = {filterID}").Tables[0].Rows[0]["name"].ToString();
        }

        public static string[] GetAllFilterIDsItem(string itemId)
        {
            string[] FilterIDs = new string[5];
            DataSet ds = RunSql($"SELECT * FROM item_filter_relations WHERE item_id = {itemId}");
            for (int i = 0; i < 5; i++)
            {
                FilterIDs[i] = ds.Tables[0].Rows[0][$"filter{i + 1}_id"].ToString();
            }
            return FilterIDs;
        }


        public static bool CreateNewItem(string ItemIdent, string ItemDescription, string ItemDescription2, string[] FilterArr, int minQuant, int minOrder, int isOrderable, int vendorID, int currencyId, string price, string imagePath)
        {
            string imagePathItem = "";
            if (imagePath == "")
            {
                imagePathItem = @"/assets/images/tools/Bilder Sonstige Lieferanten/Default/default.jpg";
            }

            else
            {
                imagePathItem = imagePath.Replace("\\", "\\\\");
            }

            if (RunSql($"SELECT * FROM item_objects WHERE item_ident = '{ItemIdent}'").Tables[0].Rows.Count > 0)
            {
                ErrorHandlerModel.ErrorText = "Es besteht bereits ein Artikel mit dieser Artikelnummer!";
                ErrorHandlerModel.ErrorType = "NOTALLOWED";
                ErrorWindow showNotallowed = new ErrorWindow();
                showNotallowed.ShowDialog();
                return false;
            }

            else
            {
                string maxIdStr = GetMaxId(RunSql($"SELECT * FROM item_objects"), "item_id");
                if (RunSqlExec($"INSERT INTO item_objects (item_id, item_ident, item_quantity_total, item_quantity_total_new,   item_onetime_use, item_quantity_min, item_orderquant_min, item_orderable, item_onorder, item_description, item_description_2, item_image_path) VALUES({maxIdStr}  , '{ItemIdent}', 0,  0,    0,   {minQuant}, {minOrder}, {isOrderable},   0, '{ItemDescription}', '{ItemDescription2}', '{imagePathItem}')"))
                {
                    if (RunSqlExec($"INSERT INTO item_filter_relations (item_id, filter1_id, filter2_id, filter3_id, filter4_id, filter5_id) VALUES ({maxIdStr}, {FilterArr[0]}, {FilterArr[1]}, {FilterArr[2]}, {FilterArr[3]}, {FilterArr[4]})"))
                    {
                        if (vendorID > -1)
                        {
                            return RunSqlExec($"INSERT INTO item_vendor_relations (item_id, vendor_id, item_price, currency_id) VALUES" +
                                  $" " +
                                  $"(" +
                                  $"{maxIdStr}," +
                                  $"{vendorID}," +
                                  $"'{price}'," +
                                  $"{currencyId}" +
                                  $")");
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }




            }
        }

        public static string GetMaxId(DataSet ds, string Prompt)
        {
            int maxID = 0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (int.Parse(row[Prompt].ToString()) > maxID)
                {
                    maxID = int.Parse(row[Prompt].ToString());
                }
            }
            maxID++;
            return maxID.ToString();
        }


        public static DataSet RunSql(string query)
        {
            try
            {
                conn.Open();
                MySqlDataAdapter adp = new MySqlDataAdapter();
                DataSet ReturnDataSet = new DataSet();
                adp = new MySqlDataAdapter(new MySqlCommand(query, conn));
                adp.Fill(ReturnDataSet);
                conn.Close();
                return ReturnDataSet;
            }
            catch (MySqlException ex)
            {
                ErrorHandlerModel.SQLQuery = query;
                ErrorLogger.LogSqlError(ex);
                return null;

            }
            finally { conn.Close(); }
        }
        private static bool RunSqlExec(string query)
        {
            try
            {
                ErrorHandlerModel.SQLQuery = query;
                conn.Open();
                new MySqlCommand(query, conn).ExecuteNonQuery();
                conn.Close();
                return true;

            }
            catch (MySqlException ex)
            {
                ErrorLogger.LogSqlError(ex);
                return false;

            }
            finally { conn.Close(); }
        }



    }
}
