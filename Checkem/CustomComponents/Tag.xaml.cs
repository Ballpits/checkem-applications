using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Checkem.Models;

namespace Checkem.CustomComponents
{
    public partial class Tag : UserControl, INotifyPropertyChanged
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
        }
        #region Event
        public event EventHandler StateChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion


        #region Property
        TagItem tagItem = new TagItem();

        public bool IsSelected { get; set; } = false;

        private SolidColorBrush _color;
        public SolidColorBrush Color
        {
            get
            {
                return _color;
            }
            set
            {
                if (_color != value)
                {
                    _color = value;

                    OnPropertyChanged();
                }
            }
        }

        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (_text != value)
                {
                    _text = value;

                    OnPropertyChanged();
                }
            }
        }
        #endregion


        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

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
    }
}
