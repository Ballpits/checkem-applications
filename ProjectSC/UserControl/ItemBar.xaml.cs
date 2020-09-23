using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectSC.UserControls.Custom
{
    public partial class ItemBar : UserControl
    {
        public ItemBar()
        {
            InitializeComponent();
        }

        public ItemBar(ToDoListUSC toDo)
        {
            InitializeComponent();

            todo = toDo;
        }

        private ToDoListUSC todo;

        #region Properties
        public int Id { get; set; }


        public string Title { get; set; }
        public string Description { get; set; }


        public bool IsCompleted { get; set; }
        public bool IsStarred { get; set; }


        public bool IsReminderOn { get; set; }
        public bool IsAdvanceReminderOn { get; set; }


        public DateTime BeginDateTime { get; set; }
        public DateTime EndDateTime { get; set; }


        public bool IsUsingTag { get; set; }
        public string TagName { get; set; }
        //public Brushes TagColor { get; set; }
        #endregion

        const int ChaeckBoxIconSize = 35;

        private bool CheckboxLoaded = false;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            textBlockTitle.Text = Title;
            //textBlock.Text = Id.ToString();

            checkBox.IsChecked = IsCompleted;
            CheckboxLoaded = true;

            StarToggle.IsChecked = IsStarred;

            Update();
        }


        private void ToDoChecked(object sender, RoutedEventArgs e)
        {
            var icon = new PackIcon { Kind = PackIconKind.Check };
            icon.Height = ChaeckBoxIconSize;
            icon.Width = ChaeckBoxIconSize;
            icon.HorizontalAlignment = HorizontalAlignment.Center;
            icon.VerticalAlignment = VerticalAlignment.Center;
            icon.Foreground = Brushes.Black;

            checkBox.Content = icon;

            textBlockTitle.Foreground = Brushes.LightGray;
            textBlockTitle.TextDecorations = TextDecorations.Strikethrough;

            if (CheckboxLoaded)
            {
                DataAccess_Json.UpdateCompletion(Id, true, todo.Inventory);
            }
        }

        private void ToDoUnchecked(object sender, RoutedEventArgs e)
        {
            if (cBoxGrid.IsMouseOver)
            {
                var icon = new PackIcon { Kind = PackIconKind.Check };
                icon.Height = 25;
                icon.Width = 25;
                icon.HorizontalAlignment = HorizontalAlignment.Center;
                icon.VerticalAlignment = VerticalAlignment.Center;
                icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));

                checkBox.Content = icon;
            }

            textBlockTitle.Foreground = Brushes.Black;
            textBlockTitle.TextDecorations = null;

            DataAccess_Json.UpdateCompletion(Id, false, todo.Inventory);
        }

        private void StarToggle_Click(object sender, RoutedEventArgs e)
        {
            if (StarToggle.IsChecked == true)
            {
                DataAccess_Json.Update(Id, true, todo.Inventory);
            }
            else
            {
                DataAccess_Json.Update(Id, false, todo.Inventory);
            }
        }


        #region Mouse down events
        private bool BorderEventCanActivate = true;
        private void Border_MouseDown(object sender, RoutedEventArgs e)
        {
            if (BorderEventCanActivate)
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    todo.OpenDetailsPanel(this);
                }
            }
        }

        private void Grid_MouseDown(object sender, RoutedEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (checkBox.IsChecked == true)
                {
                    checkBox.IsChecked = false;
                }
                else
                {
                    checkBox.IsChecked = true;
                }
            }
        }
        #endregion

        #region Mouse over events
        public void MouseEnterHighLight(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Border))
            {
                border.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFEDF7FF"));
                cBoxGrid.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFEDF7FF"));
                StarToggle.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFEDF7FF"));
            }
            if (sender.GetType() == typeof(CheckBox))
            {
                if (checkBox.IsChecked == false)
                {
                    var icon = new PackIcon { Kind = PackIconKind.Check };
                    icon.Height = ChaeckBoxIconSize;
                    icon.Width = ChaeckBoxIconSize;
                    icon.HorizontalAlignment = HorizontalAlignment.Center;
                    icon.VerticalAlignment = VerticalAlignment.Center;
                    icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));

                    checkBox.Content = icon;
                }
            }
            if (sender.GetType() == typeof(Grid))
            {
                BorderEventCanActivate = false;
                cBoxGrid.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFEDF7FF"));
                StarToggle.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFEDF7FF"));

                if (checkBox.IsChecked == false)
                {
                    var icon = new PackIcon { Kind = PackIconKind.Check };
                    icon.Height = ChaeckBoxIconSize;
                    icon.Width = ChaeckBoxIconSize;
                    icon.HorizontalAlignment = HorizontalAlignment.Center;
                    icon.VerticalAlignment = VerticalAlignment.Center;
                    icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));

                    checkBox.Content = icon;
                }
            }
        }

        public void MouseLeaveUnHighLight(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Border))
            {
                border.Background = Brushes.White;
                cBoxGrid.Background = Brushes.White;
                StarToggle.Background = Brushes.White;
            }
            if (sender.GetType() == typeof(CheckBox))
            {
                if (checkBox.IsChecked == false)
                {
                    var icon = new PackIcon { Kind = PackIconKind.Check };
                    icon.Height = ChaeckBoxIconSize;
                    icon.Width = ChaeckBoxIconSize;
                    icon.HorizontalAlignment = HorizontalAlignment.Center;
                    icon.VerticalAlignment = VerticalAlignment.Center;
                    icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));

                    checkBox.Content = icon;
                }
            }
            if (sender.GetType() == typeof(Grid))
            {
                BorderEventCanActivate = true;

                if (cBoxGrid.IsMouseOver == false && border.IsMouseOver == false)
                {
                    cBoxGrid.Background = Brushes.White;
                }

                if (checkBox.IsChecked == false)
                {
                    var icon = new PackIcon { Kind = PackIconKind.CheckboxBlankCircleOutline };
                    icon.Height = ChaeckBoxIconSize;
                    icon.Width = ChaeckBoxIconSize;
                    icon.HorizontalAlignment = HorizontalAlignment.Center;
                    icon.VerticalAlignment = VerticalAlignment.Center;
                    icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));

                    checkBox.Content = icon;
                }
            }
        }
        #endregion


        public void Update()
        {
            VisualUpdate();
        }

        public void Update(ItemBar itemBar)
        {
            textBlockTitle.Text = itemBar.Title;
            this.IsReminderOn = itemBar.IsReminderOn;

            VisualUpdate();
        }

        private void VisualUpdate()
        {
            if (IsReminderOn == true)
            {
                this.Height = 70;
                border.Height = 70;

                ReminderIcon.Visibility = Visibility.Visible;
                ReminderTimeTextBlock.Visibility = Visibility.Visible;


                if (IsAdvanceReminderOn)
                {
                    ReminderTimeTextBlock.Text = "Start on: " + SimplifiedDate(BeginDateTime) + "\tEnd on: " + SimplifiedDate(EndDateTime);
                }
                else
                {
                    ReminderTimeTextBlock.Text = SimplifiedDate(EndDateTime);
                }


                if (Passed(EndDateTime))
                {
                    ReminderTimeTextBlock.Foreground = Brushes.Red;
                }
                else
                {
                    ReminderTimeTextBlock.Foreground = Brushes.Black;
                }
            }
            else
            {
                this.Height = 50;
                border.Height = 50;

                ReminderIcon.Visibility = Visibility.Hidden;
                ReminderTimeTextBlock.Visibility = Visibility.Hidden;
                ReminderTimeTextBlock.Text = string.Empty;
            }

        }

        private string SimplifiedDate(DateTime dateTime)
        {
            if (dateTime.Year == DateTime.Now.Year && dateTime.Month == DateTime.Now.Month && dateTime.Day == DateTime.Now.Day)
            {
                return "Today, " + dateTime.ToString("hh:mm tt");
            }
            else if (dateTime.Year == DateTime.Now.Year && dateTime.Month == DateTime.Now.Month && dateTime.Day == DateTime.Now.Day + 1)
            {
                return "Tomorrow, " + dateTime.ToString("hh:mm tt");
            }
            else
            {
                return dateTime.ToString("dd/M/yyyy, hh:mm tt");
            }
        }

        private bool Passed(DateTime dateTime)
        {
            if (dateTime < DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
