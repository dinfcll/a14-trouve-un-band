using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TrouveUnBand.Models;

namespace TrouveUnBand.Controllers
{
    public class SearchController : Controller
    {      
        private const string OPTION_ALL      = "option_all";
        private const string OPTION_BAND     = "option_band";
        private const string OPTION_MUSICIAN = "option_musician";
        private const string OPTION_USER     = "option_user";

        private readonly TrouveUnBandEntities db = new TrouveUnBandEntities();

        public ActionResult Index(string searchString)
        {
            List<SearchResult> resultsList = new List<SearchResult>();

            SelectList genresDDL = new SelectList(db.Genres, "GenreId", "Name");

            List<String> genresList = db.Genres.Select(x => x.Name).ToList();
            List<List<String>> subgenresList = new List<List<String>>();

            foreach (var genre in db.Genres)
            {
                var subgenres = db.Sub_Genres.Where(x => x.GenreId == genre.GenreId).Select(x => x.Name).ToList();
                List<String> tempList = new List<String>();
                tempList.AddRange(subgenres);
                subgenresList.Add(tempList);
            }

            SelectList categoriesDDL = new SelectList(new List<Object>{
                new { value=OPTION_ALL, text="tout le monde" },
                new { value=OPTION_BAND, text="des groupes" },
                new { value=OPTION_MUSICIAN, text="des musiciens" },
                new { value=OPTION_USER, text="des utilisateurs" }
            }, "value", "text");

            List<Band> bandsList = BandDao.GetBands(0, searchString, "");
            List<Musician> musiciansList = MusicianDao.GetMusicians(0, searchString, "");

            foreach (var band in bandsList) 
            {
                resultsList.Add(new SearchResult(band)); 
            }

            foreach (var musician in musiciansList)
            {
                resultsList.Add(new SearchResult(musician));
            }

            ViewBag.GenresList = genresDDL;
            ViewBag.Genres = genresList;
            ViewBag.Subgenres = subgenresList;
            ViewBag.CategoriesList = categoriesDDL;
            ViewBag.SearchString = searchString;
            ViewBag.ResultsList = resultsList;
            ViewBag.ResultNumber = resultsList.Count();

            return View();
        }

        [HttpGet]
        public ActionResult Filter(string ddlCategories, int? ddlGenres, string searchString, string location)
        {
            List<SearchResult> resultsList = new List<SearchResult>();
            List<Band> bandsList = new List<Band>();
            List<Musician> musiciansList = new List<Musician>();
            List<User> usersList = new List<User>();

            switch (ddlCategories)
            {
                case OPTION_ALL:

                    bandsList = BandDao.GetBands(ddlGenres, searchString, location);
                    musiciansList = MusicianDao.GetMusicians(ddlGenres, searchString, location);

                    foreach (Band band in bandsList)
                    {
                        resultsList.Add(new SearchResult(band));
                    }

                    foreach (Musician musician in musiciansList)
                    {
                        resultsList.Add(new SearchResult(musician));
                    }

                    break;

                case OPTION_BAND:

                    bandsList = BandDao.GetBands(ddlGenres, searchString, location);

                    foreach (Band band in bandsList)
                    {
                        resultsList.Add(new SearchResult(band));
                    }

                    break;

                case OPTION_MUSICIAN:

                    musiciansList = MusicianDao.GetMusicians(ddlGenres, searchString, location);

                    foreach (Musician musician in musiciansList)
                    {
                        resultsList.Add(new SearchResult(musician));
                    }

                    break;

                case OPTION_USER:

                    usersList = UserDao.GetUsers(searchString, location);

                    foreach (User user in usersList)
                    {
                        resultsList.Add(new SearchResult(user));
                    }

                    break;
            }

            ViewBag.ResultsList = resultsList;
            ViewBag.ResultNumber = resultsList.Count();

            return PartialView("_SearchResults");
        }

        [HttpGet]
        public ActionResult AdvancedFilter(string searchString, string location, string radius, string rbCategories, params String[] cbSubgenres)
        {
            List<SearchResult> resultsList = new List<SearchResult>();
            List<String> SelectedGenres = new List<String>();

            foreach (string genre in cbSubgenres)
            {
                if (genre != "false")
                {
                    SelectedGenres.Add(genre);
                }
            }

            switch (rbCategories)
            {
                case OPTION_ALL:

                    List<Band> Bands = BandDao.GetBands(SelectedGenres, searchString, location);
                    List<Musician> Musicians = MusicianDao.GetMusicians(SelectedGenres, searchString, location);

                     foreach (Band band in Bands)
                     {
                         resultsList.Add(new SearchResult(band));
                     }

                    foreach (Musician musician in Musicians)
                    {
                        resultsList.Add(new SearchResult(musician));
                    }

                    break;

                case OPTION_BAND:

                    Bands = BandDao.GetBands(SelectedGenres, searchString, location);
                    
                    foreach (Band band in Bands)
                    {
                        resultsList.Add(new SearchResult(band));
                    }

                    break;

                case OPTION_MUSICIAN:

                    Musicians = MusicianDao.GetMusicians(SelectedGenres, searchString, location);

                    foreach (Musician musician in Musicians)
                    {
                        resultsList.Add(new SearchResult(musician));
                    }

                    break;

                case OPTION_USER:

                    List<User> usersList = UserDao.GetUsers(searchString, location);

                    foreach (User user in usersList)
                    {
                        resultsList.Add(new SearchResult(user));
                    }

                    break;
            }

            ViewBag.ResultsList = resultsList;
            ViewBag.ResultNumber = resultsList.Count();

            return PartialView("_SearchResults"); 
        }
    }
}
