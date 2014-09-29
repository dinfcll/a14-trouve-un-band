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
    public class GroupController : Controller
    {
        private TrouveUnBandEntities1 db = new TrouveUnBandEntities1();

        public ActionResult Index()
        {
            return View(db.Bands.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            Band band = db.Bands.Find(id);
            if (band == null)
            {
                return HttpNotFound();
            }
            return View(band);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult ConfirmCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Band band)
        {
            return RedirectToAction("ConfirmCreate", band);
        }

        [HttpPost]
        public ActionResult ConfirmCreate(Band band)
        {
            try
            {
                    db.Bands.Add(band);
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(band);
        }

        public ActionResult Edit(int id = 0)
        {
            Band band = db.Bands.Find(id);
            if (band == null)
            {
                return HttpNotFound();
            }
            return View(band);
        }

        [HttpPost]
        public ActionResult Edit(Band band)
        {
            if (ModelState.IsValid)
            {
                db.Entry(band).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(band);
        }

        public ActionResult Delete(int id = 0)
        {
            Band band = db.Bands.Find(id);
            if (band == null)
            {
                return HttpNotFound();
            }
            return View(band);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Band band = db.Bands.Find(id);
            db.Bands.Remove(band);
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
