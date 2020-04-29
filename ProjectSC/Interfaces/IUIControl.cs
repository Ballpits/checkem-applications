using System.Windows;

namespace ProjectSC
{
    interface IUIControl
    {
        void MouseEnterHighLight(object sender, RoutedEventArgs e);

        void MouseLeaveUnHighLight(object sender, RoutedEventArgs e);
    }
}
