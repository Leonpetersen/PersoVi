using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalVerwaltung
{
    public class Ort
    {
        public string Land { get; set; }
        public int Plz { get; set; }
        public string Ortsname { get; set; }
        public string LandPlz { get; set; }

        public Ort()
        {
        }

        public Ort(string land, int plz)
        {
            DbConnector dbConnector = new DbConnector();


            string query = "SELECT * FROM orte WHERE land = @land AND plz = @plz LIMIT 1";

            MySqlCommand cmd = new MySqlCommand(query, dbConnector.dbConn);
            cmd.CommandText = query;

            cmd.Parameters.AddWithValue("@land", land);
            cmd.Parameters.AddWithValue("@plz", plz);

            dbConnector.dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            this.Land = (string)reader["land"];
            this.Plz = (int)reader["plz"];
            this.Ortsname = (string)reader["ort"];
            this.LandPlz = (string)reader["land"] + " | " + (int)reader["plz"];


            dbConnector.dbConn.Close();

        }
    }
}
