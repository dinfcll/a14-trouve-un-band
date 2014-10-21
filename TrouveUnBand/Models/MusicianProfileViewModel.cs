using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrouveUnBand.Models
{
    public class MusicianProfileViewModel
    {
        public List<string> SkillList { get; private set; }

        //Musician info tab
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Influences { get; set; }
        public List<Join_Musician_Instrument> MostSkilledInstrument { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Join_Musician_Instrument> Instruments { get; set; }
        

        //Musician seeking tab
        public string Seeking { get; set; }
        public string Availability { get; set; }
        public string Frequency { get; set; }
        public string SeekingMessage { get; set; }


        public MusicianProfileViewModel()
        {
            SkillList = new List<string> { "Aucun", "Débutant", "Initié", "Intermédiaire", "Avancé", "Légendaire" };
        }
     }

}