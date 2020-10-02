using ProjectSC.ViewModels.GlobleHotek;
using ProjectSC.Views.QuickView;
using System;
using System.Windows;
using System.Windows.Input;

namespace ProjectSC
{
    public partial class App : Application
    {
        private System.Windows.Forms.NotifyIcon TrayIcon;

        private HotKey hotKey;


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow = new MainWindow();
            MainWindow.Show();

            TrayIcon = new System.Windows.Forms.NotifyIcon();
            TrayIcon.DoubleClick += (s, args) => OpenMainWindow();
            TrayIcon.Icon = ProjectSC.Properties.Resources.Icon_16;
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

        public bool IsQuickViewWindowOpen = false;
        private void OpenQuickViewWindow()
        {
            if (!IsQuickViewWindowOpen)
            {
                QuickViewWindow quickViewWindow = new QuickViewWindow(this);
                quickViewWindow.Show();

                IsQuickViewWindowOpen = true;
            }
        }

        private void ShutdownApplication()
        {
            TrayIcon.Icon = null;
            TrayIcon.Visible = false;
            Application.Current.Shutdown();
        }


        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            RegisterHotKeys();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            UnregisterHotKeys();
        }

        private void RegisterHotKeys()
        {
            if (hotKey != null) return;

            hotKey = new HotKey(ModifierKeys.Alt, Key.C, Current.MainWindow);
            hotKey.HotKeyPressed += OnHotKeyPressed;
        }

        private void UnregisterHotKeys()
        {
            if (hotKey == null) return;

            hotKey.HotKeyPressed -= OnHotKeyPressed;
            hotKey.Dispose();
        }

        private void OnHotKeyPressed(HotKey hotKey)
        {
            OpenQuickViewWindow();
        }
    }
}
