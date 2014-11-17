using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrouveUnBand.Models;
using TrouveUnBand.Classes;
using TrouveUnBand.Services;
using TrouveUnBand.ViewModels;

namespace TrouveUnBand.Controllers
{
    public class SearchController : Controller
    {
        private const int LATEST = 1;
        private const int MOST_POPULAR = 2;
        private const int HIGHEST_RATING = 3;

        private TrouveUnBandEntities db = new TrouveUnBandEntities();

        public ActionResult Index(string searchString)
        {
            var results = new List<ResultViewModels>();
            var genres = new SelectList(db.Genres.Where(x => x.Parent_ID == null), "Genre_ID", "Name");
            var categories = new SelectList(new List<Object>{
                new { value=LATEST, text="Les nouveautés" },
                new { value=MOST_POPULAR, text="Les plus populaires" },
                new { value=HIGHEST_RATING, text="Les mieux notés" }
            }, "value", "text");

            var bandsList = BandDao.GetBands(searchString);
            var musiciansList = UserDao.GetMusicians(searchString);

            foreach (Band band in bandsList)
            {
                results.Add(new ResultViewModels(band));
            }

            foreach (User musician in musiciansList)
            {
                results.Add(new ResultViewModels(musician));
            }

            ViewBag.Genres = genres;
            ViewBag.Categories = categories;
            ViewBag.SearchString = searchString;
            ViewBag.Results = results;
            ViewBag.ResultNumber = results.Count();

            return View();
        }

        [HttpGet]
        public ActionResult Filter(int selectedCategory, int? selectedGenre, string searchstring, string location, int radius,
                                   bool cbBandsChecked, bool cbMusiciansChecked, bool cbAdvertsChecked, bool cbEventsChecked)
        {
            var results = new List<ResultViewModels>();

            if (cbBandsChecked)
            {
                var bands = BandDao.GetBands(selectedGenre, searchstring, location, radius);
                foreach (var band in bands)
                {
                    results.Add(new ResultViewModels(band));
                }              
            }

            if (cbMusiciansChecked)
            {
                var musicians = UserDao.GetMusicians(selectedGenre, searchstring, location, radius);
                foreach (var musician in musicians)
                {
                    results.Add(new ResultViewModels(musician));
                }
            }

            if (cbAdvertsChecked)
            {
                var adverts = AdvertDao.GetAdverts(selectedGenre, searchstring, location, radius);
                foreach (var advert in adverts)
                {
                    results.Add(new ResultViewModels(advert));
                }
            }

            if (cbEventsChecked)
            {
                var events = EventDAO.GetAllEvents();
                foreach (var even in events)
                {
                    results.Add(new ResultViewModels(even));
                }
            }

            switch (selectedCategory)
            {
                case LATEST:
                    results.OrderBy(x => x.CreationDate);
                    break;

                case MOST_POPULAR:
                    // TODO: Add a function to filter search results by most popular artists.
                    break;
                    
                case HIGHEST_RATING:
                    // TODO: Add a function to filter search results by highest rated artists.
                    break;                 
            }

            ViewBag.Results = results;
            ViewBag.ResultNumber = results.Count();

            return PartialView("_SearchResults");
        }

        public ActionResult ViewProfile(string type, int Id)
        {
            switch (type.ToUpper())
            {
                case "MUSICIEN":
                    User musician = db.Users.FirstOrDefault(x => x.User_ID == Id);
                    MusicianProfileViewModel MusicianProfile = CreateProfile.CreateMusicianProfileView(musician);
                    return View("../Users/MusicianProfile", MusicianProfile);

                case "BAND":
                    Band band = db.Bands.FirstOrDefault(x => x.Band_ID == Id);
                    BandProfileViewModel BandProfile = CreateProfile.CreateBandProfileView(band);
                    return View("../Group/BandProfile", BandProfile);

                case "EVENT":
                    RedirectToAction("EventProfile", "Event");
                    break;

                case "PROMOTER":
                    break;

                default:
                    break;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
