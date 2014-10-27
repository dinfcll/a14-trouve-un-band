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

        public ActionResult Index(string SearchString)
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

            List<Band> bandsList = BandDao.GetBands(0, SearchString, "");
            List<Musician> musiciansList = MusicianDao.GetMusicians(0, SearchString, "");

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
            ViewBag.SearchString = SearchString;
            ViewBag.ResultsList = resultsList;
            ViewBag.ResultNumber = resultsList.Count();

            return View();
        }

        [HttpGet]
        public ActionResult Filter(string DDLCategories, int? DDLGenres, string SearchString, string Location)
        {
            List<SearchResult> resultsList = new List<SearchResult>();
            List<Band> bandsList = new List<Band>();
            List<Musician> musiciansList = new List<Musician>();
            List<User> usersList = new List<User>();

            switch (DDLCategories)
            {
                case OPTION_ALL:

                    bandsList = BandDao.GetBands(DDLGenres, SearchString, Location);
                    musiciansList = MusicianDao.GetMusicians(DDLGenres, SearchString, Location);

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

                    bandsList = BandDao.GetBands(DDLGenres, SearchString, Location);

                    foreach (Band band in bandsList)
                    {
                        resultsList.Add(new SearchResult(band));
                    }

                    break;

                case OPTION_MUSICIAN:

                    musiciansList = MusicianDao.GetMusicians(DDLGenres, SearchString, Location);

                    foreach (Musician musician in musiciansList)
                    {
                        resultsList.Add(new SearchResult(musician));
                    }

                    break;

                case OPTION_USER:

                    usersList = UserDao.GetUsers(SearchString, Location);

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
        public ActionResult AdvancedFilter(string SearchString, string Location, string Radius, string rbCategories, params String[] CbSubgenres)
        {
            List<SearchResult> resultsList = new List<SearchResult>();
            List<String> SelectedGenres = new List<String>();

            foreach (string genre in CbSubgenres)
            {
                if (genre != "false")
                {
                    SelectedGenres.Add(genre);
                }
            }

            switch (rbCategories)
            {
                case OPTION_ALL:

                    List<Band> Bands = BandDao.GetBands(SelectedGenres, SearchString, Location);
                    List<Musician> Musicians = MusicianDao.GetMusicians(SelectedGenres, SearchString, Location);

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

                    Bands = BandDao.GetBands(SelectedGenres, SearchString, Location);
                    
                    foreach (Band band in Bands)
                    {
                        resultsList.Add(new SearchResult(band));
                    }

                    break;

                case OPTION_MUSICIAN:

                    Musicians = MusicianDao.GetMusicians(SelectedGenres, SearchString, Location);

                    foreach (Musician musician in Musicians)
                    {
                        resultsList.Add(new SearchResult(musician));
                    }

                    break;

                case OPTION_USER:

                    List<User> usersList = UserDao.GetUsers(SearchString, Location);

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
