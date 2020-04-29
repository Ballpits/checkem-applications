using Notifications.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSC
{
    public static class Notifications
    {
        public static void Notify(string title, string messege)
        {
            var notificationManager = new NotificationManager();

            notificationManager.Show(new NotificationContent
            {
                Title = title,
                Message = messege,
                Type = NotificationType.Information
            });
        }

        public static string RandomMessage(string s)
        {
            switch (s)
            {
                case "begin":
                    return BeginMessages[GetRandomMessageId()];
                case "end":
                    return EndMessages[GetRandomMessageId()];
                default:
                    return "";
            }
        }

        private static int GetRandomMessageId()
        {
            Random random = new Random();

            return random.Next(0, 5);
        }

        private static string[] BeginMessages = new string[5] { "It's time to begin", "m2", "m3", "m4", "m5" };

        private static string[] EndMessages = new string[5] { "Time to wrap things up", "I hope you've finished this already,or at least on time", "You done ? Cause time's up", "Finally !", "m5" };
    }
}
