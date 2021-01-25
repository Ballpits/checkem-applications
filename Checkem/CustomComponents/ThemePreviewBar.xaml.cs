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

namespace Checkem.Windows.CustomComponents
{
    public partial class ThemePreviewBar : UserControl
    {
        public ThemePreviewBar()
        {
            DataContext = this;

            InitializeComponent();
        }





        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ThemePreviewBar), new PropertyMetadata(string.Empty));



        public Color Gradient1
        {
            get { return (Color)GetValue(Gradient1Property); }
            set { SetValue(Gradient1Property, value); }
        }

        // Using a DependencyProperty as the backing store for Gradient1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Gradient1Property =
            DependencyProperty.Register("Gradient1", typeof(Color), typeof(ThemePreviewBar), new PropertyMetadata(new Color()));



        public Color Gradient2
        {
            get { return (Color)GetValue(Gradient2Property); }
            set { SetValue(Gradient2Property, value); }
        }

        // Using a DependencyProperty as the backing store for Gradient1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Gradient2Property =
            DependencyProperty.Register("Gradient2", typeof(Color), typeof(ThemePreviewBar), new PropertyMetadata(new Color()));
    }
}
