using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrouveUnBand.Models;

namespace TrouveUnBand.Controllers
{
    
    public class SearchController : Controller
    {
        private TUBDBContext db = new TUBDBContext();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string searchString)
        {
            var query = from band in db.Band
                        where band.Name.Contains(searchString)
                        select band;
            List<BandModels> bandList = query.ToList();
            return View();
        }

    }


}
