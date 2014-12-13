using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using TrouveUnBand.Models;

namespace TrouveUnBand.Classes
{
    public static class PhotoCropper
    {
        public static Image CropImage(Image image, Rectangle CropRect)
        {
            var OriginalImage = new Bitmap(image);

            if (CropRect.Height != 0 && CropRect.Width != 0)
            {
                var NewImage = new Bitmap(CropRect.Width, CropRect.Height);

                var newImage = DrawNewImage(OriginalImage, NewImage, CropRect);

                return NewImage;
            }

            return (Image)OriginalImage;
        }

        private static Image DrawNewImage(Bitmap OriginalImage, Bitmap NewImage, Rectangle CropRect)
        {
            using (Graphics g = Graphics.FromImage(NewImage))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;

                g.DrawImage(OriginalImage, new Rectangle(0, 0, NewImage.Width, NewImage.Height), CropRect, GraphicsUnit.Pixel);
            }

            return (Image)NewImage;
        }
    }
}
