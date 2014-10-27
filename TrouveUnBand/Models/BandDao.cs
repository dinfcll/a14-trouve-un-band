using System;
using System.Collections.Generic;
using System.Linq;

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
                bands = subgenres.Aggregate(bands, (current, genreName) => current.Where(band => band.Sub_Genres.Any(genre => genre.Name.Equals(genreName))));
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