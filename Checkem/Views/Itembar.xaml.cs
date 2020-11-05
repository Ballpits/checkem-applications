using Checkem.Assets.ColorConverters;
using Cyclops.Models.Objects;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Checkem.Views
{
    /// <summary>
    /// Interaction logic for Itembar.xaml
    /// </summary>
    public partial class Itembar : UserControl
    {
        public Itembar()
        {
            InitializeComponent();
            SetupColor();
        }

        //public ItemBar(ToDoList toDo)
        //{
        //    InitializeComponent();

        //    ToDoList = toDo;

        //    SetupColor();
        //}

        #region Properties
        public ToDoItem itemProperties { get; set; }
        #endregion

        #region Variables
        //private DataAccess_Json dataAccess = new DataAccess_Json();

        //private ToDoList ToDoList;


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

        //Binding ItembarHighlightColorBindings = new Binding()
        //{
        //    Source = Properties.Settings.Default,
        //    Path = new PropertyPath("DarkMainColor", Properties.Settings.Default),
        //    Converter = new ColorToBrushConverter()
        //};

        PackIcon icon = new PackIcon();
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            textBlockTitle.Text = this.itemProperties.Title;
            //textBlock.Text = Id.ToString();

            checkBox.IsChecked = this.itemProperties.IsCompleted;
            CheckboxLoaded = true;

            StarToggle.IsChecked = this.itemProperties.IsStarred;

            Update();
        }

        #region ContextMenu
        private void contextMnuCheckbox()
        {

        }
        #endregion

        #region Checkbox events
        private void ToDoChecked(object sender, RoutedEventArgs e)
        {
            icon.Kind = PackIconKind.Check;

            textBlockTitle.SetBinding(TextBlock.ForegroundProperty, ItemCompletedTextColorBindings);
            textBlockTitle.TextDecorations = TextDecorations.Strikethrough;

            if (CheckboxLoaded)
            {
                this.itemProperties.IsCompleted = true;
                //dataAccess.Update(this.itemProperties, ToDoList.Inventory);
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

            this.itemProperties.IsCompleted = false;
            //dataAccess.Update(this.itemProperties, ToDoList.Inventory);
        }
        #endregion


        private void StarToggle_Click(object sender, RoutedEventArgs e)
        {
            //this.itemProperties.IsStarred = (bool)StarToggle.IsChecked;
            //dataAccess.Update(this.itemProperties, ToDoList.Inventory);
        }


        #region Mouse down events
        private void Border_MouseDown(object sender, RoutedEventArgs e)
        {
            if (BorderEventCanActivate)
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    //ToDoList.OpenDetailsPanel(this);
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
            //VisualUpdate();
        }

        //public void Update(ItemBar itemBar)
        //{
        //    textBlockTitle.Text = itemBar.itemProperties.Title;
        //    this.itemProperties.IsReminderOn = itemBar.itemProperties.IsReminderOn;

        //    VisualUpdate();
        //}

        private void VisualUpdate()
        {
            if (this.itemProperties.IsReminderOn == true)
            {
                this.Height = 70;
                ItembarBorder.Height = 70;

                ReminderIcon.Visibility = Visibility.Visible;
                ReminderTimeTextBlock.Visibility = Visibility.Visible;


                if (this.itemProperties.IsAdvanceReminderOn)
                {
                    ReminderTimeTextBlock.Text = "Start on: " + SimplifiedDate(this.itemProperties.BeginDateTime) + "\tEnd on: " + SimplifiedDate(this.itemProperties.EndDateTime);
                }
                else
                {
                    ReminderTimeTextBlock.Text = SimplifiedDate(this.itemProperties.EndDateTime);
                }


                if (Passed(this.itemProperties.EndDateTime))
                {
                    ReminderTimeTextBlock.Foreground = ItemPassedTextColor;
                }
            }
            else
            {
                this.Height = 50;
                ItembarBorder.Height = 50;

                ReminderIcon.Visibility = Visibility.Hidden;
                ReminderTimeTextBlock.Visibility = Visibility.Hidden;
                ReminderTimeTextBlock.Text = string.Empty;
            }
        }
        #endregion

        #region Reminder mode itembar methods
        private string SimplifiedDate(DateTime? dateTime)
        {
            if (dateTime.Value.Year == DateTime.Now.Year && dateTime.Value.Month == DateTime.Now.Month && dateTime.Value.Day == DateTime.Now.Day)
            {
                return "Today, " + dateTime.Value.ToString("hh:mm tt");
            }
            else if (dateTime.Value.Year == DateTime.Now.Year && dateTime.Value.Month == DateTime.Now.Month && dateTime.Value.Day == DateTime.Now.Day + 1)
            {
                return "Tomorrow, " + dateTime.Value.ToString("hh:mm tt");
            }
            else
            {
                return dateTime.Value.ToString("yyyy/MM/dd, hh:mm tt");
            }
        }

        private bool Passed(DateTime? dateTime)
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
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

        private SolidColorBrush ColorConverter(System.Drawing.Color color)
        {
            return new SolidColorBrush(Color.FromRgb(color.R, color.G, color.B));
        }
        #endregion

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void ItembarBorder_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void test_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemRemove_Click(object sender, RoutedEventArgs e)
        {
            //dataAccess.Remove(this.itemProperties, ToDoList.Inventory);

            //ToDoList.RemoveItemBar(this);

            //ToDoList.CloseDetailsPanel("Removed");
        }
    }
}
