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


        private int _textboxFontSize;
        public int TextboxFontSize
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
