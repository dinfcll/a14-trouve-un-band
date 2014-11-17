using System;
using System.Collections.Generic;
using System.Linq;
using TrouveUnBand.Classes;
using TrouveUnBand.Models;

namespace TrouveUnBand.Services
{
    public class BandDao
    {
        public static List<Band> GetBands(string bandName)
        {
            var lstResults = GetBands(0, bandName, "", 0);

            return lstResults;
        }

        public static List<Band> GetBands(int? genreId, string bandName, string location, int radius)
        {
            var db = new TrouveUnBandEntities();
            var lstResults = new List<Band>();
            var bands = db.Bands.ToList();

            if (genreId > 0)
            {
                bands = bands.Where(band => band.Genres.Any(genre => genre.Genre_ID == genreId)).ToList();
            }

            if (!String.IsNullOrEmpty(bandName))
            {
                bands = bands.Where(band => band.Name.Contains(bandName)).ToList();
            }

            if (!String.IsNullOrEmpty(location))
            {
                var bandsToRemove = new List<Band>();
                foreach (var band in bands)
                {
                    if (!Geolocalisation.CheckIfInRange(band.Location, location, radius))
                    {
                        bandsToRemove.Add(band);
                    }
                }

                foreach (var band in bandsToRemove)
                {
                    bands.Remove(band);
                }
            }

            lstResults.AddRange(bands);

            return lstResults;
        }

        public static List<Band> GetBands(List<String> genres, string bandName, string location, int radius)
        {
            var db = new TrouveUnBandEntities();
            var lstResults = new List<Band>();
            var bands = db.Bands.ToList();

            if (genres.Count > 0)
            {
                foreach (String genreName in genres)
                {
                    bands = bands.Where(band => band.Genres.Any(genre => genre.Name == genreName)).ToList();
                }
            }

            if (!String.IsNullOrEmpty(bandName))
            {
                bands = bands.Where(band => band.Name.Contains(bandName)).ToList();
            }

            if (!String.IsNullOrEmpty(location))
            {
                var bandsToRemove = new List<Band>();
                foreach (var band in bands)
                {
                    if (!Geolocalisation.CheckIfInRange(band.Location, location, radius))
                    {
                        bandsToRemove.Add(band);
                    }
                }

                foreach (var band in bandsToRemove)
                {
                    bands.Remove(band);
                }
            }

            lstResults.AddRange(bands);

            return lstResults;
        }

        public static List<Band> GetAllBands()
        {
            var db = new TrouveUnBandEntities();
            var eventList = new List<Band>();
            var band = db.Bands;
            eventList.AddRange(band);

            return eventList;
        }
    }
}