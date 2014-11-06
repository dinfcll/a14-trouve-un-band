using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TrouveUnBand.Models;

namespace TrouveUnBand.Controllers
{
    public class HomeController : Controller
    {
        private TrouveUnBandEntities db = new TrouveUnBandEntities();

        public ActionResult Index()
        {
            Newsfeed();
            ViewBag.Message = "Modifiez ce modèle pour dynamiser votre application ASP.NET MVC.";
            RedirectToAction("Newsfeed");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Votre page de description d’application.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Votre page de contact.";

            return View();
        }

        [HttpPost]
        public ActionResult Newsfeed()
        {
            var BandQuery = from band in db.Bands
                            orderby band.BandId descending
                            select new NewsfeedBandModel
                            {
                                BandId = band.BandId,
                                Name = band.Name,
                                Description = band.Description,
                                Location = band.Location,
                            };
            var UserQuery = from user in db.Users
                            orderby user.UserId descending
                            select new NewsfeedUserModel
                            {
                                UserId = user.UserId,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                Nickname = user.Nickname,
                                Photo = user.Photo
                            };

            var EventQuery = from events in db.Events
                            orderby events.EventId descending
                            select new NewsfeedEventModel
                            {
                                EventId = events.EventId,
                                EventName = events.EventName,
                                EventGender =  events.EventGender,
                                EventLocation = events.EventLocation,
                                EventPhoto = events.EventPhoto
                            };

            var AdvertQuery = from advert in db.Adverts
                            orderby advert.AdvertId descending
                            select new NewsfeedAdvertModel
                            {
                                AdvertId = advert.AdvertId,
                                Description = advert.Description,
                                Type = advert.Type,
                                Genre = advert.Genre,
                                AdvertPhoto = advert.AdvertPhoto
                            };

            BandQuery = BandQuery.Take(8);
            UserQuery = UserQuery.Take(8);
            EventQuery = EventQuery.Take(8);
            AdvertQuery = AdvertQuery.Take(8);

            ViewData["NewsfeedBand"] = BandQuery.ToList();
            ViewData["NewsfeedUser"] = UserQuery.ToList();
            ViewData["NewsfeedEvent"] = EventQuery.ToList();
            ViewData["NewsfeedAdvert"] = AdvertQuery.ToList();
            return View("index");
        }
    }
}


