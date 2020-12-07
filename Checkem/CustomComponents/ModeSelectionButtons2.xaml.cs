using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Checkem.CustomComponents
{
    public partial class ModeSelectionButtons2 : UserControl, INotifyPropertyChanged
    {
        public ModeSelectionButtons2()
        {
            DataContext = this;

            InitializeComponent();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private string _Option1Content;
        public string Option1Content
        {
            get
            {
                return _Option1Content;
            }
            set
            {
                if (_Option1Content != value)
                {
                    _Option1Content = value;

                    OnPropertyChanged();
                }
            }
        }

        private string _Option2Content;
        public string Option2Content
        {
            get
            {
                return _Option2Content;
            }
            set
            {
                if (_Option2Content != value)
                {
                    _Option2Content = value;

                    OnPropertyChanged();
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
