using Realms;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace RealmModel
{
    public class Building : RealmObject
    {
        public string Address { get; set; }
        public Family Owner { get; set; }
    }

    public class Family : RealmObject
    {
        public string LastName { get; set; }
        public Building House { get; set; }
    }
}
