using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Data;
using System.Windows;
using ZemnaFileRenamer.Util;
using System.Drawing;

namespace ZemnaFileRenamer.ValueConverter
{
    public class BitmapToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Bitmap)
            {
                Bitmap val = (Bitmap)value;

                return BitmapExtensions.ToImageSource(val);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
