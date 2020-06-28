using System.Windows.Controls;
using System.Windows.Media;

namespace ProjectSC
{
    public static class MainWindowMouseover
    {
        public static void Highlight(object sender)
        {
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF5CB7FF"));
        }
        public static void UnHighlight(object sender)
        {
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#002196F3"));
        }
    }
}
