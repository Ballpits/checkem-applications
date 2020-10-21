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

namespace ProjectSC.Views.Calendar
{
    public partial class CalendarDay_View : UserControl
    {
        public CalendarDay_View()
        {
            InitializeComponent();
        }

        public int Date { get; set; }
    }
}
