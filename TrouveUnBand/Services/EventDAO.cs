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
    }
}
