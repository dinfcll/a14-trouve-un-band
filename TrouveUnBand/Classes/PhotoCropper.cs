using System.Drawing;
using System.Drawing.Drawing2D;

namespace TrouveUnBand.Classes
{
    public static class PhotoCropper
    {
        public static Image CropImage(Image image, Rectangle cropRect)
        {
            var originalImage = new Bitmap(image);

            if (cropRect.Height != 0 && cropRect.Width != 0)
            {
                var NewImage = new Bitmap(cropRect.Width, cropRect.Height);

                DrawNewImage(originalImage, NewImage, cropRect);

                return NewImage;
            }

            return (Image)originalImage;
        }

        private static Image DrawNewImage(Bitmap originalImage, Bitmap newImage, Rectangle cropRect)
        {
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;

                g.DrawImage(originalImage, new Rectangle(0, 0, newImage.Width, newImage.Height), cropRect, GraphicsUnit.Pixel);
            }

            return (Image)newImage;
        }
    }
}
