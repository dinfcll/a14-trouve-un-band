using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc.Html;
using TrouveUnBand.Classes;

namespace TrouveUnBand.Models
{
    public class AdvertDAO
    {
        public static List<Advert> GetAllAdverts()
        {
            TrouveUnBandEntities db = new TrouveUnBandEntities();
            List<Advert> advertList = new List<Advert>();
            var advert = db.Adverts;
            advertList.AddRange(advert);

            return advertList;
        }

        public static List<Advert> GetAdverts(int? genre_ID, string keyWord, string location, int radius)
        {
            TrouveUnBandEntities db = new TrouveUnBandEntities();

            var adverts = (from advert in db.Adverts
                           where
                               advert.Genres.Any(x => x.Genre_ID == genre_ID)
                           select advert).ToList();

            if (!String.IsNullOrEmpty(keyWord))
            {
                adverts.Where(x => x.Description.Contains(keyWord));
            }

            if (!String.IsNullOrEmpty(location))
            {
                var advertsToRemove = new List<Advert>();
                var coordinates = Geolocalisation.GetCoordinatesByLocation(location);

                foreach (var ad in adverts)
                {
                    var distance = Geolocalisation.GetDistance(ad.Latitude, ad.Longitude, coordinates.latitude, coordinates.longitude);
                    if (distance > radius)
                        advertsToRemove.Add(ad);
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
            TrouveUnBandEntities db = new TrouveUnBandEntities();

            var adverts = from advert in db.Adverts
                          where
                              advert.Genres.Any(x => x.Genre_ID == genre_ID)
                          select advert;

            return adverts.ToList();
        }

        public static List<Advert> GetAdverts(string keyWord, string location)
        {
            TrouveUnBandEntities db = new TrouveUnBandEntities();

            var adverts = from advert in db.Adverts
                          where
                              advert.Description.Contains(keyWord) &&
                              advert.Location.Contains(location)
                          select advert;

            return adverts.ToList();
        }

        public static List<Advert> GetAdverts(string keyWord)
        {
            TrouveUnBandEntities db = new TrouveUnBandEntities();

            var adverts = from advert in db.Adverts
                          where
                              advert.Description.Contains(keyWord)
                          select advert;

            return adverts.ToList();
        }
    }
}