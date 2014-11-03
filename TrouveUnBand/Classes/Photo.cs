using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace TrouveUnBand.Classes
{
    public class Photo
    {
        byte[] m_PhotoArray;

        public string PhotoSrc { get; set; }

        public int PicX { get; set; }

        public int PicY { get; set; }

        public int PicWidth { get; set; }

        public int PicHeight { get; set; }

        public Rectangle CropRect
        {
            get
            {
                Rectangle CropRect = new Rectangle(PicX, PicY, PicWidth, PicHeight);
                return CropRect;
            }
        }

        public byte[] PhotoArray
        {
            get
            {
                return m_PhotoArray;
            }
            set
            {
                m_PhotoArray = value;
                PhotoSrc = "data:image/jpeg;base64," + Convert.ToBase64String(value);
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
            var stock = Image.FromFile(path);

            return imageToByteArray(stock);
        }

        private byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Jpeg);

            return ms.ToArray();
        }
    }
}
