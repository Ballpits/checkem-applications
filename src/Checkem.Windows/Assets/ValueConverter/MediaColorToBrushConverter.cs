using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Checkem.Assets.ValueConverter
{
    class MediaColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = (Color)value;

            return 9;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
