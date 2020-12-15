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
            DataContext = this;

            todo = item;

            InitializeComponent();

            OnCompletetionChanged();
            OnVisualUpdate();
        }


        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Click;
        public event EventHandler Remove;
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

                    OnCompletetionChanged();
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

                    OnVisualUpdate();
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


        private void OnCompletetionChanged()
        {
            if (todo.IsCompleted)
            {
                TitleTextBlock.Foreground = this.FindResource("Color.DarkGray") as SolidColorBrush;
                TitleTextBlock.TextDecorations = TextDecorations.Strikethrough;
            }
            else
            {
                TitleTextBlock.Foreground = this.FindResource("Color.Black") as SolidColorBrush;
                TitleTextBlock.TextDecorations = null;
            }
        }

        private void Itembar_Loaded(object sender, RoutedEventArgs e)
        {
            OnVisualUpdate();
        }

        private void OnVisualUpdate()
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

        private void MenuItem_Remove_Click(object sender, RoutedEventArgs e)
        {
            Storyboard storyboard = this.FindResource("ItembarRemove") as Storyboard;
            storyboard.Begin();
        }

        private void Storyboard_ItembarRemove_Completed(object sender, EventArgs e)
        {
            Remove?.Invoke(this, EventArgs.Empty);
        }
    }
}
