using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrouveUnBand.Models
{
    public class MusicianDao
    {
        public static List<Musician> GetMusicians(int? genreId, string userName, string location)
        {
            TrouveUnBandEntities db = new TrouveUnBandEntities();
            List<Musician> results = new List<Musician>();

            var musicians = from musician in db.Musicians
                            select musician;

            if (genreId != null && genreId > 0)
            {
                musicians = musicians.Where(musician => musician.Sub_Genres.Any(genre => genre.GenreId == genreId));
            }
            if (!String.IsNullOrEmpty(userName))
            {
                musicians = musicians.Where(musician => musician.User.FirstName.Contains(userName) ||
                                            musician.User.LastName.Contains(userName) ||
                                            musician.User.Nickname.Contains(userName));
            }
            if (!String.IsNullOrEmpty(location))
            {
                musicians = musicians.Where(musician => musician.User.Location.Contains(location));
            }

            results.AddRange(musicians);

            return results;
        }

        public static List<Musician> GetMusicians(List<String> subgenres, string userName, string location)
        {
            TrouveUnBandEntities db = new TrouveUnBandEntities();
            List<Musician> results = new List<Musician>();

            var musicians = from musician in db.Musicians
                            select musician;

            if (subgenres.Count > 0)
            {
                foreach (String genreName in subgenres)
                {
                    musicians = musicians.Where(musician => musician.Sub_Genres.Any(genre => genre.Name == genreName));
                }
            }

            if (!String.IsNullOrEmpty(userName))
            {
                musicians = musicians.Where(musician => musician.User.FirstName.Contains(userName) ||
                                            musician.User.LastName.Contains(userName) ||
                                            musician.User.Nickname.Contains(userName));
            }
            if (!String.IsNullOrEmpty(location))
            {
                musicians = musicians.Where(musician => musician.User.Location.Contains(location));
            }

            results.AddRange(musicians);

            return results;
        }
    }
}