﻿using Realms;

namespace RealmModel
{
    public class Task : RealmObject, ICompletable
    {
        [MapTo("text")]
        [Required]
        public string Title { get; set; } = string.Empty;

        [MapTo("completed")]
        public bool IsCompleted { get; set; }
    }
}