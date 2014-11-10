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
    }
}