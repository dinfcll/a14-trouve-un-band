using System;
using System.Collections.Generic;
using System.Linq;
using TrouveUnBand.Classes;
using TrouveUnBand.Models;

namespace TrouveUnBand.Services
{
    public class AdvertDao
    {
        public static List<Advert> GetAllAdverts()
        {
            var db = new TrouveUnBandEntities();
            var adverts = db.Adverts.ToList().Where(x => x.Status == "En cours").ToList();

            return adverts;
        }

        public static List<Advert> GetAdverts(int? genre_ID, string keyWord, string location, int radius)
        {
            var db = new TrouveUnBandEntities();

            var adverts = (from advert in db.Adverts
                           where advert.Genres.Any(x => x.Genre_ID == genre_ID)
                           select advert).ToList();

            if (!String.IsNullOrEmpty(keyWord))
            {
                adverts = adverts.Where(x => x.Description.Contains(keyWord)).ToList();
            }

            if (!String.IsNullOrEmpty(location))
            {
                var advertsToRemove = new List<Advert>();

                foreach (var ad in adverts)
                {
                    if (!Geolocalisation.CheckIfInRange(ad.Location, location, radius))
                    {
                        advertsToRemove.Add(ad);
                    }
                }

                foreach (var ad in advertsToRemove)
                {
                    adverts.Remove(ad);
                }
            }

            return adverts;
        }

        public static List<Advert> GetAdverts(string[] genres, string keyWord, string location, int radius)
        {
            var db = new TrouveUnBandEntities();

            var adverts = (from advert in db.Adverts select advert).ToList();

            if (genres != null)
            {
                foreach (String genreName in genres)
                {
                    adverts = adverts.Where(x => x.Genres.Any(genre => genre.Name == genreName)).ToList();
                }
            }

            if (!String.IsNullOrEmpty(keyWord))
            {
                adverts = adverts.Where(x => x.Description.Contains(keyWord)).ToList();
            }

            if (!String.IsNullOrEmpty(location))
            {
                var advertsToRemove = new List<Advert>();

                foreach (var ad in adverts)
                {
                    if (!Geolocalisation.CheckIfInRange(ad.Location, location, radius))
                    {
                        advertsToRemove.Add(ad);
                    }
                }

                foreach (var ad in advertsToRemove)
                {
                    adverts.Remove(ad);
                }
            }

            return adverts;
        }

        public static List<Advert> GetAdverts(int? genre_ID)
        {
            var db = new TrouveUnBandEntities();

            var adverts = from advert in db.Adverts
                          where advert.Genres.Any(x => x.Genre_ID == genre_ID)
                          select advert;

            return adverts.ToList();
        }

        public static List<Advert> GetAdverts(string keyWord, string location)
        {
            var db = new TrouveUnBandEntities();

            var adverts = from advert in db.Adverts where
                              advert.Description.Contains(keyWord) &&
                              advert.Location.Contains(location)
                          select advert;

            return adverts.ToList();
        }

        public static List<Advert> GetAdverts(string keyWord)
        {
            var db = new TrouveUnBandEntities();

            var adverts = from advert in db.Adverts
                          where advert.Description.Contains(keyWord)
                          select advert;

            return adverts.ToList();
        }
    }
}
