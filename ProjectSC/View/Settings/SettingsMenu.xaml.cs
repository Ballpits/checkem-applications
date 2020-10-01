using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectSC.View.Settings
{
    /// <summary>
    /// Interaction logic for SettingsMenu.xaml
    /// </summary>
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
            mainWindow.GridPrincipal.Children.RemoveAt(mainWindow.GridPrincipal.Children.Count - 1);
            mainWindow.GridPrincipal.Children.Add(new ToDoList_View());
        }

        private void CloseButton_MouseEnter(object sender, MouseEventArgs e)
        {
            //CloseButton.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
        }

        private void CloseButton_MouseLeave(object sender, MouseEventArgs e)
        {
            //CloseButton.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#00000000"));
        }

        private void ButtonGeneral_Click(object sender, RoutedEventArgs e)
        {
            //GridPrinciple.Children.Clear();
            //GridPrinciple.Children.Add(new GeneralUSC());
        }

        private void ButtonNotification_Click(object sender, RoutedEventArgs e)
        {
            //GridPrincipal.Children.Clear();
            //GridPrincipal.Children.Add(new NotificationUSC());
        }

        private void ButtonAppearance_Click(object sender, RoutedEventArgs e)
        {
            //GridPrinciple.Children.Clear();
            //GridPrinciple.Children.Add(new Appearance());
        }
    }
}
