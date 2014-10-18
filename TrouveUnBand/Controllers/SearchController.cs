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
        private TrouveUnBandEntities db = new TrouveUnBandEntities();

        private const string OPTION_ALL      = "option_all";
        private const string OPTION_BAND     = "option_band";
        private const string OPTION_MUSICIAN = "option_musician";
        private const string OPTION_USER     = "option_user";

        public ActionResult Index(string SearchString)
        {
            List<SearchResult> ResultsList = new List<SearchResult>();

            SelectList genresDDL = new SelectList(db.Genres, "GenreId", "Name");
            SelectList categoriesDDL = new SelectList(new List<Object>{
                new { value=OPTION_ALL, text="tout le monde" },
                new { value=OPTION_BAND, text="des groupes" },
                new { value=OPTION_MUSICIAN, text="des musiciens" },
                new { value=OPTION_USER, text="des utilisateurs" }
            }, "value", "text");

            List<Band> bandsList = GetBands(null, SearchString, "");
            List<Musician> musiciansList = GetMusicians(null, SearchString, "");

            foreach (Band band in bandsList)
            {
                ResultsList.Add(new SearchResult
                {
                    Name = band.Name,
                    Description = band.Description,
                    Location = band.Location,
                    Type = "Band", 
                    ID = band.BandId
                });
            }

            foreach (Musician musician in musiciansList)
            {
                User user = db.Users.Find(musician.UserId);

                ResultsList.Add(new SearchResult
                {
                    Name = user.FirstName + " " + user.LastName,
                    Description = musician.Description,
                    Location = user.Location,
                    Type = "Musicien",
                    ID = musician.MusicianId
                });
            }

            ViewBag.GenresList = genresDDL;
            ViewBag.CategoriesList = categoriesDDL;
            ViewBag.SearchString = SearchString;
            ViewBag.ResultsList = ResultsList;
            ViewBag.ResultNumber = ResultsList.Count();

            return View();
        }

        [HttpGet]
        public ActionResult Filter(string DDLCategories, int? DDLGenres, string SearchString, string Location)
        {
            List<SearchResult> ResultsList = new List<SearchResult>();
            
            switch (DDLCategories)
            {
                case OPTION_ALL:

                    List<Band> bandsList = GetBands(DDLGenres, SearchString, Location);
                    List<Musician> musiciansList = GetMusicians(DDLGenres, SearchString, Location);

                    foreach (Band band in bandsList)
                    {
                        ResultsList.Add(new SearchResult 
                        { 
                            Name = band.Name, 
                            Description = band.Description, 
                            Location = band.Location, 
                            Type = "Band" ,
                            ID = band.BandId
                        });
                    }

                    foreach (Musician musician in musiciansList)
                    {
                        User user = db.Users.Find(musician.UserId);

                        ResultsList.Add(new SearchResult
                        {
                            Name = user.FirstName + " " + user.LastName,
                            Description = musician.Description,
                            Location = user.Location,
                            Type = "Musicien",
                            ID = musician.MusicianId
                        });
                    }

                    break;

                case OPTION_BAND:

                    bandsList = GetBands(DDLGenres, SearchString, Location);

                    foreach (Band band in bandsList)
                    {
                        ResultsList.Add(new SearchResult
                        {
                            Name = band.Name,
                            Description = band.Description,
                            Location = band.Location,
                            Type = "Band",
                            ID = band.BandId
                        });
                    }

                    break;

                case OPTION_MUSICIAN:

                    musiciansList = GetMusicians(DDLGenres, SearchString, Location);

                    foreach (Musician musician in musiciansList)
                    {
                        User user = db.Users.Find(musician.UserId);

                        ResultsList.Add(new SearchResult
                        {
                            Name = user.FirstName + " " + user.LastName,
                            Description = musician.Description,
                            Location = user.Location,
                            Type = "Musicien" ,
                            ID = musician.MusicianId
                        });
                    }

                    break;

                case OPTION_USER:

                    List<User> usersList = GetUsers(SearchString, Location);

                    foreach (User user in usersList)
                    {
                        ResultsList.Add(new SearchResult
                        {
                            Name = user.FirstName + " " + user.LastName,
                            Description = "",
                            Location = user.Location,
                            Type = "Utilisateur",
                            ID = user.UserId
                        });
                    }

                    break;
            }

            ViewBag.ResultsList = ResultsList;
            ViewBag.ResultNumber = ResultsList.Count();

            return PartialView("_SearchResults");
        }

        public List<Band> GetBands(int? GenreID, string BandName, string Location)
        {
            List<Band> lstResults = new List<Band>();

            var bands = from band in db.Bands
                        select band;

            if (GenreID != null)
            {
                bands = bands.Where(band => band.Genres.Any(genre => genre.GenreId == GenreID));
            }
            if (!String.IsNullOrEmpty(BandName))
            {
                bands = bands.Where(band => band.Name.Contains(BandName));
            }
            if (!String.IsNullOrEmpty(Location))
            {
                bands = bands.Where(band => band.Location.Contains(Location));
            }

            lstResults.AddRange(bands);

            return lstResults;
        }

        public List<Musician> GetMusicians(int? GenreID, string UserName, string Location)
        {
            List<Musician> lstResults = new List<Musician>();

            var musicians = from musician in db.Musicians
                            select musician;

            if (GenreID != null)
            {
                musicians = musicians.Where(musician => musician.Genres.Any(genre => genre.GenreId == GenreID));
            }
            if (!String.IsNullOrEmpty(UserName))
            {
                musicians = musicians.Where(musician => musician.User.FirstName.Contains(UserName) ||
                                            musician.User.LastName.Contains(UserName) ||
                                            musician.User.Nickname.Contains(UserName));
            }
            if (!String.IsNullOrEmpty(Location))
            {
                musicians = musicians.Where(musician => musician.User.Location.Contains(Location));
            }

            lstResults.AddRange(musicians);

            return lstResults;
        }

        public List<User> GetUsers(string UserName, string Location)
        {
            List<User> lstResults = new List<User>();

            var users = from user in db.Users
                        select user;

            if (!String.IsNullOrEmpty(UserName))
            {
                users = users.Where(user => user.FirstName.Contains(UserName) ||
                                            user.LastName.Contains(UserName) ||
                                            user.Nickname.Contains(UserName));
            }
            if (!String.IsNullOrEmpty(Location))
            {
                users.Where(user => user.Location.Contains(Location));
            }


            lstResults.AddRange(users);

            return lstResults;
        }
    }
}
