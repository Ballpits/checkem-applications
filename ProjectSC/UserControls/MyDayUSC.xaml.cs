using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;

using MaterialDesignThemes.Wpf;

using ProjectSC.Classes.Functions;
using ProjectSC.UserControls.Custom;


namespace ProjectSC
{
    public partial class MyDayUSC : UserControl, IUIControl
    {
        public MyDayUSC()
        {
            InitializeComponent();

            items.LoadFullData(ref Inventory);
            items.ResetId(Inventory);
            LoadInList();
        }

        #region Variables
        ToDoItem items = new ToDoItem();

        public List<ToDoItem> Inventory = new List<ToDoItem>();

        private List<Border> borderlist = new List<Border>();
        private List<CheckBox> checkBoxList = new List<CheckBox>();
        private List<TextBlock> textBlockList = new List<TextBlock>();
        private List<Grid> cBoxList = new List<Grid>();
        #endregion

        private void LoadInList()
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                AddItem(i);
            }

            AddNewButton();
        }

        private void AddNewButton()
        {
            Button ButtonAddNew = new Button
            {
                Height = 50,
                Width = 50,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 10, 10, 0),
                Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3")),
                BorderThickness = new Thickness(0),
            };
            CornerRadius cr = new CornerRadius(100);
            ButtonAssist.SetCornerRadius(ButtonAddNew, cr);
            ShadowAssist.SetShadowDepth(ButtonAddNew, 0);
            ButtonAddNew.Click += new RoutedEventHandler(this.AddNewButton_Click);

            var icon = new PackIcon { Kind = PackIconKind.Plus };
            icon.Height = 30;
            icon.Width = 30;
            icon.HorizontalAlignment = HorizontalAlignment.Center;
            icon.VerticalAlignment = VerticalAlignment.Center;
            icon.Foreground = Brushes.White;
            ButtonAddNew.Content = icon;

            stpMain.Children.Add(ButtonAddNew);
        }

        public void Refresh()
        {
            items.ResetId(Inventory);

            borderlist.Clear();
            checkBoxList.Clear();
            textBlockList.Clear();
            cBoxList.Clear();
            stpMain.Children.Clear();

            LoadInList();
        }

        #region Item
        private void AddItem(int id)
        {
            Border border = new Border
            {
                Name = $"border{id}",
                BorderThickness = new Thickness(0, 0, 0, 0.5),
                Height = 50,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFA8A8A8")),
                Background = Brushes.White
            };
            border.MouseDown += new MouseButtonEventHandler(this.Border_MouseDown);
            border.MouseEnter += new MouseEventHandler(this.MouseEnterHighLight);
            border.MouseLeave += new MouseEventHandler(this.MouseLeaveUnHighLight);
            stpMain.Children.Add(border);
            borderlist.Add(border);

            Grid grid = new Grid
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            border.Child = grid;

            Style style = this.FindResource("MaterialDesignFlatPrimaryToggleButton") as Style;

            Grid cBoxGrid = new Grid
            {
                Name = $"cBoxGrid{id}",
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 30,
                Background = Brushes.White
            };
            cBoxList.Add(cBoxGrid);
            cBoxGrid.MouseDown += new MouseButtonEventHandler(this.Grid_MouseDown);
            cBoxGrid.MouseEnter += new MouseEventHandler(this.MouseEnterHighLight);
            cBoxGrid.MouseLeave += new MouseEventHandler(this.MouseLeaveUnHighLight);

            CheckBox checkBox = new CheckBox
            {
                Style = style,
                Name = $"checkboc{id}",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = 23,
                Width = 23,
                IsTabStop = false
            };
            checkBoxList.Add(checkBox);
            var icon = new PackIcon { Kind = PackIconKind.CheckboxBlankCircleOutline };
            icon.Height = 20;
            icon.Width = 20;
            icon.HorizontalAlignment = HorizontalAlignment.Center;
            icon.VerticalAlignment = VerticalAlignment.Center;
            icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
            checkBox.Content = icon;

            checkBox.Checked += new RoutedEventHandler(this.ToDoChecked);
            checkBox.Unchecked += new RoutedEventHandler(this.ToDoUnchecked);
            checkBox.MouseEnter += new MouseEventHandler(this.MouseEnterHighLight);
            checkBox.MouseLeave += new MouseEventHandler(this.MouseLeaveUnHighLight);


            TextBlock textBlock = new TextBlock
            {
                Name = $"textBlock{id}",
                Text = $"{Inventory[id].Title}",
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(35, 0, 0, 3.5),
                FontSize = 20
            };
            textBlockList.Add(textBlock);

            cBoxGrid.Children.Add(checkBox);
            grid.Children.Add(cBoxGrid);
            grid.Children.Add(textBlock);
        }
        #endregion

        #region Checkbox events
        private void ToDoChecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            var icon = new PackIcon { Kind = PackIconKind.Check };
            icon.Height = 19;
            icon.Width = 19;
            icon.HorizontalAlignment = HorizontalAlignment.Center;
            icon.VerticalAlignment = VerticalAlignment.Center;
            icon.Foreground = Brushes.Black;
            checkBox.Content = icon;

            textBlockList[IdParser.ParseId(checkBox)].TextDecorations = TextDecorations.Strikethrough;
        }

        private void ToDoUnchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            Grid grid = new Grid();
            grid = (Grid)checkBox.Parent;

            if (grid.IsMouseOver)
            {
                var icon = new PackIcon { Kind = PackIconKind.Check };
                icon.Height = 19;
                icon.Width = 19;
                icon.HorizontalAlignment = HorizontalAlignment.Center;
                icon.VerticalAlignment = VerticalAlignment.Center;
                icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
                checkBox.Content = icon;
            }

            textBlockList[IdParser.ParseId(checkBox)].TextDecorations = null;
        }
        #endregion

        #region Button evnts
        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            OpenDetailsPanel();
        }
        #endregion

        #region Mouse down events
        private bool BorderEvtCanActivate = true;
        private void Border_MouseDown(object sender, RoutedEventArgs e)
        {
            Border border = new Border();
            if (BorderEvtCanActivate)
            {
                border = (Border)sender;

                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    OpenDetailsPanel(IdParser.ParseId(border));
                }
            }
        }

        private void Grid_MouseDown(object sender, RoutedEventArgs e)
        {
            Grid grid = (Grid)sender;

            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (checkBoxList[IdParser.ParseId(grid)].IsChecked == true)
                {
                    checkBoxList[IdParser.ParseId(grid)].IsChecked = false;
                }
                else
                {
                    checkBoxList[IdParser.ParseId(grid)].IsChecked = true;
                }
            }
        }
        #endregion

        #region Mouse over events
        public void MouseEnterHighLight(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Border))
            {
                Border border = (Border)sender;
                border.Background = Brushes.LightGray;
                cBoxList[IdParser.ParseId(border)].Background = Brushes.LightGray;
            }
            if (sender.GetType() == typeof(CheckBox))
            {
                CheckBox checkBox = (CheckBox)sender;
                if (checkBox.IsChecked == false)
                {
                    var icon = new PackIcon { Kind = PackIconKind.Check };
                    icon.Height = 20;
                    icon.Width = 20;
                    icon.HorizontalAlignment = HorizontalAlignment.Center;
                    icon.VerticalAlignment = VerticalAlignment.Center;
                    icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
                    checkBox.Content = icon;
                }
            }
            if (sender.GetType() == typeof(Grid))
            {
                BorderEvtCanActivate = false;
                Grid grid = (Grid)sender;
                grid.Background = Brushes.LightGray;

                if (checkBoxList[IdParser.ParseId(grid)].IsChecked == false)
                {
                    var icon = new PackIcon { Kind = PackIconKind.Check };
                    icon.Height = 20;
                    icon.Width = 20;
                    icon.HorizontalAlignment = HorizontalAlignment.Center;
                    icon.VerticalAlignment = VerticalAlignment.Center;
                    icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
                    checkBoxList[IdParser.ParseId(grid)].Content = icon;
                }
            }
        }

        public void MouseLeaveUnHighLight(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Border))
            {
                Border border = (Border)sender;
                border.Background = Brushes.White;
                cBoxList[IdParser.ParseId(border)].Background = Brushes.White;
            }
            if (sender.GetType() == typeof(CheckBox))
            {
                CheckBox checkBox = (CheckBox)sender;
                if (checkBox.IsChecked == false)
                {
                    var icon = new PackIcon { Kind = PackIconKind.Check };
                    icon.Height = 20;
                    icon.Width = 20;
                    icon.HorizontalAlignment = HorizontalAlignment.Center;
                    icon.VerticalAlignment = VerticalAlignment.Center;
                    icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
                    checkBox.Content = icon;
                }
            }
            if (sender.GetType() == typeof(Grid))
            {
                BorderEvtCanActivate = true;
                Grid grid = (Grid)sender;

                if (borderlist[IdParser.ParseId(grid)].IsMouseOver == false)
                {
                    grid.Background = Brushes.White;
                }

                if (checkBoxList[IdParser.ParseId(grid)].IsChecked == false)
                {
                    var icon = new PackIcon { Kind = PackIconKind.CheckboxBlankCircleOutline };
                    icon.Height = 20;
                    icon.Width = 20;
                    icon.HorizontalAlignment = HorizontalAlignment.Center;
                    icon.VerticalAlignment = VerticalAlignment.Center;
                    icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
                    checkBoxList[IdParser.ParseId(grid)].Content = icon;
                }
            }
        }
        #endregion

        #region Details panel
        private void OpenDetailsPanel()
        {
            DetailsPanel detailsPanel = new DetailsPanel(this);

            detailsPanel.IsNew = true;

            DataGrid.Children.Add(detailsPanel);
        }

        private void OpenDetailsPanel(int id)
        {
            DetailsPanel detailsPanel = new DetailsPanel(this);

            detailsPanel.IsNew = false;

            detailsPanel.Id = Inventory[id].Id;

            detailsPanel.Title = Inventory[id].Title;
            detailsPanel.Description = Inventory[id].Description;

            detailsPanel.CanNotify = Inventory[id].CanNotify;

            detailsPanel.BeginDateTime = Inventory[id].BeginDateTime;
            detailsPanel.EndDateTime = Inventory[id].EndDateTime;

            DataGrid.Children.Add(detailsPanel);
        }

        public void CloseDetailsPanel()
        {
            DataGrid.Children.RemoveAt(DataGrid.Children.Count - 1);
        }
        #endregion
    }
}
