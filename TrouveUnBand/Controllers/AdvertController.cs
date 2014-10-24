using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrouveUnBand.Models;

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

        //
        // GET: /Advert/Details/5

        public ActionResult Details(int id = 0)
        {
            Advert advert = db.Adverts.Find(id);
            if (advert == null)
            {
                return HttpNotFound();
            }
            return View(advert);
        }

        //
        // GET: /Advert/Create

        public ActionResult Create()
        {
            ViewBag.Creator = new SelectList(db.Users, "UserId", "FirstName");
            ViewBag.GenresAdvert = new SelectList(db.Genres, "GenreId", "Name");
            return View();
        }

        //
        // POST: /Advert/Create

        [HttpPost]
        public ActionResult Create(Advert advert)
        {
            if (ModelState.IsValid)
            {
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
            if (ModelState.IsValid)
            {
                db.Entry(advert).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}