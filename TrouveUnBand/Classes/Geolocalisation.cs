using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrouveUnBand.Classes
{
    public class Geolocalisation
    {
        public int GetDistance(double LatitudeP1, double LongitudeP1, double LatitudeP2, double LongitudeP2)
        {
            double EARTHS_MEAN_RADIUS_IN_KM = 6378.137;
            var lat = ToRadians(LatitudeP2 - LatitudeP1);
            var lng = ToRadians(LongitudeP2 - LongitudeP1);
            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(ToRadians(LatitudeP1)) * Math.Cos(ToRadians(LatitudeP2)) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            int distance = (int)(EARTHS_MEAN_RADIUS_IN_KM * h2);
            return distance;
        }

        private static double ToRadians(double val)
        {
            return (Math.PI / 180) * val;
        }
    }
}