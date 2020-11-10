using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Checkem.CheckemUserControls
{
    public partial class ToggleSwitch : UserControl
    {
        public ToggleSwitch()
        {
            InitializeComponent();
        }

        public event EventHandler Checked;
        public event EventHandler Unchecked;

        public bool IsChecked { get; set; } = false;

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
            Checked?.Invoke(this, EventArgs.Empty);
            Unchecked?.Invoke(this, EventArgs.Empty);

            if (checkbox.IsChecked == true)
            {
                checkbox.IsChecked = false;
                IsChecked = false;
            }
            else
            {
                checkbox.IsChecked = true;
                IsChecked = true;
            }
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
