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

		[HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchString)
        {
            var bandQuery = from band in db.Bands
                            where band.Name.Contains(searchString)
                            select new SearchResultModel
                            {
                                Name = band.Name,
                                Description = band.Description,
                                Location = band.Location
                            };

            var userQuery = from user in db.Users
                            join musician in db.Musicians on user.UserId equals musician.MusicianId
                            where
                            user.FirstName.Contains(searchString) ||
                            user.LastName.Contains(searchString)
                            select new SearchResultModel
                            {
                                Name = user.FirstName + " " + user.LastName,
                                Description = musician.Description,
                                Location = user.Location
                            };


            List<SearchResultModel> searchResults = new List<SearchResultModel>();
            searchResults.AddRange(userQuery);
            searchResults.AddRange(bandQuery);

            ViewData["searchResults"] = searchResults;
            ViewData["searchString"] = searchString;

            return View();
		}

        [HttpPost]
        public ActionResult Newsfeed()
        {
            var NewsQuery = from band in db.Bands
                            orderby band.BandId descending
                            select new NewsfeedModel
                            {
                                BandId = band.BandId,
                                Name = band.Name,
                                Description = band.Description,
                                Location = band.Location,
                            };

            NewsQuery = NewsQuery.Take(8);

            ViewData["Newsfeed"] = NewsQuery.ToList();

            return View("index");
        }
    }
}


