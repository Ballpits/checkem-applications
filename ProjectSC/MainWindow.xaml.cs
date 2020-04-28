using ProjectSC.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ProjectSC
{
    public partial class MainWindow : Window, IUIControl
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ToDoItem todoitem = new ToDoItem();
        List<TimeRecord> timeRecord = new List<TimeRecord>();

        UserControl usc;

        DispatcherTimer timer = new DispatcherTimer();

        int TimerOffset = 60 - DateTime.Now.Second;

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            todoitem.LoadTimeData(ref timeRecord);

            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();


            UserControl usc;
            usc = new MyDayUSC();
            GridMain.Children.Add(usc);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimerOffset = 60 - DateTime.Now.Second;
            timer.Interval = TimeSpan.FromSeconds(TimerOffset);

            todoitem.LoadTimeData(ref timeRecord);

            TimeTB.Text = string.Format("{0:hh:mm tt}", DateTime.Now);
            DateTB.Text = string.Format("{0:MMMM d}", DateTime.Now);

            foreach (var item in timeRecord)
            {
                if (DateTime.Now.Year == item.BeginDateTime.Year && DateTime.Now.Month == item.BeginDateTime.Month && DateTime.Now.Hour == item.BeginDateTime.Hour && DateTime.Now.Minute == item.BeginDateTime.Minute)
                {
                    Notifications.Notify(item.Title, "It's time to finish this");
                }
            }

        }

        public void MouseEnterHighLight(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF5CB7FF"));
        }

        public void MouseLeaveUnHighLight(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
        }


        private void ButtonClipBoard_Click(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Clear();
            usc = new ClipboardUSC();
            GridMain.Children.Add(usc);
        }

        private void ButtonToDoList_Click(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Clear();
            usc = new MyDayUSC();
            GridMain.Children.Add(usc);
        }
    }
}
