using System.Collections.Generic;
using Realms;

namespace XamarinForms_SyncTask
{
    public class TaskListList : RealmObject
    {
        [PrimaryKey]
        [MapTo("id")]
        public int Id { get; set; }

        [MapTo("items")]
        public IList<TaskList> Items { get; }
    }
}
