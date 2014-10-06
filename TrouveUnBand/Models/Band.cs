using System;
using System.Collections.Generic;

namespace TrouveUnBand.Models
{
    public partial class Band
    {
        public Band()
        {
            this.Genres = new HashSet<Genre>();
            this.Musicians = new HashSet<Musician>();
        }
    
        public int BandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Musician> Musicians { get; set; }
    }
    
}
