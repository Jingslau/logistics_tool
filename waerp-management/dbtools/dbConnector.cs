using MySqlConnector;
using System.Collections.Generic;

namespace waerp_management.dbtools
{
    internal class dbConnector
    {
        public static MySqlConnection conn = new MySqlConnection(SqlConn.GetConnectionString());

        public static bool CheckTables()
        {
            List<string> dataTables = new List<string>();

            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SHOW TABLES";

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    dataTables.Add(reader.GetString(0));
                }
            }
            return true;
        }
        public static void InitEmptyDB()
        {
            //  string sqlQueries = File.ReadAllText("dbtools\\update\\0");
        }
    }
}
