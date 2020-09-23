using System;

namespace ProjectSC
{
    public class TimeRecord
    {
        public string Title { get; set; }


        public bool IsReminderOn { get; set; }
        public bool IsAdvanceOn { get; set; }


        public DateTime BeginDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
