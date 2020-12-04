using ProjectSC.Models.DataAccess;
using ProjectSC.ViewModels.AppearanceSettings;
using ProjectSC.ViewModels.Language;
using ProjectSC.ViewModels.Notification;
using ProjectSC.Views;
using ProjectSC.Views.Settings;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace ProjectSC
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AppearanceSettingHelper.DarkModeSetup();
            LanguageApplyHelper.LanguageSetup();

            this.WindowState = WindowState.Maximized;
        }

        #region Variables
        private DataAccess_Json dataAccess = new DataAccess_Json();

        private ToDoList todoList_View = new ToDoList();

        private List<Models.Object.Notification.Notifications> timeRecord = new List<Models.Object.Notification.Notifications>();
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private int TimerOffset = 60 - DateTime.Now.Second;

        private int NavbarCursorIndex = 0;
        #endregion

        #region Propertes
        public int filterMode { get; set; }
        #endregion

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //dispatcherTimer.Tick += new EventHandler(Timer_Tick);
            //dispatcherTimer.Start();

            GridPrincipal.Children.Add(todoList_View);
            todoList_View.Filter(0);
        }

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

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimerOffset = 60 - DateTime.Now.Second;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(TimerOffset);//Fix timer delay time

            dataAccess.RetrieveTimeData(ref timeRecord);


            foreach (var item in timeRecord)
            {
                if (DateTime.Now.Year == item.BeginDateTime.Year && DateTime.Now.Month == item.BeginDateTime.Month && DateTime.Now.Hour == item.BeginDateTime.Hour && DateTime.Now.Minute == item.BeginDateTime.Minute)
                {
                    NotificationController.Notify(item.Title, NotificationController.RandomMessage("begin"));
                }
                if (DateTime.Now.Year == item.EndDateTime.Year && DateTime.Now.Month == item.EndDateTime.Month && DateTime.Now.Hour == item.EndDateTime.Hour && DateTime.Now.Minute == item.EndDateTime.Minute)
                {
                    NotificationController.Notify(item.Title, NotificationController.RandomMessage("end"));
                }
            }//Check if the begin or end time is matched with the current time
        }

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

        private void ButtonMyDay_Click(object sender, RoutedEventArgs e)
        {
            ChangeFilter(0);
        }

        private void ButtonAllTasks_Click(object sender, RoutedEventArgs e)
        {
            ChangeFilter(1);
        }
        private void ButtonDueDateFilter_Click(object sender, RoutedEventArgs e)
        {
            ChangeFilter(2);
        }

        private void ButtonStarredFilter_Click(object sender, RoutedEventArgs e)
        {
            ChangeFilter(3);
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            if (MainGrid.Children.Count < 4)
            {
                SettingsMenu settingsMenu = new SettingsMenu(this);

                Grid.SetColumnSpan(settingsMenu, 2);
                Grid.SetRow(settingsMenu, 1);

                MainGrid.Children.Add(settingsMenu);

                SearchBoxBorder.Visibility = Visibility.Hidden;
                NavExpanderButtonGrid.Visibility = Visibility.Hidden;
            }
        }

        public void ChangeFilter(int mode)
        {
            MoveNavbarCursor(mode);
            filterMode = mode;
            todoList_View.Filter(mode);
        }

        private void MoveNavbarCursor(int index)
        {
            NavbarCursorIndex = index;
            Grid.SetRow(NavbarCursor, index);
        }

        private void ButtonAddNewList_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ButtonClearSearchBox_Click(object sender, RoutedEventArgs e)
        {

        }

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
    }
}
