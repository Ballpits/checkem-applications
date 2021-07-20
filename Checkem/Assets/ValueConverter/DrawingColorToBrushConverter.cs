using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Checkem.Assets.ValueConverter
{
    public class DrawingColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var drawingColor = (System.Drawing.Color)value;

            return new SolidColorBrush(new Color { A = drawingColor.A, R = drawingColor.R, G = drawingColor.G, B = drawingColor.B });
        }

        public object Convert(object value)
        {
            var drawingColor = (System.Drawing.Color)value;

            return new SolidColorBrush(new Color { A = drawingColor.A, R = drawingColor.R, G = drawingColor.G, B = drawingColor.B });
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
<<<<<<< Updated upstream:Checkem/Assets/ValueConverter/DrawingColorToBrushConverter.cs
=======

        public object ConvertBack(object value)
        {
            SolidColorBrush solidColorBrush = new SolidColorBrush((Color)value);

            return System.Drawing.Color.FromArgb(solidColorBrush.Color.A, solidColorBrush.Color.R, solidColorBrush.Color.G, solidColorBrush.Color.B);
        }
>>>>>>> Stashed changes:src/Checkem.Windows/Assets/ValueConverter/DrawingColorToBrushConverter.cs
    }
}
