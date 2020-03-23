using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectSC
{
    interface IUIControl
    {
        void MouseEnterHighLight(object sender, RoutedEventArgs e);

        void MouseLeaveUnHighLight(object sender, RoutedEventArgs e);
    }
}
