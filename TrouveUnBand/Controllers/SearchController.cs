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

            searchResults.AddRange(bandQuery);
            searchResults.AddRange(userQuery);

            ViewBag.musicalGenres = genresDDL;
            ViewBag.searchCategories = categoriesDDL;
            ViewBag.searchString = SearchString;
            ViewBag.searchResults = searchResults;

            return View();
        }

        [HttpGet]
        public ActionResult Filter(int? musicalGenres, int searchCategories, string SearchString)
        {
            try
            {
                List<SearchResult> searchResults = new List<SearchResult>();

                switch (searchCategories)
                {
                    case 1: //All categories
                        if (musicalGenres != null)
                        {
                            var bandQuery = from band in db.Bands
                                            where
                                            band.Name.Contains(SearchString)
                                            && band.Genres.Any(x => x.GenreId == musicalGenres)
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
                                            && musician.Genres.Any(x => x.GenreId == musicalGenres)
                                            select new SearchResult
                                            {
                                                Name = user.FirstName + " " + user.LastName,
                                                Location = user.Location,
                                                Description = musician.Description,
                                                Type = "Musicien"
                                            };
                        }
                        else
                        {
                            var bandQuery = from band in db.Bands
                                            where
                                            band.Name.Contains(SearchString)
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
                        }

                        searchResults.AddRange(bandQuery);
                        searchResults.AddRange(userQuery);
                        ViewBag.searchResults = searchResults;
                        break;

                    case 2: //Bands
                        var searchedGenre = db.Genres.Find(musicalGenres);

                        var query = from band in db.Bands
                                    where
                                    band.Name.Contains(SearchString) &&
                                    band.Genres.Any(x => x.GenreId == musicalGenres) ||
                                    band.Name.Contains(SearchString)
                                    select new SearchResult
                                    {
                                        Name = band.Name,
                                        Location = band.Location,
                                        Description = band.Description,
                                        Type = "Groupe"
                                    };

                        searchResults.AddRange(query);
                        break;

                    case 3: //Musicians
                        query = from user in db.Users
                                join musician in db.Musicians on user.UserId equals musician.UserId
                                where
                                    musician.Genres.Any(x => x.GenreId == musicalGenres) ||
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

                    case 4: //Users
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
                return PartialView("_SearchResults");
            }
            catch (ArgumentException)
            {
            }
            return View("Index");
        }
    }
}
