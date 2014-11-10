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
        private TrouveUnBandEntities db = new TrouveUnBandEntities();

        private const string OPTION_ALL      = "option_all";
        private const string OPTION_BAND     = "option_band";
        private const string OPTION_MUSICIAN = "option_musician";
        private const string OPTION_USER     = "option_user";
        private const string OPTION_EVENT    = "option_event";

        public ActionResult Index(string searchString)
        {
            List<ResultViewModels> results = new List<ResultViewModels>();

            SelectList genresDDL = new SelectList(db.Genres, "GenreId", "Name");
            SelectList categoriesDDL = new SelectList(new List<Object>{
                new { value=OPTION_ALL, text="tout le monde" },
                new { value=OPTION_BAND, text="des groupes" },
                new { value=OPTION_MUSICIAN, text="des musiciens" },
                new { value=OPTION_USER, text="des utilisateurs" },
                new { value=OPTION_EVENT, text="des événements" }
            }, "value", "text");

            List<Band> bandsList = BandDao.GetBands(searchString);
            List<User> musiciansList = UserDao.GetMusicians(searchString);

            foreach (Band band in bandsList)
            {
                results.Add(new ResultViewModels(band));
            }

            foreach (User musician in musiciansList)
            {
                results.Add(new ResultViewModels(musician));
            }

            ViewBag.GenresList = genresDDL;
            ViewBag.CategoriesList = categoriesDDL;
            ViewBag.SearchString = searchString;
            ViewBag.ResultsList = results;
            ViewBag.ResultNumber = results.Count();

            return View();
        }

        [HttpGet]
        public ActionResult Filter(string DDLCategories, int? DDLGenres, string SearchString, string Location)
        {
            List<ResultViewModels> ResultsList = new List<ResultViewModels>();
            
            switch (DDLCategories)
            {
                case OPTION_ALL:

                    List<Band> bandsList = BandDao.GetBands(DDLGenres, SearchString, Location);
                    List<User> musiciansList = UserDao.GetMusicians(DDLGenres, SearchString, Location);

                    foreach (Band band in bandsList)
                    {
                        ResultsList.Add(new ResultViewModels(band));
                    }

                    foreach (User musician in musiciansList)
                    {
                        ResultsList.Add(new ResultViewModels(musician));
                    }

                    break;

                case OPTION_BAND:

                    bandsList = BandDao.GetBands(DDLGenres, SearchString, Location);

                    foreach (Band band in bandsList)
                    {
                        ResultsList.Add(new ResultViewModels(band));
                    }

                    break;

                case OPTION_MUSICIAN:

                    musiciansList = UserDao.GetUsers(DDLGenres, SearchString, Location);

                    foreach (User musician in musiciansList)
                    {
                        ResultsList.Add(new ResultViewModels(musician));
                    }

                    break;

                case OPTION_USER:

                    List<User> usersList = UserDao.GetUsers(SearchString, Location);

                    foreach (User user in usersList)
                    {
                        ResultsList.Add(new ResultViewModels(user));
                    }

                    break;

                case OPTION_EVENT:

                    List<Event> EventList = EventDAO.GetEvents(SearchString, Location);

                    foreach (Event evenement in EventList)
                    {
                        ResultsList.Add(new ResultViewModels(evenement));
                    }

                    break;
            }

            ViewBag.ResultsList = ResultsList;
            ViewBag.ResultNumber = ResultsList.Count();

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
