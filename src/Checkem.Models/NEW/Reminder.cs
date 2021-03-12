using Checkem.Models.NEW.Enums;
using System;

namespace Checkem.Models.NEW
{
    public class Reminder
    {
        public int Id { get; set; }


        public string Name { get; set; }


        public DateTime ReminderDateTime { get; set; }


        public RepeatType Repeat { get; set; } = RepeatType.Never;
    }
}
