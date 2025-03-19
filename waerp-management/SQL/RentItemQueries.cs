using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using waerp_management.dbtools;
using waerp_management.errorHandling;
using waerp_management.main;
using waerp_management.store;

namespace waerp_management.sql
{

    internal class RentItemQueries
    {

        public static MySqlConnection conn = new MySqlConnection(SqlConn.GetConnectionString());


        public static DataSet GetAllMachines()
        {
            String que = "Select * from machines";
            return RunSql(que);
        }

        public static DataSet GetAllFilters_1()
        {
            String que = "Select * from filter1_names";
            return RunSql(que);
        }

        public static DataSet GetItemFilterRelationsSQL(int counter, string[] SearchParams)
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
            return RunSql(que);
        }

        public static DataSet GetFilterNames(int filterId, string[] FilterParams)
        {
            String que1 = ($"SELECT * FROM filter{filterId}_names ");
            String que2 = string.Format("WHERE filter_id IN ({0})", string.Join(", ", FilterParams));
            String queFinal = que1 + que2;
            return RunSql(queFinal);
        }

        public static DataSet GetItemFilterRelations(string StageSearchStr)
        {
            String que = $"SELECT * FROM item_filter_relations WHERE " + StageSearchStr;
            return RunSql(que);
        }

        public static DataSet GetItemDetails(string[] ItemIdArr)
        {
            String que = string.Format("SELECT * FROM item_objects WHERE item_id IN ({0})", string.Join(", ", ItemIdArr));
            return RunSql(que);
        }

