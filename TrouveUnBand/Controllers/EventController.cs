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
        private TrouveUnBandEntities db = new TrouveUnBandEntities();

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
            ViewBag.GenderListDB = new List<Genre>(db.Genres);
            return View();
        }

        [HttpPost]
        public ActionResult Create(EventValidation events)
        {
            events.EventGender = Request["EventGenderDB"];
            events.EventCreator = Request["Creator"];
            if (ModelState.IsValid)
            {
                if (Request.Files[0].ContentLength != 0)
                {
                    HttpPostedFileBase PostedPhoto = Request.Files[0];
                    Image img = Image.FromStream(PostedPhoto.InputStream, true, true);
                    byte[] bytephoto = imageToByteArray(img);
                    events.EventPhoto = bytephoto;
                }
                Event eventBD = CreateEventFromModel(events);
                db.Events.Add(eventBD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(events);
        }

        public ActionResult Edit(int id = 0)
        {
            ViewBag.GenderListDB = new List<Genre>(db.Genres);
            Event events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        [HttpPost]
        public ActionResult Edit(EventValidation events)
        {
            events.EventCreator = Request["Creator"];
            events.EventGender = Request["EventGenderDB"];

            if (ModelState.IsValid)
            {
                if (Request.Files[0].ContentLength != 0)
                {
                    HttpPostedFileBase PostedPhoto = Request.Files[0];
                    Image img = Image.FromStream(PostedPhoto.InputStream, true, true);
                    byte[] bytephoto = imageToByteArray(img);
                    events.EventPhoto = bytephoto;
                }
                else
                {
                    events.EventPhoto = GetEventPhotoByte(events.EventId);
                }

                Event eventBD = CreateEventFromModel(events);
                db.Entry(eventBD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                Event eventView = CreateEventFromModel(events);
                return View();
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
    }
}