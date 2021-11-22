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
        public int Phonenumber { get; set; }
        public string Maritalstatus { get; set; }
        public int Professionnr { get; set; }
        public DateTime Entrydate { get; set; }

        public Employee(string firstname, string lastname, string street, int zipcode, string email, int departmentnr, int phonenumber, string maritalstatus, int professionnr, DateTime entrydate)
        {
            Firstname = firstname;
            Lastname = lastname;
            Street = street;
            Zipcode = zipcode;
            Email = email;
            Departmentnr = departmentnr;
            Phonenumber = phonenumber;
            Maritalstatus = maritalstatus;
            Professionnr = professionnr;
            Entrydate = entrydate;
        }

        public bool createEmployee()
        {
            return true;
        }
    }
}
