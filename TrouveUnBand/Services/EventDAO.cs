using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrouveUnBand.Models;

namespace TrouveUnBand.Services
{
    public class EventDAO
    {   
        public static List<Event> GetAllEvents()
        {
            TrouveUnBandEntities db = new TrouveUnBandEntities();
            List<Event> eventList = new List<Event>();
            var events = db.Events;           
            eventList.AddRange(events);

            return eventList;
        }

        public static List<Event> GetEvents(string searchString, string location)
        {
            TrouveUnBandEntities db = new TrouveUnBandEntities();
            var events = db.Events.ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                events.Where(x => x.Name.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(location))
            {
                events.Where(x => x.Location.Contains(location));
            }

            return events;
        }
    }
}
