using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc.Html;

namespace TrouveUnBand.Models
{
    public class BandDao
    {
        public static List<Band> GetBands(int? genreId, string bandName, string location)
        {
            var db = new TrouveUnBandEntities();
            var lstResults = new List<Band>();
            var bands = from band in db.Bands select band;

            if (genreId > 0)
            {
                bands = bands.Where(band => band.Sub_Genres.Any(genre => genre.GenreId == genreId));
            }
            if (!String.IsNullOrEmpty(bandName))
            {
                bands = bands.Where(band => band.Name.Contains(bandName));
            }
            if (!String.IsNullOrEmpty(location))
            {
                bands = bands.Where(band => band.Location.Contains(location));
            }

            lstResults.AddRange(bands);

            return lstResults;
        }

        public static List<Band> GetBands(List<String> subgenres, string bandName, string location)
        {
            var db = new TrouveUnBandEntities();
            var lstResults = new List<Band>();
            var bands = from band in db.Bands select band;

            if (subgenres != null && subgenres.Count > 0)
            {
                foreach (String genreName in subgenres)
                {
                    bands = bands.Where(band => band.Sub_Genres.Any(genre => genre.Name == genreName));
                }
            }
            if (!String.IsNullOrEmpty(bandName))
            {
                bands = bands.Where(band => band.Name.Contains(bandName));
            }
            if (!String.IsNullOrEmpty(location))
            {
                bands = bands.Where(band => band.Location.Contains(location));
            }

            lstResults.AddRange(bands);

            return lstResults;
        }
    }
}