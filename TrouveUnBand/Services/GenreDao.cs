using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrouveUnBand.Models;

namespace TrouveUnBand.Services
{
    public class GenreDao
    {
        public static List<List<String>> GetAllSubgenresByGenres()
        {
            var db = new TrouveUnBandEntities();
            var subgenresByGenres = new List<List<String>>();
            var genres = db.Genres.Where(x => x.Parent_ID == null);
            foreach (var genre in genres)
            {
                var children = db.Genres.Where(x => x.Parent_ID == genre.Genre_ID).Select(x => x.Name);
                var templist = new List<String>();
                templist.Add(genre.Name);
                templist.AddRange(children);
                subgenresByGenres.Add(templist);
            }

            return subgenresByGenres;
        }

        public static List<Genre> GetGenresByNames(string[] namesArray)
        {
            var db = new TrouveUnBandEntities();

            return namesArray.Select(t => db.Genres.FirstOrDefault(x => x.Name == t)).ToList();
        }
    }
}
