using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void SettingToggles_StateChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

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
    }
}
