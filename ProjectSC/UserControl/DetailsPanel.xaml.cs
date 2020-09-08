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
        public DetailsPanel(ToDoListUSC todolist)
        {
            InitializeComponent();

            todo = todolist;
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

                reminderMode = 0;
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
                        BeginDatePicker_Advance.Text = $"{BeginDateTime.Month}/{BeginDateTime.Day}/{BeginDateTime.Year}";
                        BeginTimePicker_Advance.Text = $"{string.Format("{0:h:mm tt}", BeginDateTime)}";

                        EndDatePicker_Advance.Text = $"{EndDateTime.Month}/{EndDateTime.Day}/{EndDateTime.Year}";
                        EndTimePicker_Advance.Text = $"{string.Format("{0:h:mm tt}", EndDateTime)}";
                    }
                    else
                    {
                        EndDatePicker_Basic.Text = $"{EndDateTime.Month}/{EndDateTime.Day}/{EndDateTime.Year}";
                        EndTimePicker_Basic.Text = $"{string.Format("{0:h:mm tt}", EndDateTime)}";
                    }
                }//Reminder texts

                if (IsUsingTag)
                {
                    ChipGrid.Height = 50;

                    TagChip.Visibility = Visibility.Visible;

                    ChipTitleEditTextbox.Text = TagName;
                }//Tag texts
            }

            ChangeReminderButtonMode(reminderMode);
            ChangeReminderState();

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
                    if (reminderMode == 2)
                    {
                        JsonDataAccess.AddNew(textBoxTitle.Text, textBoxDescription.Text, Convert.ToDateTime(BeginDatePicker_Advance.Text + " " + BeginTimePicker_Advance.Text), Convert.ToDateTime(EndDatePicker_Advance.Text + " " + EndTimePicker_Advance.Text), DateTime.Now, todo.Inventory);
                    }
                    else
                    {
                        JsonDataAccess.AddNew(textBoxTitle.Text, textBoxDescription.Text, Convert.ToDateTime(EndDatePicker_Basic.Text + " " + EndTimePicker_Basic.Text), DateTime.Now, todo.Inventory);
                    }
                }
                else
                {
                    JsonDataAccess.AddNew(textBoxTitle.Text, textBoxDescription.Text, DateTime.Now, todo.Inventory);
                }


                JsonDataAccess.RetrieveData(ref todo.Inventory);
                JsonDataAccess.ResetId(todo.Inventory);

                todo.AddItemBar();

                //DetailsGrid.Children.Add(SnackbarControl.OpenSnackBar("Incorrect format !"));
                todo.CloseDetailsPanel("Added");
            }
            else
            {
                if (reminderMode != 0)
                {
                    if (reminderMode == 2)
                    {
                        JsonDataAccess.Update(Id, textBoxTitle.Text, textBoxDescription.Text, Convert.ToDateTime(BeginDatePicker_Advance.Text + " " + BeginTimePicker_Advance.Text), Convert.ToDateTime(EndDatePicker_Advance.Text + " " + EndTimePicker_Advance.Text), todo.Inventory);
                    }
                    else
                    {
                        JsonDataAccess.Update(Id, textBoxTitle.Text, textBoxDescription.Text, Convert.ToDateTime(EndDatePicker_Basic.Text + " " + EndTimePicker_Basic.Text), todo.Inventory);
                    }
                }
                else
                {
                    JsonDataAccess.Update(Id, textBoxTitle.Text, textBoxDescription.Text, todo.Inventory);
                }


                itemBar.Update(textBoxTitle.Text);

                DetailsGrid.Children.Add(SnackbarControl.OpenSnackBar("Saved"));
            }

        }



        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            JsonDataAccess.RemoveAt(Id, todo.Inventory);

            todo.RemoveItemBar(itemBar);

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

                    EndDatePicker_Basic.Text = $"{DateTime.Now.Month}/{DateTime.Now.Day + 1}/{DateTime.Now.Year}";
                    EndTimePicker_Basic.Text = "12:00 AM";

                    break;

                case 2:

                    StpReminder.Visibility = Visibility.Visible;

                    BasicReminderField.Visibility = Visibility.Collapsed;
                    AdvanceRemidnerField.Visibility = Visibility.Visible;

                    BeginDatePicker_Advance.Text = $"{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Year}";
                    BeginTimePicker_Advance.Text = "12:00 AM";

                    EndDatePicker_Advance.Text = $"{DateTime.Now.Month}/{DateTime.Now.Day + 1}/{DateTime.Now.Year}";
                    EndTimePicker_Advance.Text = "12:00 AM";

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
            JsonDataAccess.Update(Id, ChipTitleEditTextbox.Text, todo.Inventory);
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

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            MouseoverHighlight.Highlight(sender, "#FFF0F0F0");
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            MouseoverHighlight.Highlight(sender, "#FFFFFFFF");
        }
    }
}
