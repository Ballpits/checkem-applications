using MaterialDesignThemes.Wpf;
using System;

namespace ProjectSC.ViewModels
{
    public static class SnackbarControl
    {
        public static Snackbar OpenSnackBar(string content)
        {
            SnackbarMessageQueue messageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(1));
            messageQueue.Enqueue(content);

            SnackbarMessage snackbarMessage = new SnackbarMessage
            {
                Content = content,
            };
            Snackbar snackbar = new Snackbar
            {
                Message = snackbarMessage,
                IsActive = true,
                MessageQueue = messageQueue,
            };

            return snackbar;
        }
    }
}
