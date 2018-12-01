using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RealmModel;
using Realms;
using Realms.Sync;
using Task = System.Threading.Tasks.Task;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo1DefaultConfig();
            Demo2CustomConfig();
            Demo3Query();
            Demo4Relation11();
            Demo5Relation1N();
            Demo6Relation1NBackLink();
            Demo7Encryption();
            Demo8MultiThread();
            Demo9Notification();
            Demo10RealmObjectServer();
            Console.WriteLine("Fine Demo");
            Console.ReadLine();
        }
        static async void Demo10RealmObjectServer()
        {

            try
            {
                var authURL = new Uri("https://wpc2018.de1a.cloud.realm.io");

                var credentials = Credentials.UsernamePassword("wpc", "2018", false);

                var user = await User.LoginAsync(credentials, authURL);

                var config = new SyncConfiguration(user, new Uri($"realms://wpc2018.de1a.cloud.realm.io/WPCDEMO"))
                {
                    ObjectClasses = new[] { typeof(RealmModel.Task), typeof(TaskList), typeof(TaskListList) }
                };
                var realm = Realm.GetInstance(config);

                RealmModel.Task t = new RealmModel.Task() { Title = "nuovo" };
                realm.Write(() => realm.Add(t));

            }
            catch (Exception ex)
            {
            }
        }
        static void Demo9Notification()
        {
            var config = new RealmConfiguration("Demo9Notification.realm");
            config.ObjectClasses = new[] { typeof(People) };
            Realm.DeleteRealm(config);
            var realm = Realm.GetInstance(config);

            var person = new People();
            realm.Write(() => realm.Add(person));
            person.PropertyChanged += (sender, e) =>
            {
                Console.WriteLine($"New value set for {e.PropertyName}");
            };
            realm.Write(() => person.FirstName = "Stefano"); // => "New value set for Balance"
        }
        static void Demo8MultiThread()
        {
            var config = new RealmConfiguration("Demo8MultiThread.realm");
            config.ObjectClasses = new[] { typeof(People) };
            Realm.DeleteRealm(config);
            var realm = Realm.GetInstance(config);


            var persona = new People() { FirstName = "Riccardo" };

            realm.Write(() => realm.Add(persona));

            var referencePersona = ThreadSafeReference.Create(persona);

            Task.Run(() =>
            {
                // Da errore !!!
                //realm.Write(() =>
                //{
                //    persona.FirstName = "Marco";
                //});


                var altroRealm = Realm.GetInstance(realm.Config);
                var altraPersona = altroRealm.ResolveReference(referencePersona);
                if (altraPersona == null)
                {
                    return; // person was deleted
                }

                altroRealm.Write(() =>
                {
                    altraPersona.FirstName = "Marco";
                });
            });
        }
        static void Demo7Encryption()
        {
            string key = "Questa è una chiave composta da 64Bytes proprio come vuole Realm";


            // Specifica le classi da persistere e la configurazione
            var config = new RealmConfiguration("Demo7Encryption.realm");
            config.ObjectClasses = new[] { typeof(Employee) };
            config.EncryptionKey = Encoding.ASCII.GetBytes(key);
            Realm.DeleteRealm(config);
            var realm = Realm.GetInstance(config);


            Employee emp = new Employee();
            emp.Name = "Stefano";
            emp.BirthDate = DateTimeOffset.Parse("28/10/1966");
            emp.Email = "delfuria@gmail.com";
            emp.Photo = null;
            emp.PhotoPath = @"c:\Foto.jpg";

            realm.Write(() => realm.Add(emp));

        }
        static void Demo6Relation1NBackLink()
        {
            var config = new RealmConfiguration("Demo6Relation1NBackLink.realm");
            config.ObjectClasses = new[] { typeof(Captain), typeof(Ship) };
            Realm.DeleteRealm(config);
            var realm = Realm.GetInstance(config);

            Captain df = new Captain() { FirstName = "Stefano" };
            Ship s1 = new Ship() { Name = "Amerigo" };
            Ship s2 = new Ship() { Name = "Ventosa" };

            s1.Captain = df;
            s2.Captain = df;

            realm.Write(() =>
            {
                realm.Add(df);
                realm.Add(s1);
                realm.Add(s2);
            });

            Captain fr = new Captain() { FirstName = "Roby" };
            Ship s3 = new Ship() { Name = "Child" };

            s3.Captain = fr;

            realm.Write(() =>
            {
                realm.Add(fr);
                realm.Add(s3);
            });
        }
        static void Demo5Relation1N()
        {
            var config = new RealmConfiguration("Demo5Relation1N.realm");
            config.ObjectClasses = new[] { typeof(Book), typeof(Author) };
            Realm.DeleteRealm(config);
            var realm = Realm.GetInstance(config);

            Author df = new Author() { Name = "DelFuria" };
            Book b1 = new Book() { Title = "Programmare C#" };
            Book b2 = new Book() { Title = "Realm DB" };

            df.Books.Add(b1);
            df.Books.Add(b2);
            b1.Author = df;
            b2.Author = df;

            realm.Write(() =>
            {
                realm.Add(df);
                realm.Add(b1);
                realm.Add(b2);
            });

            Author fr = new Author() { Name = "Freato" };
            Book b3 = new Book() { Title = ".NET" };

            fr.Books.Add(b3);

            realm.Write(() =>
            {
                realm.Add(fr);
                realm.Add(b3);
            });



        }
        static void Demo4Relation11()
        {
            var config = new RealmConfiguration("Demo4Relation11.realm");
            config.ObjectClasses = new[] { typeof(Building), typeof(Family) };
            Realm.DeleteRealm(config);
            var realm = Realm.GetInstance(config);

            Family df = new Family() { LastName = "DelFuria" };
            Building h1 = new Building() { Address = "P. Matteotti" };

            df.House = h1;
            h1.Owner = df;

            realm.Write(() =>
            {
                realm.Add(df);
                realm.Add(h1);
            });

            Family gu = new Family() { LastName = "Gustinicchi" };
            Building h11 = new Building() { Address = "Corso VE" };

            gu.House = h11;
            h11.Owner = gu;

            realm.Write(() =>
            {
                realm.Add(gu);
                realm.Add(h11);
            });

            Family br = new Family() { LastName = "Bricca" };
            Building h12 = new Building() { Address = "Corso Italia" };

            br.House = h12;

            realm.Write(() =>
            {
                realm.Add(br);
                realm.Add(h12);
            });

        }
        static void Demo3Query()
        {
            // Specifica le classi da persistere e la configurazione
            var config = new RealmConfiguration("Demo3Query.realm");
            config.ObjectClasses = new[] { typeof(People) };
            Realm.DeleteRealm(config);
            var realm = Realm.GetInstance(config);

            People man = new People() { FirstName = "Stefano", Age = 52 };

            People woman = new People() { FirstName = "Vale", Age = 43 };

            People friend = new People() { FirstName = "Riccardo", Age = 51 };

            using (var trans = realm.BeginWrite())
            {
                realm.Add(man);
                realm.Add(woman);
                realm.Add(friend);
                trans.Commit();
            }


            Console.WriteLine("All");
            var all = realm.All<People>();
            foreach (var people in all)
            {
                Console.WriteLine(people.FirstName);
            }
            Console.WriteLine("-----------");

            Console.WriteLine("> 50");
            var more50 = realm.All<People>().Where(p => p.Age > 50);
            foreach (var people in more50)
            {
                Console.WriteLine(people.FirstName);
            }
            Console.WriteLine("-----------");

            Console.WriteLine("> 50");
            var more50Orderd = realm.All<People>().Where(p => p.Age > 50).OrderBy(p => p.Age);
            foreach (var people in more50Orderd)
            {
                Console.WriteLine(people.FirstName);
            }
            Console.WriteLine("-----------");

        }
        static void Demo2CustomConfig()
        {
            // Specifica le classi da persistere e la configurazione
            var config = new RealmConfiguration("Demo2CustomConfig.realm")
                { SchemaVersion = 1 };
            config.ObjectClasses = new[] { typeof(People) };
            Realm.DeleteRealm(config);
            var realm = Realm.GetInstance(config);

            People man = new People() { FirstName = "Stefano", Age = 52 };
            realm.Write(() => realm.Add(man));

            People woman = new People() { FirstName = "Vale", Age = 43 };

            using (var trans = realm.BeginWrite())
            {
                realm.Add(woman);
                trans.Commit();
            }

            using (var trans = realm.BeginWrite())
            {
                woman.FirstName = "Valentina";
                trans.Commit();
            }

            using (var trans = realm.BeginWrite())
            {
                realm.Remove(man);
                trans.Commit();
            }

        }
        static void Demo1DefaultConfig()
        {
            var realm = Realm.GetInstance();

            People man = new People() { FirstName = "Stefano", Age = 52 };
            realm.Write(() => realm.Add(man));

            People woman = new People() { FirstName = "Vale", Age = 43 };

            using (var trans = realm.BeginWrite())
            {
                realm.Add(woman);
                trans.Commit();
            }

            using (var trans = realm.BeginWrite())
            {
                woman.FirstName = "Valentina";
                trans.Commit();
            }

            using (var trans = realm.BeginWrite())
            {
                realm.Remove(man);
                trans.Commit();
            }
            realm.Dispose();
        }

    }
}
