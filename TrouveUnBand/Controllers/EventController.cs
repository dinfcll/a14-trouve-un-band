using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrouveUnBand.Models;
using System.Drawing;
using System.IO;

namespace TrouveUnBand.Controllers
{
    public class EventController : Controller
    {
        private TrouveUnBandEntities1 db = new TrouveUnBandEntities1();

        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            Event events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        public ActionResult Create()
        {
            ViewBag.GenreListDB = new List<Genre>(db.Genres);
            return View();
        }

        [HttpPost]
        public ActionResult Create(EventValidation events)
        {
            if (ModelState.IsValid)
            {
                events.EventGender = Request["EventGenreDB"];
                events.EventCreator = Request["Creator"];
                if (Request.Files[0].ContentLength != 0)
                {
                    events.EventPhoto = GetPostedEventPhoto();
                }
                Event eventBD = CreateEventFromModel(events);
                db.Events.Add(eventBD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreListDB = new List<Genre>(db.Genres);
            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            ViewBag.GenreListDB = new List<Genre>(db.Genres);
            Event events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            EventValidation EventV = new EventValidation(events);
            return View(EventV);
        }

        [HttpPost]
        public ActionResult Edit(EventValidation events)
        {
            events.EventCreator = Request["Creator"];
            events.EventGender = Request["EventGenreDB"];

            if (Request.Files[0].ContentLength != 0)
            {
                events.EventPhoto = GetPostedEventPhoto();
            }
            else
            {
                events.EventPhoto = GetEventPhotoByte(events.EventId);
            }

            if (ModelState.IsValid)
            {
                Event eventBD = CreateEventFromModel(events);
                db.Entry(eventBD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.GenreListDB = new List<Genre>(db.Genres);
                return View(events);
            }
        }

        public ActionResult Delete(int id = 0)
        {
            Event events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Event events = db.Events.Find(id);
            db.Events.Remove(events);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public byte[] GetEventPhotoByte(int eventID)
        {
            var PicQuery = (from Events in db.Events
                            where
                            Events.EventId.Equals(eventID)
                            select new Photo
                            {
                                ProfilePicture = Events.EventPhoto
                            }).FirstOrDefault();
            return PicQuery.ProfilePicture;
        }

        private Event CreateEventFromModel(EventValidation EventValid)
        {
            Event events = new Event();
            events.EventAddress = EventValid.EventAddress;
            events.EventCity = EventValid.EventCity;
            events.EventCreator = EventValid.EventCreator;
            events.EventDate = EventValid.EventDate;
            events.EventGender = EventValid.EventGender;
            events.EventId = EventValid.EventId;
            events.EventLocation = EventValid.EventLocation;
            events.EventMaxAudience = EventValid.EventMaxAudience;
            events.EventName = EventValid.EventName;
            events.EventPhoto = EventValid.EventPhoto;
            events.EventSalary = EventValid.EventSalary;
            events.EventStageSize = EventValid.EventStageSize;
            return events;
        }

        private byte[] GetPostedEventPhoto()
        {
            HttpPostedFileBase PostedPhoto = Request.Files[0];
            Image img = Image.FromStream(PostedPhoto.InputStream, true, true);
            byte[] bytephoto = imageToByteArray(img);
            return bytephoto;
        }
    }
}
