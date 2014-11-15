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

        public ActionResult LoginFromFacebook()
        {
            return View();
        }

        public ActionResult LoginFromGooglePlus()
        {
            return View();
        }

        public ActionResult RegisterFromFacebook()
        {
            return View();
        }

        public ActionResult RegisterFromGooglePlus()
        {
            return View();
        }
    }
}
