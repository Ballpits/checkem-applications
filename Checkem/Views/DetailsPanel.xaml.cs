using Checkem.CustomComponents;
using Checkem.Models;
using System;
using Sphere.Readable;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;

namespace Checkem.Views
{
    public partial class DetailsPanel : UserControl, INotifyPropertyChanged
    {
        public DetailsPanel(Itembar itembar)
        {
            this.DataContext = this;

            //copy item bar
            this.itembar = itembar;

            //get item bar's todo properties
            this.todo = itembar.todo;

            InitializeComponent();

            TryLoadTagState();

            CheckReminderState();
        }


        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Close;
        #endregion


        #region Variable
        private Itembar itembar;

        private bool ReminderFirstSetup = true;
        #endregion


        #region Property
        public Todo todo = new Todo();

        //I was Tring to Update the TodoItem After it have a tag 
        TodoManager todoManager = new TodoManager();
        
        TagManager tagManager = new TagManager();
        public string Title
        {
            get
            {
                return todo.Title;
            }
            set
            {
                //this will prevent user from saving task without title
                if (value != string.Empty)
                {
                    if (todo.Title != value)
                    {
                        todo.Title = value;

                        //update item bar's title text block's text
                        itembar.Update_Title();

                        OnPropertyChanged();
                    }
                }
            }
        }

        public string Description
        {
            get
            {
                return todo.Description;
            }
            set
            {
                if (todo.Description != value)
                {
                    todo.Description = value;

                    itembar.Update_Description();
                    OnPropertyChanged();
                }
            }
        }

        public bool IsCompleted
        {
            get
            {
                return todo.IsCompleted;
            }
            set
            {
                if (todo.IsCompleted != value)
                {
                    todo.IsCompleted = value;

                    //update completion check box's check state in item bar
                    itembar.Update_IsCompleted();
                    OnPropertyChanged();
                }
            }
        }

        public bool IsStarred
        {
            get
            {
                return todo.IsStarred;
            }
            set
            {
                if (todo.IsStarred != value)
                {
                    todo.IsStarred = value;

                    //update star toggle's check state in item bar
                    itembar.Update_IsStarred();
                    OnPropertyChanged();
                }
            }
        }

        public bool IsReminderOn
        {
            get
            {
                return todo.IsReminderOn;
            }
            set
            {
                if (todo.IsReminderOn != value)
                {
                    todo.IsReminderOn = value;

                    OnPropertyChanged();
                }
            }
        }

        public bool IsAdvanceReminderOn
        {
            get
            {
                return todo.IsAdvanceReminderOn;
            }
            set
            {
                if (todo.IsAdvanceReminderOn != value)
                {
                    todo.IsAdvanceReminderOn = value;

                    OnPropertyChanged();
                }
            }
        }

        public string CreationDateTime
        {
            get
            {
                return $"Created on {DateTimeManipulator.SimplifiedDate(todo.CreationDateTime)}";
            }
        }
        #endregion


        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private void StoryBoard_Completed(object sender, EventArgs e)
        {
            Close?.Invoke(this, EventArgs.Empty);
        }



        #region Check reminder state
        private void CheckReminderState()
        {
            /* check if reminder is on, if it's on, check if advance reminder is on to determine reminder stete
             * 
             * IsReminder is false and IsAdvanceReminderOn is false => no reminder
             * IsReminder is true and IsAdvanceReminderOn is false  => basic reminder
             * IsReminder is true and IsAdvanceReminderOn is true   => advance reminder
             */

            if (IsReminderOn)
            {
                if (!IsAdvanceReminderOn)
                {
                    //show basic reminder's date time in date time picker
                    EndDatePicker.Text = $"{this.todo.EndDateTime.Value.Month}/{this.todo.EndDateTime.Value.Day}/{this.todo.EndDateTime.Value.Year}";
                    EndTimePicker.Text = $"{string.Format("{0:h:mm tt}", this.todo.EndDateTime)}";
                    EndTimePicker.SelectedTime = Convert.ToDateTime(EndTimePicker.Text);

                    TrySetEndDateTime();
                    ReminderFirstSetup = false;

                    SetReminder(ReminderState.Basic);
                }
                else
                {
                    //show advance reminder's date time in date time picker
                    BeginDatePicker.Text = $"{this.todo.BeginDateTime.Value.Month}/{this.todo.BeginDateTime.Value.Day}/{this.todo.BeginDateTime.Value.Year}";
                    BeginTimePicker.Text = $"{string.Format("{0:h:mm tt}", this.todo.BeginDateTime)}";
                    BeginTimePicker.SelectedTime = Convert.ToDateTime(BeginTimePicker.Text);

                    EndDatePicker.Text = $"{this.todo.EndDateTime.Value.Month}/{this.todo.EndDateTime.Value.Day}/{this.todo.EndDateTime.Value.Year}";
                    EndTimePicker.Text = $"{string.Format("{0:h:mm tt}", this.todo.EndDateTime)}";
                    EndTimePicker.SelectedTime = Convert.ToDateTime(EndTimePicker.Text);

                    TrySetBeginDateTime();
                    TrySetEndDateTime();
                    ReminderFirstSetup = false;


                    SetReminder(ReminderState.Advance);
                }
            }
        }
        #endregion

