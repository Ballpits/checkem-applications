using Checkem.Assets.LanguageHelper;
using Checkem.Assets.ThemeHelper;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Checkem.Views
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;

            NotificationwToggle_StateChanged(NotificationwToggle, new EventArgs());
        }

        MainWindow mainWindow;

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SettingToggles_StateChanged(object sender, EventArgs e)
        {
            Windows.Properties.Settings.Default.Save();
        }

        #region General settings
        private void ReverseButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.Properties.Settings.Default.Reset();

            EnableNotificationSettingVarients();
        }

        private void LaunchOnStarup_Checked(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                Assembly curAssembly = Assembly.GetExecutingAssembly();
                key.SetValue(curAssembly.GetName().Name, curAssembly.Location);
            }
            catch { }
        }

        private void LaunchOnStarup_Unchecked(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                Assembly curAssembly = Assembly.GetExecutingAssembly();
                key.SetValue(curAssembly.GetName().Name, curAssembly.Location + "_");
            }
            catch { }
        }
        #endregion

        #region Notification settings
        private void NotificationwToggle_StateChanged(object sender, EventArgs e)
        {
            if (NotificationwToggle.IsChecked)
            {
                EnableNotificationSettingVarients();
            }
            else
            {
                DisableNotificationSettingVarients();
            }

            Windows.Properties.Settings.Default.Save();
        }

        private void EnableNotificationSettingVarients()
        {
            ShowDetailsToggle.IsEnabled = true;
            VacationModeToggle.IsEnabled = true;
        }

        private void DisableNotificationSettingVarients()
        {
            ShowDetailsToggle.IsEnabled = false;
            VacationModeToggle.IsEnabled = false;

            ShowDetailsToggle.IsChecked = false;
            VacationModeToggle.IsChecked = false;
        }
        #endregion

        private void LanguageCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LanguageHelper.ApplyLanguage((sender as ComboBox).SelectedIndex);
        }

        private void ThemeCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ThemeHelper.ApplyTheme((sender as ComboBox).SelectedIndex);
        }

        private void DarkModeToggle_Checked(object sender, EventArgs e)
        {
            ThemeHelper.ApplyDarkMode();
        }

        private void DarkModeToggle_Unchecked(object sender, EventArgs e)
        {
            ThemeHelper.ApplyLightMode();

        }
    }
}
