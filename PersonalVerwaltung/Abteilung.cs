using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalVerwaltung
{
    public class Abteilung
    {
        public int Abteilungsnr { get; set; }
        public string Bezeichnung { get; set; }
        public string Leitung { get; set; }

        public Abteilung()
        {

        }

        public Abteilung(int abteilungsnr)
        {
            DbConnector dbConnector = new DbConnector();


            string query = "SELECT * FROM abteilungen WHERE abteilungsnr = @nr";

            MySqlCommand cmd = new MySqlCommand(query, dbConnector.dbConn);
            cmd.CommandText = query;

            cmd.Parameters.AddWithValue("@nr", abteilungsnr);

            dbConnector.dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            this.Abteilungsnr = (int)reader["abteilungsnr"];
            this.Bezeichnung = (string)reader["bezeichnung"];
            if (!reader.IsDBNull(reader.GetOrdinal("leitung")))
            {
                this.Leitung = (string)reader["leitung"];
            } 
            

            dbConnector.dbConn.Close();

        }
    }
}
