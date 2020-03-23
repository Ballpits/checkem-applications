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
            string hour = DateTime.Now.Hour.ToString();
            string min = DateTime.Now.Minute.ToString();
            TimeTB.Text = $"{hour}:{min}";
            DateTB.Text = String.Format("{0:MMMM d}", dt);
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
            timer.Start();
            UserControl usc;
            usc = new MyDayUSC();
            GridMain.Children.Add(usc);
        }

        private void AppWindow_Loaded(object sender, RoutedEventArgs e)
        {
        //    var myPanel = new StackPanel();
        //    myPanel.Margin = new Thickness(10);

        //    var myRectangle = new Rectangle();
        //    myRectangle.Name = "myRectangle";
        //    this.RegisterName(myRectangle.Name, myRectangle);
        //    myRectangle.Width = 100;
        //    myRectangle.Height = 100;
        //    myRectangle.Fill = Brushes.Blue;
        //    myPanel.Children.Add(myRectangle);
        //    this.Content = myPanel;

        //    var myDoubleAnimation = new DoubleAnimation();
        //    myDoubleAnimation.From = 1.0;
        //    myDoubleAnimation.To = 0.0;

        //    myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(5));

        //    myDoubleAnimation.AutoReverse = true;
        //    myDoubleAnimation.RepeatBehavior = RepeatBehavior.Forever;

        //    myStoryboard = new Storyboard();
        //    myStoryboard.Children.Add(myDoubleAnimation);

        //    Storyboard.SetTargetName(myDoubleAnimation, myRectangle.Name);

        //    Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Rectangle.OpacityProperty));
        //    //Page p = new Page
        //    //{
        //    //    Background = Brushes.Violet
        //    //};
        //    //this.Content = p;
        }
    }
}
