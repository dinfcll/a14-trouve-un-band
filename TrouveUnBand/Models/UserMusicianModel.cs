using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrouveUnBand.Models
{
    public class UserMusicianModel
    {
        public UserMusicianModel()
        {
            this.Bands = new HashSet<Band>();
            this.Genres = new HashSet<Genre>();
            this.Users = new HashSet<User>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public int MusicianId { get; set; }
        public int UserId { get; set; }
    
        public virtual ICollection<Band> Bands { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}