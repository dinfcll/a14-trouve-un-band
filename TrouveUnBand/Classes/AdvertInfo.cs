using System.Collections.Generic;
using TrouveUnBand.Models;

namespace TrouveUnBand.Classes
{
    public class AdvertInfo
    {
        public int Advert_ID { get; set; }
        public string Photo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string ExpirationDate { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

        public AdvertInfo(Advert advert)
        {
            Advert_ID = advert.Advert_ID;
            Photo = advert.Photo;
            Name = advert.Type; //Pourquoi le nom égale le type?
            Description = advert.Description;
            Location = advert.Location;
            Status = advert.Status;
            Genres = advert.Genres;
            ExpirationDate = DateHelper.GetDayAndMonth(advert.ExpirationDate);
        }
    }
}
