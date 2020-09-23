using ProjectSC.UserPreference.SubPages;
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
using System.Windows.Shapes;

namespace ProjectSC
{
    /// <summary>
    /// Interaction logic for UserPrefenceWindow.xaml
    /// </summary>
    public partial class UserPrefenceWindow : UserControl
    {
        public UserPrefenceWindow(MainWindow main)
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
            GridPrinciple.Children.Add(new GeneralUSC());
        }

        private void ButtonNotification_Click(object sender, RoutedEventArgs e)
        {
            GridPrinciple.Children.Clear();
            GridPrinciple.Children.Add(new NotificationUSC());
        }

        private void ButtonAppearance_Click(object sender, RoutedEventArgs e)
        {
            GridPrinciple.Children.Clear();
            GridPrinciple.Children.Add(new Appearance());
        }
    }
}
