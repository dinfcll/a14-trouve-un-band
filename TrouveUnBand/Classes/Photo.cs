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
       public static string USER_STOCK_PHOTO = "/Photos/UserProfilePhoto/_stock_user.jpg";
       public static string BAND_STOCK_PHOTO = "BAND_PHOTO";
       public static string EVENT_STOCK_PHOTO = "/Photos/EventPhotos/_stock_event.jpg";
       public static string ADVERT_STOCK_PHOTO = "ADVERT_PHOTO";

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
                if (value != null)
                {
                    PhotoSrc = "data:image/jpeg;base64," + Convert.ToBase64String(value);
                }
            }
        }

        private static byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Jpeg);

            return ms.ToArray();
        }

        public static bool IsPhoto(HttpPostedFileBase PostedPhoto)
        {
            string extension = Path.GetExtension(PostedPhoto.FileName).ToLower();

            if (extension != ".jpe" && extension != ".jpg" && extension != ".jpeg" && extension != ".gif" && extension != ".png" &&
                extension != ".pns" && extension != ".bmp" && extension != ".ico" && extension != ".psd" && extension != ".pdd")
            {
                return false;
            }

            return true;
        }
    }
}
