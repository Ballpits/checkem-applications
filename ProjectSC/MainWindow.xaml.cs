using ProjectSC.Classes;
using ProjectSC.Classes.Functions.MainWindow;
using ProjectSC.UserControls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ProjectSC
{
    public partial class MainWindow : Window, IUIControl
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region 
        private UserControl clipBoard = new ClipboardUSC();
        private UserControl myDayUSC = new MyDayUSC();
        private UserControl noteBookUSC = new NotebookUSC();
        #endregion

        private ToDoItem todoitem = new ToDoItem();
        private List<TimeRecord> timeRecord = new List<TimeRecord>();

        private DispatcherTimer timer = new DispatcherTimer();
        private int TimerOffset = 60 - DateTime.Now.Second;

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();


            GridMain.Children.Add(myDayUSC);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimerOffset = 60 - DateTime.Now.Second;
            timer.Interval = TimeSpan.FromSeconds(TimerOffset);//Fix timer delay time

            todoitem.RetrieveTimeData(ref timeRecord);

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

        #region Mouse enter events
        public void MouseEnterHighLight(object sender, RoutedEventArgs e)
        {
            MainWindowMouseover.Highlight(sender);
        }

        public void MouseLeaveUnHighLight(object sender, RoutedEventArgs e)
        {
            MainWindowMouseover.UnHighlight(sender);
        }
        #endregion

        private void ButtonClipBoard_Click(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Clear();
            GridMain.Children.Add(clipBoard);
        }

        private void ButtonToDoList_Click(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Clear();
            
            GridMain.Children.Add(myDayUSC);
        }

        private void ButtonNotebook_Click(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Clear();
            GridMain.Children.Add(noteBookUSC);
        }
    }
}
