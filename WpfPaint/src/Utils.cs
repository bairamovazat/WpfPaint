using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfPaint
{
    class Utils
    {
        public static BitmapSource GetWhiteBitmap(int width, int height)
        {

            double dpi = 96;
            byte[] pixelData = new byte[width * height];


            for (int x = 0; x < pixelData.Length; ++x)
            {
                pixelData[x] = (byte)(255);
            }

            BitmapSource bmpSource = BitmapSource.Create(width, height, dpi, dpi,
                PixelFormats.Gray8, null, pixelData, width);

            return bmpSource;
        }
    }
}
