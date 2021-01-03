using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Checkem.Views
{
    public partial class TagCreationPanel : UserControl
    {
        public TagCreationPanel()
        {
            InitializeComponent();
        }

        public event EventHandler CreateButtonClicked;
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
            CreateButtonClicked?.Invoke(this, EventArgs.Empty);
            Close?.Invoke(this, EventArgs.Empty);
        }
    }
}
