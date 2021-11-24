using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalVerwaltung
{
    public class OrtList:List<Ort>
    {
        public DbConnector dbConnector = new DbConnector();

        public OrtList()
        {
            Refresh();
        }

        public void Refresh()
        {
            string query;

            if (dbConnector.dbConn != null)
            {
                this.Clear();

                query = "SELECT * FROM orte";
                MySqlCommand cmd = new MySqlCommand(query, dbConnector.dbConn);

                dbConnector.dbConn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Ort ort = new Ort()
                    {
                        Land = (string)reader["land"],
                        Plz = (int)reader["plz"],
                        Ortsname = (string)reader["ort"],
                        LandPlz = (string)reader["land"] + " | " + (int)reader["plz"]
                    };
                    this.Add(ort);
                }
                dbConnector.dbConn.Close();
            }
            
        }
    }
}
