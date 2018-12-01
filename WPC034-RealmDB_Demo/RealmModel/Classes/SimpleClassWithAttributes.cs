using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealmModel
{
    public class Employee : RealmObject
    {
        [PrimaryKey]
        public string EmployeeID { get; set; }
        [Required]
        public string Name { get; set; }
        public string PhotoPath { get; set; }
        [Ignored]
        public Byte[] Photo { get; set; }
        [Indexed]
        public DateTimeOffset BirthDate { get; set; }

        [MapTo("Email")]
        private string _email { get; set; }
        public string Email
        {
            get { return _email ?? "NA"; }
            set { _email = value; }
        }
    }
}
