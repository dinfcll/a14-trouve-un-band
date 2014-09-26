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
            List<SearchResult> searchResults = new List<SearchResult>();

            SelectList genresDDL = new SelectList(db.Genres, "GenreId", "Name");
            SelectList categoriesDDL = new SelectList(new List<Object>{
                new { value=1, text="Groupes" },
                new { value=2, text="Musiciens" },
                new { value=3, text="Utilisateurs" }
            }, "value", "text");
       
            ViewBag.musicalGenres = genresDDL;
            ViewBag.searchCategories = categoriesDDL;
            ViewBag.searchString = SearchString;

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

            searchResults.AddRange(bandQuery);
            searchResults.AddRange(userQuery);
            ViewBag.searchResults = searchResults;

            return View();
        }

        [HttpGet]
        public ActionResult Filter(int musicalGenres, int searchCategories, string SearchString)
        {
            List<SearchResult> searchResults = new List<SearchResult>();
            
            switch (searchCategories)
            {
                case 0:
                    var bandQuery = from band in db.Bands
                                    where band.Name.Contains(SearchString)
                                    && band.Genres.FirstOrDefault().GenreId == musicalGenres
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
                                    && musician.Genres.FirstOrDefault().GenreId == musicalGenres
                                    select new SearchResult
                                    {
                                        Name = user.FirstName + " " + user.LastName,
                                        Location = user.Location,
                                        Description = musician.Description,
                                        Type = "Musicien"
                                    };

                    searchResults.AddRange(bandQuery);
                    searchResults.AddRange(userQuery);
                    ViewBag.searchResults = searchResults;
                    break;

                case 1:
                    var query = from band in db.Bands
                                where band.Name.Contains(SearchString) &&
                                band.Genres.FirstOrDefault().GenreId == musicalGenres
                                select new SearchResult
                                {
                                    Name = band.Name,
                                    Location = band.Location,
                                    Description = band.Description,
                                    Type = "Groupe"
                                };
                                
                    searchResults.AddRange(query);
                    break;

                case 2:
                        query = from user in db.Users
                                join musician in db.Musicians on user.UserId equals musician.UserId
                                where
                                    musician.Genres.FirstOrDefault().GenreId == musicalGenres &&
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
                        searchResults.AddRange(query);
                    break;

                case 3:
                    query = from user in db.Users
                            where
                                user.FirstName.Contains(SearchString) ||
                                user.LastName.Contains(SearchString) ||
                                user.Nickname.Contains(SearchString)
                            select new SearchResult
                            {
                                Name = user.FirstName + " " + user.LastName,
                                Location = user.Location,
                                Description = String.Empty,
                                Type = "Utilisateur"
                            };

                    searchResults.AddRange(query);
                    break;
            }

            ViewBag.searchResults = searchResults;
            return View();
        }

        [HttpPost]
        public ActionResult Filter()
        {
            return View();
        }

    }
}
