using Checkem.Views;
using System.Windows;

namespace Checkem
{
    public partial class App : Application
    {
        private System.Windows.Forms.NotifyIcon TrayIcon = new System.Windows.Forms.NotifyIcon();

        public int TimeOut { get; set; } = 3000;
        public System.Windows.Forms.ToolTipIcon toolTipIcon { get; set; } = System.Windows.Forms.ToolTipIcon.Info;


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //MainWindow = new MainWindow(this);
            MainWindow = new SettingsWindow();
            MainWindow.Show();

            TrayIcon.Icon = Checkem.Properties.Resources.CheckemIcon;

            TrayIcon.Click += (s, args) => OpenMainWindow();
            TrayIcon.DoubleClick += (s, args) => OpenMainWindow();

            TrayIcon.Visible = true;

            ShowContextMenu();
        }

        private void ShowContextMenu()
        {
            TrayIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();

            TrayIcon.ContextMenuStrip.Items.Add("Check'em").Click += (s, e) => OpenMainWindow();
            TrayIcon.ContextMenuStrip.Items.Add("-");
            TrayIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ShutdownApplication();
        }

        private void OpenMainWindow()
        {
            if (MainWindow != null)
            {
                if (MainWindow.WindowState == WindowState.Minimized)
                {
                    MainWindow.WindowState = WindowState.Maximized;
                    MainWindow.Activate();
                }
            }
            else
            {
                MainWindow = new MainWindow(this);
                MainWindow.Show();
            }
        }

        private void ShutdownApplication()
        {
            TrayIcon.Icon = null;
            TrayIcon.Visible = false;
            Application.Current.Shutdown();
        }

        public void Notify(string title, string message)
        {
            //prevent from displaying empty message string
            if (message == "")
            {
                message = " ";
            }

            //show notification with specified message and title
            TrayIcon.ShowBalloonTip(TimeOut, title, message, toolTipIcon);
        }
    }
}
