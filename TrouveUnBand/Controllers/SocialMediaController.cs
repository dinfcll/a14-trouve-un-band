using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrouveUnBand.Models;

namespace TrouveUnBand.Controllers
{
    public class SocialMediaController : baseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult test()
        {
            return View();
        }

        public static Band GetBand(int i)
        {
            TrouveUnBandEntities statdb = new TrouveUnBandEntities();
            var Query = statdb.Bands.FirstOrDefault(x => x.Band_ID == i);
            return Query;
        }

        public static Event GetEvent(int i)
        {
            TrouveUnBandEntities statdb = new TrouveUnBandEntities();
            var Query = statdb.Events.FirstOrDefault(x => x.Event_ID == i);
            return Query;
        }
    }
}
