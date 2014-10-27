using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;

namespace TrouveUnBand.Models
{
    //Class used to structure information returned by the search function.
    public class SearchResult
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public List<string> Subgenres { get; set; }

        private readonly TrouveUnBandEntities db = new TrouveUnBandEntities();

        public SearchResult(Band band)
        {
            Name = band.Name;
            Description = band.Description;
            Location = band.Location;
            Type = "Band";
            Subgenres = band.Sub_Genres.Select(x => x.Name).ToList();
        }

        public SearchResult(Musician musician)
        {
            User user = db.Users.Find(musician.UserId);
            Name = user.FirstName + " " + user.LastName;
            Description = musician.Description;
            Location = user.Location;
            Type = "Musician";
            Subgenres = musician.Sub_Genres.Select(x => x.Name).ToList();
        }

        public SearchResult(User user)
        {
            Name = user.FirstName + " " + user.LastName;
            Description = "";
            Location = user.Location;
            Type = "Utilisateur";
            Subgenres = null;
        }
    }
}
