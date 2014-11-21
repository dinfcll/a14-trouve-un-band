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
            var nestedList = new List<List<String>>();
            var genres = db.Genres.Where(x => x.Parent_ID == null);
            foreach (var genre in genres)
            {
                var children = db.Genres.Where(x => x.Parent_ID == genre.Genre_ID).Select(x => x.Name);
                var templist = new List<String>();
                templist.Add(genre.Name);
                templist.AddRange(children);
                nestedList.Add(templist);
            }

            return nestedList;
        }
    }
}