using System.Windows;

namespace ProjectSC
{
    public partial class App : Application
    {
        private System.Windows.Forms.NotifyIcon TrayIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow = new MainWindow();

            TrayIcon = new System.Windows.Forms.NotifyIcon();
            TrayIcon.DoubleClick += (s, args) => OpenMainWindow();
            TrayIcon.Icon = ProjectSC.Properties.Resources.Icon_16;
            TrayIcon.Visible = true;

            CreateContextMenu();
        }

        private void CreateContextMenu()
        {
            TrayIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();

            TrayIcon.ContextMenuStrip.Items.Add("Check'em").Click += (s, e) => OpenMainWindow();
            //TrayIcon.ContextMenuStrip.Items.Add("Preferences...").Click += (s, e) => OpenPrefWindow();
            TrayIcon.ContextMenuStrip.Items.Add("-");
            TrayIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ShutdownApplication();
        }

        private void OpenMainWindow()
        {
            if (MainWindow.IsVisible)
            {
                if (MainWindow.WindowState == WindowState.Minimized)
                {
                    MainWindow.WindowState = WindowState.Normal;
                }
                MainWindow.Activate();
            }
            else
            {
                MainWindow.Show();
            }
        }

        private void OpenPrefWindow()
        {
            if (MainWindow.IsVisible)
            {
                if (MainWindow.WindowState == WindowState.Minimized)
                {
                    MainWindow.WindowState = WindowState.Normal;
                }
                MainWindow.Activate();
            }
            else
            {
                MainWindow.Show();
            }
        }

        private void ShutdownApplication()
        {
            TrayIcon.Visible = false;
            Application.Current.Shutdown();
        }
    }
}
