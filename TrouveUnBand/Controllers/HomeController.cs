using System.Collections.Generic;
using System.Web.Mvc;
using TrouveUnBand.Models;
using TrouveUnBand.Services;
using TrouveUnBand.ViewModels;

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
            var eventResults = new List<ResultViewModels>();
            var usersResults = new List<ResultViewModels>();
            var bandsResults = new List<ResultViewModels>();
            var advertsResults = new List<ResultViewModels>();
            
            var events = EventDAO.GetAllEvents();
            foreach (var evenement in events)
            {
                eventResults.Add(new ResultViewModels(evenement));
            }

            var users = UserDao.GetAllUsers();
            foreach (var user in users)
            {
                usersResults.Add(new ResultViewModels(user));
            }

            var bands = BandDao.GetAllBands();
            foreach (var band in bands)
            {
                bandsResults.Add(new ResultViewModels(band));
            }

            var adverts = AdvertDao.GetAllAdverts();
            foreach (var advert in adverts)
            {
                advertsResults.Add(new ResultViewModels(advert));
            }

            ViewData["NewsfeedBand"] = bandsResults;
            ViewData["NewsfeedUser"] = usersResults;
            ViewData["NewsfeedEvent"] = eventResults;
            ViewData["NewsfeedAdvert"] = advertsResults;
            return View("index");
        }
    }
}


