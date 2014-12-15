using System.Collections.Generic;
using TrouveUnBand.Classes;

namespace TrouveUnBand.ViewModels
{
    public class HomePageViewModel
    {
        public List<UserInfo> UserList { get; set; }
        public List<BandInfo> BandList { get; set; }
        public List<EventInfo> EventList { get; set; }
        public List<AdvertInfo> AdvertList { get; set; }

        public HomePageViewModel()
        {
            UserList = new List<UserInfo>();
            EventList = new List<EventInfo>();
            BandList = new List<BandInfo>();
            AdvertList = new List<AdvertInfo>();
        }
    }
}