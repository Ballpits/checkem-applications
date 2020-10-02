using MaterialDesignThemes.Wpf;
using ProjectSC.Model.DataAccess;
using ProjectSC.ViewModels.SnackBar;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectSC.View
{
    public partial class DetailsPanel_View : UserControl
    {
        public DetailsPanel_View(ToDoList_View todolist)
        {
            InitializeComponent();

            todo = todolist;
        }

        public DetailsPanel_View(ToDoList_View todolist, ItemBar_View itembar)
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


        private DataAccess_Json dataAccess = new DataAccess_Json();

        ToDoList_View todo;
        ItemBar_View itemBar;

        #region Save
        private void Save()
        {
            if (IsNew)
            {
                if (IsReminderOn)
                {
                    if (IsAdvanceReminderOn)
                    {
                        dataAccess.AddNew(textBoxTitle.Text, textBoxDescription.Text, Convert.ToDateTime(BeginDatePicker_Advance.Text + " " + BeginTimePicker_Advance.Text), Convert.ToDateTime(EndDatePicker_Advance.Text + " " + EndTimePicker_Advance.Text), DateTime.Now, todo.Inventory);
                    }
                    else
                    {
                        dataAccess.AddNew(textBoxTitle.Text, textBoxDescription.Text, Convert.ToDateTime(EndDatePicker_Basic.Text + " " + EndTimePicker_Basic.Text), DateTime.Now, todo.Inventory);
                    }
                }
                else
                {
                    dataAccess.AddNew(textBoxTitle.Text, textBoxDescription.Text, DateTime.Now, todo.Inventory);
                }


                dataAccess.RetrieveData(ref todo.Inventory);
                dataAccess.ResetId(todo.Inventory);

                todo.AddItemBar();

                //DetailsGrid.Children.Add(SnackbarControl.OpenSnackBar("Incorrect format !"));
                todo.CloseDetailsPanel("Added");
            }
            else
            {
                if (IsReminderOn)
                {
                    if (IsAdvanceReminderOn)
                    {
                        dataAccess.Update(Id, textBoxTitle.Text, textBoxDescription.Text, Convert.ToDateTime(BeginDatePicker_Advance.Text + " " + BeginTimePicker_Advance.Text), Convert.ToDateTime(EndDatePicker_Advance.Text + " " + EndTimePicker_Advance.Text), todo.Inventory);

                        itemBar.BeginDateTime = Convert.ToDateTime(BeginDatePicker_Advance.Text + " " + BeginTimePicker_Advance.Text);
                        itemBar.EndDateTime = Convert.ToDateTime(EndDatePicker_Advance.Text + " " + EndTimePicker_Advance.Text);
                    }
                    else
                    {
                        dataAccess.Update(Id, textBoxTitle.Text, textBoxDescription.Text, Convert.ToDateTime(EndDatePicker_Basic.Text + " " + EndTimePicker_Basic.Text), todo.Inventory);

                        itemBar.EndDateTime = Convert.ToDateTime(EndDatePicker_Basic.Text + " " + EndTimePicker_Basic.Text);
                    }

                }
                else
                {
                    dataAccess.Update(Id, textBoxTitle.Text, textBoxDescription.Text, todo.Inventory);
                }

                itemBar.Title = textBoxTitle.Text;
                itemBar.Description = textBoxDescription.Text;
                itemBar.IsReminderOn = IsReminderOn;
                itemBar.IsAdvanceReminderOn = IsAdvanceReminderOn;

                itemBar.Update(itemBar);


                switch (Properties.Settings.Default.DetailsPanelClosingMode)
                {
                    case 0:
                        todo.CloseDetailsPanel();
                        break;

                    case 1:
                        DetailsGrid.Children.Add(SnackbarController.OpenSnackBar("Saved"));
                        break;

                    default:
                        break;
                }
            }
        }
        #endregion

        private void DarkGrid_Loaded(object sender, RoutedEventArgs e)
        {
            DetailsGrid.Focus();

            if (IsNew)
            {
                RemoveButton.IsEnabled = false;
                RemoveButton.Visibility = Visibility.Hidden;

                SetReminderState(0);
            }
            else
            {
                textBoxTitle.Text = Title;
                textBoxDescription.Text = Description;

                if (IsReminderOn)
                {
                    if (IsAdvanceReminderOn)
                    {
                        SetReminderState(2);


                        BeginDatePicker_Advance.Text = $"{BeginDateTime.Month}/{BeginDateTime.Day}/{BeginDateTime.Year}";
                        BeginTimePicker_Advance.Text = $"{string.Format("{0:h:mm tt}", BeginDateTime)}";

                        EndDatePicker_Advance.Text = $"{EndDateTime.Month}/{EndDateTime.Day}/{EndDateTime.Year}";
                        EndTimePicker_Advance.Text = $"{string.Format("{0:h:mm tt}", EndDateTime)}";
                    }
                    else
                    {
                        SetReminderState(1);


                        EndDatePicker_Basic.Text = $"{EndDateTime.Month}/{EndDateTime.Day}/{EndDateTime.Year}";
                        EndTimePicker_Basic.Text = $"{string.Format("{0:h:mm tt}", EndDateTime)}";
                    }
                }
                else
                {
                    SetReminderState(0);
                }
            }
        }



        #region Button click event
        private void RetunButton_Click(object sender, RoutedEventArgs e)
        {
            todo.CloseDetailsPanel();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            dataAccess.RemoveAt(Id, todo.Inventory);

            todo.RemoveItemBar(itemBar);

            todo.CloseDetailsPanel("Removed");
        }
        #endregion

        #region Chip function
        private void Chip_DeleteClick(object sender, RoutedEventArgs e)
        {
            Chip chip = (Chip)sender;

            chip.Visibility = Visibility.Collapsed;
        }

        private void ChipTitleEditTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            //JsonDataAccess.Update(Id, ChipTitleEditTextbox.Text, todo.Inventory);
        }
        #endregion

        #region Reminder selector funtion
        private void ReminderBasicButton_Click(object sender, RoutedEventArgs e)
        {
            SetReminderState(1);
        }

        private void ReminderNoneButton_Click(object sender, RoutedEventArgs e)
        {
            SetReminderState(0);
        }

        private void ReminderAdvanceButton_Click(object sender, RoutedEventArgs e)
        {
            SetReminderState(2);
        }

        private void SetReminderState(int mode)
        {
            switch (mode)
            {
                case 0://no reminder
                    ReminderBasicButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFFF"));
                    ReminderNoneButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
                    ReminderAdvanceButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFFF"));

                    ReminderBasicButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
                    ReminderNoneButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFFF"));
                    ReminderAdvanceButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));


                    StpReminder.Visibility = Visibility.Collapsed;

                    IsReminderOn = false;
                    IsAdvanceReminderOn = false;
                    break;

                case 1://basic reminder
                    ReminderBasicButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
                    ReminderNoneButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFFF"));
                    ReminderAdvanceButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFFF"));

                    ReminderBasicButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFFF"));
                    ReminderNoneButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
                    ReminderAdvanceButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));


                    StpReminder.Visibility = Visibility.Visible;

                    BasicReminderField.Visibility = Visibility.Visible;
                    AdvanceRemidnerField.Visibility = Visibility.Collapsed;

                    ReminderTitleGrid.Margin = new Thickness(10, 30, 10, 5);

                    EndDatePicker_Basic.Text = $"{DateTime.Now.Month}/{DateTime.Now.Day + 1}/{DateTime.Now.Year}";
                    EndTimePicker_Basic.Text = "12:00 AM";

                    IsReminderOn = true;
                    IsAdvanceReminderOn = false;
                    break;

                case 2://advance reminder
                    ReminderBasicButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFFF"));
                    ReminderNoneButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFFF"));
                    ReminderAdvanceButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));

                    ReminderBasicButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
                    ReminderNoneButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
                    ReminderAdvanceButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFFF"));


                    StpReminder.Visibility = Visibility.Visible;

                    BasicReminderField.Visibility = Visibility.Collapsed;
                    AdvanceRemidnerField.Visibility = Visibility.Visible;

                    BeginDatePicker_Advance.Text = $"{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Year}";
                    BeginTimePicker_Advance.Text = "12:00 AM";

                    EndDatePicker_Advance.Text = $"{DateTime.Now.Month}/{DateTime.Now.Day + 1}/{DateTime.Now.Year}";
                    EndTimePicker_Advance.Text = "12:00 AM";

                    IsReminderOn = true;
                    IsAdvanceReminderOn = true;
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region Mouse event
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            //Button button = (Button)sender;
            //MouseoverHighlight.Highlight(sender, "#FFF0F0F0");
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            //Button button = (Button)sender;
            //MouseoverHighlight.Highlight(sender, "#FFFFFFFF");
        }

        private void DarkGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed || Mouse.XButton1 == MouseButtonState.Pressed)
            {
                todo.CloseDetailsPanel();
            }
        }
        #endregion        

        bool IsLCtrlPressed = false;
        bool IsSPressed = false;

        private void DetailsGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
            {
                IsLCtrlPressed = true;
            }

            if (e.Key == Key.S)
            {
                IsSPressed = true;
            }

            if (IsLCtrlPressed && IsSPressed)
            {
                Save();
            }
        }

        private void DetailsGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
            {
                IsLCtrlPressed = false;
            }

            if (e.Key == Key.S)
            {
                IsSPressed = false;
            }
        }
    }
}
