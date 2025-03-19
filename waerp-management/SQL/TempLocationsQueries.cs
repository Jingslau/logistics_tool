using MySqlConnector;
using System.Data;
using waerp_management.dbtools;
using waerp_management.errorHandling;
using waerp_management.store;

namespace waerp_management.sql
{
    internal class TempLocationsQueries
    {
        public static MySqlConnection conn = new MySqlConnection(SqlConn.GetConnectionString());
        public static DataSet GetFloorZones()
        {
            return RunSql("SELECT * FROM floor_objects");
        }

        public static DataSet GetZoneGroups()
        {
            DataSet ds = RunSql("SELECT * FROM group_objects");
            ds.Tables[0].Rows.Clear();
            DataSet tmp = RunSql($"SELECT * FROM floor_group_objects WHERE floor_id = {TempLocationsModel.FloorID}");
            for (int i = 0; i < tmp.Tables[0].Rows.Count; i++)
            {
                ds.Tables[0].ImportRow(RunSql($"SELECT * FROM group_objects WHERE group_id = {tmp.Tables[0].Rows[i]["group_id"].ToString()}").Tables[0].Rows[0]);
            }

            return ds;
        }

        public static DataSet GetItemsInGroup()
        {
            DataSet ds = RunSql($"SELECT * FROM floor_group_item_relations WHERE group_id = {TempLocationsModel.GroupID}");
            DataSet output = RunSql("SELECT * FROM item_objects");
            output.Tables[0].Rows.Clear();
            output.Tables[0].Columns.Add("GroupQuantity");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                output.Tables[0].ImportRow(RunSql($"SELECT * FROM item_objects WHERE item_id = {ds.Tables[0].Rows[i]["item_id"]}").Tables[0].Rows[0]);
                output.Tables[0].Rows[i]["GroupQuantity"] = ds.Tables[0].Rows[i]["item_quantity"].ToString();
            }
            return output;
        }
        public static bool DeleteGroup()
        {
            RunSqlExec($"DELETE FROM group_objects WHERE group_id = {TempLocationsModel.GroupID} ");
            string floorId = RunSql($"SELECT * FROM floor_group_objects WHERE group_id = {TempLocationsModel.GroupID}").Tables[0].Rows[0]["floor_id"].ToString();
            RunSqlExec($"DELETE FROM floor_group_objects WHERE floor_id = {floorId} AND group_id = {TempLocationsModel.GroupID}");
            RunSqlExec($"UPDATE floor_objects SET floor_quantity = floor_quantity - 1 WHERE floor_id = {floorId}");

            DataSet groupItems = RunSql($"SELECT * FROM floor_group_item_relations WHERE group_id = {TempLocationsModel.GroupID}");
            for (int i = 0; i < groupItems.Tables[0].Rows.Count; i++)
            {

                RunSqlExec($"UPDATE item_objects SET item_quantity_total = item_quantity_total - {groupItems.Tables[0].Rows[i]["item_quantity"]} WHERE item_id = {groupItems.Tables[0].Rows[i]["item_id"]}");

            }
            RunSqlExec($"DELETE FROM floor_group_item_relations WHERE group_id = {TempLocationsModel.GroupID}");
            return true;
        }
        public static void DeleteItemFromGroup()
        {
            RunSqlExec($"DELETE FROM floor_group_item_relations WHERE item_id = {CurrentRentModel.ItemIdent} AND group_id = {TempLocationsModel.GroupID} ");
            RunSqlExec($"UPDATE item_objects SET item_quantity_total = item_quantity_total - {TempLocationsModel.ItemQuant} WHERE item_id = {CurrentRentModel.ItemIdent}");


            DataSet GroupSelected = RunSql($"SELECT * FROM group_objects WHERE group_id = {TempLocationsModel.GroupID}");
            if ((int.Parse(GroupSelected.Tables[0].Rows[0]["group_quantity"].ToString()) - int.Parse(TempLocationsModel.ItemQuant)) <= 0)
            {
                RunSqlExec($"DELETE FROM group_objects WHERE group_id = {TempLocationsModel.GroupID}");
                RunSqlExec($"DELETE FROM floor_group_objects WHERE group_id = {TempLocationsModel.GroupID} AND floor_id = {TempLocationsModel.FloorID}");
                RunSqlExec($"UPDATE floor_objects SET floor_quantity = floor_quantity - 1 WHERE floor_id = {TempLocationsModel.FloorID}");
            }
            else
            {
                RunSqlExec($"UPDATE group_objects SET group_quantity = group_quantity - {TempLocationsModel.ItemQuant} WHERE group_id = {TempLocationsModel.GroupID}");
            }

            HistoryLogger.CreateHistory(CurrentRentModel.ItemIdent, TempLocationsModel.ItemQuant, "0", "0", TempLocationsModel.GroupID, "0", "6");

            ErrorHandlerModel.ErrorText = $"Der Artikel {TempLocationsModel.ItemName} wurde erfolgreich von der Palette entfernt.";
            ErrorHandlerModel.ErrorType = "SUCCESS";
            ErrorWindow openSuccess = new ErrorWindow();
            openSuccess.ShowDialog();

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
