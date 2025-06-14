using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Foundry.Autocrat.Extensions.Drawing
{
    public static class DrawingExtensions
    {
        #region Color Comparison Methods

        public static bool IsNear(this Color color, Color other, int threshold)
        {
            if (threshold > 255 || threshold < 0)
                throw new ArgumentException("threshold must be between 0 and 255, inclusive", "threshold");
            return (
                (color.R <= (other.R + threshold) && color.R >= (other.R - threshold)) &&
                (color.G <= (other.G + threshold) && color.G >= (other.G - threshold)) &&
                (color.B <= (other.B + threshold) && color.B >= (other.B - threshold))
                );
        }

        public static bool IsRed(this Color color, int threshold)
        {
            return color.IsNear(Color.Red, threshold);
        }

        public static bool IsGreen(this Color color, int threshold)
        {
            return color.IsNear(Color.Green, threshold);
        }

        public static bool IsBlue(this Color color, int threshold)
        {
            return color.IsNear(Color.Blue, threshold);
        }

        public static bool IsWhite(this Color color, int threshold)
        {
            return color.IsNear(Color.White, threshold);
        }

        public static bool IsBlack(this Color color, int threshold)
        {
            return color.IsNear(Color.Black, threshold);
        }

        #endregion

        #region Bitmap Hashing

        public static byte[] HashBitmap(this Bitmap b)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                b.Save(ms, ImageFormat.Bmp);
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin);

                MD5 md5 = MD5.Create();
                byte[] hash = md5.ComputeHash(ms);

                return hash;
            }
        }

        #endregion
    }
}
