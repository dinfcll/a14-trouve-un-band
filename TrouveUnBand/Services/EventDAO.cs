using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrouveUnBand.Models;
using TrouveUnBand.Classes;

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

        public static List<Event> GetEvents(string searchString, string location, int radius)
        {
            TrouveUnBandEntities db = new TrouveUnBandEntities();
            var events = db.Events.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                events.Where(x => x.Name.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(location))
            {
                var eventsToRemove = new List<Event>();
                var coordinates = Geolocalisation.GetCoordinatesByLocation(location);

                foreach (var even in events)
                {
                    var distance = Geolocalisation.GetDistance(even.Latitude, even.Longitude, coordinates.latitude, coordinates.longitude);
                    if (distance > radius)
                        eventsToRemove.Add(even);
                }

                foreach (var even in eventsToRemove)
                {
                    events.Remove(even);
                }
            }

            return events;
        }
    }
}
