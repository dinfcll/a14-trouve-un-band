using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrouveUnBand.Models
{
    public class BandProfileViewModel
    {
        public Photo ProfilePicture { get; set; }

        //Band info
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Influences { get; set; }
        public List<Musician_Instrument> InstrumentInfoList { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }

        public BandProfileViewModel()
        {
            InstrumentInfoList = new List<Musician_Instrument>();
            ProfilePicture = new Photo();
        }
    }
}