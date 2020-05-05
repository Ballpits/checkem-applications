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

namespace ProjectSC.UserControls.Custom
{
    /// <summary>
    /// Interaction logic for DetailsPanel.xaml
    /// </summary>
    public partial class DetailsPanel : UserControl
    {
        MyDayUSC MyDay;
        public DetailsPanel(MyDayUSC myDayUSC)
        {
            InitializeComponent();
            MyDay = myDayUSC;
        }

        private void RetunButton_Click(object sender, RoutedEventArgs e)
        {
            MyDay.CloseDetailsPanel();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReminderToggle_Click(object sender, RoutedEventArgs e)
        {
            if (ReminderToggle.IsChecked == true)
            {
                ReminderToggle.Content = "1";
                ReminderExpander.IsEnabled = true;
            }
            else
            {
                ReminderToggle.Content = "0";
                ReminderExpander.IsEnabled = false;
            }
        }
    }
}
