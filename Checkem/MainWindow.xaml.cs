using Checkem.CustomComponents;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Checkem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show(ScreenResolution.Vertical.ToString());
        }



        #region Window chrome buttons

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion



        #region Resize and state changed events
        private void AppWindow_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                TrainsitioningContent.Margin = new Thickness(7);
            }
            else
            {
                TrainsitioningContent.Margin = new Thickness(0);
            }

            CheckWidth();
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CheckWidth();
        }

        private void CheckWidth()
        {
            if (this.WindowState == WindowState.Maximized || this.Width >= 1400)
            {
                SearchBoxBorder.HorizontalAlignment = HorizontalAlignment.Center;
            }
            else
            {
                SearchBoxBorder.HorizontalAlignment = HorizontalAlignment.Left;
            }
        }
        #endregion



        #region Navigation bar

        #region Expander buttons

        private void ButtonExpand_Click(object sender, RoutedEventArgs e)
        {
            ButtonCollaps.Visibility = Visibility.Visible;
            ButtonExpand.Visibility = Visibility.Collapsed;
        }


        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCollaps.Visibility = Visibility.Collapsed;
            ButtonExpand.Visibility = Visibility.Visible;
        }

        #endregion

        #region Buttons

        private void ButtonMyDay_Click(object sender, RoutedEventArgs e)
        {

        }


        private void ButtonAllTasks_Click(object sender, RoutedEventArgs e)
        {

        }


        private void ButtonDueDateFilter_Click(object sender, RoutedEventArgs e)
        {

        }


        private void ButtonStarredFilter_Click(object sender, RoutedEventArgs e)
        {

        }


        private void ButtonAddNewList_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        private void MoveNavbarCursor(int index)
        {

        }


        public void ChangeFilter(int mode)
        {

        }

        #endregion



        #region Searchbox

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ButtonClearSearchBox_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion



        #region Hotkeys

        bool IsLCtrlPressed = false;
        bool IsFPressed = false;

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
            {
                IsLCtrlPressed = true;
            }

            if (e.Key == Key.F)
            {
                IsFPressed = true;
            }

            if (IsLCtrlPressed && IsFPressed)
            {
                SearchBox.Focus();
            }

            if (e.Key == Key.F11)
            {
                if (this.WindowStyle == WindowStyle.None)
                {
                    this.WindowStyle = WindowStyle.SingleBorderWindow;
                    this.WindowState = WindowState.Maximized;
                    TrainsitioningContent.Margin = new Thickness(7);
                }
                else
                {
                    this.WindowStyle = WindowStyle.None;
                    this.WindowState = WindowState.Minimized;
                    this.WindowState = WindowState.Maximized;
                    TrainsitioningContent.Margin = new Thickness(6);
                }
            }
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
            {
                IsLCtrlPressed = false;
            }

            if (e.Key == Key.F)
            {
                IsFPressed = false;
            }
        }

        #endregion

        private void Tag_StateChanged(object sender, EventArgs e)
        {
            Tag tag = sender as Tag;

            System.Windows.Forms.MessageBox.Show($"Tag.IsSelected = {tag.IsSelected}");
        }
    }
}
