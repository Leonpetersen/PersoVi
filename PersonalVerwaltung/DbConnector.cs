using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalVerwaltung
{
    public class DbConnector
    {
        public MySqlConnection dbConn { get; set; }

        public DbConnector()
        {
            MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder();
            stringBuilder.Server = "85.13.135.199";
            stringBuilder.UserID = "d0381cb3";
            stringBuilder.Password = "eyXUd6bsRdgZ4mQ2";
            stringBuilder.Database = "d0381cb3";

            string conString = stringBuilder.ToString();

            dbConn = new MySqlConnection(conString);
        }
    }
}
