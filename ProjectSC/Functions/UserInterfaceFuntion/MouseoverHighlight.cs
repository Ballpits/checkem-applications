using System.Windows.Controls;
using System.Windows.Media;

namespace ProjectSC
{
    public static class MouseoverHighlight
    {
        public static void Highlight(object sender,string ColorHexCode)
        {
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(ColorHexCode));
        }

        public static void UnHighlight(object sender, string ColorHexCode)
        {
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(ColorHexCode));
        }
    }
}
