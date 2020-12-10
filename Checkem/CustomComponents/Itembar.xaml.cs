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
        public Itembar(ToDoItem item)
        {
            DataContext = this;

            ItemProperties = item;

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

        public ToDoItem ItemProperties;

        public string Title
        {
            get
            {
                return ItemProperties.Title;
            }
            set
            {
                if (ItemProperties.Title != value)
                {
                    ItemProperties.Title = value;

                    OnPropertyChanged();
                }
            }
        }

        public bool IsCompleted
        {
            get
            {
                return ItemProperties.IsCompleted;
            }
            set
            {
                if (ItemProperties.IsCompleted != value)
                {
                    ItemProperties.IsCompleted = value;

                    OnCompletetionChanged();
                    OnPropertyChanged();
                }
            }
        }

        public bool IsStarred
        {
            get
            {
                return ItemProperties.IsStarred;
            }
            set
            {
                if (ItemProperties.IsStarred != value)
                {
                    ItemProperties.IsStarred = value;

                    OnPropertyChanged();
                }
            }
        }

        public bool IsReminderOn
        {
            get
            {
                return ItemProperties.IsReminderOn;
            }
            set
            {
                if (ItemProperties.IsReminderOn != value)
                {
                    ItemProperties.IsReminderOn = value;

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
            if (ItemProperties.IsCompleted)
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
            if (ItemProperties.IsReminderOn)
            {
                ReminderDetailStackPanel.Visibility = Visibility.Visible;

                if (this.ItemProperties.IsAdvanceReminderOn)
                {
                    ReminderDetailTextBlock.Text = "Start on: " + DateTimeManipulator.SimplifiedDate(this.ItemProperties.BeginDateTime.Value) + "\tEnd on: " + DateTimeManipulator.SimplifiedDate(this.ItemProperties.EndDateTime.Value);
                }
                else
                {
                    ReminderDetailTextBlock.Text = DateTimeManipulator.SimplifiedDate(this.ItemProperties.EndDateTime.Value);
                }

                if (DateTimeManipulator.IsPassed(this.ItemProperties.EndDateTime.Value))
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
