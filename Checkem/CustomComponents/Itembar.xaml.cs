using Checkem.Models;
using MaterialDesignThemes.Wpf;
using Sphere.Readable;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Checkem.CustomComponents
{
    public partial class Itembar : UserControl, INotifyPropertyChanged
    {
        public Itembar(Todo item)
        {
            //Get todo properties
            todo = item;

            DataContext = this;

            InitializeComponent();

            //Update title foregrond and check box state
            OnCompletionChanged();

            //Update star toggle state
            OnStarChanged();

            //Update itembar height
            OnReminderTypeChanged();
        }


        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Click;
        public event EventHandler Remove;
        public event EventHandler Update;
        #endregion


        #region Properties

        public Todo todo;

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

                    OnStarChanged();
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

                    OnReminderTypeChanged();
                    OnPropertyChanged();
                }
            }
        }

        #endregion


        #region Variables
        PackIcon icon = new PackIcon();
        #endregion


        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private void OnCompletionChanged()
        {
            if (todo.IsCompleted)
            {
                TitleTextBlock.Foreground = this.FindResource("Color.DarkGray") as SolidColorBrush;
                TitleTextBlock.TextDecorations = TextDecorations.Strikethrough;

                MenuItemIsCompleted.Header = this.FindResource("Dict_MarkAsNotCompleted") as string;
            }
            else
            {
                TitleTextBlock.Foreground = this.FindResource("Color.Black") as SolidColorBrush;
                TitleTextBlock.TextDecorations = null;

                MenuItemIsCompleted.Header = this.FindResource("Dict_MarkAsCompleted") as string;
            }
        }

        private void OnStarChanged()
        {
            if (todo.IsStarred)
            {
                MenuItemIsStarred.Header = this.FindResource("Dict_MarkAsNotStarred") as string;
            }
            else
            {
                MenuItemIsStarred.Header = this.FindResource("Dict_MarkAsStarred") as string;
            }
        }

        private void Itembar_Loaded(object sender, RoutedEventArgs e)
        {
            OnReminderTypeChanged();
        }

        private void OnReminderTypeChanged()
        {
            if (todo.IsReminderOn)
            {
                ReminderDetailStackPanel.Visibility = Visibility.Visible;

                if (this.todo.IsAdvanceReminderOn)
                {
                    ReminderDetailTextBlock.Text = "Start on: " + DateTimeManipulator.SimplifiedDate(this.todo.BeginDateTime.Value) + "\tEnd on: " + DateTimeManipulator.SimplifiedDate(this.todo.EndDateTime.Value);
                }
                else
                {
                    ReminderDetailTextBlock.Text = DateTimeManipulator.SimplifiedDate(this.todo.EndDateTime.Value);
                }

                if (DateTimeManipulator.IsPassed(this.todo.EndDateTime.Value))
                {
                    //ReminderDetailTextBlock.SetBinding(TextBlock.ForegroundProperty, OverDueTextColorBindings);
                }
                else
                {
                    //ReminderDetailTextBlock.SetBinding(TextBlock.ForegroundProperty, NormalTextColorBindings);
                }
            }
            else
            {
                ReminderDetailStackPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void ItembarBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            ItembarBorder.Background = this.FindResource("HighlightColor.Primary") as Brush;
            ItembarBorder.BorderBrush = this.FindResource("HighlightColor.Secondary") as Brush;
        }

        private void ItembarBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            ItembarBorder.Background = this.FindResource("Color.White") as Brush;
            ItembarBorder.BorderBrush = this.FindResource("Color.Gray") as Brush;
        }

        private void Itembar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                //Storyboard sb = this.FindResource("ItembarClick") as Storyboard;

                //sb.Begin();

                Click?.Invoke(this, EventArgs.Empty);
            }
        }

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

        private void MenuItemCompletion_Click(object sender, RoutedEventArgs e)
        {
            Update_IsCompleted();
        }

        private void MenuItemIsStarred_Click(object sender, RoutedEventArgs e)
        {
            Update_IsStarred();
        }

        private void MenuItem_Remove_Click(object sender, RoutedEventArgs e)
        {
            Storyboard storyboard = this.FindResource("ItembarRemove") as Storyboard;
            storyboard.Begin();
        }

        private void Storyboard_ItembarRemove_Completed(object sender, EventArgs e)
        {
            Remove?.Invoke(this, EventArgs.Empty);
        }

        private void Update_IsCompleted()
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

        private void Update_IsStarred()
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
    }
}
