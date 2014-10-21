using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace TrouveUnBand.Models
{
    public class MusicianProfileViewModel
    {
        public Photo ProfilePicture { get; set; }

        //Musician info tab
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Influences { get; set; }
        public List<string> InstrumentName { get; set; }
        public List<string> InstrumentSkill { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        

        //Musician seeking tab
        public string Seeking { get; set; }
        public string Availability { get; set; }
        public string Frequency { get; set; }
        public string SeekingMessage { get; set; }
     }

}