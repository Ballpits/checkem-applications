using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;

namespace Checkem.CustomComponents
{
    public partial class PreviewTag : UserControl, INotifyPropertyChanged
    {
        public PreviewTag()
        {
            DataContext = this;

            InitializeComponent();
        }


        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion


        #region Property
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
    }
}
