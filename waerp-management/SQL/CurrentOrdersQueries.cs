using MySqlConnector;
using System.Data;
using waerp_management.dbtools;
using waerp_management.errorHandling;
using waerp_management.models;

namespace waerp_management.sql
{
    internal class CurrentOrdersQueries
    {
        public static MySqlConnection conn = new MySqlConnection(SqlConn.GetConnectionString());


        public static DataSet GetAllVendors()
        {

            DataSet ds = RunSql("SELECT * FROM vendor_objects");
            DataSet ds2 = ds.Copy();
            ds2.Clear();

            if (RunSql("SELECT * FROM order_item_relations").Tables.Count > 0 && RunSql("SELECT * FROM order_item_relations").Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataSet tmp = RunSql($"SELECT * FROM order_item_relations WHERE vendor_id = {ds.Tables[0].Rows[i]["vendor_id"]} AND isOpen = 1");

                    if (tmp.Tables[0].Rows.Count > 0)
                    {
                        ds2.Tables[0].ImportRow(ds.Tables[0].Rows[i]);
                    }
                }
                return ds2;
            }
            else
            {
                return null;
            }
        }

        public static bool DeleteOrder()
        {
            RunSqlExec($"DELETE FROM order_item_relations WHERE item_id = {OrderStore.item_id} AND order_ident = '{OrderStore.OrderIdent}'");
            DataSet ds2 = RunSql($"SELECT * FROM order_item_relations WHERE order_ident = '{OrderStore.OrderIdent}'");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                RunSqlExec($"DELETE FROM order_objects WHERE order_ident = '{OrderStore.OrderIdent}'");
            }

            DataSet ds = RunSql($"SELECT * FROM order_item_relations WHERE item_id = {OrderStore.item_id} AND isOpen = 1");
            if (ds.Tables[0].Rows.Count == 0)
            {
                RunSqlExec($"UPDATE item_objects SET item_onorder = 0 WHERE item_id = {OrderStore.item_id}");
            }
            return true;

        }

        public static bool CheckIfUsed(string location_id)
        {
            if (RunSql($"SELECT * FROM item_location_relations WHERE location_id = {location_id}").Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void EditOrder(string newQuant, string item_id)
        {
            RunSqlExec($"UPDATE order_item_relations SET order_quantity = {newQuant} WHERE item_id = {item_id}");
        }


        public static DataSet GetOrderedItems(string vendor_id)
        {
            DataSet ds = RunSql($"SELECT * FROM order_item_relations WHERE vendor_id = {vendor_id} AND isOpen = 1");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add("item_ident");
                ds.Tables[0].Columns.Add("item_description");

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataSet CurrentItem = AdministrationQueries.RunSql($"SELECT * FROM item_objects WHERE item_id = {ds.Tables[0].Rows[i]["item_id"].ToString()}");

                    if (CurrentItem.Tables[0].Rows.Count > 0)
                    {
                        ds.Tables[0].Rows[i]["item_ident"] = CurrentItem.Tables[0].Rows[0]["item_ident"];
                        ds.Tables[0].Rows[i]["item_description"] = CurrentItem.Tables[0].Rows[0]["item_description"];

                    }

                }
                return ds;
            }
            else
            {
                return null;
            }

        }

        public static string GetMaxIdLocation()
        {
            return GetMaxId(RunSql("SELECT * FROM item_location_relations"), "id");
        }

        public static string GetMaxId(DataSet ds, string Prompt)
        {
            if (ds.Tables[0].Rows.Count == 0)
            {
                return "0";
            }
            else
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

        }
        private static DataSet RunSql(string query)
        {
            try
            {
                ErrorHandlerModel.SQLQuery = query;
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
