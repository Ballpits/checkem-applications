using ProjectSC.Classes.Functions.Common;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectSC.UserControls.Custom
{
    public partial class DetailsPanel : UserControl
    {
        public DetailsPanel(MyDayUSC myDayUSC)
        {
            InitializeComponent();

            MyDay = myDayUSC;
        }

        public bool IsNew { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public bool IsCompleted { get; set; }
        public bool IsImportant { get; set; }

        public bool CanNotify { get; set; }
        public int NotifyType { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public DateTime BeginDateTime { get; set; }
        public DateTime EndDateTime { get; set; }


        MyDayUSC MyDay = new MyDayUSC();
        ToDoItem item = new ToDoItem();

        private void DarkGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsNew)
            {
                RemoveButtonBar.IsEnabled = false;
                RemoveButtonBar.Visibility = Visibility.Hidden;

                BeginDatePicker.Text = $"{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Year}";
                BeginTimePicker.Text = $"{string.Format("{0:h:mm tt}", DateTime.Now)}";

                EndDatePicker.Text = $"{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Year}";
                EndTimePicker.Text = $"{string.Format("{0:h:mm tt}", DateTime.Now)}";
            }
            else
            {
                textBoxTitle.Text = Title;
                textBoxDescription.Text = Description;

                ReminderToggle.IsChecked = CanNotify;
                CheckToggleState();

                if (CanNotify)
                {
                    BeginDatePicker.Text = $"{BeginDateTime.Month}/{BeginDateTime.Day}/{BeginDateTime.Year}";
                    BeginTimePicker.Text = $"{string.Format("{0:h:mm tt}", BeginDateTime)}";

                    EndDatePicker.Text = $"{EndDateTime.Month}/{EndDateTime.Day}/{EndDateTime.Year}";
                    EndTimePicker.Text = $"{string.Format("{0:h:mm tt}", EndDateTime)}";
                }
                else
                {
                    BeginDatePicker.Text = $"{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Year}";
                    BeginTimePicker.Text = $"{string.Format("{0:h:mm tt}", DateTime.Now)}";

                    EndDatePicker.Text = $"{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Year}";
                    EndTimePicker.Text = $"{string.Format("{0:h:mm tt}", DateTime.Now)}";
                }

                RemoveButtonBar.IsEnabled = true;
                RemoveButtonBar.Visibility = Visibility.Visible;
            }
        }

        private void RetunButton_Click(object sender, RoutedEventArgs e)
        {
            MyDay.CloseDetailsPanel();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            bool canNotify;
            if (ReminderToggle.IsChecked == true)
            {
                canNotify = true;
            }
            else
            {
                canNotify = false;
            }

            if (IsNew)
            {
                item.AddNew(textBoxTitle.Text, textBoxDescription.Text, canNotify, Convert.ToDateTime(BeginDatePicker.Text + " " + BeginTimePicker.Text), Convert.ToDateTime(EndDatePicker.Text + " " + EndTimePicker.Text), DateTime.Now, MyDay.Inventory);
            }
            else
            {
                item.Update(Id, textBoxTitle.Text, textBoxDescription.Text, canNotify, Convert.ToDateTime(BeginDatePicker.Text + " " + BeginTimePicker.Text), Convert.ToDateTime(EndDatePicker.Text + " " + EndTimePicker.Text), MyDay.Inventory);
            }

            MyDay.Refresh();

            DetailsGrid.Children.Add(SnackbarControl.OpenSnackBar("Saved"));
        }


        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            item.Remove(Id, MyDay.Inventory);
            MyDay.stpMain.Children.RemoveAt(Id);
            item.ResetId(MyDay.Inventory);

            MyDay.Refresh();

            MyDay.CloseDetailsPanel();
        }

        private void ReminderToggle_Click(object sender, RoutedEventArgs e)
        {
            CheckToggleState();
        }

        private void CheckToggleState()
        {
            if (ReminderToggle.IsChecked == true)
            {
                ReminderExpander.IsEnabled = true;
                ReminderExpander.Foreground = Brushes.Black;
                ReminderExpander.IsExpanded = true;
            }
            else
            {
                ReminderExpander.IsEnabled = false;
                ReminderExpander.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#66000000"));
                ReminderExpander.IsExpanded = false;
            }
        }

        private void DarkGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed || Mouse.XButton1 == MouseButtonState.Pressed)
            {
                MyDay.CloseDetailsPanel();
            }
        }
    }
}
