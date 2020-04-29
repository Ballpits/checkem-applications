using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;

using ProjectSC.Classes.Functions;

namespace ProjectSC
{
    public partial class MyDayUSC : UserControl, IUIControl
    {
        public MyDayUSC()
        {
            InitializeComponent();
        }


        ToDoItem items = new ToDoItem();

        private List<ToDoItem> Inventory = new List<ToDoItem>();

        private List<Border> borderlist = new List<Border>();
        private List<CheckBox> checkBoxList = new List<CheckBox>();
        private List<TextBlock> textBlockList = new List<TextBlock>();
        private List<Grid> cBoxList = new List<Grid>();


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //items.StoreTestData(Inventory);
            items.LoadFullData(ref Inventory);
            items.ResetId(Inventory);
            LoadInList();
        }

        private void AddItem(int id)
        {
            Border border = new Border
            {
                Name = $"border{id}",
                BorderThickness = new Thickness(0, 0, 0, 0.5),
                Height = 30,
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
                Margin = new Thickness(35, 0, 0, 0),
                FontSize = 20
            };
            textBlockList.Add(textBlock);

            cBoxGrid.Children.Add(checkBox);
            grid.Children.Add(cBoxGrid);
            grid.Children.Add(textBlock);
        }

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

        private void MarkButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            items.Remove(IdParser.ParseId(btn), Inventory);
            stpMain.Children.RemoveAt(IdParser.ParseId(btn));
            items.ResetId(Inventory);

            borderlist.Clear();
            checkBoxList.Clear();
            textBlockList.Clear();
            cBoxList.Clear();
            stpMain.Children.Clear();

            CloseDetailsPanel();

