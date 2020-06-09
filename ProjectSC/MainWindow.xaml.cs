using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;


using ProjectSC.Classes;
using ProjectSC.Classes.Functions.MainWindow;
using ProjectSC.UserControls;

namespace ProjectSC
{
    public partial class MainWindow : Window, IUIControl
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ClipboardUSC clipBoard = new ClipboardUSC();
        UserControl myDayUSC = new MyDayUSC();
        UserControl noteBookUSC = new NotebookUSC();

        ToDoItem todoitem = new ToDoItem();
        List<TimeRecord> timeRecord = new List<TimeRecord>();

        DispatcherTimer timer = new DispatcherTimer();

        int TimerOffset = 60 - DateTime.Now.Second;

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            todoitem.LoadTimeData(ref timeRecord);

            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
            GridMain.Children.Add(myDayUSC);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimerOffset = 60 - DateTime.Now.Second;
            timer.Interval = TimeSpan.FromSeconds(TimerOffset);

            todoitem.LoadTimeData(ref timeRecord);

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
            }

        }

        public void MouseEnterHighLight(object sender, RoutedEventArgs e)
        {
            MainWindowMouseover.Highlight(sender);
        }

        public void MouseLeaveUnHighLight(object sender, RoutedEventArgs e)
        {
            MainWindowMouseover.UnHighlight(sender);
        }


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
