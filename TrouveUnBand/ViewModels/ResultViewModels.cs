using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrouveUnBand.Models;

namespace TrouveUnBand.ViewModels
{
    public class ResultViewModels
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public List<string> Genres { get; set; }

        private readonly TrouveUnBandEntities db = new TrouveUnBandEntities();

        public ResultViewModels(Band band)
        {
            Name = band.Name;
            Description = band.Description;
            Location = band.Location;
            Type = "Band";
            Genres = band.Genres.Select(x => x.Name).ToList();
        }

        public ResultViewModels(User user)
        {
            Name = user.FirstName + " " + user.LastName;
            Description = user.Description;
            Genres = user.Genres.Select(x => x.Name).ToList();
            Location = user.Location;
            Type = "Utilisateur";
            if (user.isMusician())
            {
                Type = "Musician";
            }                    
        }

        public ResultViewModels(Event evenement)
        {
            
        }
    }
}