            LoadInList();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            CloseDetailsPanel();
        }

        int ID;
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsNew)
            {
                items.AddNew(textBoxTitle.Text, textBoxDescription.Text, Convert.ToDateTime(BeginDatePicker.Text + " " + BeginTimePicker.Text), Convert.ToDateTime(EndDatePicker.Text + " " + EndTimePicker.Text), Inventory);
            }
            else
            {
                items.Update(ID, textBoxTitle.Text, textBoxDescription.Text, Convert.ToDateTime(BeginDatePicker.Text + " " + BeginTimePicker.Text), Convert.ToDateTime(EndDatePicker.Text + " " + EndTimePicker.Text), Inventory);
            }

            items.ResetId(Inventory);

            borderlist.Clear();
            checkBoxList.Clear();
            textBlockList.Clear();
            cBoxList.Clear();
            stpMain.Children.Clear();

            LoadInList();

            OpenSnakeBar("Saved");
        }

        private void OpenSnakeBar(string content)
        {
            SnackbarMessageQueue msgQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(1));
            msgQueue.Enqueue("Saved");

            SnackbarMessage snackbarMessage = new SnackbarMessage
            {
                Content = content,
            };
            Snackbar snackbar = new Snackbar
            {
                Message = snackbarMessage,
                IsActive = true,
                MessageQueue = msgQueue
            };
            //snackbarMessage.ActionClick += new RoutedEventHandler(this.ReturnButton_Click);
            DetailsGrid.Children.Add(snackbar);
        }
        #endregion

        #region Mouse down events
        bool BorderEvtCanActivate = true;
        private void Border_MouseDown(object sender, RoutedEventArgs e)
        {
            Border border = new Border();
            if (BorderEvtCanActivate)
            {
                border = (Border)sender;
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    if (!IsOpen)
                    {
                        ID = IdParser.ParseId(border);
                        OpenDetailsPanel(IdParser.ParseId(border));
                    }
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

        private void LoadInList()
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                AddItem(i);
            }

            AddAddNewButton();


        }

        private void AddAddNewButton()
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

        bool IsOpen = false;
        bool IsNew;
        Grid darkenGrid, DetailsGrid;

        TextBox textBoxTitle;
        TextBox textBoxDescription;
        DatePicker BeginDatePicker;
        DatePicker EndDatePicker;
        TimePicker BeginTimePicker;
        TimePicker EndTimePicker;

        private void OpenDetailsPanel()
        {
            IsNew = true;

            int id = Inventory.Count + 1;

            darkenGrid = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#CC000000")),
            };

            DetailsGrid = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Stretch,
                Background = Brushes.White,
                Width = 400
            };

            Button buttonreturn = new Button
            {
                Height = 50,
                Width = 50,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Background = Brushes.White,
                BorderThickness = new Thickness(0),
                ToolTip = "Return"
            };
            CornerRadius cr = new CornerRadius(100);
            ButtonAssist.SetCornerRadius(buttonreturn, cr);
            ShadowAssist.SetShadowDepth(buttonreturn, 0);
            buttonreturn.Click += new RoutedEventHandler(this.ReturnButton_Click);

            var icon = new PackIcon { Kind = PackIconKind.ArrowBack };
            icon.Height = 30;
            icon.Width = 30;
            icon.HorizontalAlignment = HorizontalAlignment.Center;
            icon.VerticalAlignment = VerticalAlignment.Center;
            icon.Foreground = Brushes.Black;
            buttonreturn.Content = icon;

            Button buttonSave = new Button
            {
                Height = 50,
                Width = 50,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                Background = Brushes.White,
                BorderThickness = new Thickness(0),
                ToolTip = "Save"
            };
            ButtonAssist.SetCornerRadius(buttonSave, cr);
            ShadowAssist.SetShadowDepth(buttonSave, 0);
            buttonSave.Click += new RoutedEventHandler(this.SaveButton_Click);

            icon = new PackIcon { Kind = PackIconKind.ContentSaveEdit };
            icon.Height = 30;
            icon.Width = 30;
            icon.HorizontalAlignment = HorizontalAlignment.Center;
            icon.VerticalAlignment = VerticalAlignment.Center;
            icon.Foreground = Brushes.Black;
            buttonSave.Content = icon;
            ScrollViewer scrollViewer = new ScrollViewer()

            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                Margin = new Thickness(0, 50, 0, 0)
            };

            StackPanel DetailsSTP = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
            };
            scrollViewer.Content = DetailsSTP;

            Style style = this.FindResource("MaterialDesignFilledTextFieldTextBox") as Style;

            textBoxTitle = new TextBox
            {
                Style = style,
                Margin = new Thickness(10, 5, 10, 5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextWrapping = TextWrapping.Wrap,
                MaxLength = 100,
                FontSize = 20
            };
            HintAssist.SetHint(textBoxTitle, "Title");

            textBoxDescription = new TextBox
            {
                Style = style,
                Margin = new Thickness(10, 5, 10, 5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextWrapping = TextWrapping.Wrap,
                MaxLength = 500,
                FontSize = 20
            };
            HintAssist.SetHint(textBoxDescription, "Description");

            style = this.FindResource("MaterialDesignFloatingHintDatePicker") as Style;

            BeginDatePicker = new DatePicker
            {
                Style = style,
                Margin = new Thickness(20, 5, 0, 5),
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 120,
                FontSize = 20
            };
            HintAssist.SetHint(BeginDatePicker, "Begin Date");

            EndDatePicker = new DatePicker
            {
                Style = style,
                Margin = new Thickness(20, 5, 0, 5),
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 120,
                FontSize = 20
            };
            HintAssist.SetHint(EndDatePicker, "End Date");

            style = this.FindResource("MaterialDesignFloatingHintTimePicker") as Style;

            BeginTimePicker = new TimePicker
            {
                Style = style,
                Margin = new Thickness(0, 5, 20, 5),
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 120,
                FontSize = 20
            };
            HintAssist.SetHint(BeginTimePicker, "Begin Time");

            EndTimePicker = new TimePicker
            {
                Style = style,
                Margin = new Thickness(0, 5, 20, 5),
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 120,
                FontSize = 20
            };
            HintAssist.SetHint(EndTimePicker, "End Time");

            Grid BeginRow = new Grid();
            BeginRow.Margin = new Thickness(10, 5, 10, 5);
            BeginRow.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#89E7E7E7"));
            BeginRow.Children.Add(BeginDatePicker);
            BeginRow.Children.Add(BeginTimePicker);

            Grid EndRow = new Grid();
            EndRow.Margin = new Thickness(10, 5, 10, 5);
            EndRow.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#89E7E7E7"));
            EndRow.Children.Add(EndDatePicker);
            EndRow.Children.Add(EndTimePicker);

            darkenGrid.Children.Add(DetailsGrid);

            DetailsSTP.Children.Add(textBoxTitle);
            DetailsSTP.Children.Add(textBoxDescription);
            DetailsSTP.Children.Add(BeginRow);
            DetailsSTP.Children.Add(EndRow);

            DetailsGrid.Children.Add(buttonreturn);
            DetailsGrid.Children.Add(buttonSave);
            DetailsGrid.Children.Add(scrollViewer);

            DataGrid.Children.Add(darkenGrid);
            IsOpen = true;
        }

        private void OpenDetailsPanel(int id)
        {
            IsNew = false;

            darkenGrid = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#CC000000")),
            };

            DetailsGrid = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Stretch,
                Background = Brushes.White,
                Width = 400
            };

            Button buttonreturn = new Button
            {
                Height = 50,
                Width = 50,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Background = Brushes.White,
                BorderThickness = new Thickness(0),
                ToolTip = "Return"
            };
            CornerRadius cr = new CornerRadius(100);
            ButtonAssist.SetCornerRadius(buttonreturn, cr);
            ShadowAssist.SetShadowDepth(buttonreturn, 0);
            buttonreturn.Click += new RoutedEventHandler(this.ReturnButton_Click);

            var icon = new PackIcon { Kind = PackIconKind.ArrowBack };
            icon.Height = 30;
            icon.Width = 30;
            icon.HorizontalAlignment = HorizontalAlignment.Center;
            icon.VerticalAlignment = VerticalAlignment.Center;
            icon.Foreground = Brushes.Black;
            buttonreturn.Content = icon;

            Button buttonSave = new Button
            {
                Height = 50,
                Width = 50,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                Background = Brushes.White,
                BorderThickness = new Thickness(0),
                ToolTip = "Save"
            };
            ButtonAssist.SetCornerRadius(buttonSave, cr);
            ShadowAssist.SetShadowDepth(buttonSave, 0);
            buttonSave.Click += new RoutedEventHandler(this.SaveButton_Click);

            icon = new PackIcon { Kind = PackIconKind.ContentSaveEdit };
            icon.Height = 30;
            icon.Width = 30;
            icon.HorizontalAlignment = HorizontalAlignment.Center;
            icon.VerticalAlignment = VerticalAlignment.Center;
            icon.Foreground = Brushes.Black;
            buttonSave.Content = icon;

            Button buttonRemove = new Button
            {
                Name = $"removeButton{id}",
                Height = 50,
                Width = 50,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 0, 50, 0),
                Background = Brushes.White,
                BorderThickness = new Thickness(0),
                ToolTip = "Remove"
            };
            ButtonAssist.SetCornerRadius(buttonRemove, cr);
            ShadowAssist.SetShadowDepth(buttonRemove, 0);
            buttonRemove.Click += new RoutedEventHandler(this.RemoveButton_Click);

            icon = new PackIcon { Kind = PackIconKind.TrashCan };
            icon.Height = 30;
            icon.Width = 30;
            icon.HorizontalAlignment = HorizontalAlignment.Center;
            icon.VerticalAlignment = VerticalAlignment.Center;
            icon.Foreground = Brushes.Black;
            buttonRemove.Content = icon;

            ScrollViewer scrollViewer = new ScrollViewer()
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                Margin = new Thickness(0, 50, 0, 0)
            };

            StackPanel DetailsSTP = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
            };
            scrollViewer.Content = DetailsSTP;

            Style style = this.FindResource("MaterialDesignFilledTextFieldTextBox") as Style;

            textBoxTitle = new TextBox
            {
                Text = $"{Inventory[id].Title}",
                Style = style,
                Margin = new Thickness(10, 5, 10, 5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextWrapping = TextWrapping.Wrap,
                MaxLength = 100,
                FontSize = 20
            };
            HintAssist.SetHint(textBoxTitle, "Title");

            textBoxDescription = new TextBox
            {
                Text = $"{Inventory[id].Description}",
                Style = style,
                Margin = new Thickness(10, 5, 10, 5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextWrapping = TextWrapping.Wrap,
                MaxLength = 500,
                FontSize = 20
            };
            HintAssist.SetHint(textBoxDescription, "Description");

            style = this.FindResource("MaterialDesignFloatingHintDatePicker") as Style;

            BeginDatePicker = new DatePicker
            {
                Text = $"{Inventory[id].BeginDateTime.Month}/{Inventory[id].BeginDateTime.Day}/{Inventory[id].BeginDateTime.Year}",
                Style = style,
                Margin = new Thickness(20, 5, 0, 5),
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 120,
                FontSize = 20
            };
            HintAssist.SetHint(BeginDatePicker, "Begin Date");

            EndDatePicker = new DatePicker
            {
                Text = $"{Inventory[id].EndDateTime.Month}/{Inventory[id].EndDateTime.Day}/{Inventory[id].EndDateTime.Year}",
                Style = style,
                Margin = new Thickness(20, 5, 0, 5),
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 120,
                FontSize = 20
            };
            HintAssist.SetHint(EndDatePicker, "End Date");

            style = this.FindResource("MaterialDesignFloatingHintTimePicker") as Style;

            BeginTimePicker = new TimePicker
            {
                Text = $"{string.Format("{0:h:mm tt}", Inventory[id].BeginDateTime)}",
                Style = style,
                Margin = new Thickness(0, 5, 20, 5),
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 120,
                FontSize = 20
            };
            HintAssist.SetHint(BeginTimePicker, "Begin Time");

            EndTimePicker = new TimePicker
            {
                Text = $"{string.Format("{0:h:mm tt}", Inventory[id].EndDateTime)}",
                Style = style,
                Margin = new Thickness(0, 5, 20, 5),
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 120,
                FontSize = 20
            };
            HintAssist.SetHint(EndTimePicker, "End Time");

            Grid BeginRow = new Grid();
            BeginRow.Margin = new Thickness(10, 5, 10, 5);
            BeginRow.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#89E7E7E7"));
            BeginRow.Children.Add(BeginDatePicker);
            BeginRow.Children.Add(BeginTimePicker);

            Grid EndRow = new Grid();
            EndRow.Margin = new Thickness(10, 5, 10, 5);
            EndRow.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#89E7E7E7"));
            EndRow.Children.Add(EndDatePicker);
            EndRow.Children.Add(EndTimePicker);

            darkenGrid.Children.Add(DetailsGrid);

            DetailsSTP.Children.Add(textBoxTitle);
            DetailsSTP.Children.Add(textBoxDescription);
            DetailsSTP.Children.Add(BeginRow);
            DetailsSTP.Children.Add(EndRow);

            DetailsGrid.Children.Add(buttonreturn);
            DetailsGrid.Children.Add(buttonSave);
            DetailsGrid.Children.Add(buttonRemove);
            DetailsGrid.Children.Add(scrollViewer);

            DataGrid.Children.Add(darkenGrid);
            IsOpen = true;
        }

        private void CloseDetailsPanel()
        {
            DataGrid.Children.Remove(darkenGrid);
            IsOpen = false;
        }
    }
}
