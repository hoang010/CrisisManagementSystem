using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS2.Models;

namespace CMS2.Controllers
{
    public class LevelOfEmergenciesController : Controller
    {
        private CMS2Context db = new CMS2Context();

        // GET: LevelOfEmergencies
        public ActionResult Index()
        {
            return View(db.LevelOfEmergencies.ToList());
        }

        // GET: LevelOfEmergencies/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LevelOfEmergency levelOfEmergency = db.LevelOfEmergencies.Find(id);
            if (levelOfEmergency == null)
            {
                return HttpNotFound();
            }
            return View(levelOfEmergency);
        }

        // GET: LevelOfEmergencies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LevelOfEmergencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Level")] LevelOfEmergency levelOfEmergency)
        {
            if (ModelState.IsValid)
            {
                db.LevelOfEmergencies.Add(levelOfEmergency);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(levelOfEmergency);
        }

        // GET: LevelOfEmergencies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LevelOfEmergency levelOfEmergency = db.LevelOfEmergencies.Find(id);
            if (levelOfEmergency == null)
            {
                return HttpNotFound();
            }
            return View(levelOfEmergency);
        }

        // POST: LevelOfEmergencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Level")] LevelOfEmergency levelOfEmergency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(levelOfEmergency).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(levelOfEmergency);
        }

        // GET: LevelOfEmergencies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LevelOfEmergency levelOfEmergency = db.LevelOfEmergencies.Find(id);
            if (levelOfEmergency == null)
            {
                return HttpNotFound();
            }
            return View(levelOfEmergency);
        }

        // POST: LevelOfEmergencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            LevelOfEmergency levelOfEmergency = db.LevelOfEmergencies.Find(id);
            db.LevelOfEmergencies.Remove(levelOfEmergency);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
