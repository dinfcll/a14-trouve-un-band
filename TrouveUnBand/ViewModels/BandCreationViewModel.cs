using System;
using System.Collections.Generic;
using TrouveUnBand.Models;
using TrouveUnBand.Services;

namespace TrouveUnBand.ViewModels
{
    public class BandCreationViewModel
    {
        public List<List<String>> GenresMultiselect = GenreDao.GetAllSubgenresByGenres();
        public List<User> BandMembers { get; set; }
        public Band Band { get; set; }

        public BandCreationViewModel()
        {
            Band = new Band();
            BandMembers = new List<User>();
        }
    }
}
