using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrouveUnBand.Models;

namespace TrouveUnBand.Controllers
{
    public class HomeController : Controller
    {

        private TrouveUnBandEntities1 db = new TrouveUnBandEntities1();

        public ActionResult Index()
        {
            ViewBag.Message = "Modifiez ce modèle pour dynamiser votre application ASP.NET MVC.";

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
		
		
        public ActionResult CreateGroup()
        {
            ViewBag.Message = "Votre page de contact.";
			
            return View();
        }
		
    }
}


