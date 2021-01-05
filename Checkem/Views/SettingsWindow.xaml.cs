using Checkem.Assets.LanguageHelper;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Checkem.Views
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            NotificationwToggle_StateChanged(NotificationwToggle, new EventArgs());
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SettingToggles_StateChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        #region General settings
        private void ReverseButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Reset();
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
                ShowDetailsToggle.IsEnabled = true;
                VacationModeToggle.IsEnabled = true;
            }
            else
            {
                ShowDetailsToggle.IsEnabled = false;
                VacationModeToggle.IsEnabled = false;

                ShowDetailsToggle.IsChecked = false;
                VacationModeToggle.IsChecked = false;
            }

            Properties.Settings.Default.Save();
        }
        #endregion

        private void LanguageCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LanguageHelper.ApplyLanguage((sender as ComboBox).SelectedIndex);
        }
    }
}
