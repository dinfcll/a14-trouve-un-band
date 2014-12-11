using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            this.Event_ID = eventIn.Event_ID;
            this.Name = eventIn.Name;
            this.Location = eventIn.Location;
            this.Photo = eventIn.Photo;
            this.DayOfTheWeek = DateHelper.GetDayOfWeek(eventIn.EventDate);
            this.DayAndMonth = DateHelper.GetDayAndMonth(eventIn.EventDate);
            this.Bands = eventIn.Bands;
            this.UserNickname = eventIn.User.Nickname;
        }
    }
}