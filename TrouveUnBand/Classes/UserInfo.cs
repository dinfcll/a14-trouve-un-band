using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrouveUnBand.Models;

namespace TrouveUnBand.Classes
{
    public class UserInfo
    {
        public int User_ID { get; set; }
        public string Photo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

        public UserInfo(User user)
        {
            this.User_ID = user.User_ID;
            this.Photo = user.Photo;
            this.Name = user.FirstName + " " + user.LastName;
            this.Description = user.Description;
            this.Location = user.Location;
            this.Type = "Musicien";
            this.Genres = user.Genres;
        }
    }
}
