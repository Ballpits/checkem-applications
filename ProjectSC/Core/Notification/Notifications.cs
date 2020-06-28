using Notifications.Wpf;
using System;

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

        public static string RandomMessage(string msgType)
        {
            switch (msgType)
            {
                case "passed":
                    return BeginMessages[RandomMessageId()];
                case "begin":
                    return BeginMessages[RandomMessageId()];
                case "end":
                    return EndMessages[RandomMessageId()];
                default:
                    return "";
            }
        }

        private static int RandomMessageId()
        {
            Random random = new Random();

            return random.Next(0, 5);
        }

        private static string[] BeginMessages = new string[5] { "It's time to begin", "m2", "m3", "m4", "m5" };

        private static string[] EndMessages = new string[5] { "Time to wrap things up", "I hope you've finished this already,or at least on time", "You done ? Cause time's up", "Finally !", "m5" };
    }
}
