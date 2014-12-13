using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrouveUnBand.Classes;
using TrouveUnBand.ViewModels;

namespace TrouveUnBand.Models
{
    public class EventPageViewModel
    {
        public List<EventInfo> EventList { get; set; }

        public EventPageViewModel(IEnumerable<Event> ListOfEvent)
        {
            EventList = new List<EventInfo>();

            foreach(var item in ListOfEvent)
            {
                var eventInfo = new EventInfo(item);
                EventList.Add(eventInfo);
            }
        }
    }
}
