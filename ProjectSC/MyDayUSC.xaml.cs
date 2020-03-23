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

            for (int i = 0; i < Inventory.Count; i++)
            {
                AddItem(i);
            }
        }

        Items items = new Items();

        private List<Items> Inventory = new List<Items>();

        private List<Border> borderlist = new List<Border>();
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
            grid.MouseDown += new MouseButtonEventHandler(this.Border_MouseDown);
            border.Child = grid;

            Style style = this.FindResource("MaterialDesignFlatPrimaryToggleButton") as Style;

            CheckBox checkBox = new CheckBox
            {
                Name = $"checkboc{id}",
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Left,
                Height = 23,
                Width = 23,
                Margin = new Thickness(5, 0, 0, 0),
                Style = style,
                IsTabStop = false,
            };
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
                Margin = new Thickness(30, 0, 0, 0),
                FontSize = 20
            };
            textBlockList.Add(textBlock);

            Button buttonImp = new Button
            {
                Content = "Mark As Important"
            };
            buttonImp.Click += new RoutedEventHandler(this.MarkButton_Click);

            Button buttonMd = new Button
            {
                Name = $"buttonMd{id}",
                Content = "More Details"
            };
            buttonMd.Click += new RoutedEventHandler(this.DetailButton_Click);

            Button buttonEd = new Button
            {
                Content = "Edit"
            };
            buttonEd.Click += new RoutedEventHandler(this.EditButton_Click);

            Separator separator = new Separator();

            Button buttonRm = new Button
            {
                Name = $"buttonRm{id}",
                Content = "Remove"
            };
            buttonRm.Click += new RoutedEventHandler(this.RemoveButton_Click);

            StackPanel sp = new StackPanel();
            sp.Children.Add(buttonImp);
            sp.Children.Add(buttonMd);
            sp.Children.Add(buttonEd);
            sp.Children.Add(separator);
            sp.Children.Add(buttonRm);

            PopupBox popupBox = new PopupBox
            {
                Margin = new Thickness(0, 0, 10, 0),
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Right,
                PopupContent = sp
            };
            grid.Children.Add(checkBox);
            grid.Children.Add(textBlock);
            grid.Children.Add(popupBox);
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

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

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

            for (int i = 0; i < Inventory.Count; i++)
            {
                AddItem(i);
            }
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            DataGrid.Children.Remove(darkenGrid);
            IsOpen = false;
        }

        bool IsOpen = false;
        Grid darkenGrid;
        private void Border_MouseDown(object sender, RoutedEventArgs e)
        {
            Border border = new Border();
            try
            {
                border = (Border)sender;
            }
            catch { }

            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (!IsOpen)
                {
                    darkenGrid = new Grid
                    {
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#CC000000")),
                        Margin = new Thickness(0, 70, 0, 0)
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


                    Grid grid = new Grid
                    {
                        HorizontalAlignment = HorizontalAlignment.Right,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        Width = 400,
                        Background = Brushes.White
                    };
                    darkenGrid.Children.Add(grid);
                    grid.Children.Add(buttonreturn);
                    DataGrid.Children.Add(darkenGrid);
                    IsOpen = true;
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
                    icon.Height = 19;
                    icon.Width = 19;
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
    }
}
