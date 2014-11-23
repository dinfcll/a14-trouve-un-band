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
                string path = HttpContext.Current.Server.MapPath("~/Photos/"+name+".jpg");
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
        //private static void CreateDirectory(string name, )
        //{
        //    Directory.CreateDirectory();
        //}
    }
}