using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrouveUnBand.Models
{
    public class BandMemberModel
    {
        public int User_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string Location { get; set; }
    }
}
