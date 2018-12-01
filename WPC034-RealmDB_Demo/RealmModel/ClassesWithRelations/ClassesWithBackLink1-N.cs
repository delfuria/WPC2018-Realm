using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Realms;

namespace RealmModel
{
    public class Ship : RealmObject
    {
        public string Name { get; set; }
        public Captain Captain { get; set; }
    }

    public class Captain : RealmObject
    {
        public string FirstName { get; set; }
        [Backlink(nameof(Ship.Captain))]
        public IQueryable<Ship> Ships { get; }

        // Equivalente al BackLink
        public IQueryable<Ship> ShipsLink
        {
            get
            {
                return this.Realm.All<Ship>().Where(s => s.Captain.Equals(this));
            }
        }
    }
}
