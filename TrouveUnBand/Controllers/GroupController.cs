﻿using System;
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
        private TrouveUnBandEntities db = new TrouveUnBandEntities();


        //
        // GET: /Group/

        public ActionResult Index()
        {
            return View(db.Bands.ToList());
        }

        //
        // GET: /Group/Details/5

        public ActionResult Details(int id = 0)
        {
            Band band = db.Bands.Find(id);
            if (band == null)
            {
                return HttpNotFound();
            }
            return View(band);
        }

        //
        // GET: /Group/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.GenrelistDD = new List<Genre>(db.Genres);
            return View();
        }

        //
        // POST: /Group/Create
        [HttpPost]
        public PartialViewResult Create(Band band)
        {
            string ViewNameToReturn = "";
            if ((from t in db.Bands where t.Name == band.Name select t) == null)
            {
                ViewNameToReturn = "_ConfirmCreate";
            }
            else
            {
                ViewBag.Message = "Le nom de groupe existe déja";
                ViewNameToReturn = "_ConfirmError";
            }

            return PartialView(ViewNameToReturn, band);
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

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Group/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Band band = db.Bands.Find(id);
            if (band == null)
            {
                return HttpNotFound();
            }
            return View(band);
        }

        //
        // POST: /Group/Edit/5

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

        //
        // GET: /Group/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Band band = db.Bands.Find(id);
            if (band == null)
            {
                return HttpNotFound();
            }
            return View(band);
        }

        //
        // POST: /Group/Delete/5

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
