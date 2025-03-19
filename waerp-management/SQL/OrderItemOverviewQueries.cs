using MySqlConnector;
using System;
using System.Data;
using System.Windows;
using waerp_management.dbtools;
using waerp_management.errorHandling;
using waerp_management.store;

namespace waerp_management.sql
{


    internal class OrderItemOverviewQueries
    {

        public static MySqlConnection conn = new MySqlConnection(SqlConn.GetConnectionString());

        public static int GetCurrentStock()
        {
            String que = "SELECT * FROM item_objects";
            DataSet ds = RunSql(que);
            int output = 0;
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                output += int.Parse(ds.Tables[0].Rows[i]["item_quantity_total"].ToString());
            }
            return output;
        }
        public static int GetCurrentRent()
        {
            String que = "SELECT * FROM item_rents";
            DataSet ds = RunSql(que);
            int output = 0;
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                output += int.Parse(ds.Tables[0].Rows[i]["rent_quantity"].ToString());
            }
            return output;
        }

        public static int GetCurrentNew()
        {
            String que = "SELECT * FROM item_objects";
            DataSet ds = RunSql(que);
            int output = 0;
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                output += int.Parse(ds.Tables[0].Rows[i]["item_quantity_total_new"].ToString());
            }
            return output;
        }

        public static bool CreateOrder()
        {
            DateTime orderDateTime = DateTime.Now;
            string sqlFormattedDate = orderDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            string MaxIdStringOrder = GetMaxId(RunSql("SELECT * FROM order_objects"), "order_id");

            string CurrentOderString1 = orderDateTime.ToString("yyyy") + orderDateTime.ToString("MM") + orderDateTime.ToString("dd");
            DataSet ds = RunSql($"SELECT * FROM order_objects WHERE order_ident = {CurrentOderString1}");
            int CurrentOrderNo = ds.Tables[0].Rows.Count + 1;
            string CurrentOrderIdent = CurrentOderString1 + "-" + CurrentOrderNo.ToString();

            for (int i = 0; i < ShoppingCartModel.ShoppingCartInput.Tables[0].Rows.Count; i++)
            {
                string maxIdStr = GetMaxId(RunSql("SELECT * FROM order_item_relations"), "order_id");
                string vendorID = AdministrationQueries.RunSql($"SELECT * FROM item_vendor_relations WHERE item_id = {ShoppingCartModel.ShoppingCartInput.Tables[0].Rows[i]["item_id"]}").Tables[0].Rows[0]["vendor_id"].ToString();




                RunSqlExec($"INSERT INTO order_item_relations (order_id, order_ident, item_id, order_quantity,order_quantity_org, vendor_id, isOpen, createdAt) VALUES ({maxIdStr}, '{CurrentOrderIdent}', {ShoppingCartModel.ShoppingCartInput.Tables[0].Rows[i]["item_id"]}, {ShoppingCartModel.ShoppingCartInput.Tables[0].Rows[i]["order_quantity"]}, {ShoppingCartModel.ShoppingCartInput.Tables[0].Rows[i]["order_quantity"]}, {vendorID}, 1,'{sqlFormattedDate}')");
            }

            RunSqlExec($"INSERT INTO order_objects (order_id, order_ident, order_status) VALUES ({MaxIdStringOrder}, '{CurrentOrderIdent}', 1)");
            return true;
        }


        public static DataSet GetAllItems()
        {
            String que = "Select * from item_objects WHERE item_orderable = 1";
            DataSet ds = RunSql(que);
            ds.Tables[0].Columns.Add("vendor");
            ds.Tables[0].Columns.Add("vendor_id");
            ds.Tables[0].Columns.Add("order_quantity");
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                que = $"SELECT * FROM item_vendor_relations WHERE item_id = {ds.Tables[0].Rows[i]["item_id"]}";
                DataSet ds2 = RunSql(que);
                if (ds2.Tables[0].Rows.Count != 0)
                {
                    que = $"SELECT * FROM vendor_objects WHERE vendor_id = {ds2.Tables[0].Rows[0]["vendor_id"]}";
                    DataSet ds3 = RunSql(que);
                    if (ds3.Tables[0].Rows.Count != 0)
                    {
                        ds.Tables[0].Rows[i]["vendor"] = ds3.Tables[0].Rows[0]["vendor_name"];
                        ds.Tables[0].Rows[i]["vendor_id"] = ds3.Tables[0].Rows[0]["vendor_id"];
                    }
                }


            }
            return ds;
        }
        public static DataSet GetAllItemsNeeded()
        {
            String que = "Select * from item_objects WHERE item_quantity_total_new < item_quantity_min AND item_onorder = 0 AND item_orderable = 1";
            DataSet ds = RunSql(que);
            ds.Tables[0].Columns.Add("vendor");
            ds.Tables[0].Columns.Add("vendor_id");
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                que = $"SELECT * FROM item_vendor_relations WHERE item_id = {ds.Tables[0].Rows[i]["item_id"]}";
                DataSet ds2 = RunSql(que);
                if (ds2.Tables[0].Rows.Count != 0)
                {
                    que = $"SELECT * FROM vendor_objects WHERE vendor_id = {ds2.Tables[0].Rows[0]["vendor_id"]}";
                    DataSet ds3 = RunSql(que);
                    if (ds3.Tables[0].Rows.Count != 0)
                    {
                        ds.Tables[0].Rows[i]["vendor"] = ds3.Tables[0].Rows[0]["vendor_name"];
                        ds.Tables[0].Rows[i]["vendor_id"] = ds3.Tables[0].Rows[0]["vendor_id"];
                    }
                }


            }
            return ds;
        }

        public static DataSet GetAllItemsMin()
        {
            String que = "Select * from item_objects WHERE item_quantity_total_new - item_quantity_min <= 2 AND item_onorder = 0 AND item_orderable = 1";
            DataSet ds = RunSql(que);
            ds.Tables[0].Columns.Add("vendor");
            ds.Tables[0].Columns.Add("vendor_id");
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                que = $"SELECT * FROM item_vendor_relations WHERE item_id = {ds.Tables[0].Rows[i]["item_id"]}";
                DataSet ds2 = RunSql(que);
                if (ds2.Tables[0].Rows.Count != 0)
                {
                    que = $"SELECT * FROM vendor_objects WHERE vendor_id = {ds2.Tables[0].Rows[0]["vendor_id"]}";
                    DataSet ds3 = RunSql(que);
                    if (ds3.Tables[0].Rows.Count != 0)
                    {
                        ds.Tables[0].Rows[i]["vendor"] = ds3.Tables[0].Rows[0]["vendor_name"];
                        ds.Tables[0].Rows[i]["vendor_id"] = ds3.Tables[0].Rows[0]["vendor_id"];
                    }
                }


            }
            return ds;
        }
        public static DataSet GetAllItemsOrdered()
        {
            String que = "Select * from item_objects WHERE item_onorder = 1 AND item_orderable = 1";
            DataSet ds = RunSql(que);
            ds.Tables[0].Columns.Add("vendor");
            ds.Tables[0].Columns.Add("vendor_id");
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                que = $"SELECT * FROM item_vendor_relations WHERE item_id = {ds.Tables[0].Rows[i]["item_id"]}";
                DataSet ds2 = RunSql(que);
                if (ds2.Tables[0].Rows.Count != 0)
                {
                    que = $"SELECT * FROM vendor_objects WHERE vendor_id = {ds2.Tables[0].Rows[0]["vendor_id"]}";
                    DataSet ds3 = RunSql(que);
                    if (ds3.Tables[0].Rows.Count != 0)
                    {
                        ds.Tables[0].Rows[i]["vendor"] = ds3.Tables[0].Rows[0]["vendor_name"];
                        ds.Tables[0].Rows[i]["vendor_id"] = ds3.Tables[0].Rows[0]["vendor_id"];
                    }
                }


            }
            return ds;
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
        private static void RunSqlExec(string query)
        {
            try
            {
                conn.Open();
                new MySqlCommand(query, conn).ExecuteNonQuery();
                conn.Close();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("ASSDJSJ " + ex);
                //  ErrorLogger.LogSqlError(ex);

            }
            finally { conn.Close(); }
        }
    }
}
