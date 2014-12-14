using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrouveUnBand.Classes;
using TrouveUnBand.Models;
using TrouveUnBand.POCO;

namespace TrouveUnBand.Controllers
{
    public class BaseController : Controller
    {
        protected TrouveUnBandEntities db = new TrouveUnBandEntities();

        protected bool CurrentUserIsAuthenticated()
        {
            return System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        }

        protected User GetAuthenticatedUser()
        {
            if (!CurrentUserIsAuthenticated())
            {
                throw new Exception("User is not authenticated");
            }
            var userName = System.Web.HttpContext.Current.User.Identity.Name;
            var authenticatedUser = db.Users.FirstOrDefault(x => x.Nickname == userName);

            return authenticatedUser;
        }

        public void Success(string message, bool dismissable = false)
        {
            AddAlert(Styles.Success, message, dismissable);
        }

        public void Information(string message, bool dismissable = false)
        {
            AddAlert(Styles.Information, message, dismissable);
        }

        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(Styles.Warning, message, dismissable);
        }

        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(Styles.Danger, message, dismissable);
        }

        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>) TempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });

            TempData[Alert.TempDataKey] = alerts;
        }


        public ActionResult ViewProfile(string type, int Id)
        {
            switch (type.ToUpper())
            {
                case "MUSICIEN":
                    var musician = db.Users.FirstOrDefault(x => x.User_ID == Id);
                    var musicianProfile = CreateProfile.CreateMusicianProfileView(musician);
                    return View("../Users/MusicianProfile", musicianProfile);

                case "BAND":
                    var band = db.Bands.FirstOrDefault(x => x.Band_ID == Id);
                    var bandProfile = CreateProfile.CreateBandProfileView(band);
                    return View("../Group/BandProfile", bandProfile);

                case "EVENT":
                    var events = db.Events.FirstOrDefault(x => x.Event_ID == Id);
                    return View("../Event/EventProfile", events);

                case "ADVERT":
                    var advert = db.Adverts.FirstOrDefault(x => x.Advert_ID == Id);
                    return View("../Advert/AdvertProfile", advert);

                case "PROMOTER":
                    break;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
