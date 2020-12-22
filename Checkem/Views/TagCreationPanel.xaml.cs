using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Checkem.Views
{
    public partial class TagCreationPanel : UserControl
    {
        public TagCreationPanel()
        {
            InitializeComponent();
        }

        public event EventHandler Create;
        public event EventHandler Close;

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close?.Invoke(this, EventArgs.Empty);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close?.Invoke(this, EventArgs.Empty);
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            Create?.Invoke(this, EventArgs.Empty);
            Close?.Invoke(this, EventArgs.Empty);
        }
    }
}
