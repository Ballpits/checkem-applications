using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Checkem.CustomComponents
{
    public partial class ToggleSwitch : UserControl
    {
        public ToggleSwitch()
        {
            DataContext = this;

            InitializeComponent();
        }

        public event EventHandler Checked;
        public event EventHandler Unchecked;
        public event EventHandler StateChanged;


        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(ToggleSwitch), new PropertyMetadata(null));


        private void ToggleGrid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (checkbox.IsChecked == true)
                {
                    Storyboard sb = this.FindResource("ToggleDown1") as Storyboard;

                    sb.Begin();
                }
                else
                {
                    Storyboard sb = this.FindResource("ToggleDown0") as Storyboard;

                    sb.Begin();
                }
            }
        }

        private void ToggleGrid_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (checkbox.IsChecked == true)
            {
                checkbox.IsChecked = false;
                IsChecked = false;
                Unchecked?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                checkbox.IsChecked = true;
                IsChecked = true;
                Checked?.Invoke(this, EventArgs.Empty);
            }

            StateChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ToggleGrid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (checkbox.IsChecked == true)
                {
                    Storyboard sb = this.FindResource("ToggleLeave1") as Storyboard;

                    sb.Begin();
                }
                else
                {
                    Storyboard sb = this.FindResource("ToggleLeave0") as Storyboard;

                    sb.Begin();
                }
            }
        }
    }
}
