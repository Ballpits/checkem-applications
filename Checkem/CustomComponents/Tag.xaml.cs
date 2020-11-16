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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Checkem.CustomComponents
{
    public partial class Tag : UserControl
    {
        public Tag()
        {
            InitializeComponent();
        }

        public bool IsChecked { get; set; }

        public SolidColorBrush Color { get; set; }

        public string Text { get; set; }

        private void checkbox_Checked(object sender, RoutedEventArgs e)
        {
            IsChecked = true;
        }

        private void checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            IsChecked = false;
        }

        private void TagGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (checkbox.IsChecked == true)
            {
                checkbox.IsChecked = false;
            }
            else
            {
                checkbox.IsChecked = true;
            }
        }
    }
}
