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

        public ActionResult Index(string SearchString)
        {
            List<SearchResult> ResultsList = new List<SearchResult>();

            SelectList genresDDL = new SelectList(db.Genres, "GenreId", "Name");
            SelectList categoriesDDL = new SelectList(new List<Object>{
                new { value=1, text="Toutes les catégories" },
                new { value=2, text="Groupe" },
                new { value=3, text="Musicien" },
                new { value=4, text="Utilisateur" }
            }, "value", "text");
       
            var bandQuery = from band in db.Bands
                            where band.Name.Contains(SearchString)
                            select new SearchResult
                            {
                                Name = band.Name,
                                Location = band.Location,
                                Description = band.Description,
                                Type = "Groupe"
                            };

            var userQuery = from user in db.Users
                            join musician in db.Musicians on user.UserId equals musician.UserId
                            where
                            user.FirstName.Contains(SearchString) ||
                            user.LastName.Contains(SearchString) ||
                            user.Nickname.Contains(SearchString)
                            select new SearchResult
                            {
                                Name = user.FirstName + " " + user.LastName,
                                Location = user.Location,
                                Description = musician.Description,
                                Type = "Musicien"
                            };

            ResultsList.AddRange(bandQuery);
            ResultsList.AddRange(userQuery);

            ViewBag.GenresList = genresDDL;
            ViewBag.CategoriesList = categoriesDDL;
            ViewBag.SearchString = SearchString;
            ViewBag.ResultsList = ResultsList;

            return View();
        }

        [HttpGet]
        public ActionResult Filter(int DDLCategories, int? DDLGenres, string SearchString, string Location)
        {
            const int RESULTS_PER_PAGES = 25;
            List<SearchResult> ResultsList = new List<SearchResult>();

            switch (DDLCategories)
            {
                case 1: //All categories dropdown list option

                    List<Band> bandsList = GetBands(DDLGenres, SearchString, Location, RESULTS_PER_PAGES);
                    List<Musician> musiciansList = GetMusicians(DDLGenres, SearchString, RESULTS_PER_PAGES);

                    foreach (Band band in bandsList)
                    {
                        ResultsList.Add(new SearchResult 
                        { 
                            Name = band.Name, 
                            Description = band.Description, 
                            Location = band.Location, 
                            Type = "Band" 
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
                            Type = "Musicien"
                        });
                    }

                    break;

                case 2: //Band dropdown list option

                    bandsList = GetBands(DDLGenres, SearchString, Location, RESULTS_PER_PAGES);

                    foreach (Band band in bandsList)
                    {
                        ResultsList.Add(new SearchResult
                        {
                            Name = band.Name,
                            Description = band.Description,
                            Location = band.Location,
                            Type = "Band"
                        });
                    }

                    break;

                case 3: //Musician dropdown list option

                    musiciansList = GetMusicians(DDLGenres, SearchString, RESULTS_PER_PAGES);

                    foreach (Musician musician in musiciansList)
                    {
                        User user = db.Users.Find(musician.UserId);

                        ResultsList.Add(new SearchResult
                        {
                            Name = user.FirstName + " " + user.LastName,
                            Description = musician.Description,
                            Location = user.Location,
                            Type = "Musicien"
                        });
                    }

                    break;

                case 4: //User dropdown list option

                    List<User> usersList = GetUsers(SearchString, RESULTS_PER_PAGES);

                    foreach (User user in usersList)
                    {
                        ResultsList.Add(new SearchResult
                        {
                            Name = user.FirstName + " " + user.LastName,
                            Description = "",
                            Location = user.Location,
                            Type = "Utilisateur"
                        });
                    }

                    break;
            }

            ViewBag.ResultsList = ResultsList;

            return PartialView("_SearchResults");
        }

        public List<Band> GetBands(int? GenreID, string BandName, string Location, int Number)
        {
            List<Band> lstResults = new List<Band>();

            if (GenreID != null)
            {
                var bands = from band in db.Bands
                            where
                                band.Name.Contains(BandName) &&
                                band.Genres.Any(genre => genre.GenreId == GenreID)
                            select band;

                bands.Take(Number);
                lstResults.AddRange(bands);
            }
            else
            {
                var bands = from band in db.Bands
                            where
                                band.Name.Contains(BandName)
                            select band;

                bands.Take(Number);
                lstResults.AddRange(bands);
            }
            return lstResults;
        }

        public List<Musician> GetMusicians(int? GenreID, string UserName, int Number)
        {
            List<Musician> lstResults = new List<Musician>();

            if (GenreID != null)
            {
                var musicians = from user in db.Users
                                join musician in db.Musicians
                                on user.UserId equals musician.MusicianId
                                where(
                                    user.FirstName.Contains(UserName)  ||
                                    user.LastName.Contains(UserName)   ||
                                    user.Nickname.Contains(UserName))  &&
                                    musician.Genres.Any(genre => genre.GenreId == GenreID)
                                select musician;

                musicians.Take(Number);
                lstResults.AddRange(musicians);
            }
            else
            {
                var musicians = from user in db.Users
                                join musician in db.Musicians
                                on user.UserId equals musician.MusicianId
                                where(
                                    user.FirstName.Contains(UserName)  ||
                                    user.LastName.Contains(UserName)   ||
                                    user.Nickname.Contains(UserName)
                                    )
                                select musician;

                musicians.Take(Number);
                lstResults.AddRange(musicians);
            }
            return lstResults;
        }

        public List<User> GetUsers(string UserName, int Number)
        {
            List<User> lstResults = new List<User>();

            var users = from user in db.Users
                        where(
                            user.FirstName.Contains(UserName)  ||
                            user.LastName.Contains(UserName)   ||
                            user.Nickname.Contains(UserName)
                            )
                        select user;

            users.Take(Number);
            lstResults.AddRange(users);
            return lstResults;
        }
    }
}
