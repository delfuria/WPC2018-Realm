﻿using System.Collections.Generic;
using Realms;

namespace RealmModel
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
