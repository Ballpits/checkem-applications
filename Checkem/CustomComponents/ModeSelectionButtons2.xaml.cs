using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;

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

        private string packIcon;

        public string MyProperty
        {
            get
            {
                return packIcon;
            }
            set
            {
                if (packIcon != value)
                {
                    packIcon = value;

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
