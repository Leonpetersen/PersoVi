using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PersonalVerwaltung
{
    public static class LoginHandler
    {
        
        public static bool checkCredentials(string user, string pw)
        {
            DbConnector dbConnector = new DbConnector();

            //Passwort in SHA1 umwandeln
            byte[] daten = Encoding.ASCII.GetBytes(pw);
            byte[] hashdaten = new SHA1Managed().ComputeHash(daten);

            string hash = string.Empty;
            foreach (var eintrag in hashdaten)
            {
                hash += eintrag.ToString("X2");
            }

            string count = "SELECT COUNT(benutzername) FROM zugangsdaten WHERE benutzername = @user AND passwort = @pw";
            MySqlCommand count_cmd = new MySqlCommand(count, dbConnector.dbConn);
            count_cmd.Parameters.AddWithValue("@user", user);
            count_cmd.Parameters.AddWithValue("@pw", hash);

            dbConnector.dbConn.Open();
            int i = (int)(long)count_cmd.ExecuteScalar();
            
            dbConnector.dbConn.Close();

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
