using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using waerp_management.dbtools;
using waerp_management.errorHandling;
using waerp_management.store;

namespace waerp_management.sql
{
    internal class ReturnItemQueries
    {
        public static MySqlConnection conn = new MySqlConnection(SqlConn.GetConnectionString());


        public static DataSet GetAllLocations()
        {
            return RunSql("SELECT * FROM location_objects");
        }

        public static DataSet GetItemLocations()
        {
            DataSet tmp = RunSql($"Select * from item_location_relations WHERE item_id = {CurrentReturnModel.ItemIdent}");
            List<string> strDetailIDList = new List<string>();

            foreach (DataRow row in tmp.Tables[0].Rows)
            {
                strDetailIDList.Add(row["location_id"].ToString());
            }

            String[] tmpArr = new string[strDetailIDList.Count];
            for (int i = 0; i < strDetailIDList.Count; i++)
            {
                tmpArr[i] = strDetailIDList[i].ToString();
            }
            if (tmpArr.Length > 0)
            {
                DataSet ds = RunSql(string.Format("SELECT * FROM location_objects WHERE location_id IN ({0})", string.Join(", ", tmpArr)));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["location_quantity"] = RunSql($"SELECT * FROM item_location_relations WHERE " +
                        $"item_id = {CurrentReturnModel.ItemIdent} " +
                        $"AND " +
                        $"location_id = {ds.Tables[0].Rows[i]["location_id"]}").Tables[0].Rows[0]["location_item_quantity"];
                }
                return ds;
            }
            else
            {
                return null;
            }
        }
        public static DataSet GetItemFilterRelationsSQL(int counter, string[] SearchParams, string[] itemIDs)
        {
            String que = "SELECT * FROM item_filter_relations WHERE ";
            for (int i = 0; i < counter; i++)
            {
                que += $"filter{i + 1}_id = '{SearchParams[i]}'";
                if (i < counter - 1)
                {
                    que += " AND ";
                }
            }

            que = que + " AND " + string.Format(" item_id IN ({0})", string.Join(", ", itemIDs));
            return RunSql(que);
        }

        public static void ReturnItemNewLocation()
        {
            string maxIdStr = GetMaxId(RunSql("SELECT * FROM item_location_relations"), "id");

            RunSqlExec($"INSERT INTO item_location_relations (id, item_id, location_id, location_item_quantity) VALUES ({maxIdStr}, {CurrentReturnModel.ItemIdent}, {CurrentReturnModel.ReturnLocationID}, {CurrentReturnModel.ReturnQuantity})");
            RunSqlExec($"UPDATE item_objects SET item_quantity_total = item_quantity_total + {CurrentReturnModel.ReturnQuantity} WHERE item_id = {CurrentReturnModel.ItemIdent}");
            RunSqlExec($"UPDATE location_objects SET location_quantity = location_quantity + {CurrentReturnModel.ReturnQuantity} WHERE location_id = {CurrentReturnModel.ReturnLocationID}");
            HistoryLogger.CreateHistory(CurrentReturnModel.ItemIdent, CurrentReturnModel.ReturnQuantity, "0", CurrentReturnModel.ReturnLocationID, "0", "0", "2");
        }

        public static void ReturnItemLocation()
        {
            RunSqlExec($"UPDATE item_objects SET item_quantity_total = item_quantity_total + {CurrentReturnModel.ReturnQuantity} WHERE item_id = {CurrentReturnModel.ItemIdent}");
            RunSqlExec($"UPDATE item_location_relations SET location_item_quantity = location_item_quantity + {CurrentReturnModel.ReturnQuantity} WHERE item_id = {CurrentReturnModel.ItemIdent} AND location_id = {CurrentReturnModel.ReturnLocationID}");
            RunSqlExec($"UPDATE location_objects SET location_quantity = location_quantity + {CurrentReturnModel.ReturnQuantity} WHERE location_id = {CurrentReturnModel.ReturnLocationID}");
            HistoryLogger.CreateHistory(CurrentReturnModel.ItemIdent, CurrentReturnModel.ReturnQuantity, "0", CurrentReturnModel.ReturnLocationID, "0", "0", "2");
        }

        public static void DeleteRent()
        {
            RunSqlExec($"DELETE FROM item_rents WHERE item_id = {CurrentReturnModel.ItemIdent} AND rent_quantity = {CurrentReturnModel.ItemTotalQuantity}");
        }

        public static void UpdateRent()
        {
            RunSqlExec($"UPDATE item_rents SET rent_quantity = rent_quantity - {CurrentReturnModel.ReturnQuantity} WHERE item_id = {CurrentReturnModel.ItemIdent} AND rent_quantity = {CurrentReturnModel.ItemTotalQuantity}");
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
                ErrorHandlerModel.SQLQuery = query;
                ErrorLogger.LogSqlError(ex);
                return null;

            }
            finally { conn.Close(); }
        }
        private static void RunSqlExec(string query)
        {
            try
            {
                ErrorHandlerModel.SQLQuery = query;
                conn.Open();
                new MySqlCommand(query, conn).ExecuteNonQuery();
                conn.Close();

            }
            catch (MySqlException ex)
            {
                ErrorLogger.LogSqlError(ex);

            }
            finally { conn.Close(); }
        }
    }
}
