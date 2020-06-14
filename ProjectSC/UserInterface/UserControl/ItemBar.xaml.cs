using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectSC.UserControls.Custom
{
    public partial class ItemBar : UserControl
    {
        public ItemBar(MyDayUSC myDay)
        {
            InitializeComponent();

            MyDay = myDay;
        }

        private MyDayUSC MyDay;
        private ToDoItem item = new ToDoItem();

        public int Id { get; set; }
        public string Title { get; set; }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //textBlock.Text = Title;
            textBlock.Text = Id.ToString();

            if (MyDay.Inventory[Id].IsImportant == true)
            {
                StarToggle.IsChecked = true;
            }
            else
            {
                StarToggle.IsChecked = false;
            }
        }


        private void ToDoChecked(object sender, RoutedEventArgs e)
        {
            var icon = new PackIcon { Kind = PackIconKind.Check };
            icon.Height = 27;
            icon.Width = 27;
            icon.HorizontalAlignment = HorizontalAlignment.Center;
            icon.VerticalAlignment = VerticalAlignment.Center;
            icon.Foreground = Brushes.Black;

            checkBox.Content = icon;

            textBlock.TextDecorations = TextDecorations.Strikethrough;
        }

        private void ToDoUnchecked(object sender, RoutedEventArgs e)
        {
            if (cBoxGrid.IsMouseOver)
            {
                var icon = new PackIcon { Kind = PackIconKind.Check };
                icon.Height = 27;
                icon.Width = 27;
                icon.HorizontalAlignment = HorizontalAlignment.Center;
                icon.VerticalAlignment = VerticalAlignment.Center;
                icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));

                checkBox.Content = icon;
            }

            textBlock.TextDecorations = null;
        }

        #region Mouse down events
        private bool BorderEvtCanActivate = true;
        private void Border_MouseDown(object sender, RoutedEventArgs e)
        {
            if (BorderEvtCanActivate)
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    MyDay.OpenDetailsPanel(Id);
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
                border.Background = Brushes.LightGray;
                cBoxGrid.Background = Brushes.LightGray;
                StarToggle.Background = Brushes.LightGray;
            }
            if (sender.GetType() == typeof(CheckBox))
            {
                if (checkBox.IsChecked == false)
                {
                    var icon = new PackIcon { Kind = PackIconKind.Check };
                    icon.Height = 27;
                    icon.Width = 27;
                    icon.HorizontalAlignment = HorizontalAlignment.Center;
                    icon.VerticalAlignment = VerticalAlignment.Center;
                    icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));

                    checkBox.Content = icon;
                }
            }
            if (sender.GetType() == typeof(Grid))
            {
                BorderEvtCanActivate = false;
                cBoxGrid.Background = Brushes.LightGray;
                StarToggle.Background = Brushes.LightGray;

                if (checkBox.IsChecked == false)
                {
                    var icon = new PackIcon { Kind = PackIconKind.Check };
                    icon.Height = 27;
                    icon.Width = 27;
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
                    icon.Height = 27;
                    icon.Width = 27;
                    icon.HorizontalAlignment = HorizontalAlignment.Center;
                    icon.VerticalAlignment = VerticalAlignment.Center;
                    icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));

                    checkBox.Content = icon;
                }
            }
            if (sender.GetType() == typeof(Grid))
            {
                BorderEvtCanActivate = true;

                if (cBoxGrid.IsMouseOver == false && border.IsMouseOver == false)
                {
                    cBoxGrid.Background = Brushes.White;
                }

                if (checkBox.IsChecked == false)
                {
                    var icon = new PackIcon { Kind = PackIconKind.CheckboxBlankCircleOutline };
                    icon.Height = 27;
                    icon.Width = 27;
                    icon.HorizontalAlignment = HorizontalAlignment.Center;
                    icon.VerticalAlignment = VerticalAlignment.Center;
                    icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));

                    checkBox.Content = icon;
                }
            }
        }
        #endregion

        private void StarToggle_Click(object sender, RoutedEventArgs e)
        {
            if (StarToggle.IsChecked == true)
            {
                DataAccess.Update(Id, true, MyDay.Inventory);
            }
            else
            {
                DataAccess.Update(Id, false, MyDay.Inventory);
            }
        }
    }
}
