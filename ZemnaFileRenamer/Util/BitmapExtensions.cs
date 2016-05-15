using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ZemnaFileRenamer.Util
{
    public static class BitmapExtensions
    {
        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr hObject);

        public static ImageSource ToImageSource(this Bitmap bitmap)
        {
            var hbitmap = bitmap.GetHbitmap();
            try
            {
                var imageSource = Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(bitmap.Width, bitmap.Height));

                return imageSource;
            }
            finally
            {
                DeleteObject(hbitmap);
            }
        }
    }
}
