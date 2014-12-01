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
using TrouveUnBand.Classes;
using System.Data.Entity.Infrastructure;
using System.Data.Objects.DataClasses;
using TrouveUnBand.POCO;

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

        public ActionResult EventProfile(int id = 0)
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
            ViewBag.BandsListDB = new List<Band>(db.Bands);
            return View();
        }

        [HttpPost]
        public ActionResult Create(Event events, string[] EventGenreDB, string Creator, string[] BandsListDB)
        {
            if (ModelState.IsValid && EventGenreDB != null && BandsListDB != null)
            {
                for (int i = 0; i < EventGenreDB.Length; i++)
                {
                    string GenreName = EventGenreDB[i];
                    var UnGenre = db.Genres.FirstOrDefault(x => x.Name == GenreName);
                    events.Genres.Add(UnGenre);
                }

                for (int i = 0; i < BandsListDB.Length; i++)
                {
                    string BandName = BandsListDB[i];
                    var UnBand = db.Bands.FirstOrDefault(x => x.Name == BandName);
                    events.Bands.Add(UnBand);
                }

                events.Creator_ID = db.Users.FirstOrDefault(x => x.Nickname == Creator).User_ID;
                if (Request.Files[0].ContentLength != 0)
                {
                    events.Photo = GetPostedEventPhoto();
                }
                db.Events.Add(events);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            string messageAlert = AlertMessages.NOT_MUSICIAN;
            ViewBag.GenreListDB = new List<Genre>(db.Genres);
            ViewBag.BandsListDB = new List<Band>(db.Bands);
            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            ViewBag.GenreListDB = new List<Genre>(db.Genres);
            ViewBag.BandsListDB = new List<Band>(db.Bands);
            Event events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        [HttpPost]
        public ActionResult Edit(Event events, string[] EventGenreDB, string Creator, string[] BandsListDB)
        {
            events.Creator_ID = Convert.ToInt32(Creator);
            events.User = db.Users.FirstOrDefault(x => x.User_ID == events.Creator_ID);
            
            if (Request.Files[0].ContentLength != 0)
            {
                events.Photo = GetPostedEventPhoto();
            }
            else
            {
                if (GetEventPhotoByte(events.Event_ID) != null)
                {
                    events.Photo = GetEventPhotoByte(events.Event_ID);
                }
            }

            if (ModelState.IsValid && EventGenreDB != null && BandsListDB != null)
            {
                db.Entry(events).State = EntityState.Modified;
                db.SaveChanges();
                ((IObjectContextAdapter)db).ObjectContext.Detach(events);
                
                var eventBD = db.Events.FirstOrDefault(x => x.Event_ID == events.Event_ID);
                db.Set(typeof(Event)).Attach(eventBD);
                eventBD.Genres.Clear();
                eventBD.Bands.Clear();
                for (int i = 0; i < EventGenreDB.Length; i++)
                {
                    string GenreName = EventGenreDB[i];
                    var UnGenre = db.Genres.FirstOrDefault(x => x.Name == GenreName);
                    eventBD.Genres.Add(UnGenre);
                }
                for (int i = 0; i < BandsListDB.Length; i++)
                {
                    string BandName = BandsListDB[i];
                    var UnBand = db.Bands.FirstOrDefault(x => x.Name == BandName);
                    eventBD.Bands.Add(UnBand);
                }
                db.Entry(eventBD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreListDB = new List<Genre>(db.Genres);
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
            events.Genres.Clear();
            db.SaveChanges();
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
                            Events.Event_ID.Equals(eventID)
                            select new Photo
                            {
                                PhotoArray = Events.Photo
                            }).FirstOrDefault();
            return PicQuery.PhotoArray;
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
