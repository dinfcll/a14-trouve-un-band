using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrouveUnBand.Models
{
    //Class used to structure information returned by the search function.
    public class NewsfeedBandModel
    {
        public int BandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    }

    public class NewsfeedUserModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public byte[] Photo { get; set; }
    }

    public class NewsfeedEventModel
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventLocation { get; set; }
        public string Nickname { get; set; }
        public string EventGender { get; set; }
        public byte[] EventPhoto { get; set; }
    }

    public class NewsfeedAdvertModel
    {
        public int AdvertId { get; set; }
        public string Type { get; set; }
        public Genre Genre { get; set; }
        public string Description { get; set; }
        public byte[] AdvertPhoto { get; set; }
    }
}
