using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalVerwaltung
{
    public class Employee
    {
        public int Employeenr { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Street { get; set; }
        public int Zipcode { get; set; }
        public string Email { get; set; }
        public int Departmentnr { get; set; }
        public string Phonenumber { get; set; }
        public string Maritalstatus { get; set; }
        public int Professionnr { get; set; }
        public DateTime Entrydate { get; set; }
        

        public bool createEmployee()
        {
            DbConnector dbConnector = new DbConnector();
            string query;

            query = "INSERT INTO mitarbeiter (vorname, nachname, strasse, plz, familienstand, telefon, email, abteilungsnr, berufsnr, eintritt)" +
                            "VALUES (@vor, @nach, @strasse, @plz, @fam, @tel, @email, @abt, @ber, @ein)";

            MySqlCommand insert = new MySqlCommand(query, dbConnector.dbConn);
            insert.Parameters.AddWithValue("@vor", this.Firstname);
            insert.Parameters.AddWithValue("@nach", this.Lastname);
            insert.Parameters.AddWithValue("@strasse", this.Street);
            insert.Parameters.AddWithValue("@plz", this.Zipcode);
            insert.Parameters.AddWithValue("@fam", this.Maritalstatus);
            insert.Parameters.AddWithValue("@tel", this.Phonenumber);
            insert.Parameters.AddWithValue("@email", this.Email);
            insert.Parameters.AddWithValue("@abt", this.Departmentnr);
            insert.Parameters.AddWithValue("@ber", this.Professionnr);
            insert.Parameters.AddWithValue("@ein", this.Entrydate);

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

        public bool deleteEmployee()
        {
            DbConnector dbConnector = new DbConnector();

            if (dbConnector.dbConn != null)
            {

                string delete = "DELETE FROM mitarbeiter WHERE personalnr = @pnr";

                MySqlCommand delete_cmd = new MySqlCommand(delete, dbConnector.dbConn);

                delete_cmd.Parameters.AddWithValue("@pnr", this.Employeenr);

                dbConnector.dbConn.Open();
                int status = delete_cmd.ExecuteNonQuery();
                dbConnector.dbConn.Close();

                if (status > 0) //Delete erfolgreich
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }
    }
}
