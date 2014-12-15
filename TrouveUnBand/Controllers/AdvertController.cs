using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using TrouveUnBand.Classes;
using TrouveUnBand.Models;
using TrouveUnBand.POCO;

namespace TrouveUnBand.Controllers
{
    public class AdvertController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.UsersListDB = new List<User>(db.Users);
            var adverts = db.Adverts.Include(a => a.User).Include(a => a.Genres);
            return View(adverts.ToList());
        }

        public ActionResult MyAdverts()
        {
            if (!CurrentUserIsAuthenticated())
            {
                return View("../Shared/Authentication");
            }
            ViewBag.UsersListDB = new List<User>(db.Users);
            var adverts = db.Adverts.Include(a => a.User).Include(a => a.Genres);
            return View(adverts.ToList());
        }

        public ActionResult Create()
        {
            if (!CurrentUserIsAuthenticated())
            {
                return View("../Shared/Authentication");
            }
            ViewBag.GenreListDB = new List<Genre>(db.Genres);
            return View();
        }

        [HttpPost]
        public ActionResult Create(Advert advertToCreate, string[] genreAdvertDb, string creatorName)
        {
            for (int i = 0; i < genreAdvertDb.Length; i++)
            {
                string genreName = genreAdvertDb[i];
                var unGenre = db.Genres.FirstOrDefault(x => x.Name == genreName);
                advertToCreate.Genres.Add(unGenre);
            }

            advertToCreate.Creator_ID = db.Users.FirstOrDefault(x => x.Nickname == creatorName).User_ID;
            advertToCreate.CreationDate = (DateTime)DateTime.Now;

            if (ModelState.IsValid)
            {
                string genresList = Request["GenreAdvertDB"];
                string[] genresArray = genresList.Split(',');

                for (int i = 0; i < genresArray.Length; i++)
                {
                    string genreName = genresArray[i];
                    var unGenre = db.Genres.FirstOrDefault(x => x.Name == genreName);
                    advertToCreate.Genres.Add(unGenre);
                }

                advertToCreate.Creator_ID = db.Users.FirstOrDefault(x => x.Nickname == creatorName).User_ID;
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
        public ActionResult Edit(Advert newAdvertInfo, string[] genreAdvertDb)
        {
            var oldAdvert = db.Adverts.FirstOrDefault(x => x.Advert_ID == newAdvertInfo.Advert_ID);
            newAdvertInfo.Photo = oldAdvert.Photo;
            newAdvertInfo.User = oldAdvert.User;
            newAdvertInfo.CreationDate = oldAdvert.CreationDate;
            oldAdvert.Genres.Clear();
            if (ModelState.IsValid)
            {
                foreach (var genreName in genreAdvertDb)
                {
                    oldAdvert.Genres.Add(db.Genres.FirstOrDefault(x => x.Name == genreName));
                }
                db.Entry(oldAdvert).CurrentValues.SetValues(newAdvertInfo);
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

                    Success(Messages.PICTURE_CHANGED, true);
                }

                return RedirectToAction("Edit", new { id = advertWithPhoto.Advert_ID });
            }
            catch
            {
                Danger(Messages.INTERNAL_ERROR, true);
                return RedirectToAction("Edit", new { id = advertWithPhoto.Advert_ID});
            }
        }

        public ActionResult ViewAdvertProfil(Advert myAdvert)
        {
            return View("AdvertProfil", myAdvert);
        }

        public ActionResult ViewAdvertProfil(int advertId)
        {
            var myAdvert = db.Adverts.FirstOrDefault(x => x.Advert_ID == advertId);
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
