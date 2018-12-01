using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;
namespace XamarinForms_CRUD.Models
{
    public class Student:RealmObject
    {
        [PrimaryKey]
        public int StudentID { get; set; }
        public string StudentName { get; set; }
    }
}
