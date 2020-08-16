using System;
using System.Windows;
using System.Windows.Input;

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

            ShowContextMenu();
        }

        private void ShowContextMenu()
        {
            TrayIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();

            TrayIcon.ContextMenuStrip.Items.Add("Check'em").Click += (s, e) => OpenMainWindow();
            TrayIcon.ContextMenuStrip.Items.Add("Preferences...").Click += (s, e) => OpenPrefWindow();
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
                UserPrefenceWindow userPrefenceWindow = new UserPrefenceWindow();
                userPrefenceWindow.Show();
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
            TrayIcon.Visible = false;
            Application.Current.Shutdown();
        }

        private HotKey _hotKey;

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
            if (_hotKey != null) return;

            _hotKey = new HotKey(ModifierKeys.Alt, Key.C, Current.MainWindow);
            _hotKey.HotKeyPressed += OnHotKeyPressed;
        }

        private void UnregisterHotKeys()
        {
            if (_hotKey == null) return;

            _hotKey.HotKeyPressed -= OnHotKeyPressed;
            _hotKey.Dispose();
        }

        private void OnHotKeyPressed(HotKey hotKey)
        {
            OpenQuickViewWindow();
        }
    }
}
