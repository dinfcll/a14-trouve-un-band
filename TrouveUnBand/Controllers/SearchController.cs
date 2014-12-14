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
    public class SearchController : BaseController
    {
        private const int LATEST = 1;
        private const int MOST_POPULAR = 2;
        private const int HIGHEST_RATING = 3;

        public ActionResult Index(string searchString)
        {
            var results = new List<ResultViewModels>();
            var subgenres = GenreDao.GetAllSubgenresByGenres();
            var genres = new SelectList(db.Genres.Where(x => x.Parent_ID == null), "Genre_ID", "Name");
            var categories = new SelectList(new List<Object>{
                new { value=LATEST, text="Les nouveautés" },
                new { value=MOST_POPULAR, text="Les plus populaires" },
                new { value=HIGHEST_RATING, text="Les mieux notés" }
            }, "value", "text");


            
            var bandsList = BandDao.GetBands(searchString);
            var musiciansList = UserDao.GetMusicians(searchString);
            var eventsList = EventDao.GetEvents(searchString);
            var advertsList = AdvertDao.GetAdverts(searchString);

            foreach (Band band in bandsList)
            {
                results.Add(new ResultViewModels(band));
            }

            foreach (User musician in musiciansList)
            {
                results.Add(new ResultViewModels(musician));
            }

            foreach (Event events in eventsList)
            {
                results.Add(new ResultViewModels(events));
            }

            foreach (Advert adverts in advertsList)
            {
                results.Add(new ResultViewModels(adverts));
            }

            ViewBag.Genres = genres;
            ViewBag.Subgenres = subgenres;
            ViewBag.Categories = categories;
            ViewBag.SearchString = searchString;
            ViewBag.Results = results;
            ViewBag.ResultNumber = results.Count();

            return View();
        }

        [HttpGet]
        public ActionResult Filter(int selectedCategory, string[] cbSelectedGenres, string searchstring, string location,
                                   bool cbBandsChecked, bool cbMusiciansChecked, bool cbAdvertsChecked, bool cbEventsChecked, int radius = 20)
        {
            var results = new List<ResultViewModels>();

            if (cbBandsChecked)
            {
                var bands = BandDao.GetBands(cbSelectedGenres, searchstring, location, radius);
                foreach (var band in bands)
                {
                    results.Add(new ResultViewModels(band));
                }              
            }

            if (cbMusiciansChecked)
            {
                var musicians = UserDao.GetMusicians(cbSelectedGenres, searchstring, location, radius);
                foreach (var musician in musicians)
                {
                    results.Add(new ResultViewModels(musician));
                }
            }

            if (cbAdvertsChecked)
            {
                var adverts = AdvertDao.GetAdverts(cbSelectedGenres, searchstring, location, radius);
                foreach (var advert in adverts)
                {
                    results.Add(new ResultViewModels(advert));
                }
            }

            if (cbEventsChecked)
            {
                var events = EventDao.GetEvents(cbSelectedGenres, searchstring, location, radius);
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
    }
}
