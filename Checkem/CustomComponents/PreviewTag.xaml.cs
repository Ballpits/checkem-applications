using Checkem.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Checkem.CustomComponents
{
    public partial class PreviewTag : UserControl
    {
        public PreviewTag()
        {
            DataContext = this;

            InitializeComponent();
        }

        public PreviewTag(TagItem item)
        {
            DataContext = this;

            tagItem = item;

            ValueSetting();

            InitializeComponent();
        }


        #region Event

        #endregion


        #region Property

        public TagItem tagItem;



        public SolidColorBrush Color
        {
            get { return (SolidColorBrush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(SolidColorBrush), typeof(PreviewTag), new PropertyMetadata(Brushes.Transparent));




        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(PreviewTag), new PropertyMetadata(string.Empty));

        #endregion


        //Get Data from Tag and show it as preview
        //my English Suck
        private void ValueSetting()
        {
            if (tagItem != null)
            {
                Text = tagItem.Content;
                Color = tagItem.TagColor;
            }
        }

        private void MenuItem_Remove(object sender, RoutedEventArgs e)
        {

        }
    }
}
