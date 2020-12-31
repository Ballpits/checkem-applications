using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Checkem.CustomComponents
{
    public partial class CalendarComponents_EventBar : UserControl
    {
        public CalendarComponents_EventBar()
        {
            DataContext = this;

            InitializeComponent();
        }



        public string Title
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(CalendarComponents_EventBar), new PropertyMetadata(string.Empty));




        public int EventBeginColumn
        {
            get { return (int)GetValue(BeginColumnProperty); }
            set { SetValue(BeginColumnProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BeginColumn.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BeginColumnProperty =
            DependencyProperty.Register(nameof(EventBeginColumn), typeof(int), typeof(CalendarComponents_EventBar), new PropertyMetadata(0));




        public int EventColumnSpan
        {
            get { return (int)GetValue(EventColumnDurationProperty); }
            set { SetValue(EventColumnDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EventColumnDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EventColumnDurationProperty =
            DependencyProperty.Register(nameof(EventColumnSpan), typeof(int), typeof(CalendarComponents_EventBar), new PropertyMetadata(0));




        public SolidColorBrush Color
        {
            get { return (SolidColorBrush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(nameof(Color), typeof(SolidColorBrush), typeof(CalendarComponents_EventBar), new PropertyMetadata(Brushes.Red));
    }
}
