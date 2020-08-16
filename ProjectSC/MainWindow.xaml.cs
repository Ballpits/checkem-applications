using ProjectSC.Classes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace ProjectSC
{
    public partial class MainWindow : Window, IMouseEvents
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region 
        private MyDayUSC myDayUSC = new MyDayUSC();
        #endregion

        #region Reminder variables
        private List<TimeRecord> timeRecord = new List<TimeRecord>();
        private DispatcherTimer timer = new DispatcherTimer();
        private int TimerOffset = 60 - DateTime.Now.Second;
        #endregion

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();

            GridMain.Children.Add(myDayUSC);
            myDayUSC.ListFilter(2);

            //QuickViewWindow quickView = new QuickViewWindow();
            //quickView.Show();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimerOffset = 60 - DateTime.Now.Second;
            timer.Interval = TimeSpan.FromSeconds(TimerOffset);//Fix timer delay time

            DataAccess.RetrieveTimeData(ref timeRecord);


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
            MainWindowMouseover.Highlight(sender);
        }

        public void MouseLeaveUnHighLight(object sender, RoutedEventArgs e)
        {
            MainWindowMouseover.UnHighlight(sender);
        }
        #endregion

        private void ButtonImportantFilter_Click(object sender, RoutedEventArgs e)
        {
            myDayUSC.ListFilter(0);
            myDayUSC.CloseDetailsPanel();
        }
        private void ButtonDueDateFilter_Click(object sender, RoutedEventArgs e)
        {
            myDayUSC.ListFilter(1);
            myDayUSC.CloseDetailsPanel();
        }

        private void ButtonToDoList_Click(object sender, RoutedEventArgs e)
        {
            myDayUSC.ListFilter(2);
            myDayUSC.CloseDetailsPanel();
        }

        private void ButtonUserPref_Click(object sender, RoutedEventArgs e)
        {
            UserPrefenceWindow userPrefenceWindow = new UserPrefenceWindow();
            userPrefenceWindow.Show();
        }
    }
}
