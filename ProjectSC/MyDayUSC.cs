using System;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace ProjectSC
{
    public partial class MyDayUSC : UserControl, IUIControl
    {
        public MyDayUSC()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            items.StoreTestData(Inventory);
            //items.LoadInData(ref Inventory);
            items.ResetId(Inventory);
            LoadInList();
        }

        Items items = new Items();

        private List<Items> Inventory = new List<Items>();

        private List<Border> borderlist = new List<Border>();
        private List<CheckBox> checkBoxList = new List<CheckBox>();
        private List<TextBlock> textBlockList = new List<TextBlock>();

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
                Margin = new Thickness(50, 0, 0, 0),
                FontSize = 20
            };
            textBlockList.Add(textBlock);

            cBoxGrid.Children.Add(checkBox);
            grid.Children.Add(cBoxGrid);
            grid.Children.Add(textBlock);
        }

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

            textBlockList[ParseId(checkBox)].TextDecorations = TextDecorations.Strikethrough;
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

            textBlockList[ParseId(checkBox)].TextDecorations = null;
        }

        private void MarkButton_Click(object sender, RoutedEventArgs e)
        {

        }

        readonly List<UserControl> usclist = new List<UserControl>();

        private void DetailButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            UserControl usc = new UserControl
            {
                Background = Brushes.White,
            };

            ColumnDefinition ColDef1 = new ColumnDefinition
            {
                Width = new GridLength(440, GridUnitType.Star)
            };
            ColumnDefinition ColDef2 = new ColumnDefinition
            {
                Width = new GridLength(300, GridUnitType.Star)
            };
            ColumnDefinition ColDef3 = new ColumnDefinition
            {
                Width = new GridLength(10, GridUnitType.Star)
            };

            RowDefinition RowDef1 = new RowDefinition
            {
                Height = new GridLength(60, GridUnitType.Star)
            };
            RowDefinition RowDef2 = new RowDefinition
            {
                Height = new GridLength(280, GridUnitType.Star)
            };
            RowDefinition RowDef3 = new RowDefinition
            {
                Height = new GridLength(70, GridUnitType.Star)
            };

            Grid MoreDetailGrid = new Grid();
            MoreDetailGrid.ColumnDefinitions.Add(ColDef1);
            MoreDetailGrid.ColumnDefinitions.Add(ColDef2);
            MoreDetailGrid.ColumnDefinitions.Add(ColDef3);
            MoreDetailGrid.RowDefinitions.Add(RowDef1);
            MoreDetailGrid.RowDefinitions.Add(RowDef2);
            MoreDetailGrid.RowDefinitions.Add(RowDef3);

            Viewbox ViewboxTB = new Viewbox();
            Grid.SetRow(ViewboxTB, 0);
            Grid.SetColumn(ViewboxTB, 1);

            TextBlock TitleTB = new TextBlock();
            //TitleTB.Text = $"{Inventory[stpMain.Children.IndexOf(borderlist[ParseId(btn)])].Title}";
            TitleTB.Text = $"{Inventory[ParseId(btn)].Title}";
            ViewboxTB.Child = TitleTB;

            MoreDetailGrid.Children.Add(ViewboxTB);

            TextBlock DesTB = new TextBlock
            {
                Text = "Description:",
                FontSize = 30,
                Margin = new Thickness(5, 0, 0, 0)
            };
            Grid.SetColumn(DesTB, 0);
            Grid.SetRow(DesTB, 1);

            TextBlock DescriptionTB = new TextBlock
            {
                //Text = $"{Inventory[stpMain.Children.IndexOf(borderlist[ParseId(btn)])].Description}",
                Text = $"{Inventory[ParseId(btn)].Description}",
                FontSize = 20,
                Margin = new Thickness(10, 10, 0, 0),
                TextWrapping = TextWrapping.Wrap
            };

            ScrollViewer scrollViewer = new ScrollViewer
            {
                Margin = new Thickness(0, 40, 0, 0),
                Content = DescriptionTB,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto
            };
            Grid.SetColumn(scrollViewer, 0);
            Grid.SetRow(scrollViewer, 1);

            MoreDetailGrid.Children.Add(DesTB);
            MoreDetailGrid.Children.Add(scrollViewer);


            Button buttonreturn = new Button
            {
                Height = 50,
                Width = 50,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Background = Brushes.White,
                BorderThickness = new Thickness(0),
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

            MoreDetailGrid.Children.Add(buttonreturn);
            DataGrid.Children.Add(usc);
            usclist.Add(usc);

            usc.Content = MoreDetailGrid;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            //items.Remove(stpMain.Children.IndexOf(borderlist[ParseId(btn)]), Inventory);
            //stpMain.Children.RemoveAt(stpMain.Children.IndexOf(borderlist[ParseId(btn)]));
            items.Remove(ParseId(btn), Inventory);
            stpMain.Children.RemoveAt(ParseId(btn));
            items.ResetId(Inventory);

            borderlist.Clear();
            textBlockList.Clear();
            stpMain.Children.Clear();

            LoadInList();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            DataGrid.Children.Remove(darkenGrid);
            IsOpen = false;
        }

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
                        OpenDetailsPanel(ParseId(border));
                    }
                }
            }
        }

        private void Grid_MouseDown(object sender, RoutedEventArgs e)
        {
            Grid grid = (Grid)sender;

            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (checkBoxList[ParseId(grid)].IsChecked == true)
                {
                    checkBoxList[ParseId(grid)].IsChecked = false;
                }
                else
                {
                    checkBoxList[ParseId(grid)].IsChecked = true;
                }
            }
        }

        public void MouseEnterHighLight(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Border))
            {
                Border border = (Border)sender;
                border.Background = Brushes.LightGray;
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

                if (checkBoxList[ParseId(grid)].IsChecked == false)
                {
                    var icon = new PackIcon { Kind = PackIconKind.Check };
                    icon.Height = 20;
                    icon.Width = 20;
                    icon.HorizontalAlignment = HorizontalAlignment.Center;
                    icon.VerticalAlignment = VerticalAlignment.Center;
                    icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
                    checkBoxList[ParseId(grid)].Content = icon;
                }
            }
        }

        public void MouseLeaveUnHighLight(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Border))
            {
                Border border = (Border)sender;
                border.Background = Brushes.White;
            }
            if (sender.GetType() == typeof(CheckBox))
            {
                CheckBox checkBox = (CheckBox)sender;
                if (checkBox.IsChecked == false)
                {
                    var icon = new PackIcon { Kind = PackIconKind.CheckboxBlankCircleOutline };
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
                grid.Background = Brushes.White;

                if (checkBoxList[ParseId(grid)].IsChecked == false)
                {
                    var icon = new PackIcon { Kind = PackIconKind.CheckboxBlankCircleOutline };
                    icon.Height = 20;
                    icon.Width = 20;
                    icon.HorizontalAlignment = HorizontalAlignment.Center;
                    icon.VerticalAlignment = VerticalAlignment.Center;
                    icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));
                    checkBoxList[ParseId(grid)].Content = icon;
                }
            }
        }

        private int ParseId(Button btn)
        {
            string idtext = string.Empty;

            for (int i = 0; i < btn.Name.Length; i++)
            {
                if (char.IsDigit(btn.Name[i]))
                    idtext += btn.Name[i];
            }

            return int.Parse(idtext);
        }

        private int ParseId(CheckBox checkbox)
        {
            string idtext = string.Empty;

            for (int i = 0; i < checkbox.Name.Length; i++)
            {
                if (char.IsDigit(checkbox.Name[i]))
                    idtext += checkbox.Name[i];
            }

            return int.Parse(idtext);
        }

        private int ParseId(Border border)
        {
            string idtext = string.Empty;

            for (int i = 0; i < border.Name.Length; i++)
            {
                if (char.IsDigit(border.Name[i]))
                    idtext += border.Name[i];
            }

            return int.Parse(idtext);
        }

        private int ParseId(Grid grid)
        {
            string idtext = string.Empty;

            for (int i = 0; i < grid.Name.Length; i++)
            {
                if (char.IsDigit(grid.Name[i]))
                    idtext += grid.Name[i];
            }

            return int.Parse(idtext);
        }


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
            //ButtonAddNew.Click += new RoutedEventHandler(this.ReturnButton_Click);

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
        Grid darkenGrid;

        private void OpenDetailsPanel(int id)
        {
            darkenGrid = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#CC000000")),
                Margin = new Thickness(0, 70, 0, 0)
            };

            Grid DetailsGrid = new Grid
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
                Margin = new Thickness(5, 5, 0, 0)
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

            TextBox textBoxTitle = new TextBox
            {
                Style = style,
                Margin = new Thickness(10, 5, 10, 5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextWrapping = TextWrapping.Wrap,
                MaxLength = 100,
                FontSize = 20,
                Text = $"{Inventory[id].Title}"
            };
            HintAssist.SetHint(textBoxTitle, "Title");

            TextBox textBoxDetails = new TextBox
            {
                Style = style,
                Margin = new Thickness(10, 5, 10, 5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextWrapping = TextWrapping.Wrap,
                MaxLength = 500,
                FontSize = 20,
                Text = $"{Inventory[id].Description}"
            };
            HintAssist.SetHint(textBoxDetails, "Details");

            style = this.FindResource("MaterialDesignFloatingHintDatePicker") as Style;

            DatePicker BeginDatePicker = new DatePicker
            {
                Style = style,
                Margin = new Thickness(20, 5, 0, 5),
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 120,
                FontSize = 20,
                Text = $"{string.Format("{0:MM/dd}", Inventory[id].BeginDateTime)}"
            };
            HintAssist.SetHint(BeginDatePicker, "Begin Date");

            DatePicker EndDatePicker = new DatePicker
            {
                Style = style,
                Margin = new Thickness(20, 5, 0, 5),
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 120,
                FontSize = 20,
                Text = $"{string.Format("{0:MM/dd}", Inventory[id].EndDateTime)}"
            };
            HintAssist.SetHint(EndDatePicker, "End Date");

            style = this.FindResource("MaterialDesignFloatingHintTimePicker") as Style;

            TimePicker BeginTimePicker = new TimePicker
            {
                Style = style,
                Margin = new Thickness(0, 5, 20, 5),
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 120,
                FontSize = 20,
                Text = $"{string.Format("{0:h:mm tt}", Inventory[id].BeginDateTime)}"
            };
            HintAssist.SetHint(BeginTimePicker, "Begin Time");

            TimePicker EndTimePicker = new TimePicker
            {
                Style = style,
                Margin = new Thickness(0, 5, 20, 5),
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 120,
                FontSize = 20,
                Text = $"{string.Format("{0:h:mm tt}", Inventory[id].EndDateTime)}"
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
            DetailsSTP.Children.Add(textBoxDetails);
            DetailsSTP.Children.Add(BeginRow);
            DetailsSTP.Children.Add(EndRow);

            DetailsGrid.Children.Add(buttonreturn);
            DetailsGrid.Children.Add(scrollViewer);

            DataGrid.Children.Add(darkenGrid);
            IsOpen = true;
        }
    }
}
