using System;
using Realms;

namespace XamarinForms_MVVM.Models
{
    public class Todo: RealmObject
    {
        public string Text { get; set; }
        public DateTimeOffset Date { get; set; }

    }
}