        public static DataSet GetLocations()
        {
            DataSet ds = RunSql($"SELECT * FROM item_location_relations WHERE item_id = {CurrentRentModel.ItemIdent}");
            List<string> strDetailIDList = new List<string>();

            foreach (DataRow row in ds.Tables[0].Rows)
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
                string que = string.Format("SELECT * FROM location_objects WHERE location_id IN ({0})", string.Join(", ", tmpArr));
                DataSet ds2 = RunSql(que);
                ds2.Tables[0].Columns.Add("item_quantity");
                for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                {
                    ds2.Tables[0].Rows[i]["item_quantity"] = RunSql($"SELECT * FROM item_location_relations WHERE " +
                        $"item_id = {CurrentRentModel.ItemIdent} " +
                        $"AND " +
                        $"location_id = {ds2.Tables[0].Rows[i]["location_id"]}").Tables[0].Rows[0]["location_item_quantity"];
                }
                return ds2;
            }
            else
            {
                return new DataSet();
            }
        }

        public static DataSet GetGroups()
        {
            DataSet ds = RunSql($"SELECT * FROM floor_group_item_relations WHERE item_id = {CurrentRentModel.ItemIdent}");

            ds.Tables[0].Columns.Add("group_name");
            ds.Tables[0].Columns.Add("floor_name");

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string tmpFloorId = RunSql($"SELECT * FROM floor_group_objects WHERE group_id = {ds.Tables[0].Rows[i]["group_id"]}").Tables[0].Rows[0]["floor_id"].ToString();

                    ds.Tables[0].Rows[i]["floor_name"] = RunSql($"SELECT * FROM floor_objects WHERE floor_id = {tmpFloorId}").Tables[0].Rows[0]["floor_name"].ToString();
                    ds.Tables[0].Rows[i]["group_name"] = RunSql($"SELECT * FROM group_objects WHERE group_id = {ds.Tables[0].Rows[i]["group_id"]}").Tables[0].Rows[0]["group_name"].ToString();

                }
                return ds;
            }

            return new DataSet();

        }

        public static Boolean RentItemExec(string NewQuant, string InputQuant, string LocationId, string LocationQuant, string floor_name)
        {
            if (CurrentRentModel.IsGroup)
            {
                if (int.Parse(LocationQuant) - int.Parse(InputQuant) == 0)
                {

                    //IF GROUP HAS MORE THEN ONE ITEM
                    if (int.Parse(RunSql($"SELECT * FROM group_objects WHERE group_id = {LocationId}").Tables[0].Rows[0]["group_quantity"].ToString()) > 1)
                    {
                        RunSqlExec($"UPDATE item_objects SET item_quantity_total={NewQuant} WHERE item_id={CurrentRentModel.ItemIdent}");
                        RunSqlExec($"DELETE FROM floor_group_item_relations WHERE group_id = {LocationId} AND item_id = {CurrentRentModel.ItemIdent}");
                        RunSqlExec($"UPDATE group_objects SET group_quantity = group_quantity -1 WHERE group_id = {LocationId}");
                        string floor_id = RunSql($"SELECT * FROM floor_objects WHERE floor_name = '{floor_name}'").Tables[0].Rows[0]["floor_id"].ToString();
                        RunSqlExec($"DELETE from floor_group_objects WHERE group_id = {LocationId} AND floor_id = {floor_id}");
                        RunSqlExec($"UPDATE floor_objects SET floor_quantity = floor_quantity - 1");
                        DataSet ds = RunSql("SELECT * FROM item_rents");
                        DateTime rentDateTime = DateTime.Now;
                        string sqlFormattedDate = rentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                        string maxIdStr = GetMaxId(ds, "rent_id");
                        RunSqlExec($"INSERT INTO item_rents (rent_id, item_id, user_id, location_id, rent_quantity, machine_id, createdAt) VALUES ({maxIdStr},  {CurrentRentModel.ItemIdent}, {MainWindowViewModel.UserID}, {LocationId}, {InputQuant}, 1, '{sqlFormattedDate}')");
                        HistoryLogger.CreateHistory(CurrentRentModel.ItemIdent, InputQuant, "0", "0", LocationId, "0", "1");
                        return true;
                    }

                    //IF GROUP HAS ONLY ONE ITEM
                    else
                    {
                        RunSqlExec($"UPDATE item_objects SET item_quantity_total={NewQuant} WHERE item_id={CurrentRentModel.ItemIdent}");
                        RunSqlExec($"DELETE FROM floor_group_item_relations WHERE group_id = {LocationId} AND item_id = {CurrentRentModel.ItemIdent}");
                        RunSqlExec($"DELETE from group_objects WHERE group_id = {LocationId}");
                        string floor_id = RunSql($"SELECT * FROM floor_objects WHERE floor_name = '{floor_name}'").Tables[0].Rows[0]["floor_id"].ToString();
                        RunSqlExec($"DELETE from floor_group_objects WHERE group_id = {LocationId} AND floor_id = {floor_id}");
                        RunSqlExec($"UPDATE floor_objects SET floor_quantity = floor_quantity - 1");
                        DataSet ds = RunSql("SELECT * FROM item_rents");
                        DateTime rentDateTime = DateTime.Now;
                        string sqlFormattedDate = rentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                        string maxIdStr = GetMaxId(ds, "rent_id");
                        RunSqlExec($"INSERT INTO item_rents (rent_id, item_id, user_id, location_id, rent_quantity, machine_id, createdAt) VALUES ({maxIdStr},  {CurrentRentModel.ItemIdent}, {MainWindowViewModel.UserID}, {LocationId}, {InputQuant}, 1, '{sqlFormattedDate}')");
                        HistoryLogger.CreateHistory(CurrentRentModel.ItemIdent, InputQuant, "0", "0", LocationId, "0", "1");
                        return true;
                    }




                }
                else
                {
                    RunSqlExec($"UPDATE item_objects SET item_quantity_total={NewQuant} WHERE item_id={CurrentRentModel.ItemIdent}");
                    RunSqlExec($"UPDATE floor_group_item_relations SET item_quantity = item_quantity - {InputQuant} WHERE group_id = {LocationId} AND item_id = {CurrentRentModel.ItemIdent}");

                    DataSet ds = RunSql("SELECT * FROM item_rents");
                    DateTime rentDateTime = DateTime.Now;
                    string sqlFormattedDate = rentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    string maxIdStr = GetMaxId(ds, "rent_id");
                    RunSqlExec($"INSERT INTO item_rents (rent_id, item_id, user_id, location_id, rent_quantity, machine_id, createdAt) VALUES ({maxIdStr},  {CurrentRentModel.ItemIdent}, {MainWindowViewModel.UserID}, {LocationId}, {InputQuant}, 1, '{sqlFormattedDate}')");

                    HistoryLogger.CreateHistory(CurrentRentModel.ItemIdent, InputQuant, "0", "0", LocationId, "0", "1");
                    return true;
                }



            }
            else
            {

                if (int.Parse(LocationQuant) - int.Parse(InputQuant) == 0)
                {
                    RunSqlExec($"UPDATE item_objects SET item_quantity_total={NewQuant} WHERE item_id={CurrentRentModel.ItemIdent}");
                    RunSqlExec("UPDATE location_objects SET location_quantity = location_quantity - '" + InputQuant + "' WHERE location_id = '" + LocationId + "'");
                    RunSqlExec($"DELETE FROM item_location_relations WHERE item_id = {CurrentRentModel.ItemIdent} AND location_id = {LocationId}");
                    DataSet ds = RunSql("SELECT * FROM item_rents");
                    DateTime rentDateTime = DateTime.Now;
                    string sqlFormattedDate = rentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    string maxIdStr = GetMaxId(ds, "rent_id");
                    RunSqlExec($"INSERT INTO item_rents (rent_id, item_id, user_id, location_id, rent_quantity, machine_id, createdAt) VALUES ({maxIdStr},  {CurrentRentModel.ItemIdent}, {MainWindowViewModel.UserID}, {LocationId}, {InputQuant},1, '{sqlFormattedDate}')");

                    HistoryLogger.CreateHistory(CurrentRentModel.ItemIdent, InputQuant, LocationId, "0", "0", "0", "1");
                    return true;
                }
                else
                {
                    RunSqlExec($"UPDATE item_objects SET item_quantity_total={NewQuant} WHERE item_id={CurrentRentModel.ItemIdent}");
                    RunSqlExec("UPDATE location_objects SET location_quantity = location_quantity - '" + InputQuant + "' WHERE location_id = '" + LocationId + "'");
                    RunSqlExec($"UPDATE item_location_relations SET location_item_quantity = {int.Parse(LocationQuant) - int.Parse(InputQuant)} WHERE item_id = {CurrentRentModel.ItemIdent} AND location_id = {LocationId}");
                    DataSet ds = RunSql("SELECT * FROM item_rents");
                    DateTime rentDateTime = DateTime.Now;
                    string sqlFormattedDate = rentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    string maxIdStr = GetMaxId(ds, "rent_id");
                    RunSqlExec($"INSERT INTO item_rents (rent_id, item_id, user_id, location_id, rent_quantity, machine_id, createdAt) VALUES ({maxIdStr},  {CurrentRentModel.ItemIdent}, {MainWindowViewModel.UserID}, {LocationId}, {InputQuant}, 1, '{sqlFormattedDate}')");
                    HistoryLogger.CreateHistory(CurrentRentModel.ItemIdent, InputQuant, LocationId, "0", "0", "0", "1");
                    return true;
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

        public static DataSet GetLastStage(int counter, string[] filterArr)
        {
            if (counter == 1)
            {
                String que = "SELECT * FROM filter1_names";
                return RunSql(que);
            }
            else
            {
                return new DataSet();
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
                conn.Open();
                new MySqlCommand(query, conn).ExecuteNonQuery();
                conn.Close();
                return true;

            }
            catch (MySqlException ex)
            {
                ErrorHandlerModel.SQLQuery = query;
                ErrorLogger.LogSqlError(ex);
                return false;

            }
            finally { conn.Close(); }
        }
    }
}
