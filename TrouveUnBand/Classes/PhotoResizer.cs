using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TrouveUnBand.Classes
{
    public static class PhotoResizer
    {
        public static Image ResizeImage(Image ImageToResize, int MinHeight, int MinWidth, int MaxHeight, int MaxWidth)
        {
            Bitmap NewImage;

            int OriginalHeight = ImageToResize.Height;
            int OriginalWidth = ImageToResize.Width;
            int NewHeight;
            int NewWidth;

            if (OriginalHeight < MinHeight || OriginalWidth < MinWidth)
            {
                if (OriginalHeight < MinHeight)
                {
                    NewHeight = MinHeight;
                }
                else
                {
                    NewHeight = ImageToResize.Height;
                }

                if (OriginalWidth < MinWidth)
                {
                    NewWidth = MinWidth;
                }
                else
                {
                    NewWidth = ImageToResize.Width;
                }
            }
            else
            {
                if (OriginalHeight > MaxHeight)
                {
                    NewHeight = MaxHeight;
                }
                else
                {
                    NewHeight = ImageToResize.Height;
                }

                if (OriginalWidth > MaxWidth)
                {
                    NewWidth = MaxWidth;
                }
                else
                {
                    NewWidth = ImageToResize.Width;
                }
            }

            NewImage = new Bitmap(NewWidth, NewHeight);
            using (Graphics gr = Graphics.FromImage(NewImage))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.DrawImage(ImageToResize, new Rectangle(0, 0, NewWidth, NewHeight));
            }

            return (Image)NewImage;

        }
    }
}