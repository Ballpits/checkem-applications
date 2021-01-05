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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Checkem.CustomComponents.CalendarComponents
{
    public partial class CalendarComponents_Week : UserControl
    {
        public CalendarComponents_Week()
        {
            InitializeComponent();
        }



        public int Week
        {
            get { return (int)GetValue(WeekProperty); }
            set
            {
                SetValue(WeekProperty, value);
                SetupDays();
            }
        }

        // Using a DependencyProperty as the backing store for Week.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WeekProperty =
            DependencyProperty.Register("Week", typeof(int), typeof(CalendarComponents_Week), new PropertyMetadata(1));


        private void SetupDays()
        {

        }
    }
}
