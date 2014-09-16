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

        private DatabaseContext db = new DatabaseContext();

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
            var bandQuery = from band in db.Band where
                            band.Name.Contains(searchString)
                            select band;

            var userQuery = from user in db.User where
                            user.FirstName.Contains(searchString) ||
                            user.LastName.Contains(searchString)
                            select user;
                              
            List<Band> bandList = bandQuery.ToList();
            List<Users> userList = userQuery.ToList();

            ViewData["bandList"] = bandList;
            ViewData["userList"] = userList;
            
            return View();
        }
    }
}

