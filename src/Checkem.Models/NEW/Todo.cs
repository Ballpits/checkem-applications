using System;
using System.Collections.Generic;

namespace Checkem.Models.NEW
{
    public class Todo
    {
        public int Id { get; set; }


        public string Title { get; set; }
        public string Description { get; set; }


        public bool IsComplteted { get; set; } = false;
        public bool IsStarred { get; set; } = false;


        public List<Reminder> Reminders { get; set; } = new List<Reminder>();
        public List<Tag> Tags { get; set; } = new List<Tag>();


        public DateTime CreationDateTime { get; set; }
    }
}
