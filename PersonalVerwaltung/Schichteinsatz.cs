using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalVerwaltung
{
    public class Schichteinsatz
    {
        public int Schichtnr { get; set; }
        public int Personalnr { get; set; }
        public string Name { get; set; }
        public DateTime Beginn { get; set; }
        public DateTime Ende { get; set; }
        public string Art { get; set; }

        public static bool createShiftEntry(int employeenr, int shiftnr)
        {
            DbConnector dbConnector = new DbConnector();
            string query;

            query = "INSERT INTO schichteinsatz (schichtnr, personalnr)" +
                            "VALUES (@shift, @pnr)";

            MySqlCommand insert = new MySqlCommand(query, dbConnector.dbConn);
            insert.Parameters.AddWithValue("@shift", shiftnr);
            insert.Parameters.AddWithValue("@pnr", employeenr);
      

            dbConnector.dbConn.Open();
            int status = insert.ExecuteNonQuery();
            dbConnector.dbConn.Close();

            if (status > 0) //Insert erfolgreich
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int getShiftNr(DateTime? date, string type)
        {
            DbConnector dbConnector = new DbConnector();
            int shiftNr = 0;
            string query = "";

            MySqlCommand query_cmd = new MySqlCommand(query, dbConnector.dbConn);

            if (dbConnector.dbConn != null)
            {
                query = "SELECT schichtnr FROM schichten WHERE art = @art AND beginn = @beg LIMIT 1";
                query_cmd.CommandText = query;
                query_cmd.Parameters.AddWithValue("@beg", date);
                query_cmd.Parameters.AddWithValue("@art", type);


                dbConnector.dbConn.Open();

                MySqlDataReader reader = query_cmd.ExecuteReader();

                while (reader.Read())
                {
                    shiftNr = (int)reader["schichtnr"];
                }
                dbConnector.dbConn.Close();
                
            }
            return shiftNr;
        }

    }
}
