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
        }

        #region Variables
        private DataAccess_Json dataAccess = new DataAccess_Json();

        private ToDoListUSC todoListUSC = new ToDoListUSC();

        private List<TimeRecord> timeRecord = new List<TimeRecord>();
        private DispatcherTimer timer = new DispatcherTimer();
        private int TimerOffset = 60 - DateTime.Now.Second;
        #endregion

        #region Propertes
        public int FilterMode { get; set; }
        #endregion

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();

            GridPrincipal.Children.Add(todoListUSC);
            todoListUSC.ListFilter(0);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimerOffset = 60 - DateTime.Now.Second;
            timer.Interval = TimeSpan.FromSeconds(TimerOffset);//Fix timer delay time

            dataAccess.RetrieveTimeData(ref timeRecord);


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

        private void ButtonToDoList_Click(object sender, RoutedEventArgs e)
        {
            this.FilterMode = 0;

            ChangeFIlter(this.FilterMode);
        }
        private void ButtonDueDateFilter_Click(object sender, RoutedEventArgs e)
        {
            this.FilterMode = 1;

            ChangeFIlter(this.FilterMode);
        }

        private void ButtonStarredFilter_Click(object sender, RoutedEventArgs e)
        {
            this.FilterMode = 2;

            ChangeFIlter(this.FilterMode);
        }

        private void ButtonUserPref_Click(object sender, RoutedEventArgs e)
        {
            GridPrincipal.Children.Clear();

            UserPrefenceWindow userPrefenceWindow = new UserPrefenceWindow(this);

            GridPrincipal.Children.Add(userPrefenceWindow);
        }

        private void MoveNavbarCursor(int index)
        {
            Grid.SetRow(NavbarCursor, index);
        }

        private void ButtonColorTester1_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Reset();
            Properties.Settings.Default.Save();
        }

        private void ButtonColorTester2_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.PrimaryColor = System.Drawing.Color.FromArgb(255, 218, 30, 99);
            Properties.Settings.Default.SecondaryColor = System.Drawing.Color.FromArgb(255, 117, 58, 136);

            Properties.Settings.Default.Save();
        }

        public void ChangeFIlter(int mode)
        {
            MoveNavbarCursor(mode);
            todoListUSC.ListFilter(mode);

            todoListUSC.CloseDetailsPanel();
        }
    }
}
