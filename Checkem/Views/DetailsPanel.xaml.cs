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

            //Copy the item bar
            this.itembar = itembar;


            //Get item bar's todo properties
            this.todo = itembar.todo;


            InitializeComponent();


            //Load in available tag choices
            LoadAvailableTagItemsChoices();


            //Load in the current item's tags
            LoadTagItems();


            ShowReminderDetails();
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
        //I was Tring to Update the TodoItem After it have a tag 
        TodoManager todoManager = new TodoManager();
        TagManager tagManager = new TagManager();


        public Todo todo = new Todo();

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

        public string CreationDateTime
        {
            get
            {
                return $"Created on {DateTimeManipulator.SimplifiedDate(todo.CreationDateTime)}";
            }
        }

        public ReminderState ReminderState
        {
            get
            {
                return itembar.ReminderState;
            }
            set
            {
                if (itembar.ReminderState != value)
                {
                    itembar.ReminderState = value;

                    SetReminderDetailsVisualState();
                }
            }
        }

        #endregion


        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private void StoryBoard_Completed(object sender, EventArgs e)
        {
            TrySetBeginDateTime();
            TrySetEndDateTime();

            Close?.Invoke(this, EventArgs.Empty);
        }


        #region Reminder

        #region Show reminder dateails
        private void ShowReminderDetails()
        {
            switch (ReminderState)
            {
                case ReminderState.None:
                    {
                        SetReminderDetailsVisualState();

                        break;
                    }
                case ReminderState.Basic:
                    {
                        SetReminderDetailsVisualState();

                        //Show basic reminder's date time in date time picker
                        EndDatePicker.Text = $"{this.todo.EndDateTime.Value.Month}/{this.todo.EndDateTime.Value.Day}/{this.todo.EndDateTime.Value.Year}";
                        EndTimePicker.SelectedTime = Convert.ToDateTime(string.Format("{0:h:mm tt}", this.todo.EndDateTime));

                        TrySetEndDateTime();

                        break;
                    }
                case ReminderState.Advance:
                    {
                        SetReminderDetailsVisualState();

                        //Show advance reminder's date time in date time picker
                        BeginDatePicker.Text = $"{this.todo.BeginDateTime.Value.Month}/{this.todo.BeginDateTime.Value.Day}/{this.todo.BeginDateTime.Value.Year}";
                        BeginTimePicker.SelectedTime = Convert.ToDateTime(string.Format("{0:h:mm tt}", this.todo.BeginDateTime));


                        EndDatePicker.Text = $"{this.todo.EndDateTime.Value.Month}/{this.todo.EndDateTime.Value.Day}/{this.todo.EndDateTime.Value.Year}";
                        EndTimePicker.SelectedTime = Convert.ToDateTime(string.Format("{0:h:mm tt}", this.todo.EndDateTime));

                        TrySetBeginDateTime();
                        TrySetEndDateTime();

                        break;
                    }
                default:
                    break;
            }

            ReminderFirstSetup = false;
        }
        #endregion



        #region Set reminder details visual state
        private void SetReminderDetailsVisualState()
        {
            BeginDateTimeWarningTextBlock.Visibility = Visibility.Collapsed;
            EndDateTimeWarningTextBlock.Visibility = Visibility.Collapsed;


            #region Prevent empty entries
            if (BeginDatePicker.SelectedDate != null && todo.BeginDateTime.HasValue)
            {
                BeginDatePicker.SelectedDate = todo.BeginDateTime.Value;
            }
            else
            {
                BeginDatePicker.SelectedDate = DateTime.Now.Date;
            }


            if (BeginTimePicker.SelectedTime != null && todo.BeginDateTime.HasValue)
            {
                BeginTimePicker.SelectedTime = todo.BeginDateTime.Value;
            }
            else
            {
                BeginTimePicker.SelectedTime = DateTime.Now.Date;
            }


            if (EndDatePicker.SelectedDate != null && todo.EndDateTime.HasValue)
            {
                EndDatePicker.SelectedDate = todo.EndDateTime.Value;
            }
            else
            {
                EndDatePicker.SelectedDate = DateTime.Now.Date;
            }


            if (EndTimePicker.SelectedTime != null && todo.EndDateTime.HasValue)
            {
                EndTimePicker.SelectedTime = todo.EndDateTime.Value;
            }
            else
            {
                EndTimePicker.SelectedTime = DateTime.Now.Date;
            }
            #endregion


            switch (ReminderState)
            {
                case ReminderState.None:
                    {
                        ReminderSelecter.SelectedIndex = 2;

                        BeginDateTimeField.Visibility = Visibility.Collapsed;
                        EndDateTimeField.Visibility = Visibility.Collapsed;

                        break;
                    }
                case ReminderState.Basic:
                    {
                        ReminderSelecter.SelectedIndex = 0;

                        BeginDateTimeField.Visibility = Visibility.Collapsed;
                        EndDateTimeField.Visibility = Visibility.Visible;

                        break;
                    }
                case ReminderState.Advance:
                    {
                        ReminderSelecter.SelectedIndex = 1;

                        BeginDateTimeField.Visibility = Visibility.Visible;
                        EndDateTimeField.Visibility = Visibility.Visible;

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
                string dateTimeString = BeginDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd") + "T" + BeginTimePicker.SelectedTime.Value.ToString("HH:mm:ss");
                todo.BeginDateTime = Convert.ToDateTime(dateTimeString);

                itembar.Update_Reminder();

                BeginDateTimeWarningTextBlock.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {
                BeginDateTimeWarningTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void TrySetEndDateTime()
        {
            try
            {
                string dateTimeString = EndDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd") + "T" + EndTimePicker.SelectedTime.Value.ToString("HH:mm:ss");
                todo.EndDateTime = Convert.ToDateTime(dateTimeString);

                itembar.Update_Reminder();

                EndDateTimeWarningTextBlock.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {
                EndDateTimeWarningTextBlock.Visibility = Visibility.Visible;
            }
        }
        #endregion



        #region Reminder selector events
        private void ListBoxItem_NoReminder_Selected(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ReminderState = ReminderState.None;
        }

        private void ListBoxItem_BasicReminder_Selected(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ReminderState = ReminderState.Basic;
        }

        private void ListBoxItem_AdvanceReminder_Selected(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ReminderState = ReminderState.Advance;
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
            DateTime? dateTime = EndTimePicker.SelectedTime;
            //DateTime? dateTime2 = EndTimePicker.SelectedTime;
            if (!ReminderFirstSetup)
            {
                TrySetEndDateTime();
            }
        }
        #endregion

        #endregion



        #region Load available tag items coices
        //Load all available tag choices from Tag.json
        private void LoadAvailableTagItemsChoices()
        {
            if (tagManager.Inventory != null)
            {
                foreach (TagItem item in tagManager.Inventory)
                {
                    TagItemCombobox.Items.Add(new PreviewTag(item));
                }
            }
        }


        //Load in the current item's tags
        private void LoadTagItems()
        {
            if (todo.TagItems != null)
            {
                foreach (var item in todo.TagItems)
                {
                    TagDisplay.Children.Add(new PreviewTag(item));
                }
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

        private void TagComBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TagItemCombobox.SelectedItem.GetType().Name == "PreviewTag")
            {
                //MessageBox.Show(TagItemCombobox.SelectedItem.GetType().Name);
                PreviewTag tag = TagItemCombobox.SelectedItem as PreviewTag;
                TagItem tagItem = tag.tagItem;

                todo.TagItems.Add(tagItem);
                LoadTag();

                TagItemCombobox.SelectedIndex = 0;
                TagItemCombobox.Items.Remove(tag);
            }
            itembar.LoadTagItems();
        }

        private void LoadTag()
        {
            TagDisplay.Children.Clear();
            foreach (var item in todo.TagItems)
            {
                PreviewTag previewTag = new PreviewTag(item);
                previewTag.tagItem = item;

                TagDisplay.Children.Add(previewTag);
            }
        }
    }
}
