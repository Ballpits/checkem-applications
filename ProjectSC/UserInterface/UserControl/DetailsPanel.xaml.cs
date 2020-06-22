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

            myDay = myDayUSC;
        }

        public DetailsPanel(MyDayUSC myDayUSC, ItemBar itembar)
        {
            InitializeComponent();

            myDay = myDayUSC;
            itemBar = itembar;
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


        MyDayUSC myDay = new MyDayUSC();
        ItemBar itemBar = new ItemBar();

        private void DarkGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsNew)
            {
                RemoveButton.IsEnabled = false;
                RemoveButton.Visibility = Visibility.Hidden;

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
            }
        }

        private void RetunButton_Click(object sender, RoutedEventArgs e)
        {
            myDay.CloseDetailsPanel();
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
                DataAccess.AddNew(textBoxTitle.Text, textBoxDescription.Text, canNotify, Convert.ToDateTime(BeginDatePicker.Text + " " + BeginTimePicker.Text), Convert.ToDateTime(EndDatePicker.Text + " " + EndTimePicker.Text), DateTime.Now, myDay.Inventory);
            }
            else
            {
                DataAccess.Update(Id, textBoxTitle.Text, textBoxDescription.Text, canNotify, Convert.ToDateTime(BeginDatePicker.Text + " " + BeginTimePicker.Text), Convert.ToDateTime(EndDatePicker.Text + " " + EndTimePicker.Text), myDay.Inventory);

                itemBar.Update(textBoxTitle.Text);
            }

            DetailsGrid.Children.Add(SnackbarControl.OpenSnackBar("Saved"));
        }


        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            DataAccess.RemoveAt(Id, myDay.Inventory);

            myDay.RemoveItemBar(Id);

            myDay.CloseDetailsPanel();
        }

        private void ReminderToggle_Click(object sender, RoutedEventArgs e)
        {
            CheckToggleState();
        }

        private void CheckToggleState()
        {
            if (ReminderToggle.IsChecked == true)
            {
                AdvReminderExpander.IsEnabled = true;
                AdvReminderExpander.Foreground = Brushes.Black;
                AdvReminderExpander.IsExpanded = true;
            }
            else
            {
                AdvReminderExpander.IsEnabled = false;
                AdvReminderExpander.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#66000000"));
                AdvReminderExpander.IsExpanded = false;
            }
        }

        private void DarkGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed || Mouse.XButton1 == MouseButtonState.Pressed)
            {
                myDay.CloseDetailsPanel();
            }
        }
    }
}
