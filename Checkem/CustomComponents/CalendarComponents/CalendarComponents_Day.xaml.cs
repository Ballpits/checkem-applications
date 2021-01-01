using System.Windows;
using System.Windows.Controls;

namespace Checkem.CustomComponents.CalendarComponents
{
    public partial class CalendarComponents_Day : UserControl
    {
        public CalendarComponents_Day()
        {
            DataContext = this;

            InitializeComponent();
        }



        public string Day
        {
            get { return (string)GetValue(DayProperty); }
            set { SetValue(DayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Day.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DayProperty =
            DependencyProperty.Register("Day", typeof(string), typeof(CalendarComponents_Day), new PropertyMetadata(string.Empty));
    }
}
