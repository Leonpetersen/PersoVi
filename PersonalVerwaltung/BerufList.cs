using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalVerwaltung
{
    public class BerufList:List<Beruf>
    {
        public DbConnector dbConnector = new DbConnector();
        public BerufList()
        {
            Refresh();
        }

        public void Refresh()
        {
            string query;

            if (dbConnector.dbConn != null)
            {
                this.Clear();

                query = "SELECT * FROM berufe";
                MySqlCommand cmd = new MySqlCommand(query, dbConnector.dbConn);

                dbConnector.dbConn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Beruf beruf = new Beruf()
                    {
                        Berufsnummer = (int)reader["berufsnr"],
                        Bezeichnung = (string)reader["bezeichnung"],
                    };
                    this.Add(beruf);
                }
                dbConnector.dbConn.Close();
            }

        }
    }
}
