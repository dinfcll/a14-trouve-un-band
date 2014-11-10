using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using TrouveUnBand.Classes;

namespace TrouveUnBand.Models
{
    public class MusicianProfileViewModel
    {
        public Photo ProfilePicture { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Influences { get; set; }
        public Musician_Instrument InstrumentInfo { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        

        //Musician seeking tab
        public string Seeking { get; set; }
        public string Availability { get; set; }
        public string Frequency { get; set; }
        public string SeekingMessage { get; set; }

        public MusicianProfileViewModel()
        {
            InstrumentInfo = new Musician_Instrument();
            ProfilePicture = new Photo();
        }
     }

}