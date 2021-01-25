using Checkem.Models;
using Sphere.Readable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Checkem.Windows.CustomComponents
{
    public partial class Itembar : UserControl, INotifyPropertyChanged
    {
        public Itembar()
        {
            this.DataContext = this;

            InitializeComponent();

            TitleTextBox.Visibility = Visibility.Visible;
            TitleTextBlock.Visibility = Visibility.Collapsed;
            CheckboxGrid.IsEnabled = false;
            StarToggle.IsEnabled = false;

            TitleTextBox.Focus();

            SaveNewTask?.Invoke(this, EventArgs.Empty);
        }

        public Itembar(Todo item)
        {
            this.DataContext = this;

            //Get todo properties
            this.todo = item;

            InitializeComponent();

            //Update title foregrond and check box state
            OnCompletionChanged();

            //Update star toggle state
            OnStarChanged();

            //Load in the current item's tags
            LoadTagItems();

            //Update itembar height for reminder details
            OnReminderStateChanged();
        }


        #region Event
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler Click;
        public event EventHandler SaveNewTask;
        public event EventHandler Remove;
        public event EventHandler Update;

        public event EventHandler UpdateChosenTag;
        #endregion


        #region Properties
        public Todo todo = new Todo();

        public int ID
        {
            get
            {
                return todo.ID;
            }
            set
            {
                if (todo.ID != value)
                {
                    todo.ID = value;

                    OnPropertyChanged();
                }
            }
        }

        public string Title
        {
            get
            {
                return todo.Title;
            }
            set
            {
                if (todo.Title != value)
                {
                    todo.Title = value;

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

                    //Update title foregrond and check box state
                    OnCompletionChanged();

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

                    //Update star toggle state
                    OnStarChanged();

                    OnPropertyChanged();
                }
            }
        }

        public ReminderState ReminderState
        {
            get
            {
                return todo.ReminderState;
            }
            set
            {
                if (todo.ReminderState != value)
                {
                    todo.ReminderState = value;

                    OnReminderStateChanged();
                }
            }
        }

        public List<TagItem> TagItems
        {
            get
            {
                return todo.TagItems;
            }
            set
            {
                if (todo.TagItems != value)
                {
                    todo.TagItems = value;

                    OnSelectedChange();

                    OnPropertyChanged();
                }
            }
        }
        #endregion


        #region Update property value
        //update completion check box's state than save
        public void Update_IsCompleted()
        {
            CompletionCheckBox.SetBinding(CheckBox.IsCheckedProperty, "IsCompleted");

            OnCompletionChanged();
            Update?.Invoke(this, EventArgs.Empty);
        }


        //update star toggle's check state than save
        public void Update_IsStarred()
        {
            StarToggle.SetBinding(CheckBox.IsCheckedProperty, "IsStarred");

            Update?.Invoke(this, EventArgs.Empty);
        }


        //update title text block's text than save
        public void Update_Title()
        {
            TitleTextBlock.SetBinding(TextBlock.TextProperty, "Title");

            Update?.Invoke(this, EventArgs.Empty);
        }


        //update title text block's text than save
        public void Update_Description()
        {
            Update?.Invoke(this, EventArgs.Empty);
        }


        public void Update_Reminder()
        {
            OnReminderStateChanged();

            Update?.Invoke(this, EventArgs.Empty);
        }


        //check IsCompleted, change check state than save
        private void Set_IsCompleted()
        {
            if (IsCompleted == true)
            {
                IsCompleted = false;
            }
            else
            {
                IsCompleted = true;
            }

            Update?.Invoke(this, EventArgs.Empty);
        }


        //check IsStarred value, change check state than save
        private void Set_IsStarred()
        {
            if (IsStarred == true)
            {
                IsStarred = false;
            }
            else
            {
                IsStarred = true;
            }

            Update?.Invoke(this, EventArgs.Empty);
        }


        //Check if user can enter title, if they can, set new title
        private void Set_NewTitle()
        {
            if (TitleTextBox.Visibility == Visibility.Visible)
            {
                if (TitleTextBox.Text != string.Empty)
                {
                    Title = TitleTextBox.Text;

                    TitleTextBlock.Visibility = Visibility.Visible;
                    TitleTextBox.Visibility = Visibility.Collapsed;

                    CheckboxGrid.IsEnabled = true;
                    StarToggle.IsEnabled = true;

                    SaveNewTask?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    Remove?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        #endregion


        public void LoadTagItems()
        {
            TagItemPreviewStackPanel.Children.Clear();
            if (TagItems != null)
            {
                foreach (var item in TagItems)
                {
                    PreviewTag previewTag = new PreviewTag() { Color = item.TagColor, Text = item.Content, tagItem = item };
                    previewTag.RemovePreTag += new EventHandler(this.RemovePreTag);
                    TagItemPreviewStackPanel.Children.Add(previewTag);
                }
            }
        }

        private void RemovePreTag(object sender, EventArgs e)
        {
            PreviewTag tag = sender as PreviewTag;

            todo.TagItems.RemoveAt(todo.TagItems.FindIndex(x => x.ID == tag.tagItem.ID));
            LoadTagItems();
            UpdateChosenTag?.Invoke(this, EventArgs.Empty);
        }

        #region Property Changed event handler
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void OnCompletionChanged()
        {
            if (todo.IsCompleted)
            {
                //set foregrond to dark gray and strike out title text
                TitleTextBlock.Opacity = 0.5;
                TitleTextBlock.TextDecorations = TextDecorations.Strikethrough;

                //change context menu text
                MenuItemIsCompleted.Header = this.FindResource("Dict_MarkAsNotCompleted") as string;
            }
            else
            {
                //set foregrond back to black and clear text decoration
                TitleTextBlock.Opacity = 1.0;
                TitleTextBlock.TextDecorations = null;

                //change context menu text
                MenuItemIsCompleted.Header = this.FindResource("Dict_MarkAsCompleted") as string;
            }
        }

        private void OnStarChanged()
        {
            if (todo.IsStarred)
            {
                //change context menu text
                MenuItemIsStarred.Header = this.FindResource("Dict_MarkAsNotStarred") as string;
            }
            else
            {
                //change context menu text
                MenuItemIsStarred.Header = this.FindResource("Dict_MarkAsStarred") as string;
            }
        }


        private void OnReminderStateChanged()
        {
            if (todo.ReminderState != ReminderState.None)
            {
                ReminderDetailStackPanel.Visibility = Visibility.Visible;

                ReminderDetailTextBlock.Text = "Waiting for input";
            }
            switch (todo.ReminderState)
            {
                case ReminderState.None:
                    {
                        //Hide reminder details
                        ReminderDetailStackPanel.Visibility = Visibility.Collapsed;

                        break;
                    }
                case ReminderState.Basic:
                    {
                        //Show reminder details
                        ReminderDetailStackPanel.Visibility = Visibility.Visible;

                        if (todo.EndDateTime != null)
                        {
                            ReminderDetailTextBlock.Text = DateTimeManipulator.SimplifiedDate(this.todo.EndDateTime.Value);
                        }
                        else
                        {
                            ReminderDetailTextBlock.Text = "Waiting for input";
                        }

                        break;
                    }
                case ReminderState.Advance:
                    {
                        //Show reminder details
                        ReminderDetailStackPanel.Visibility = Visibility.Visible;

                        if (todo.BeginDateTime != null && todo.EndDateTime != null)
                        {
                            ReminderDetailTextBlock.Text = $"Start on: {DateTimeManipulator.SimplifiedDate(this.todo.BeginDateTime.Value)}\tEnd on: {DateTimeManipulator.SimplifiedDate(this.todo.EndDateTime.Value)} ";
                        }
                        else
                        {
                            ReminderDetailTextBlock.Text = "Waiting for input";
                        }

                        break;
                    }
                default:
                    break;
            }

            //if (DateTimeManipulator.IsPassed(todo.EndDateTime.Value))
            //{
            //    //ReminderDetailTextBlock.SetBinding(TextBlock.ForegroundProperty, OverDueTextColorBindings);
            //}
            //else
            //{
            //    //ReminderDetailTextBlock.SetBinding(TextBlock.ForegroundProperty, NormalTextColorBindings);
            //}
        }
        private void OnSelectedChange()
        {
            LoadTagItems();
        }
        #endregion


        #region Mouse events for item bar border
        private void ItembarBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            //item bar highlight
            ItembarBorder.Background = this.FindResource("HighlightColor.Primary") as Brush;
            ItembarBackBorder.BorderBrush = this.FindResource("HighlightColor.Secondary") as Brush;
        }

        private void ItembarBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            //item bar highlight
            ItembarBorder.Background = this.FindResource("SolidColorBrush.Transparent") as Brush;
            ItembarBackBorder.BorderBrush = this.FindResource("Color.Gray") as Brush;
        }

        private void Itembar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                //Set new title
                Set_NewTitle();

                //Play click animation
                //Storyboard sb = this.FindResource("ItembarClick") as Storyboard;
                //sb.Begin();

                if (TitleTextBlock.Visibility == Visibility.Visible)
                {
                    //trigger click event
                    Click?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        #endregion


        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            IsCompleted = true;
            Update?.Invoke(this, EventArgs.Empty);
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            IsCompleted = false;
            Update?.Invoke(this, EventArgs.Empty);
        }

        private void StarToggle_Checked(object sender, RoutedEventArgs e)
        {
            IsStarred = true;
            Update?.Invoke(this, EventArgs.Empty);
        }

        private void StarToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            IsStarred = false;
            Update?.Invoke(this, EventArgs.Empty);
        }


        #region Menu item click events
        private void MenuItemCompletion_Click(object sender, RoutedEventArgs e)
        {
            Set_IsCompleted();
        }

        private void MenuItemIsStarred_Click(object sender, RoutedEventArgs e)
        {
            Set_IsStarred();
        }

        private void MenuItem_Remove_Click(object sender, RoutedEventArgs e)
        {
            //play remove animation
            Storyboard storyboard = this.FindResource("ItembarRemove") as Storyboard;
            storyboard.Begin();
        }

        private void Storyboard_ItembarRemove_Completed(object sender, EventArgs e)
        {
            //trigger remove event after animation
            Remove?.Invoke(this, EventArgs.Empty);
        }
        #endregion


        private void TitleTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Set_NewTitle();
        }

        private void TitleTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Set_NewTitle();
            }
        }
    }
}
