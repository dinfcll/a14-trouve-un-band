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

        private TUBDBContext db = new TUBDBContext();

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
            var queryResults = from band in db.Band
                        where band.Name.Contains(searchString)
                        select band;

            List<BandModels> bandList = queryResults.ToList();

            ViewData["bandList"] = bandList;

            return View();
        }
    }
}
