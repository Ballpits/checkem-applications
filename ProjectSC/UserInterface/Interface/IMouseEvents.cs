using System.Windows;

namespace ProjectSC
{
    interface IMouseEvents
    {
        void MouseEnterHighLight(object sender, RoutedEventArgs e);

        void MouseLeaveUnHighLight(object sender, RoutedEventArgs e);
    }
}
