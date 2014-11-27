using System;
using System.Collections.Generic;
using System.Linq;
using TrouveUnBand.Models;
using TrouveUnBand.Classes;

namespace TrouveUnBand.Services
{
    public class EventDao
    {   
        public static List<Event> GetAllEvents()
        {
            var db = new TrouveUnBandEntities();
            var eventList = new List<Event>();
            var events = db.Events;           
            eventList.AddRange(events);

            return eventList;
        }

        public static List<Event> GetEvents(string[] genres, string searchString, string location, int radius)
        {
            var db = new TrouveUnBandEntities();
            var events = db.Events.ToList();

            if (genres != null)
            {
                foreach (String genreName in genres)
                {
                    events = events.Where(x => x.Genres.Any(genre => genre.Name == genreName)).ToList();
                }
            }

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

        public static List<Event> GetEvents(int? genre_ID, string searchString, string location, int radius)
        {
            var db = new TrouveUnBandEntities();
            var events = db.Events.ToList();

            if (genre_ID != null)
            {
                events = events.Where(x => x.Genres.Any(genre => genre.Genre_ID == genre_ID)).ToList();
            }

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
