using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
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


        #region dpColorTest
        //public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(nameof(Color), typeof(Color), typeof(ColorPicker),
        //    new FrameworkPropertyMetadata(default(SolidColorBrush), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ColorPropertyChangedCallback));

        //public SolidColorBrush Color
        //{
        //    get => (SolidColorBrush)GetValue(ColorProperty);
        //    set => SetValue(ColorProperty, value);
        //}
        #endregion

        private SolidColorBrush _Color;
        public SolidColorBrush Color
        {
            get
            {
                return _Color;
            }
            set
            {
                if (_Color != value)
                {
                    _Color = value;
                }

                OnPropertyChanged();
            }
        }



        //public SolidColorBrush Color
        //{
        //    get { return (SolidColorBrush)GetValue(ColorProperty); }
        //    set { SetValue(ColorProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ColorProperty =
        //    DependencyProperty.Register("Color", typeof(Brush), typeof(PreviewTag), new PropertyMetadata(0));


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
