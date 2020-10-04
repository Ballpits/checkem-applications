using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectSC.Views.Settings
{
    public partial class SettingsMenu : UserControl
    {
        public SettingsMenu(MainWindow main)
        {
            InitializeComponent();

            mainWindow = main;
        }

        MainWindow mainWindow;

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.MainGrid.Children.RemoveAt(mainWindow.MainGrid.Children.Count - 1);
        }

        private void CloseButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CloseButton.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
        }

        private void CloseButton_MouseLeave(object sender, MouseEventArgs e)
        {
            CloseButton.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#00000000"));
        }

        private void ButtonGeneral_Click(object sender, RoutedEventArgs e)
        {
            GridPrinciple.Children.Clear();
            GridPrinciple.Children.Add(new GeneralSettings_View());
        }

        private void ButtonNotification_Click(object sender, RoutedEventArgs e)
        {
            GridPrinciple.Children.Clear();
            GridPrinciple.Children.Add(new NotificationSettings_View());
        }

        private void ButtonAppearance_Click(object sender, RoutedEventArgs e)
        {
            GridPrinciple.Children.Clear();
            GridPrinciple.Children.Add(new AppearanceSettings_View());
        }
    }
}
