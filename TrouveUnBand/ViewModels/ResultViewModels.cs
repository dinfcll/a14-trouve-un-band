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
        public string Address { get; set; }
        public string City { get; set; }
        public string Type { get; set; }
        public List<string> Genres { get; set; }
        public string Photo { get; set; }
        public DateTime CreationDate { get; set; }
        public int ID { get; set; }


        private readonly TrouveUnBandEntities db = new TrouveUnBandEntities();

        public ResultViewModels(Band band)
        {
            Name = band.Name;
            Description = band.Description;
            Location = band.Location;
            Type = "Band";
            Genres = band.Genres.Select(x => x.Name).ToList();
            Photo = band.Photo;
            CreationDate = band.CreationDate;
            ID = band.Band_ID;
        }

        public ResultViewModels(User user)
        {
            Name = user.FirstName + " " + user.LastName;
            Description = user.Description;
            Genres = user.Genres.Select(x => x.Name).ToList();
            Location = user.Location;
            Type = "Musicien";
            if (!user.isMusician())
            {
                Type = "Utilisateur";
                Description = "";
            }
            Photo = user.Photo;
            CreationDate = user.CreationDate;
            ID = user.User_ID;
        }

        public ResultViewModels(Event evenement)
        {
            Name = evenement.Name;
            Description = evenement.Description;
            Genres = evenement.Genres.Select(x => x.Name).ToList();
            Location = evenement.Location;
            Address = evenement.Address;
            City = evenement.City;
            Type = "Événement";
            Photo = evenement.Photo;
            CreationDate = evenement.CreationDate;
            ID = evenement.Event_ID;
        }

        public ResultViewModels(Advert advert)
        {
            Name = advert.Type;
            Description = advert.Description;
            Genres = advert.Genres.Select(x => x.Name).ToList();
            Location = advert.Location;
            Type = "Annonce";
            Photo = advert.Photo;
            CreationDate = advert.CreationDate;
            ID = advert.Advert_ID;
        }
    }
}
