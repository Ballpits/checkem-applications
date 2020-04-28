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
    }
}
