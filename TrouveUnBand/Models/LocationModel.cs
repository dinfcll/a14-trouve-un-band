using System.Collections.Generic;

namespace TrouveUnBand.Models
{
    public class LocationModels
    {
        public List<Results> results { get; set; }
    }

    public class Results
    {
        public Geometry geometry { get; set; }
        public string formatted_address { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
}
