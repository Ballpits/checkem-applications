using System;

namespace Checkem.Models
{
    public class Todo
    {
        #region Properties
        public int ID { get; set; }


        public string Title { get; set; }
        public string Description { get; set; } = string.Empty;


        public bool IsCompleted { get; set; } = false;
        public bool IsStarred { get; set; } = false;


        public bool IsReminderOn { get; set; } = false;
        public bool IsAdvanceReminderOn { get; set; } = false;


        public DateTime? BeginDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }


        public DateTime CreationDateTime { get; set; }


        public int[] Tags { get; set; }
        #endregion
    }
}
