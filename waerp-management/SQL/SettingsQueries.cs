using MySqlConnector;
using System.Data;
using System.Windows;
using waerp_management.dbtools;
using waerp_management.errorHandling;

namespace waerp_management.sql
{
    internal class SettingsQueries
    {
        public static MySqlConnection conn = new MySqlConnection(SqlConn.GetConnectionString());

        public static void SyncDataLocations()
        {
            int LocationsNotInDatabase = 0;
            int ItemNotInDatabase = 0;
            bool check = false;
            DataSet dsSync = RunSql("SELECT * FROM syncdatatable");

            AdministrationQueries.RunSqlExec("DELETE FROM item_location_relations");
            AdministrationQueries.RunSqlExec("UPDATE location_objects SET location_quantity = 0");
            AdministrationQueries.RunSqlExec("UPDATE item_objects SET item_quantity_total = 0");



            //DataSet tmp = RunSql("SELECT * FROM item_objects");
            //for (int i = 0; i < tmp.Tables[0].Rows.Count; i++)
            //{
            //    if (tmp.Tables[0].Rows[i]["item_description"].ToString().Contains("|"))
            //    {
            //        if (tmp.Tables[0].Rows[i]["item_description"].ToString().Contains("O-Ring") || tmp.Tables[0].Rows[i]["item_description"].ToString().Contains("Lanzenkopf"))
            //        {
            //            RunSqlExec($"INSERT INTO item_filter_relations(item_id, filter1_id, filter2_id, filter3_id, filter4_id, filter5_id) VALUES ({tmp.Tables[0].Rows[i]["item_id"]}, 0, 0, 0, 0, 0)");
            //        }

            //        string tmpStr = tmp.Tables[0].Rows[i]["item_description"].ToString().Split('|')[0];
            //        string[] tmpStrArr = tmpStr.Split(new char[0]);

            //        if (tmpStrArr.Length > 2)
            //        {
            //            tmpStr = tmpStrArr[0] + " " + tmpStrArr[1];
            //        }
            //        else
            //        {
            //            tmpStr = tmpStrArr[0];
            //        }


            //        DataSet companyData = RunSql($"SELECT * FROM customer_objects WHERE customer_name = '{tmpStr}'");
            //        DataSet filterData = RunSql($"SELECT * FROM filter1_names WHERE name = '{tmpStr}'");

            //        string maxIDCustomer = GetMaxId(RunSql("SELECT * FROM customer_objects"), "customer_id");
            //        string maxIDFilter = GetMaxId(RunSql("SELECT * FROM filter1_names"), "filter_id");

            //        if (companyData.Tables[0].Rows.Count <= 0)
            //        {
            //            RunSqlExec($"INSERT INTO customer_objects(customer_id, customer_company_id, customer_name) VALUES({maxIDCustomer}, '{"000" + maxIDCustomer}', '{tmpStr}')");
            //        }
            //        if (filterData.Tables[0].Rows.Count <= 0)
            //        {
            //            RunSqlExec($"INSERT INTO filter1_names(filter_id, name) VALUES({maxIDFilter}, '{tmpStr}')");
            //        }

            //        RunSqlExec($"INSERT INTO item_filter_relations(item_id, filter1_id, filter2_id, filter3_id, filter4_id, filter5_id) VALUES ({tmp.Tables[0].Rows[i]["item_id"]}, {maxIDFilter}, 0, 0, 0, 0)");
            //    }
            //    else
            //    {
            //        RunSqlExec($"INSERT INTO item_filter_relations(item_id, filter1_id, filter2_id, filter3_id, filter4_id, filter5_id) VALUES ({tmp.Tables[0].Rows[i]["item_id"]}, 0, 0, 0, 0, 0)");
            //    }


            //}

            for (int i = 0; i < dsSync.Tables[0].Rows.Count; i++)
            {

                if (RunSql($"SELECT * FROM location_objects WHERE location_name = '{dsSync.Tables[0].Rows[i]["item_location"]}'").Tables[0].Rows.Count <= 0)
                {
                    RunSql($"INSERT INTO location_objects (location_id, location_name, location_size, location_quantity, item_used, item_constructed)" +
                        $"VALUES (" +
                        $"{GetMaxId(RunSql("SELECT * FROM location_objects"), "location_id")}" +
                        $", " +
                        $"'{dsSync.Tables[0].Rows[i]["item_location"]}'" +
                        $", " +
                        $"''" +
                        $", " +
                        $"0" +
                        $", " +
                        $"0" +
                        $"," +
                        $"0" +
                        $")");
                    LocationsNotInDatabase++;
                }
                if (RunSql($"SELECT * FROM item_objects WHERE item_ident = '{dsSync.Tables[0].Rows[i]["item_ident"]}'").Tables[0].Rows.Count <= 0)
                {
                    check = true;
                    ItemNotInDatabase++;

                }
                if (!check)
                {
                    string MaxIDStr = GetMaxId(RunSql("SELECT * FROM item_location_relations"), "id");



                    RunSqlExec($"UPDATE location_objects SET location_quantity = location_quantity + {dsSync.Tables[0].Rows[i]["item_location_quantity"]} WHERE location_name = '{dsSync.Tables[0].Rows[i]["item_location"]}'");

                    RunSqlExec($"UPDATE item_objects SET item_quantity_total = item_quantity_total + {dsSync.Tables[0].Rows[i]["item_location_quantity"]} WHERE item_ident = '{dsSync.Tables[0].Rows[i]["item_ident"]}'");

                    RunSqlExec($"INSERT INTO item_location_relations (id, item_id, location_id, location_item_quantity)" +
                        $" VALUES(" +
                        $"{MaxIDStr}" +
                        $", " +
                        $"{RunSql($"SELECT * FROM item_objects WHERE item_ident = '{dsSync.Tables[0].Rows[i]["item_ident"]}'").Tables[0].Rows[0]["item_id"]}" +
                        $", " +
                        $"{RunSql($"SELECT * FROM location_objects WHERE location_name = '{dsSync.Tables[0].Rows[i]["item_location"]}'").Tables[0].Rows[0]["location_id"]}" +
                        $", " +
                        $"{dsSync.Tables[0].Rows[i]["item_location_quantity"]}" +
                        $")");
                }






            }
            MessageBox.Show("Fehlender Lagerorte: " + LocationsNotInDatabase.ToString() + "\n" + "Fehlende Artikel: " + ItemNotInDatabase);
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
                MessageBox.Show(ex.Message + " : " + query);
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

                MessageBox.Show(ex.Message + " : " + query);
                ErrorLogger.LogSqlError(ex);

            }
            finally { conn.Close(); }
        }
    }
}
