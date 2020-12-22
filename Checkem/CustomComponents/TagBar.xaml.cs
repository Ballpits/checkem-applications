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


namespace Checkem.CustomComponents
{
    public partial class TagBar : UserControl
    {
        public TagBar()
        {
            InitializeComponent();
        }

        private void ButtonCreateTag_Click(object sender, RoutedEventArgs e)
        {
            Tag tag = new Tag()
            {
                Text = "Test",
                Color = Brushes.Red,
            };

            tag.StateChanged += new EventHandler(this.Tag_StateChange);

            StpTagList.Children.Add(tag);
        }

        private void Tag_StateChange(object sender, EventArgs e)
        {
            Tag tag = sender as Tag;
            System.Windows.Forms.MessageBox.Show($"Tag.IsSelected = {tag.IsSelected}");
        }
    }
}
