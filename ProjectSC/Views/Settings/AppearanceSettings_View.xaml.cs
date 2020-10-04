using ProjectSC.ViewModels.AppearanceSettings;
using ProjectSC.Views.ThemePicker;
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
        }

        private void AppearanceButton_Click(object sender, RoutedEventArgs e)
        {
            AppearanceSettingHelper.ApplyColor("#FFFFAFBD", "#FFFFC3A0");
        }
    }
}
