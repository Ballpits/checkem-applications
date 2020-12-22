using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Checkem.CustomComponents
{
    public partial class TagBar : UserControl
    {
        public TagBar()
        {
            InitializeComponent();
        }

        public event EventHandler OpenPanel;

        public void Create()
        {
            #region create new tag function goes here
            Tag tag = new Tag()
            {
                Text = "Test",
                Color = Brushes.Red,
            };

            tag.StateChanged += new EventHandler(this.Tag_StateChange);

            StpTagList.Children.Add(tag);
            #endregion
        }

        private void ButtonCreateTag_Click(object sender, RoutedEventArgs e)
        {
            //tell parent control to open panel
            OpenPanel?.Invoke(this, EventArgs.Empty);
        }

        private void Tag_StateChange(object sender, EventArgs e)
        {
            Tag tag = sender as Tag;
            System.Windows.Forms.MessageBox.Show($"Tag.IsSelected = {tag.IsSelected}");
        }
    }
}
