using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TrouveUnBand.Classes
{
    public static class PhotoResizer
    {
        public static Image ResizeImage(Image ImageToResize, int minWidth, int minHeight, int maxWidth ,int maxHeight)
        {
            Bitmap NewImage;

            double height = ImageToResize.Height;
            double width = ImageToResize.Width;

            if (width < minWidth)
            {
                height *= minWidth / width;
                width = minWidth;
            }

            if (height < minHeight)
            {
                width *= minHeight / height;
                height = minHeight;
            }

            if (width > maxWidth)
            {
                height *= maxWidth / width;
                width = maxWidth;
            }

            if (height > maxHeight)
            {
                width *= maxHeight / height;
                height = maxHeight;
            }

            NewImage = new Bitmap((int)width, (int)height);
            using (Graphics gr = Graphics.FromImage(NewImage))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.DrawImage(ImageToResize, new Rectangle(0, 0, (int)width, (int)height));
            }

            return (Image)NewImage;

        }
    }
}