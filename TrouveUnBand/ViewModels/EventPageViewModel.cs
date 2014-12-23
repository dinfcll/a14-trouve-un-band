using System.Collections.Generic;
using TrouveUnBand.Classes;

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
