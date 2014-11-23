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
        public static string SavePhoto(Image image)
        {
            try
            {
                string name = GenerateRandomName();
                string path = HttpContext.Current.Server.MapPath("~/Photos/" + name + ".jpg");
                image.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                return path;
            }
            catch
            {
                return "";
            }
            
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

        //public static void SaveAlbum(List<Image> Photos, )
        //{

        //}

        //private static void CreateDirectory(string name, )
        //{
        //    Directory.CreateDirectory();
        //}

        public static string SaveProfilePhoto(Image image,string nickName)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath("~/Photos/UserProfilePhoto/" + nickName + ".jpg");
                image.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                return path;
            }
            catch
            {
                return "";
            }

        }
    }
}