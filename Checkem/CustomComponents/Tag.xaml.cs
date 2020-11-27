using System.Windows;
using Checkem.CustomComponents;
using Cyclops.Models.Objects;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace Checkem.CustomComponents
{
    public partial class Tag : UserControl, INotifyPropertyChanged
    {
        public Tag()
        {
            DataContext = this;

            InitializeComponent();
        }

        public bool IsChecked { get; set; }

        public event EventHandler StateChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        private Brush _color = Brushes.Red;

        public Brush Color
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

        public string Text { get; set; }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void checkbox_Checked(object sender, RoutedEventArgs e)
        {
            IsChecked = true;
        }

        private void checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            IsChecked = false;
        }

        private void TagGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            StateChanged?.Invoke(this, EventArgs.Empty);

            if (checkbox.IsChecked == true)
            {
                checkbox.IsChecked = false;
            }
            else
            {
                checkbox.IsChecked = true;
            }
        }
    }
}
