using System;
using System.Collections.Generic;
using System.Linq;
using TrouveUnBand.Models;
using TrouveUnBand.Classes;

namespace TrouveUnBand.Services
{
    public class EventDAO
    {   
        public static List<Event> GetAllEvents()
        {
            var db = new TrouveUnBandEntities();
            var eventList = new List<Event>();
            var events = db.Events;           
            eventList.AddRange(events);

            return eventList;
        }

        public static List<Event> GetEvents(string searchString, string location, int radius)
        {
            var db = new TrouveUnBandEntities();
            var events = db.Events.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                events = events.Where(x => x.Name.Contains(searchString)).ToList();
            }
            if (!String.IsNullOrEmpty(location))
            {
                var eventsToRemove = new List<Event>();

                foreach (var even in events)
                {
                    if (!Geolocalisation.CheckIfInRange(even.Location, location, radius))
                    {
                        eventsToRemove.Add(even);
                    }
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
