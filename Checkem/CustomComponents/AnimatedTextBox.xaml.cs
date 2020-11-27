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


        private double _textboxFontSize;
        public double TextboxFontSize
        {
            get
            {
                return _textboxFontSize;
            }

            set
            {
                if (_textboxFontSize != value)
                {
                    _textboxFontSize = value;

                    OnPropertyChanged();
                }
            }
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

        private string _textboxText;
        public string TextboxText
        {
            get
            {
                return _textboxText;
            }

            set
            {
                if (_textboxText != value)
                {
                    _textboxText = value;

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
