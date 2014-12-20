using System.Drawing;
using System.IO;
using System.Web;

namespace TrouveUnBand.Classes
{
    public class Photo
    {
        public static string USER_STOCK_PHOTO_MALE = "/Photos/UserProfilePhoto/_stock_user_male.jpg";
        public static string USER_STOCK_PHOTO_FEMALE = "/Photos/UserProfilePhoto/_stock_user_female.jpg";
        public static string BAND_STOCK_PHOTO = "/Photos/_StockPhotos/_stock_band.jpg";
        public static string EVENT_STOCK_PHOTO = "/Photos/EventPhotos/_stock_event.jpg";
        public static string ADVERT_STOCK_PHOTO = "/Photos/AdvertPhotos/_stock_advert_.jpg";

        public string PhotoSrc { get; set; }

        public int PicX { get; set; }

        public int PicY { get; set; }

        public int PicWidth { get; set; }

        public int PicHeight { get; set; }

        public Rectangle CropRect
        {
            get
            {
                return new Rectangle(PicX, PicY, PicWidth, PicHeight);
            }
        }

        public static bool IsPhoto(HttpPostedFileBase postedPhoto)
        {
            string extension = Path.GetExtension(postedPhoto.FileName).ToLower();
            bool isPhoto = VerifyExtension(extension);

            return isPhoto;
        }

        public static bool IsPhoto(string postedPhotoSrc)
        {
            string extension = Path.GetExtension(postedPhotoSrc).ToLower();
            bool isPhoto = VerifyExtension(extension);

            return isPhoto;
        }

        private static bool VerifyExtension(string extension)
        {
            if (extension != ".jpe" && extension != ".jpg" && extension != ".jpeg" && extension != ".gif" && extension != ".png" &&
                extension != ".pns" && extension != ".bmp" && extension != ".ico" && extension != ".psd" && extension != ".pdd")
            {
                return false;
            }

            return true;
        }
    }
}
