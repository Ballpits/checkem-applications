using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NotificationwToggle_StateChanged(object sender, EventArgs e)
        {
            if (NotificationwToggle.IsChecked)
            {
                ShowDetailsToggle.IsEnabled = true;
                PlaySoundToggle.IsEnabled = true;
                VacationModeToggle.IsEnabled = true;
            }
            else
            {
                ShowDetailsToggle.IsEnabled = false;
                PlaySoundToggle.IsEnabled = false;
                VacationModeToggle.IsEnabled = false;

                ShowDetailsToggle.IsChecked = false;
                PlaySoundToggle.IsChecked = false;
                VacationModeToggle.IsChecked = false;
            }

            Properties.Settings.Default.Save();
        }

        private void ShowDetailsToggle_StateChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void PlaySoundToggle_StateChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void VacationModeToggle_StateChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
