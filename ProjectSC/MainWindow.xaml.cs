using ProjectSC.Models.DataAccess;
using ProjectSC.ViewModels.AppearanceSettings;
using ProjectSC.ViewModels.Notification;
using ProjectSC.Views;
using ProjectSC.Views.Settings;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ProjectSC
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AppearanceSettingHelper.DarkModeSetup();
        }

        #region Variables
        private DataAccess_Json dataAccess = new DataAccess_Json();

        private ToDoList_View todoList_View = new ToDoList_View();

        private List<Models.Object.Notification.Notifications> timeRecord = new List<Models.Object.Notification.Notifications>();
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private int TimerOffset = 60 - DateTime.Now.Second;

        private int NavbarCursorIndex = 0;
        #endregion

        #region Propertes

        #endregion

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Tick += new EventHandler(Timer_Tick);
            dispatcherTimer.Start();

            GridPrincipal.Children.Add(todoList_View);
            todoList_View.Filter(0);
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

        private void ButtonToDoList_Click(object sender, RoutedEventArgs e)
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
            SettingsMenu settingsMenu = new SettingsMenu(this);

            Grid.SetColumnSpan(settingsMenu, 2);

            MainGrid.Children.Add(settingsMenu);
        }

        private void ChangeFilter(int mode)
        {
            MoveNavbarCursor(mode);

            todoList_View.Filter(mode);
        }

        private void MoveNavbarCursor(int index)
        {
            NavbarCursorIndex = index;
            Grid.SetRow(NavbarCursor, index + 1);
        }

        private void ButtonAddNewList_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
