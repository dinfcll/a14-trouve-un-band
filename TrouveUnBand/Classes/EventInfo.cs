using System.Collections.Generic;
using TrouveUnBand.Models;

namespace TrouveUnBand.Classes
{
    public class EventInfo
    {
        public int Event_ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Photo { get; set; }
        public string DayOfTheWeek { get; set; }
        public string DayAndMonth { get; set; }
        public string UserNickname { get; set; }

        public virtual ICollection<Band> Bands { get; set; }

        public EventInfo(Event eventIn)
        {
            Event_ID = eventIn.Event_ID;
            Name = eventIn.Name;
            Location = eventIn.Location;
            Photo = eventIn.Photo;
            DayOfTheWeek = DateHelper.GetDayOfWeek(eventIn.EventDate);
            DayAndMonth = DateHelper.GetDayAndMonth(eventIn.EventDate);
            Bands = eventIn.Bands;
            UserNickname = eventIn.User.Nickname;
        }
    }
}
