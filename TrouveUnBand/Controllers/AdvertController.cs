using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrouveUnBand.Models;
using System.IO;
using System.Drawing;
using TrouveUnBand.Classes;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using TrouveUnBand.POCO;

namespace TrouveUnBand.Controllers
{
    public class AdvertController : Controller
    {
        private TrouveUnBandEntities db = new TrouveUnBandEntities();

        public ActionResult Index()
        {
            ViewBag.UsersListDB = new List<User>(db.Users);
            var adverts = db.Adverts.Include(a => a.User).Include(a => a.Genres);
            return View(adverts.ToList());
        }

        public ActionResult MyAdverts()
        {
            return View("../Shared/Authentication");
            ViewBag.UsersListDB = new List<User>(db.Users);
            var adverts = db.Adverts.Include(a => a.User).Include(a => a.Genres);
            return View(adverts.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.GenreListDB = new List<Genre>(db.Genres);
            return View();
        }

        [HttpPost]
        public ActionResult Create(Advert advertToCreate, string[] GenreAdvertDB, string CreatorName)
        {
            for (int i = 0; i < GenreAdvertDB.Length; i++)
            {
                string GenreName = GenreAdvertDB[i];
                var UnGenre = db.Genres.FirstOrDefault(x => x.Name == GenreName);
                advertToCreate.Genres.Add(UnGenre);
            }

            advertToCreate.Creator_ID = db.Users.FirstOrDefault(x => x.Nickname == CreatorName).User_ID;
            advertToCreate.CreationDate = (DateTime)DateTime.Now;

            if (ModelState.IsValid)
            {
                string GenresList = Request["GenreAdvertDB"];
                string[] GenresArray = GenresList.Split(',');

                for (int i = 0; i < GenresArray.Length; i++)
                {
                    string GenreName = GenresArray[i];
                    var UnGenre = db.Genres.FirstOrDefault(x => x.Name == GenreName);
                    advertToCreate.Genres.Add(UnGenre);
                }

                advertToCreate.Creator_ID = db.Users.FirstOrDefault(x => x.Nickname == CreatorName).User_ID;
                advertToCreate.CreationDate = (DateTime)DateTime.Now;

                db.Adverts.Add(advertToCreate);
                db.SaveChanges();

                var savedPhotoPath = CropAndSavePhoto(advertToCreate);
                advertToCreate.Photo = savedPhotoPath;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Creator = new SelectList(db.Users, "UserId", "FirstName", advertToCreate.Creator_ID);
            ViewBag.GenreListDB = new List<Genre>(db.Genres);
            return View(advertToCreate);
        }

        public ActionResult Edit(int id = 0)
        {
            Advert advert = db.Adverts.Find(id);
            if (advert == null)
            {
                return HttpNotFound();
            }

            ViewBag.Creator = new SelectList(db.Users, "UserId", "FirstName", advert.Creator_ID);
            ViewBag.GenreListDB = new List<Genre>(db.Genres);
            return View(advert);
        }

        [HttpPost]
        public ActionResult Edit(Advert newAdvertInfo, string[] GenreAdvertDB, string CreatorName)
        {

            var oldAdvert = db.Adverts.FirstOrDefault(x => x.Advert_ID == newAdvertInfo.Advert_ID);
            newAdvertInfo.Photo = oldAdvert.Photo;
            newAdvertInfo.User = oldAdvert.User;

            if (ModelState.IsValid && newAdvertInfo.Genres.Count>0)
            {
                string GenresList = Request["GenreAdvertDB"];
                string[] GenresArray = GenresList.Split(',');

                oldAdvert.Genres.Clear();
                for (int i = 0; i < GenresArray.Length; i++)
                {
                    string GenreName = GenreAdvertDB[i];
                    var UnGenre = db.Genres.FirstOrDefault(x => x.Name == GenreName);
                    oldAdvert.Genres.Add(UnGenre);
                }

                db.Entry(oldAdvert).State = EntityState.Modified;
                db.SaveChanges();

                ((IObjectContextAdapter)db).ObjectContext.Detach(oldAdvert);
                db.Entry(newAdvertInfo).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("MyAdverts", "Advert", "MyAdverts");
            }
            ViewBag.Creator = new SelectList(db.Users, "UserId", "FirstName", newAdvertInfo.Creator_ID);
            ViewBag.GenreListDB = new List<Genre>(db.Genres);

            return View(newAdvertInfo);
        }

        public ActionResult Delete(int id = 0)
        {
            Advert advert = db.Adverts.Find(id);
            if (advert == null)
            {
                return HttpNotFound();
            }
            return View(advert);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Advert advert = db.Adverts.Find(id);
            advert.Genres.Clear();
            db.SaveChanges();
            db.Adverts.Remove(advert);
            db.SaveChanges();
            FileHelper.DeletePhoto(id.ToString(), FileHelper.Category.ADVERT_PHOTO);

            return RedirectToAction("MyAdverts", "Advert", "MyAdverts");
        }

        public ActionResult ReOpen(int id = 0)
        {
            Advert advert = db.Adverts.Find(id);
            if (advert == null)
            {
                return HttpNotFound();
            }
            advert.Status = "En cours";
            db.SaveChanges();

            return RedirectToAction("MyAdverts", "Advert", "MyAdverts");
        }

        public ActionResult Close(int id = 0)
        {
            Advert advert = db.Adverts.Find(id);
            if (advert == null)
            {
                return HttpNotFound();
            }
            return View(advert);
        }

        [HttpPost, ActionName("Close")]
        public ActionResult CloseConfirmed(int id)
        {
            Advert advert = db.Adverts.Find(id);
            db.Adverts.FirstOrDefault(x => x.Advert_ID == advert.Advert_ID).Status = "Fermée";
            db.SaveChanges();
            return RedirectToAction("MyAdverts", "Advert", "MyAdverts");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CropImageDialog(Advert advertWithPhoto)
        {
            try
            {
                Advert existingAdvert = db.Adverts.FirstOrDefault(x => x.Advert_ID == advertWithPhoto.Advert_ID);
                string savedPhotoPath = CropAndSavePhoto(advertWithPhoto);

                if (savedPhotoPath != "")
                {
                    existingAdvert.Photo = savedPhotoPath;
                    db.SaveChanges();

                    TempData["success"] = AlertMessages.PICTURE_CHANGED;
                }

                return RedirectToAction("Edit", new { id = advertWithPhoto.Advert_ID });
            }
            catch
            {
                TempData["TempDataError"] = AlertMessages.INTERNAL_ERROR;
                return RedirectToAction("Edit", new { id = advertWithPhoto.Advert_ID});
            }
        }

        public ActionResult ViewAdvertProfil(Advert myAdvert)
        {
            return View("AdvertProfil", myAdvert);
        }

        public ActionResult ViewAdvertProfil(int AdvertId)
        {
            var myAdvert = db.Adverts.FirstOrDefault(x => x.Advert_ID == AdvertId);
            return View("AdvertProfil", myAdvert);
        }

        private string CropAndSavePhoto(Advert advertWithPhoto)
        {
            var postedPhoto = Request.Files[0];

            if (postedPhoto.ContentLength == 0)
            {
                return Photo.ADVERT_STOCK_PHOTO;
            }

            Image image = Image.FromStream(postedPhoto.InputStream, true, true);

            if (image.Width < 250 || image.Height < 172 || image.Width > 800 || image.Height > 600)
            {
                image = PhotoResizer.ResizeImage(image, 250, 172, 800, 600);
            }

            var croppedPhoto = PhotoCropper.CropImage(image, advertWithPhoto.PhotoCrop.CropRect);
            string advertPhotoName = advertWithPhoto.Advert_ID.ToString();
            var savedPhotoPath = FileHelper.SavePhoto(croppedPhoto, advertPhotoName, FileHelper.Category.ADVERT_PHOTO);

            if (savedPhotoPath == "")
            {
                savedPhotoPath = Photo.ADVERT_STOCK_PHOTO;
            }

            return savedPhotoPath;
        }
    }
}
