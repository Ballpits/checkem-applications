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
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window, IUIControl
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        DateTime dt;

        DispatcherTimer timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };

        private void Timer_Tick(object sender, EventArgs e)
        {
            dt = DateTime.Now;
            TimeTB.Text = string.Format("{0:hh:mm tt}",dt);
            DateTB.Text = string.Format("{0:MMMM d}", dt);
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

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Tick += new EventHandler(Timer_Tick);
            //timer.Start();
            UserControl usc;
            usc = new MyDayUSC();
            GridMain.Children.Add(usc);
        }

        private void AppWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
