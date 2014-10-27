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

namespace TrouveUnBand.Controllers
{
    public class AdvertController : Controller
    {
        private TrouveUnBandEntities db = new TrouveUnBandEntities();

        //
        // GET: /Advert/

        public ActionResult Index()
        {
            var adverts = db.Adverts.Include(a => a.User).Include(a => a.Genre);
            return View(adverts.ToList());
        }

        public ActionResult MyAdverts()
        {
            var adverts = db.Adverts.Include(a => a.User).Include(a => a.Genre);
            return View(adverts.ToList());
        }

        //
        // GET: /Advert/Create

        public ActionResult Create()
        {
            ViewBag.GenresAdvert = new SelectList(db.Genres, "GenreId", "Name");
            return View();
        }

        //
        // POST: /Advert/Create

        [HttpPost]
        public ActionResult Create(Advert advert)
        {
            string CreatorNameDB = Request["CreatorName"];
            advert.Creator = db.Users.FirstOrDefault(x => x.Nickname == CreatorNameDB).UserId;
            advert.GenresAdvert = Convert.ToInt32(Request["GenreAdvertDB"]);
            advert.CreationDate = (DateTime)DateTime.Now;
            if (ModelState.IsValid)
            {
                if (Request.Files[0].ContentLength != 0)
                {
                    advert.AdvertPhoto = GetPostedAdvertPhoto();
                }
                db.Adverts.Add(advert);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Creator = new SelectList(db.Users, "UserId", "FirstName", advert.Creator);
            ViewBag.GenresAdvert = new SelectList(db.Genres, "GenreId", "Name", advert.GenresAdvert);
            return View(advert);
        }

        //
        // GET: /Advert/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Advert advert = db.Adverts.Find(id);
            if (advert == null)
            {
                return HttpNotFound();
            }

            ViewBag.Creator = new SelectList(db.Users, "UserId", "FirstName", advert.Creator);
            ViewBag.GenresAdvert = new SelectList(db.Genres, "GenreId", "Name", advert.GenresAdvert);
            return View(advert);
        }

        //
        // POST: /Advert/Edit/5

        [HttpPost]
        public ActionResult Edit(Advert advert)
        {
            string CreatorNameDB = Request["CreatorName"];
            advert.Creator = db.Users.FirstOrDefault(x => x.Nickname == CreatorNameDB).UserId;
            advert.GenresAdvert = Convert.ToInt32(Request["GenreAdvertDB"]);
            if (ModelState.IsValid)
            {
                if (Request.Files[0].ContentLength != 0)
                {
                    advert.AdvertPhoto = GetPostedAdvertPhoto();
                }
                else
                {
                    advert.AdvertPhoto = GetAdvertPhotoByte(advert.AdvertId);
                }
                db.Entry(advert).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyAdverts", "Advert", "MyAdverts");
            }
            ViewBag.Creator = new SelectList(db.Users, "UserId", "FirstName", advert.Creator);
            ViewBag.GenresAdvert = new SelectList(db.Genres, "GenreId", "Name", advert.GenresAdvert);
            return View(advert);
        }

        //
        // GET: /Advert/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Advert advert = db.Adverts.Find(id);
            if (advert == null)
            {
                return HttpNotFound();
            }
            return View(advert);
        }

        //
        // POST: /Advert/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Advert advert = db.Adverts.Find(id);
            db.Adverts.Remove(advert);
            db.SaveChanges();
            return RedirectToAction("Index");
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

        //
        // POST: /Advert/Delete/5

        [HttpPost, ActionName("Close")]
        public ActionResult CloseConfirmed(int id)
        {
            Advert advert = db.Adverts.Find(id);
            db.Adverts.FirstOrDefault(x => x.AdvertId == advert.AdvertId).Status = "Fermée";
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

        public byte[] GetAdvertPhotoByte(int AdvertIDView)
        {
            var PicQuery = (from Adverts in db.Adverts
                            where
                            Adverts.AdvertId.Equals(AdvertIDView)
                            select new Photo
                            {
                                ProfilePicture = Adverts.AdvertPhoto
                            }).FirstOrDefault();
            return PicQuery.ProfilePicture;
        }

        private byte[] GetPostedAdvertPhoto()
        {
            HttpPostedFileBase PostedPhoto = Request.Files[0];
            Image img = Image.FromStream(PostedPhoto.InputStream, true, true);
            byte[] bytephoto = imageToByteArray(img);
            return bytephoto;
        }
    }
}