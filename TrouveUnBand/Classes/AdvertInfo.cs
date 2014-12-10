using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            this.Advert_ID = advert.Advert_ID;
            this.Photo = advert.Photo;
            this.Name = advert.Type; //Pourquoi le nom égale le type?
            this.Description = advert.Description;
            this.Location = advert.Location;
            this.Status = advert.Status;
            this.Genres = advert.Genres;
            this.ExpirationDate = DateHelper.GetDayAndMonth(advert.ExpirationDate);
        }
    }
}