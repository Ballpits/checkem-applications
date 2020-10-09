using ProjectSC.ViewModels.AppearanceSettings;
using ProjectSC.Views.ThemePicker;
using ProjectSC.Views.ThemePreview;
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
            ThemePickerScrollbar.Content = new ThemePicker_View();
            GridPreviewWindow.Children.Add(new ThemePreview_View());

            DarkModeToggle.IsChecked = Properties.Settings.Default.IsDarkModeApplied;
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (DarkModeToggle.IsChecked == true)
            {
                DarkModeHelper.ApplyDarkMode();
            }
            else
            {
                DarkModeHelper.ApplyLightMode();
            }
        }
    }
}
