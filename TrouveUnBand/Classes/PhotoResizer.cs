using System.Drawing;
using System.Drawing.Drawing2D;

namespace TrouveUnBand.Classes
{
    public static class PhotoResizer
    {
        public static Image ResizeImage(Image imageToResize, int minWidth, int minHeight, int maxWidth ,int maxHeight)
        {
            Bitmap newImage;

            double height = imageToResize.Height;
            double width = imageToResize.Width;

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

            newImage = new Bitmap((int)width, (int)height);
            using (Graphics gr = Graphics.FromImage(newImage))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.DrawImage(imageToResize, new Rectangle(0, 0, (int)width, (int)height));
            }

            return (Image)newImage;

        }
    }
}