using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace ProjectSC.Views.Calendar
{
    public partial class CalendarDay_View : UserControl, INotifyPropertyChanged
    {
        public CalendarDay_View(int data)
        {
            _Date = data;

            DataContext = this;

            InitializeComponent();
        }


        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion


        #region Property
        private int _Date;
        public int Date
        {
            get
            {
                return _Date;
            }
            set
            {
                if (_Date != value)
                {
                    _Date = value;

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
