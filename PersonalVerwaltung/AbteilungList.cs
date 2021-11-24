using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalVerwaltung
{
    public class AbteilungList:List<Abteilung>
    {

        public DbConnector dbConnector = new DbConnector();

        public AbteilungList()
        {
            Refresh();
        }

        public void Refresh()
        {
            string query;

            if (dbConnector.dbConn != null)
            {
                this.Clear();

                query = "SELECT * FROM abteilungen";
                MySqlCommand cmd = new MySqlCommand(query, dbConnector.dbConn);

                dbConnector.dbConn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Abteilung abteilung = new Abteilung()
                    {
                        Abteilungsnr = (int)reader["abteilungsnr"],
                        Bezeichnung = (string)reader["bezeichnung"],
                    };
                    this.Add(abteilung);
                }
                dbConnector.dbConn.Close();
            }

        }

    }
}
