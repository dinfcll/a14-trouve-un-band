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

        //
        // GET: /Event/

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
            return View();
        }

        [HttpPost]
        public ActionResult Create(Event events)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(events);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(events);
        }

        public ActionResult Edit(int id = 0)
        {
            Event events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        [HttpPost]
        public ActionResult Edit(Event events)
        {
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
                db.Entry(events).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(events);
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
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
    }
}