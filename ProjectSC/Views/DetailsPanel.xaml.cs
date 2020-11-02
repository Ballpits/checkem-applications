using MaterialDesignThemes.Wpf;
using ProjectSC.Models.DataAccess;
using ProjectSC.Models.ToDo;
using ProjectSC.ViewModels.SnackBar;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectSC.Views
{
    public partial class DetailsPanel : UserControl
    {
        public DetailsPanel(ToDoList todolist, TagList taglist)
        {
            InitializeComponent();

            tagList = taglist;
            ToDoList = todolist;
        }

        public DetailsPanel(ToDoList todolist, ItemBar itembar, TagList taglist)
        {
            InitializeComponent();

            ToDoList = todolist;
            itemBar = itembar;
            tagList = taglist;
        }

        #region Properties
        public bool IsNew { get; set; } = false;

        public ToDoItem itemProperties { get; set; }
        #endregion


        private DataAccess_Json dataAccess = new DataAccess_Json();
        private ToDoList ToDoList;
        private ItemBar itemBar;
        private TagList tagList;

        #region Save
        private void Save()
        {
            if (IsNew)
            {
                ToDoItem toDoItem = new ToDoItem()
                {
                    Id = ToDoList.Inventory.Count,
                    Title = textBoxTitle.Text,
                    Description = textBoxDescription.Text,
                };

                dataAccess.AddNew(toDoItem, ToDoList.Inventory);

                ToDoList.AddItemBar();
            }
            else
            {
                this.itemProperties.Title = textBoxTitle.Text;
                this.itemProperties.Description = textBoxDescription.Text;

                if (this.itemProperties.IsReminderOn)
                {
                    itemBar.itemProperties.IsReminderOn = true;

                    if (this.itemProperties.IsAdvanceReminderOn)
                    {
                        itemBar.itemProperties.IsAdvanceReminderOn = true;

                        this.itemProperties.BeginDateTime = Convert.ToDateTime(BeginDatePicker_Advance.Text + " " + BeginTimePicker_Advance.Text);
                        this.itemProperties.EndDateTime = Convert.ToDateTime(EndDatePicker_Advance.Text + " " + EndTimePicker_Advance.Text);
                    }
                    else
                    {
                        this.itemProperties.EndDateTime = Convert.ToDateTime(EndDatePicker_Basic.Text + " " + EndTimePicker_Basic.Text);
                    }
                }

                dataAccess.Update(this.itemProperties, ToDoList.Inventory);

                itemBar.itemProperties.Title = textBoxTitle.Text;
                itemBar.itemProperties.Description = textBoxDescription.Text;

                itemBar.Update(itemBar);


                switch (Properties.Settings.Default.DetailsPanelClosingMode)
                {
                    case 0:
                        ToDoList.CloseDetailsPanel();
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
                textBoxTitle.Focus();

                SetReminderState(0);
            }
            else
            {
                textBoxTitle.Text = this.itemProperties.Title;
                textBoxDescription.Text = this.itemProperties.Description;

                if (this.itemProperties.IsReminderOn)
                {
                    if (this.itemProperties.IsAdvanceReminderOn)
                    {
                        SetReminderState(2);


                        BeginDatePicker_Advance.Text = $"{this.itemProperties.BeginDateTime.Value.Month}/{this.itemProperties.BeginDateTime.Value.Day}/{this.itemProperties.BeginDateTime.Value.Year}";
                        BeginTimePicker_Advance.Text = $"{string.Format("{0:h:mm tt}", this.itemProperties.BeginDateTime)}";

                        EndDatePicker_Advance.Text = $"{this.itemProperties.EndDateTime.Value.Month}/{this.itemProperties.EndDateTime.Value.Day}/{this.itemProperties.EndDateTime.Value.Year}";
                        EndTimePicker_Advance.Text = $"{string.Format("{0:h:mm tt}", this.itemProperties.EndDateTime)}";
                    }
                    else
                    {
                        SetReminderState(1);


                        EndDatePicker_Basic.Text = $"{this.itemProperties.EndDateTime.Value.Month}/{this.itemProperties.EndDateTime.Value.Day}/{this.itemProperties.EndDateTime.Value.Year}";
                        EndTimePicker_Basic.Text = $"{string.Format("{0:h:mm tt}", this.itemProperties.EndDateTime)}";
                    }
                }
                else
                {
                    SetReminderState(0);
                }
            }

            //LoadTagChoice();
            //LoadUsingTag();
        }

        //private void ViewTag_Click(object sender, RoutedEventArgs e)
        //{
        //    Button viewTagbtn = sender as Button;
        //    dataAccess.Update(Id, tagList.tagInventory[tagList.tagInventory.FindIndex(x => x.Text == (string)viewTagbtn.Content)], ToDoList.Inventory);
        //    dataAccess.RetrieveData(ref ToDoList.Inventory);
        //    StpSelectTag.Children.Add(new Button()
        //    {
        //        Style = FindResource("TagButton") as Style,
        //        Name = "Tag_" + ToDoList.Inventory[Id].TagText,
        //        Content = ToDoList.Inventory[Id].TagText,
        //        FontFamily = new System.Windows.Media.FontFamily("Arial"),
        //        FontSize = 16
        //    });
        //}

        ////Load All The Tag Into PopBox
        //private void LoadTagChoice()
        //{
        //    foreach (var tagitem in tagList.tagInventory)
        //    {
        //        Button tagviewbtn = new Button()
        //        {
        //            Content = tagitem.Text,
        //            FontFamily = new System.Windows.Media.FontFamily("Arial"),
        //            FontSize = 16,
        //            Name = "Tag_" + tagitem.Text
        //        };
        //        tagviewbtn.Click += new RoutedEventHandler(ViewTag_Click);
        //        StpTagPopup.Children.Add(tagviewbtn);
        //    }
        //}

        ////Load the Tag that use on this ItemBar
        //private void LoadUsingTag()
        //{
        //    if (this.IsUsingTag)
        //    {
        //        StpSelectTag.Children.Add(new Button()
        //        {
        //            Style = FindResource("TagButton") as Style,
        //            Name = "Tag_" + this.TagText,
        //            Content = this.TagText,
        //            FontFamily = new System.Windows.Media.FontFamily("Arial"),
        //            FontSize = 16
        //        });
        //    }
        //}


        #region Button click event
        private void RetunButton_Click(object sender, RoutedEventArgs e)
        {
            ToDoList.CloseDetailsPanel();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            dataAccess.Remove(this.itemProperties, ToDoList.Inventory);

            ToDoList.RemoveItemBar(itemBar);

            ToDoList.CloseDetailsPanel("Removed");
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

                    this.itemProperties.IsReminderOn = false;
                    this.itemProperties.IsAdvanceReminderOn = false;
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

                    this.itemProperties.IsReminderOn = true;
                    this.itemProperties.IsAdvanceReminderOn = false;
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

                    this.itemProperties.IsReminderOn = true;
                    this.itemProperties.IsAdvanceReminderOn = true;
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
                ToDoList.CloseDetailsPanel();
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
