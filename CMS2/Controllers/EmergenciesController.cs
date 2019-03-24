﻿using System;
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
    public class EmergenciesController : Controller
    {
        private CMS2Context db = new CMS2Context();

        // GET: Emergencies
        public ActionResult Index()
        {
            return View(db.Emergencies.ToList());
        }

        // GET: Emergencies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emergency emergency = db.Emergencies.Find(id);
            if (emergency == null)
            {
                return HttpNotFound();
            }
            return View(emergency);
        }

        // GET: Emergencies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Emergencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Level")] Emergency emergency)
        {
            if (ModelState.IsValid)
            {
                db.Emergencies.Add(emergency);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emergency);
        }

        // GET: Emergencies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emergency emergency = db.Emergencies.Find(id);
            if (emergency == null)
            {
                return HttpNotFound();
            }
            return View(emergency);
        }

        // POST: Emergencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Level")] Emergency emergency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emergency).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emergency);
        }

        // GET: Emergencies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emergency emergency = db.Emergencies.Find(id);
            if (emergency == null)
            {
                return HttpNotFound();
            }
            return View(emergency);
        }

        // POST: Emergencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Emergency emergency = db.Emergencies.Find(id);
            db.Emergencies.Remove(emergency);
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