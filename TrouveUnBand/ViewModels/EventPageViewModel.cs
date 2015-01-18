using System.Collections.Generic;
using TrouveUnBand.Classes;

namespace TrouveUnBand.Models
{
    public class EventPageViewModel
    {
        public List<EventInfo> EventList { get; set; }
        public string DayWithEvent { get; set; }

        public EventPageViewModel(IEnumerable<Event> ListOfEvent)
        {
            EventList = new List<EventInfo>();
            DayWithEvent = "";

            foreach(var item in ListOfEvent)
            {
                var eventInfo = new EventInfo(item);
                EventList.Add(eventInfo);

                var day = DateHelper.GetDay(item.EventDate);
                if (!DayWithEvent.Contains(day))
                {
                    DayWithEvent += day + "-";
                }
            }
        }
    }
}
