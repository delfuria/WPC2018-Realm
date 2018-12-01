using System;
using System.Collections.Generic;
using System.Text;
using Realms;

namespace RealmModel
{
    public class Book : RealmObject
    {
        public string Title { get; set; }
        public Author Author { get; set; }
    }

    public class Author : RealmObject
    {
        public string Name { get; set; }
        public IList<Book> Books { get; }
    }
}
