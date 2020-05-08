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
        ToDoItem items = new ToDoItem();

        private void DarkGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsNew)
            {
                RemoveButtonBar.IsEnabled = false;
                RemoveButtonBar.Visibility = Visibility.Hidden;
            }
            else
            {
                textBoxTitle.Text = Title;
                textBoxDescription.Text = Description;

                ReminderToggle.IsChecked = CanNotify;
                CheckToggleState();

                BeginDatePicker.Text = $"{BeginDateTime.Month}/{BeginDateTime.Day}/{BeginDateTime.Year}";
                BeginTimePicker.Text = $"{string.Format("{0:h:mm tt}", BeginDateTime)}";

                EndDatePicker.Text = $"{EndDateTime.Month}/{EndDateTime.Day}/{EndDateTime.Year}";
                EndTimePicker.Text = $"{string.Format("{0:h:mm tt}", EndDateTime)}";

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
                items.AddNew(textBoxTitle.Text, textBoxDescription.Text, canNotify, Convert.ToDateTime(BeginDatePicker.Text + " " + BeginTimePicker.Text), Convert.ToDateTime(EndDatePicker.Text + " " + EndTimePicker.Text), DateTime.Now, MyDay.Inventory);
            }
            else
            {
                items.Update(Id, textBoxTitle.Text, textBoxDescription.Text,canNotify, Convert.ToDateTime(BeginDatePicker.Text + " " + BeginTimePicker.Text), Convert.ToDateTime(EndDatePicker.Text + " " + EndTimePicker.Text), MyDay.Inventory);

            }

            MyDay.Refresh();

            DetailsGrid.Children.Add(SnackbarControl.OpenSnackBar("Saved"));
        }


        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            items.Remove(Id, MyDay.Inventory);
            MyDay.stpMain.Children.RemoveAt(Id);
            items.ResetId(MyDay.Inventory);

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
