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

namespace TrouveUnBand.Controllers
{
    public class AdvertController : Controller
    {
        private TrouveUnBandEntities db = new TrouveUnBandEntities();

        public ActionResult Index()
        {
            var adverts = db.Adverts.Include(a => a.User).Include(a => a.Genres);
            return View(adverts.ToList());
        }

        public ActionResult MyAdverts()
        {
            var adverts = db.Adverts.Include(a => a.User).Include(a => a.Genres);
            return View(adverts.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.GenresAdvert = new List<Genre>(db.Genres);
            return View();
        }

        [HttpPost]
        public ActionResult Create(Advert advert)
        {
            string GenresList = Request["GenreAdvertDB"];
            string[] GenresArray = GenresList.Split(',');

            for (int i = 0; i < GenresArray.Length; i++)
            {
                string GenreName = GenresArray[i];
                var UnGenre = db.Genres.FirstOrDefault(x => x.Name == GenreName);
                advert.Genres.Add(UnGenre);
            }

            string CreatorNameDB = Request["CreatorName"];
            advert.Creator_ID = db.Users.FirstOrDefault(x => x.Nickname == CreatorNameDB).User_ID;
            advert.CreationDate = (DateTime)DateTime.Now;

            if (ModelState.IsValid)
            {
                if (Request.Files[0].ContentLength != 0)
                {
                    advert.Photo = GetPostedAdvertPhoto();
                }
                db.Adverts.Add(advert);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Creator = new SelectList(db.Users, "UserId", "FirstName", advert.Creator_ID);
            ViewBag.GenresAdvert = new SelectList(db.Genres, "Genre_Id", "Name", advert.Genres.Any());
            return View(advert);
        }

        public ActionResult Edit(int id = 0)
        {
            Advert advert = db.Adverts.Find(id);
            if (advert == null)
            {
                return HttpNotFound();
            }

            ViewBag.Creator = new SelectList(db.Users, "UserId", "FirstName", advert.Creator_ID);
            ViewBag.GenresAdvert = new List<Genre>(db.Genres);
            return View(advert);
        }

        [HttpPost]
        public ActionResult Edit(Advert advert)
        {
            string GenresList = Request["GenreAdvertDB"];
            string[] GenresArray = GenresList.Split(',');

            string CreatorNameDB = Request["CreatorName"];
            advert.User = db.Users.FirstOrDefault(x => x.Nickname == CreatorNameDB);
            advert.Creator_ID = advert.User.User_ID;

            if (Request.Files[0].ContentLength != 0)
            {
                advert.Photo = GetPostedAdvertPhoto();
            }
            else
            {
                advert.Photo = GetAdvertPhotoByte(advert.Advert_ID);
            }

            if (ModelState.IsValid)
            {
                db.Entry(advert).State = EntityState.Modified;
                db.SaveChanges();
                ((IObjectContextAdapter)db).ObjectContext.Detach(advert);

                var advertBD = db.Adverts.FirstOrDefault(x => x.Advert_ID == advert.Advert_ID);
                db.Set(typeof(Advert)).Attach(advertBD);

                advertBD.Genres.Clear();
                for (int i = 0; i < GenresArray.Length; i++)
                {
                    string GenreName = GenresArray[i];
                    var UnGenre = db.Genres.FirstOrDefault(x => x.Name == GenreName);
                    advertBD.Genres.Add(UnGenre);
                }

                db.Entry(advertBD).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("MyAdverts", "Advert", "MyAdverts");
            }
            ViewBag.Creator = new SelectList(db.Users, "UserId", "FirstName", advert.Creator_ID);
            ViewBag.GenresAdvert = new SelectList(db.Genres, "Genre_Id", "Name", advert.Genres.Any());

            return View(advert);
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

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            var ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public byte[] GetAdvertPhotoByte(int AdvertIDView)
        {
            var PicQuery = (from Adverts in db.Adverts
                            where
                            Adverts.Advert_ID.Equals(AdvertIDView)
                            select new Photo
                            {
                                PhotoArray = Adverts.Photo
                            }).FirstOrDefault();
            return PicQuery.PhotoArray;
        }

        private byte[] GetPostedAdvertPhoto()
        {
            HttpPostedFileBase PostedPhoto = Request.Files[0];
            Image img = Image.FromStream(PostedPhoto.InputStream, true, true);
            byte[] bytephoto = imageToByteArray(img);
            return bytephoto;
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
    }
}