        #region Set Tag state
        // I feel I need to improve it,but right now I cannot tell why
        //Load all choices from Tag.json
        private void TryLoadTagState()
        {
            if (tagManager.Inventory != null)
            {
                foreach (TagItem items in tagManager.Inventory)
                {
                    TagComBox.Items.Add(new CustomComponents.PreviewTag(items));
                }
            }

            //Tried to load tag from Todo 
            if (itembar.TagItems != null)
            {
                TagComBox.SelectedItem = itembar.TagItems[0];
            }
        }

        #endregion

        #region Set reminder picker visibility
        private void SetReminder(ReminderState reminderState)
        {
            //show the corresponding date time picker 
            switch (reminderState)
            {
                case ReminderState.None:
                    {
                        IsReminderOn = false;
                        IsAdvanceReminderOn = false;

                        todo.BeginDateTime = null;
                        todo.EndDateTime = null;

                        BeginDateTimeField.Visibility = Visibility.Collapsed;
                        EndDateTimeField.Visibility = Visibility.Collapsed;

                        ReminderSelecter.SelectedIndex = 2;

                        itembar.Update_Reminder();

                        break;
                    }
                case ReminderState.Basic:
                    {
                        IsReminderOn = true;
                        IsAdvanceReminderOn = false;

                        BeginDateTimeField.Visibility = Visibility.Collapsed;
                        EndDateTimeField.Visibility = Visibility.Visible;

                        ReminderSelecter.SelectedIndex = 0;

                        break;
                    }
                case ReminderState.Advance:
                    {
                        IsReminderOn = true;
                        IsAdvanceReminderOn = true;

                        BeginDateTimeField.Visibility = Visibility.Visible;
                        EndDateTimeField.Visibility = Visibility.Visible;

                        ReminderSelecter.SelectedIndex = 1;

                        break;
                    }
                default:
                    break;
            }
        }
        #endregion



        #region Set date time value
        private void TrySetBeginDateTime()
        {
            try
            {
                string dateTimeString = BeginDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd") + "T" + BeginTimePicker.SelectedTime.Value.ToString("hh:mm:ss");
                todo.BeginDateTime = Convert.ToDateTime(dateTimeString);

                itembar.Update_Reminder();

                BeginDateTimeWarning.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {
                BeginDateTimeWarning.Visibility = Visibility.Visible;
            }
        }

        private void TrySetEndDateTime()
        {
            try
            {
                string dateTimeString = EndDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd") + "T" + EndTimePicker.SelectedTime.Value.ToString("hh:mm:ss");
                todo.EndDateTime = Convert.ToDateTime(dateTimeString);

                itembar.Update_Reminder();

                EndDateTimeWarning.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {
                EndDateTimeWarning.Visibility = Visibility.Visible;
            }
        }
        #endregion



        #region Reminder selector events
        private void ListBoxItem_NoReminder_Selected(object sender, System.Windows.RoutedEventArgs e)
        {
            SetReminder(ReminderState.None);
        }

        private void ListBoxItem_BasicReminder_Selected(object sender, System.Windows.RoutedEventArgs e)
        {
            SetReminder(ReminderState.Basic);
        }

        private void ListBoxItem_AdvanceReminder_Selected(object sender, System.Windows.RoutedEventArgs e)
        {
            SetReminder(ReminderState.Advance);
        }
        #endregion



        #region Date time picker value changed event handlers
        private void BeginDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!ReminderFirstSetup)
            {
                TrySetBeginDateTime();
            }
        }

        private void BeginTimePicker_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if (!ReminderFirstSetup)
            {
                TrySetBeginDateTime();
            }
        }

        private void EndDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!ReminderFirstSetup)
            {
                TrySetEndDateTime();
            }
        }

        private void EndTimePicker_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if (!ReminderFirstSetup)
            {
                TrySetEndDateTime();
            }
        }
        #endregion

        private void BeginDatePicker_LostFocus(object sender, RoutedEventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show("Test");
        }
        //Due to itembar.TagItem or Todo.TagItems didn't
        //get a actual data(get;set;) so it cannot use "Add" or
        //"itembar.TagItems[x] = TagItem[y]"
        // I guseed you already knew it but I still want to make a note
        List<TagItem> Test = new List<TagItem>();

        
        private void TagComBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //example

            //Dispose the choice (I don't how to say it... so I use a random word
            if (TagComBox.SelectedIndex == 0 && itembar.TagItems != null)
                itembar.TagItems.RemoveAt(0);
            
            //Tried to save tag choice and 
            if (itembar.TagItems == null && TagComBox.SelectedIndex != 0)
            {
                //itembar.Add(tagManager.Inventory[TagComBox.SelectedIndex-1]);
                Test.Add(tagManager.Inventory.Find(x => x.ID == TagComBox.SelectedIndex - 1));
                itembar.TagItems =Test;
            }
        }
    }
}
