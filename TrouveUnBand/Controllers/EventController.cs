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
        public ActionResult Create(Event eventToCreate, string[] EventGenreDB, string Creator, string[] BandsListDB)
        {
            if (ModelState.IsValid && EventGenreDB != null)
            {
                foreach(var GenreName in EventGenreDB)
                {
                    eventToCreate.Genres.Add(db.Genres.FirstOrDefault(x => x.Name == GenreName));
                }

                if (BandsListDB != null)
                {
                    foreach (var BandName in BandsListDB)
                    {
                        eventToCreate.Bands.Add(db.Bands.FirstOrDefault(x => x.Name == BandName));
                    }
                }

                eventToCreate.Creator_ID = db.Users.FirstOrDefault(x => x.Nickname == Creator).User_ID;
                db.Events.Add(eventToCreate);
                db.SaveChanges();

                var savedPhotoPath = CropAndSavePhoto(eventToCreate);
                eventToCreate.Photo = savedPhotoPath;
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

            if (ModelState.IsValid && EventGenreDB != null)
            {
                db.Entry(events).State = EntityState.Modified;
                db.SaveChanges();
                ((IObjectContextAdapter)db).ObjectContext.Detach(events);
                
                var eventBD = db.Events.FirstOrDefault(x => x.Event_ID == events.Event_ID);
                db.Set(typeof(Event)).Attach(eventBD);
                eventBD.Genres.Clear();
                eventBD.Bands.Clear();

                foreach (var GenreName in EventGenreDB)
                {
                    eventBD.Genres.Add(db.Genres.FirstOrDefault(x => x.Name == GenreName));
                }

                if (BandsListDB != null)
                {
                    foreach (var BandName in BandsListDB)
                    {
                        eventBD.Bands.Add(db.Bands.FirstOrDefault(x => x.Name == BandName));
                    }
                }

                db.Entry(eventBD).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.GenreListDB = new List<Genre>(db.Genres);
            ViewBag.BandsListDB = new List<Band>(db.Bands);
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
            FileHelper.DeletePhoto(id.ToString(), FileHelper.Category.EVENT_PHOTO);

            return RedirectToAction("Index");
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CropImageDialog(Event eventWithPhoto)
        {
            try
            {
                Event existingEvent = db.Events.FirstOrDefault(x => x.Event_ID == eventWithPhoto.Event_ID);
                string savedPhotoPath = CropAndSavePhoto(eventWithPhoto);

                if (savedPhotoPath != "")
                {
                    existingEvent.Photo = savedPhotoPath;
                    db.SaveChanges();

                    TempData["success"] = AlertMessages.PICTURE_CHANGED;
                }

                return RedirectToAction("Edit", new { id = eventWithPhoto.Event_ID });
            }
            catch
            {
                TempData["TempDataError"] = AlertMessages.INTERNAL_ERROR;
                return RedirectToAction("Edit", new { id = eventWithPhoto.Event_ID });
            }
        }


        private string CropAndSavePhoto(Event eventWithPhoto)
        {
            var postedPhoto = Request.Files[0];

            if (postedPhoto.ContentLength == 0)
            {
                return Photo.EVENT_STOCK_PHOTO;
            }

            Image image = Image.FromStream(postedPhoto.InputStream, true, true);

            if (image.Width < 250 || image.Height < 172 || image.Width > 800 || image.Height > 600)
            {
                image = PhotoResizer.ResizeImage(image, 250, 172, 800, 600);
            }

            var croppedPhoto = PhotoCropper.CropImage(image, eventWithPhoto.PhotoCrop.CropRect);
            string eventPhotoName = eventWithPhoto.Event_ID.ToString();
            var savedPhotoPath = FileHelper.SavePhoto(croppedPhoto, eventPhotoName, FileHelper.Category.EVENT_PHOTO);

            if (savedPhotoPath == "")
            {
                savedPhotoPath = Photo.EVENT_STOCK_PHOTO;
            }

            return savedPhotoPath;
        }
    }
}
