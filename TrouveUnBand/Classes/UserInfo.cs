using System.Collections.Generic;
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
            User_ID = user.User_ID;
            Photo = user.Photo;
            Name = user.FirstName + " " + user.LastName;
            Description = user.Description;
            Location = user.Location;
            Type = "Musicien";
            Genres = user.Genres;
        }
    }
}
