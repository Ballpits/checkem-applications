using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ProjectSC.Functions
{
    class ColorToGradientStopConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var drawingColor = (System.Drawing.Color)value;
            return new Color { A = drawingColor.A, R = drawingColor.R, G = drawingColor.G, B = drawingColor.B };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
