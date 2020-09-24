using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace ProjectSC
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Variables
        private DataAccess_Json dataAccess = new DataAccess_Json();

        private ToDoListUSC myDayUSC = new ToDoListUSC();

        private string path = "Inventory.json";

        private List<TimeRecord> timeRecord = new List<TimeRecord>();
        private DispatcherTimer timer = new DispatcherTimer();
        private int TimerOffset = 60 - DateTime.Now.Second;
        #endregion

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();

            GridPrincipal.Children.Add(myDayUSC);
            myDayUSC.ListFilter(2);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimerOffset = 60 - DateTime.Now.Second;
            timer.Interval = TimeSpan.FromSeconds(TimerOffset);//Fix timer delay time

            dataAccess.RetrieveTimeData(ref timeRecord, path);


            foreach (var item in timeRecord)
            {
                if (DateTime.Now.Year == item.BeginDateTime.Year && DateTime.Now.Month == item.BeginDateTime.Month && DateTime.Now.Hour == item.BeginDateTime.Hour && DateTime.Now.Minute == item.BeginDateTime.Minute)
                {
                    Notifications.Notify(item.Title, Notifications.RandomMessage("begin"));
                }
                if (DateTime.Now.Year == item.EndDateTime.Year && DateTime.Now.Month == item.EndDateTime.Month && DateTime.Now.Hour == item.EndDateTime.Hour && DateTime.Now.Minute == item.EndDateTime.Minute)
                {
                    Notifications.Notify(item.Title, Notifications.RandomMessage("end"));
                }
            }//Check if the begin or end time is matched with the current time
        }

        #region Mouse over events
        public void MouseEnterHighLight(object sender, RoutedEventArgs e)
        {
            MouseoverHighlight.Highlight(sender, "#665CB7FF");
        }

        public void MouseLeaveUnHighLight(object sender, RoutedEventArgs e)
        {
            MouseoverHighlight.UnHighlight(sender, "#002196F3");
        }
        #endregion

        private void ButtonToDoList_Click(object sender, RoutedEventArgs e)
        {
            MoveNavbarCursor(0);
            myDayUSC.ListFilter(2);
            myDayUSC.CloseDetailsPanel();
        }
        private void ButtonDueDateFilter_Click(object sender, RoutedEventArgs e)
        {
            MoveNavbarCursor(1);
            myDayUSC.ListFilter(1);
            myDayUSC.CloseDetailsPanel();
        }

        private void ButtonStarredFilter_Click(object sender, RoutedEventArgs e)
        {
            MoveNavbarCursor(2);
            myDayUSC.ListFilter(0);
            myDayUSC.CloseDetailsPanel();
        }

        private void ButtonUserPref_Click(object sender, RoutedEventArgs e)
        {
            UserPrefenceWindow userPrefenceWindow = new UserPrefenceWindow(this);
            MainGrid.Children.Add(userPrefenceWindow);
        }

        private void MoveNavbarCursor(int index)
        {
            NavbarCursor.Margin = new Thickness(0, (70 * index), 0, 0);
        }

        private void AppWindow_Closed(object sender, EventArgs e)
        {
            //app.IsMainWindowOpen = false;
        }
    }
}
