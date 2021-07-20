using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Checkem.Models;

namespace Checkem.Windows.CustomComponents
{
    public partial class Tag : UserControl
    {
        public Tag()
        {
            DataContext = this;

            InitializeComponent();
        }

        public Tag(TagItem item)
        {
            InitializeComponent();

            tagItem = item;

            LoadupTag();
        }


<<<<<<< Updated upstream:Checkem/CustomComponents/Tag.xaml.cs
=======
        DrawingColorToBrushConverter DrawingColorToBrushConverter = new DrawingColorToBrushConverter();


>>>>>>> Stashed changes:src/Checkem.Windows/CustomComponents/Tag.xaml.cs
        #region Event
        public event EventHandler StateChanged;
        public event EventHandler Remove;
        #endregion


        #region Property
        public TagItem tagItem = new TagItem();


        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(Tag), new PropertyMetadata(false));


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(Tag), new PropertyMetadata(string.Empty));


        public SolidColorBrush Color
        {
            get { return (SolidColorBrush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(SolidColorBrush), typeof(Tag), new PropertyMetadata(Brushes.Transparent));
        #endregion


        private void checkbox_Checked(object sender, RoutedEventArgs e)
        {
            IsSelected = true;
        }

        private void checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            IsSelected = false;
        }

        private bool LeftMousePressed = false;
        private void TagGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && IsMouseOver)
            {
                LeftMousePressed = true;
            }
            else
            {
                LeftMousePressed = false;
            }
        }

        private void TagGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released && IsMouseOver && LeftMousePressed)
            {
                if (checkbox.IsChecked == true)
                {
                    checkbox.IsChecked = false;
                }
                else
                {
                    checkbox.IsChecked = true;
                }

                StateChanged?.Invoke(this, EventArgs.Empty);
            }
        }
<<<<<<< Updated upstream:Checkem/CustomComponents/Tag.xaml.cs
        //It was used get all the tag has been created
        //It didn't work, but I think I did something wrong so I keep it and want to fix it later
        private void LoadupTag()
=======


        private void LoadTag()
>>>>>>> Stashed changes:src/Checkem.Windows/CustomComponents/Tag.xaml.cs
        {
            if (tagItem != null)
            {
<<<<<<< Updated upstream:Checkem/CustomComponents/Tag.xaml.cs
                Color = tagItem.TagColor;
                Text = tagItem.Content;
=======
                Color = (SolidColorBrush)DrawingColorToBrushConverter.Convert(item.Color);
                Text = item.Content;
>>>>>>> Stashed changes:src/Checkem.Windows/CustomComponents/Tag.xaml.cs
            }
        }

        private void MenuItem_Remove(object sender, RoutedEventArgs e)
        {
            Remove?.Invoke(this, EventArgs.Empty);
        }
    }
}
