using MaterialDesignThemes.Wpf;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectSC.UserControls.Custom
{
    public partial class DetailsPanel : UserControl
    {
        public DetailsPanel(ToDoListUSC myDayUSC)
        {
            InitializeComponent();

            todo = myDayUSC;
        }

        public DetailsPanel(ToDoListUSC todolist, ItemBar itembar)
        {
            InitializeComponent();

            todo = todolist;
            itemBar = itembar;
        }

        #region Properties
        public bool IsNew { get; set; }

        public int Id { get; set; }


        public string Title { get; set; }
        public string Description { get; set; }


        public bool IsCompleted { get; set; }
        public bool IsImportant { get; set; }


        public bool IsReminderOn { get; set; }
        public bool IsAdvanceReminderOn { get; set; }
        public int NotifyType { get; set; }


        public DateTime BeginDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public bool IsUsingTag { get; set; }
        public string TagName { get; set; }
        #endregion

        ToDoListUSC todo = new ToDoListUSC();
        ItemBar itemBar = new ItemBar();

        private void DarkGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsNew)
            {
                RemoveButton.IsEnabled = false;
                RemoveButton.Visibility = Visibility.Hidden;
            }
            else
            {
                textBoxTitle.Text = Title;
                textBoxDescription.Text = Description;

                if (IsReminderOn == true)
                {
                    if (IsAdvanceReminderOn == true)
                    {
                        reminderMode = 2;
                    }
                    else
                    {
                        reminderMode = 1;
                    }
                }
                else
                {
                    reminderMode = 0;
                }

                if (reminderMode != 0)
                {
                    if (reminderMode == 2)
                    {
                        BeginDatePicker.Text = $"{BeginDateTime.Month}/{BeginDateTime.Day}/{BeginDateTime.Year}";
                        BeginTimePicker.Text = $"{string.Format("{0:h:mm tt}", BeginDateTime)}";

                        EndDatePicker.Text = $"{EndDateTime.Month}/{EndDateTime.Day}/{EndDateTime.Year}";
                        EndTimePicker.Text = $"{string.Format("{0:h:mm tt}", EndDateTime)}";
                    }
                    else
                    {
                        EndDatePickerBasic.Text = $"{EndDateTime.Month}/{EndDateTime.Day}/{EndDateTime.Year}";
                        EndTimePickerBasic.Text = $"{string.Format("{0:h:mm tt}", EndDateTime)}";
                    }
                }//Reminder texts

                ChangeReminderButtonMode(reminderMode);
                ChangeReminderState();

                if (IsUsingTag)
                {
                    ChipGrid.Height = 50;

                    TagChip.Visibility = Visibility.Visible;

                    ChipTitleEditTextbox.Text = TagName;
                }//Tag texts
            }
        }

        private void RetunButton_Click(object sender, RoutedEventArgs e)
        {
            todo.CloseDetailsPanel();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsNew)
            {
                if (reminderMode != 0)
                {
                    DataAccess.AddNew(textBoxTitle.Text, textBoxDescription.Text, Convert.ToDateTime(BeginDatePicker.Text + " " + BeginTimePicker.Text), Convert.ToDateTime(EndDatePicker.Text + " " + EndTimePicker.Text), DateTime.Now, todo.Inventory);
                }
                else
                {
                    DataAccess.AddNew(textBoxTitle.Text, textBoxDescription.Text, DateTime.Now, todo.Inventory);
                }


                DataAccess.RetrieveData(ref todo.Inventory);
                DataAccess.ResetId(todo.Inventory);

                todo.AddItemBar();

                DetailsGrid.Children.Add(SnackbarControl.OpenSnackBar("Added"));
            }
            else
            {
                if (reminderMode != 0)
                {
                    if (reminderMode == 2)
                    {
                        DataAccess.Update(Id, textBoxTitle.Text, textBoxDescription.Text, Convert.ToDateTime(BeginDatePicker.Text + " " + BeginTimePicker.Text), Convert.ToDateTime(EndDatePicker.Text + " " + EndTimePicker.Text), todo.Inventory);
                    }
                    else
                    {
                        DataAccess.Update(Id, textBoxTitle.Text, textBoxDescription.Text, Convert.ToDateTime(EndDatePickerBasic.Text + " " + EndTimePickerBasic.Text), todo.Inventory);
                    }
                }
                else
                {
                    DataAccess.Update(Id, textBoxTitle.Text, textBoxDescription.Text, todo.Inventory);
                }


                itemBar.Update(textBoxTitle.Text);

                DetailsGrid.Children.Add(SnackbarControl.OpenSnackBar("Saved"));
            }

        }



        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            DataAccess.RemoveAt(Id, todo.Inventory);

            todo.RemoveItemBar(Id);

            todo.CloseDetailsPanel();
        }


        private void ChangeReminderState()
        {
            switch (reminderMode)
            {
                case 0:

                    StpReminder.Visibility = Visibility.Collapsed;

                    break;

                case 1:

                    StpReminder.Visibility = Visibility.Visible;

                    BasicReminderField.Visibility = Visibility.Visible;
                    AdvanceRemidnerField.Visibility = Visibility.Collapsed;

                    ReminderTitleGrid.Margin = new Thickness(10, 30, 10, 5);

                    EndDatePickerBasic.Text = $"{DateTime.Now.Month}/{DateTime.Now.Day + 1}/{DateTime.Now.Year}";
                    EndTimePickerBasic.Text = "12:00 AM";

                    break;

                case 2:

                    StpReminder.Visibility = Visibility.Visible;

                    BasicReminderField.Visibility = Visibility.Collapsed;
                    AdvanceRemidnerField.Visibility = Visibility.Visible;

                    BeginDatePicker.Text = $"{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Year}";
                    BeginTimePicker.Text = "12:00 AM";

                    EndDatePicker.Text = $"{DateTime.Now.Month}/{DateTime.Now.Day + 1}/{DateTime.Now.Year}";
                    EndTimePicker.Text = "12:00 AM";

                    break;

                default:
                    break;
            }
        }


        private void DarkGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed || Mouse.XButton1 == MouseButtonState.Pressed)
            {
                todo.CloseDetailsPanel();
            }
        }

        private void Chip_DeleteClick(object sender, RoutedEventArgs e)
        {
            Chip chip = (Chip)sender;

            chip.Visibility = Visibility.Collapsed;
        }

        private void ChipTitleEditTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            DataAccess.Update(Id, ChipTitleEditTextbox.Text, todo.Inventory);
        }

        int reminderMode = 0;
        private void ReminderBasicButton_Click(object sender, RoutedEventArgs e)
        {
            reminderMode = 1;
            ChangeReminderButtonMode(reminderMode);
        }

        private void ReminderNoneButton_Click(object sender, RoutedEventArgs e)
        {
            reminderMode = 0;
            ChangeReminderButtonMode(reminderMode);
        }

        private void ReminderAdvanceButton_Click(object sender, RoutedEventArgs e)
        {
            reminderMode = 2;
            ChangeReminderButtonMode(reminderMode);
        }

        private void ChangeReminderButtonMode(int i)
        {
            switch (i)
            {
                case 0:
                    ReminderBasicButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFFF"));
                    ReminderNoneButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
                    ReminderAdvanceButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFFF"));

                    ReminderBasicButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
                    ReminderNoneButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFFF"));
                    ReminderAdvanceButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
                    break;
                case 1:
                    ReminderBasicButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
                    ReminderNoneButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFFF"));
                    ReminderAdvanceButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFFF"));

                    ReminderBasicButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFFF"));
                    ReminderNoneButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
                    ReminderAdvanceButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
                    break;
                case 2:
                    ReminderBasicButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFFF"));
                    ReminderNoneButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFFF"));
                    ReminderAdvanceButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));

                    ReminderBasicButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
                    ReminderNoneButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
                    ReminderAdvanceButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFFF"));
                    break;
                default:
                    break;
            }

            ChangeReminderState();
        }
    }
}
