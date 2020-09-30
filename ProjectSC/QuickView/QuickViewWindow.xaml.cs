using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectSC
{
    public partial class QuickViewWindow : Window
    {
        App application;
        public QuickViewWindow(App app)
        {
            InitializeComponent();
            application = app;
            this.Focus();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            application.IsQuickViewWindowOpen = false;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                this.Close();
            }
        }

        private void CloseButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CloseButton.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
        }

        private void CloseButton_MouseLeave(object sender, MouseEventArgs e)
        {
            CloseButton.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#00000000"));
        }
    }
}
