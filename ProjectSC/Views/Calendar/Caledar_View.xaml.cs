using System;
using System.Windows;
using System.Windows.Controls;

namespace ProjectSC.Views.Calendar
{
    public partial class Caledar_View : UserControl
    {
        public Caledar_View()
        {
            InitializeComponent();
        }


        private void DateGrid_Loaded(object sender, RoutedEventArgs e)
        {
            int currnenYear = DateTime.Now.Year;
            int currentMounth = DateTime.Now.Month;
            int lastMounth = DateTime.Now.Month - 1;
            int lastDayOfCurrentMonth = DateTime.DaysInMonth(currnenYear, currentMounth);
            int lastDayOfLastMounth = DateTime.DaysInMonth(currnenYear, lastMounth);

            for (int r = 1; r < 6; r++)
            {
                for (int c = 0; c < 7; c++)
                {
                    CalendarDay_View caledar_View = new CalendarDay_View(c);
                    Grid.SetColumn(caledar_View, c);
                    Grid.SetRow(caledar_View, r);
                    DateGrid.Children.Add(caledar_View);
                }
            }
        }

        public void myMethod()
        {
            int dayOfWeek = (int)DateTime.Now.DayOfWeek;
            int lastDayOfPrevMonth = Convert.ToInt32(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1));

            System.Windows.Forms.MessageBox.Show(lastDayOfPrevMonth.ToString());
        }
    }
}
