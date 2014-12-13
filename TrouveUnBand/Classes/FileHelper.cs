using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace TrouveUnBand.Classes
{
    public static class FileHelper
    {
        public static class Category
        {
            public static string USER_PROFILE_PHOTO = "/Photos/UserProfilePhoto/";
            public static string BAND_PHOTO = "BAND_PHOTO";
            public static string EVENT_PHOTO = "/Photos/EventPhotos/";
            public static string ADVERT_PHOTO = "/Photos/AdvertPhotos/";
        }
        
        public static string SavePhoto(Image image, string name, string category)
        {
            bool isSaved = false;
            string path = category + name + ".jpg";

             isSaved = Save(image, path);

            if(!isSaved)
            {
                path = "";
            }

            return path;
        }

        public static string SavePhoto(Image image, string category)
        {
            string name = GenerateRandomName();
            
            return SavePhoto(image, name, category);
        }

        private static string GenerateRandomName()
        {
            string name = "";
            string possibleChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

            for(int i=0;i<16;i++)
            {
                name += possibleChar.ElementAt(random.Next(possibleChar.Length));
            }

            return name;
        }

        private static bool Save(Image image,string path)
        {
            try
            {
                string serverPath = HttpContext.Current.Server.MapPath(path);
                image.Save(serverPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeletePhoto(string name, string category)
        {
            string path = category + name + ".jpg";
            string serverPath = HttpContext.Current.Server.MapPath(path);

            try
            {
                File.Delete(serverPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //public static void SaveAlbum(List<Image> Photos, )
        //{

        //}

        //private static void CreateDirectory(string name, )
        //{
        //    Directory.CreateDirectory();
        //}
    }
}