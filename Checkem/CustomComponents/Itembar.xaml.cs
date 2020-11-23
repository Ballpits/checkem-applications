using Checkem.Assets.ColorConverters;
using Cyclops.Models.Objects;
using MaterialDesignThemes.Wpf;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Visual;

namespace Checkem.CustomComponents
{
    public partial class Itembar : UserControl, INotifyPropertyChanged
    {
        public Itembar()
        {
            InitializeComponent();
        }

        public Itembar(ToDoItem item)
        {
            DataContext = this;

            ItemProperties = item;

            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Click;


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

        #endregion


        #region Variables
        PackIcon icon = new PackIcon();
        #endregion



        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private void Itembar_Loaded(object sender, RoutedEventArgs e)
        {
            VisualUpdate();
        }

        private void VisualUpdate()
        {
            if (ItemProperties.IsReminderOn)
            {
                this.Height = 70;

                ReminderIcon.Visibility = Visibility.Visible;
                ReminderDetailTextBlock.Visibility = Visibility.Visible;

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
                this.Height = 50;

                ReminderIcon.Visibility = Visibility.Hidden;
                ReminderDetailTextBlock.Visibility = Visibility.Hidden;
            }
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            Title = "binding works!!";
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
                Storyboard sb = this.FindResource("ItembarClick") as Storyboard;

                sb.Begin();

                Click?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
