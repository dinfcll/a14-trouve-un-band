using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrouveUnBand.Models
{
    public class Photo
    {
        public byte[] byteProfilePicture { get; set; }

        public string stringProfilePicture { get; set; }

        public int PicX { get; set; }

        public int PicY { get; set; }

        public int PicWidth { get; set; }

        public int PicHeight { get; set; }

    }
}
