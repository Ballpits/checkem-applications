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
        public bool IsStarred { get; set; }


        public bool IsReminderOn { get; set; }
        public bool IsAdvanceReminderOn { get; set; }


        public DateTime BeginDateTime { get; set; }
        public DateTime EndDateTime { get; set; }


        public bool IsUsingTag { get; set; }
        public string TagName { get; set; }
        //public Brushes TagColor { get; set; }


        public DateTime CreationDateTime { get; set; }
        #endregion
    }
}
