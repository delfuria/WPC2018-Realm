using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmModel;
using Realms;

namespace ConsoleDemoMigrations
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo2CustomConfigMigrations();
            Console.WriteLine("Fine Demo");
            Console.ReadLine();
        }

        static void Demo2CustomConfigMigrations()
        {
            // Specifica le classi da persistere e la configurazione
            var config = new RealmConfiguration("Demo2CustomConfig.realm")
            {
                SchemaVersion = 2,
                MigrationCallback = (migration, oldSchemaVersion) =>
                {
                    var newPeople = migration.NewRealm.All<People>();

                    // Si utilizzano le dynamic API per oldPeople per accedere a Age 
                    var oldPeople = migration.OldRealm.All("People");

                    for (var i = 0; i < newPeople.Count(); i++)
                    {
                        var oldPerson = oldPeople.ElementAt(i);
                        var newPerson = newPeople.ElementAt(i);

                        newPerson.BirthYear = 2018 - (int)oldPerson.Age;
                    }
                }
            };

            config.ObjectClasses = new[] { typeof(People) };
            var realm = Realm.GetInstance(config);

        }

    }
}
