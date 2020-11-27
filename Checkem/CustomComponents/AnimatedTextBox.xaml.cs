using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Checkem.CustomComponents
{
    public partial class AnimatedTextBox : UserControl, INotifyPropertyChanged
    {
        public AnimatedTextBox()
        {
            DataContext = this;

            InitializeComponent();
        }

        private Thickness _textboxPadding;
        public Thickness TextboxPadding
        {
            get
            {
                return _textboxPadding;
            }

            set
            {
                if (_textboxPadding != value)
                {
                    _textboxPadding = value;

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
