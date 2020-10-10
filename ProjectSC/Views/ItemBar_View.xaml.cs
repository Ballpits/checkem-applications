﻿using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using ProjectSC.Models.DataAccess;
using ProjectSC.ViewModels.ColorConvert;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectSC.Views
{
    public partial class ItemBar_View : UserControl
    {
        public ItemBar_View()
        {
            InitializeComponent();

            SetupColor();
        }

        public ItemBar_View(ToDoList_View toDo)
        {
            InitializeComponent();

            todo = toDo;

            SetupColor();
        }

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
        #endregion

        #region Variables
        private DataAccess_Json dataAccess = new DataAccess_Json();

        private ToDoList_View todo;


        const int ChaeckBoxIconSize = 35;

        private bool CheckboxLoaded = false;

        private bool BorderEventCanActivate = true;

        System.Drawing.Color ItemCompletedTextColor_D = Properties.Settings.Default.ItemCompletedTextColor;
        System.Drawing.Color ItemPassedTextColor_D = Properties.Settings.Default.ItemPassedTextColor;

        SolidColorBrush ItemCompletedTextColor, ItemPassedTextColor;

        Binding ItemCompletedTextColorBindings = new Binding()
        {
            Source = Properties.Settings.Default,
            Path = new PropertyPath("ItemCompletedTextColor", Properties.Settings.Default),
            Converter = new ColorToBrushConverter()
        };

        Binding DarkMainColorBindings = new Binding()
        {
            Source = Properties.Settings.Default,
            Path = new PropertyPath("DarkMainColor", Properties.Settings.Default),
            Converter = new ColorToBrushConverter()
        };

        PackIcon icon = new PackIcon();
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            textBlockTitle.Text = Title;
            //textBlock.Text = Id.ToString();

            checkBox.IsChecked = IsCompleted;
            CheckboxLoaded = true;

            StarToggle.IsChecked = IsStarred;

            Update();
        }


        #region Checkbox events
        private void ToDoChecked(object sender, RoutedEventArgs e)
        {
            icon.Kind = PackIconKind.Check;

            textBlockTitle.SetBinding(TextBlock.ForegroundProperty, ItemCompletedTextColorBindings);
            textBlockTitle.TextDecorations = TextDecorations.Strikethrough;

            if (CheckboxLoaded)
            {
                dataAccess.UpdateCompletion(Id, true, todo.Inventory);
            }
        }

        private void ToDoUnchecked(object sender, RoutedEventArgs e)
        {
            if (CheckboxGrid.IsMouseOver)
            {
                icon.Kind = PackIconKind.Check;
            }

            textBlockTitle.SetBinding(TextBlock.ForegroundProperty, DarkMainColorBindings);
            textBlockTitle.TextDecorations = null;

            dataAccess.UpdateCompletion(Id, false, todo.Inventory);
        }
        #endregion


        private void StarToggle_Click(object sender, RoutedEventArgs e)
        {
            if (StarToggle.IsChecked == true)
            {
                dataAccess.Update(Id, true, todo.Inventory);
            }
            else
            {
                dataAccess.Update(Id, false, todo.Inventory);
            }
        }


        #region Mouse down events
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
            }
            if (sender.GetType() == typeof(CheckBox))
            {
                if (checkBox.IsChecked == false)
                {
                    icon.Kind = PackIconKind.Check;
                }
            }
            if (sender.GetType() == typeof(Grid))
            {
                BorderEventCanActivate = false;

                if (checkBox.IsChecked == false)
                {
                    icon.Kind = PackIconKind.Check;
                }
            }
        }

        public void MouseLeaveUnHighLight(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(CheckBox))
            {
                if (checkBox.IsChecked == false)
                {
                    icon.Kind = PackIconKind.Check;
                }
            }
            if (sender.GetType() == typeof(Grid))
            {
                BorderEventCanActivate = true;

                if (checkBox.IsChecked == false)
                {
                    icon.Kind = PackIconKind.CheckboxBlankCircleOutline;
                }
            }
        }
        #endregion

        #region Visual updates
        public void Update()
        {
            VisualUpdate();
        }

        public void Update(ItemBar_View itemBar)
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
                    ReminderTimeTextBlock.Foreground = ItemPassedTextColor;
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
        #endregion

        #region Reminder mode itembar methods
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
                return dateTime.ToString("yyyy/MM/dd, hh:mm tt");
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
        #endregion

        #region Color functions
        public void SetupColor()
        {
            GetAllColor();

            icon.Kind = PackIconKind.CheckboxBlankCircleOutline;
            icon.Height = ChaeckBoxIconSize;
            icon.Width = ChaeckBoxIconSize;
            icon.HorizontalAlignment = HorizontalAlignment.Center;
            icon.VerticalAlignment = VerticalAlignment.Center;

            checkBox.Content = icon;
        }

        private void GetAllColor()
        {
            ItemCompletedTextColor = ColorConverter(ItemCompletedTextColor_D);
            ItemPassedTextColor = ColorConverter(ItemPassedTextColor_D);
        }

        private SolidColorBrush ColorConverter(System.Drawing.Color color)
        {
            return new SolidColorBrush(Color.FromRgb(color.R, color.G, color.B));
        }
        #endregion
    }
}
