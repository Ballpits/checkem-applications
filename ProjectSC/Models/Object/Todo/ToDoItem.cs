using System;

namespace ProjectSC.Models.ToDo
{
    public class ToDoItem
    {
        #region Properties
        public int Id { get; set; }


        public string Title { get; set; }
        public string Description { get; set; } = string.Empty;


        public bool IsCompleted { get; set; } = false;

        public bool IsStarred { get; set; } = false;


        public bool IsReminderOn { get; set; } = false;
        public bool IsAdvanceReminderOn { get; set; } = false;


        public DateTime? BeginDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }


        public bool IsUsingTag { get; set; } = false;
        public string TagText { get; set; } = string.Empty;

        public int? TagId { get; set; }

        public DateTime CreationDateTime { get; set; }
        #endregion
    }
}
