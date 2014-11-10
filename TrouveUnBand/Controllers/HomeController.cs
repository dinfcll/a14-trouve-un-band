using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TrouveUnBand.Models;
using TrouveUnBand.Services;

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
                            orderby band.Band_ID descending
                            select new NewsfeedBandModel
                            {
                                BandId = band.Band_ID,
                                Name = band.Name,
                                Description = band.Description,
                                Location = band.Location,
                            };
            var UserQuery = from user in db.Users
                            orderby user.User_ID descending
                            select new NewsfeedUserModel
                            {
                                UserId = user.User_ID,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                Nickname = user.Nickname,
                                Photo = user.Photo
                            };
            var EventQuery = from events in db.Events
                            orderby events.Event_ID descending
                            select new NewsfeedEventModel
                            {
                                EventId = events.Event_ID,
                                EventName = events.Name,
                                Genres = events.Genres.ToList(),
                                EventLocation = events.Location,
                                EventPhoto = events.Photo
                            };
            var events_ = EventDAO.GetAllEvents();

            var AdvertQuery = from advert in db.Adverts
                            orderby advert.Advert_ID descending
                            select new NewsfeedAdvertModel
                            {
                                AdvertId = advert.Advert_ID,
                                Description = advert.Description,
                                Type = advert.Type,
                                Genre = advert.Genres.FirstOrDefault(),
                                AdvertPhoto = advert.Photo
                            };

            BandQuery = BandQuery.Take(4);
            UserQuery = UserQuery.Take(12);
            EventQuery = EventQuery.Take(3);
            AdvertQuery = AdvertQuery.Take(3);

            ViewData["NewsfeedBand"] = BandQuery.ToList();
            ViewData["NewsfeedUser"] = UserQuery.ToList();
            ViewData["NewsfeedEvent"] = events_;
            ViewData["NewsfeedAdvert"] = AdvertQuery.ToList();
            return View("index");
        }
    }
}


