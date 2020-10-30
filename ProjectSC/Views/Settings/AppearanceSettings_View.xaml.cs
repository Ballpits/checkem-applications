using ProjectSC.ViewModels.AppearanceSettings;
using ProjectSC.Views.Theme;
using System.Windows;
using System.Windows.Controls;

namespace ProjectSC.Views.Settings
{
    public partial class AppearanceSettings_View : UserControl
    {
        public AppearanceSettings_View()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ThemePickerScrollbar.Content = new Theme.ThemePicker();

            DarkModeToggle.IsChecked = Properties.Settings.Default.IsDarkModeApplied;
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (DarkModeToggle.IsChecked == true)
            {
                AppearanceSettingHelper.ApplyDarkMode();
            }
            else
            {
                AppearanceSettingHelper.ApplyLightMode();
            }
        }
    }
}
