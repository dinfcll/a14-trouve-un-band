using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrouveUnBand.Models;
using System.Data.Entity

namespace TrouveUnBand.Controllers
{
    public class GroupController : Controller
    {
        private TUBBDContext db = new TUBBDContext();

        //
        // GET: /Group/

        public ActionResult Index()
        {
            return View(db.Group.ToList());
        }

        //
        // GET: /Group/Details/5

        public ActionResult Details(int id = 0)
        {
            CreateGroupModel creategroupmodel = db.Group.Find(id);
            if (creategroupmodel == null)
            {
                return HttpNotFound();
            }
            return View(creategroupmodel);
        }

        //
        // GET: /Group/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Group/Create

        [HttpPost]
        public ActionResult Create(CreateGroupModel creategroupmodel)
        {
            if (ModelState.IsValid)
            {
                db.Group.Add(creategroupmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(creategroupmodel);
        }

        //
        // GET: /Group/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CreateGroupModel creategroupmodel = db.Group.Find(id);
            if (creategroupmodel == null)
            {
                return HttpNotFound();
            }
            return View(creategroupmodel);
        }

        //
        // POST: /Group/Edit/5

        [HttpPost]
        public ActionResult Edit(CreateGroupModel creategroupmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(creategroupmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(creategroupmodel);
        }

        //
        // GET: /Group/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CreateGroupModel creategroupmodel = db.Group.Find(id);
            if (creategroupmodel == null)
            {
                return HttpNotFound();
            }
            return View(creategroupmodel);
        }

        //
        // POST: /Group/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CreateGroupModel creategroupmodel = db.Group.Find(id);
            db.Group.Remove(creategroupmodel);
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