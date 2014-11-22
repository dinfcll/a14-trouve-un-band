using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrouveUnBand.Models;

namespace TrouveUnBand.Controllers
{
    public class SocialMediaController : Controller
    {
        private TrouveUnBandEntities db = new TrouveUnBandEntities();

        public ActionResult Index()
        {
            return View();
        }

        public static Band GetBand(int i)
        {
            TrouveUnBandEntities statdb = new TrouveUnBandEntities();
            var Query = statdb.Bands.FirstOrDefault(x => x.BandId == i);
            return Query;
        }

        public static Event GetEvent(int i)
        {
            TrouveUnBandEntities statdb = new TrouveUnBandEntities();
            var Query = statdb.Events.FirstOrDefault(x => x.EventId == i);
            return Query;
        }
    }
}
