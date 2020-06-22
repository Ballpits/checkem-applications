using System;

namespace ProjectSC
{
    public class ToDoItem
    {
        #region Properties
        public int Id { get; set; }


        public string Title { get; set; }
        public string Description { get; set; }


        public bool IsCompleted { get; set; }
        public bool IsImportant { get; set; }


        public bool IsReminderOn { get; set; }
        public bool IsAdvRemider { get; set; }
        public int NotifyType { get; set; }


        public DateTime BeginDateTime { get; set; }
        public DateTime EndDateTime { get; set; }


        public DateTime CreationDateTime { get; set; }
        #endregion
    }
}
