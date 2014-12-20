using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using TrouveUnBand.Classes;
using TrouveUnBand.Models;
using TrouveUnBand.POCO;

namespace TrouveUnBand.Controllers
{
    public class EventController : BaseController
    {
        public ActionResult Index()
        {
            var listOfEvent = new EventPageViewModel(db.Events.ToList());
            return View(listOfEvent.EventList);
        }

        public ActionResult test()
        {
            return View();
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
            if (!CurrentUserIsAuthenticated())
            {
                return View("../Shared/Authentication");
            }
            ViewBag.GenreListDB = new List<Genre>(db.Genres);
            ViewBag.BandsListDB = new List<Band>(db.Bands);
            return View();
        }

        [HttpPost]
        public ActionResult Create(Event eventToCreate, string[] eventGenreDb, string creator, string[] bandsListDb)
        {
            if (ModelState.IsValid && eventGenreDb != null)
            {
                foreach(var genreName in eventGenreDb)
                {
                    eventToCreate.Genres.Add(db.Genres.FirstOrDefault(x => x.Name == genreName));
                }

                if (bandsListDb != null)
                {
                    foreach (var bandName in bandsListDb)
                    {
                        eventToCreate.Bands.Add(db.Bands.FirstOrDefault(x => x.Name == bandName));
                    }
                }

                eventToCreate.Creator_ID = db.Users.FirstOrDefault(x => x.Nickname == creator).User_ID;
                db.Events.Add(eventToCreate);
                db.SaveChanges();

                var savedPhotoPath = CropAndSavePhoto(eventToCreate);
                eventToCreate.Photo = savedPhotoPath;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            Danger(Messages.NOT_MUSICIAN,true);
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
        public ActionResult Edit(Event events, string[] eventGenreDb, string[] bandsListDb)
        {
            events.User = db.Users.FirstOrDefault(x => x.User_ID == events.Creator_ID);

            if (ModelState.IsValid && eventGenreDb != null)
            {
                var eventBd = db.Events.FirstOrDefault(x => x.Event_ID == events.Event_ID);
                events.Photo = eventBd.Photo;
                eventBd.Genres.Clear();
                eventBd.Bands.Clear();

                foreach (var genreName in eventGenreDb)
                {
                    eventBd.Genres.Add(db.Genres.FirstOrDefault(x => x.Name == genreName));
                }

                if (bandsListDb != null)
                {
                    foreach (var bandName in bandsListDb)
                    {
                        eventBd.Bands.Add(db.Bands.FirstOrDefault(x => x.Name == bandName));
                    }
                }
                db.Entry(eventBd).CurrentValues.SetValues(events);
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
            Event eventToDelete = db.Events.Find(id);

            foreach (var band in eventToDelete.Bands)
            {
                band.Events.Remove(eventToDelete);
            }

            eventToDelete.Genres.Clear();
            db.Events.Remove(eventToDelete);
            db.SaveChanges();
            FileHelper.DeletePhoto(id.ToString(), FileHelper.Category.EVENT_PHOTO);

            return RedirectToAction("Index");
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

                    Success(Messages.PICTURE_CHANGED,true);
                }

                return RedirectToAction("Edit", new { id = eventWithPhoto.Event_ID });
            }
            catch
            {
                Danger(Messages.INTERNAL_ERROR,true);
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
