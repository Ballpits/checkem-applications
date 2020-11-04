using System.IO;
using System.Windows;

namespace Checkem
{
    public partial class App : Application
    {
        private System.Windows.Forms.NotifyIcon TrayIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow = new MainWindow();
            MainWindow.Show();

            TrayIcon = new System.Windows.Forms.NotifyIcon();
            TrayIcon.DoubleClick += (s, args) => OpenMainWindow();
            MemoryStream ms = new MemoryStream(Checkem.Properties.Resources.CheckemIcon);
            TrayIcon.Icon = new System.Drawing.Icon(ms);
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
                MainWindow = new MainWindow();
                MainWindow.Show();
            }
        }

        private void ShutdownApplication()
        {
            TrayIcon.Icon = null;
            TrayIcon.Visible = false;
            Application.Current.Shutdown();
        }
    }
}
