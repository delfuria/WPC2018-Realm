using System;
using Realms;

namespace RealmModel
{
    public class People : RealmObject
    {
        public string FirstName { get; set; }
        public int BirthYear { get; set; }
    }
}
