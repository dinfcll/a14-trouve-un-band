using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrouveUnBand.Models
{
    public class geometry
    {
        public Coord coord { get; set; }
    }

    public class Coord
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
}