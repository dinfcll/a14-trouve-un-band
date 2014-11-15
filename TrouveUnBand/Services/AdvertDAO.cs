using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc.Html;

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

        public static List<Advert> GetAdverts(int? genre_ID, string keyWord, string location)
        {
            TrouveUnBandEntities db = new TrouveUnBandEntities();

            var adverts = from advert in db.Adverts
                          where
                              advert.Genres.Any(x => x.Genre_ID == genre_ID)
                          select advert;

            if (!String.IsNullOrEmpty(keyWord))
            {
                adverts.Where(x => x.Description.Contains(keyWord));
            }

            if (!String.IsNullOrEmpty(location))
            {
                adverts.Where(x => x.Location.Contains(location));
            }

            return adverts.ToList();
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