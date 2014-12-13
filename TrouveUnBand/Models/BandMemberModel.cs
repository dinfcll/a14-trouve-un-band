using System.Collections.Generic;

namespace TrouveUnBand.Models
{
    public class BandMemberModel
    {
        public List<BandMemberModel> BandMembers { get; set; }

        public int User_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string Location { get; set; }

        public BandMemberModel()
        {
            this.BandMembers = new List<BandMemberModel>();
        }
    }
}
