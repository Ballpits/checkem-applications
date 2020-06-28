using MaterialDesignThemes.Wpf;
using System;

namespace ProjectSC
{
    public static class SnackbarControl
    {
        public static Snackbar OpenSnackBar(string content)
        {
            SnackbarMessageQueue msgQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(1));
            msgQueue.Enqueue(content);

            SnackbarMessage snackbarMessage = new SnackbarMessage
            {
                Content = content,
            };
            Snackbar snackbar = new Snackbar
            {
                Message = snackbarMessage,
                IsActive = true,
                MessageQueue = msgQueue
            };

            return snackbar;
        }
    }
}
