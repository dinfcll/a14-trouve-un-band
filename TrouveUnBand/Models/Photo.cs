using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace TrouveUnBand.Models
{
    public class Photo
    {
        byte[] m_PhotoArray;

        string m_PhotoSrc;

        public int PicX { get; set; }

        public int PicY { get; set; }

        public int PicWidth { get; set; }

        public int PicHeight { get; set; }

        public byte[] PhotoArray
        {
            get
            {
                return m_PhotoArray;
            }
            set
            {
                m_PhotoArray = value;
                m_PhotoSrc = "data:image/jpeg;base64," + Convert.ToBase64String(value);
            }
        }

        public string PhotoSrc
        {
            get
            {
                return m_PhotoSrc;
            }
            set
            {
                m_PhotoSrc = value;
            }
        }

        public byte[] StockPhoto
        {
            get
            {
                return getStockPhoto();
            }
        }

        private byte[] getStockPhoto()
        {
            string path = HttpContext.Current.Server.MapPath("~/Images/stock_user.jpg");
            Image stock = Image.FromFile(path);
            return imageToByteArray(stock);
        }

        private byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public void CropImage(Image image)
        {
            if (image.Height < 172 || image.Width < 250 || image.Height > 413 || image.Width > 600)
            {
                image = ResizeOriginalImage(image, 172, 250, 413, 600);
            }
            
            var CropRect = new Rectangle(PicX, PicY, PicWidth, PicHeight);
            Bitmap btmOriginalImage = new Bitmap(image);
            Bitmap btmNewImage = new Bitmap(CropRect.Width, CropRect.Height);

            using (Graphics g = Graphics.FromImage(btmNewImage))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;

                g.DrawImage(btmOriginalImage, new Rectangle(0, 0, btmNewImage.Width, btmNewImage.Height), CropRect, GraphicsUnit.Pixel);
            }

            m_PhotoArray = GetBitmapBytes(btmNewImage);
        }

        private static byte[] GetBitmapBytes(Bitmap source)
        {
            ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders()[4];
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

            using (MemoryStream tmpStream = new MemoryStream())
            {
                source.Save(tmpStream, codec, parameters);

                byte[] result = new byte[tmpStream.Length];
                tmpStream.Seek(0, SeekOrigin.Begin);
                tmpStream.Read(result, 0, (int)tmpStream.Length);

                return result;
            }
        }

        private Image ResizeOriginalImage(Image ImageToResize, int MinHeight, int MinWidth, int MaxHeight, int MaxWidth)
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
