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
    public class SocialMediaUpdatesController : Controller
    {
        private CMS2Context db = new CMS2Context();

        // GET: SocialMediaUpdates
        public ActionResult Index()
        {
            var socialMediaUpdates = db.SocialMediaUpdates.Include(s => s.SocialMediaType);
            return View(socialMediaUpdates.ToList());
        }

        // GET: SocialMediaUpdates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMediaUpdates socialMediaUpdates = db.SocialMediaUpdates.Find(id);
            if (socialMediaUpdates == null)
            {
                return HttpNotFound();
            }
            return View(socialMediaUpdates);
        }

        // GET: SocialMediaUpdates/Create
        public ActionResult Create()
        {
            ViewBag.SocialMediaTypeId = new SelectList(db.SocialMediaTypes, "Id", "Name");
            return View();
        }

        // POST: SocialMediaUpdates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,SocialMediaTypeId")] SocialMediaUpdates socialMediaUpdates)
        {
            if (ModelState.IsValid)
            {
                db.SocialMediaUpdates.Add(socialMediaUpdates);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SocialMediaTypeId = new SelectList(db.SocialMediaTypes, "Id", "Name", socialMediaUpdates.SocialMediaTypeId);
            return View(socialMediaUpdates);
        }

        // GET: SocialMediaUpdates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMediaUpdates socialMediaUpdates = db.SocialMediaUpdates.Find(id);
            if (socialMediaUpdates == null)
            {
                return HttpNotFound();
            }
            ViewBag.SocialMediaTypeId = new SelectList(db.SocialMediaTypes, "Id", "Name", socialMediaUpdates.SocialMediaTypeId);
            return View(socialMediaUpdates);
        }

        // POST: SocialMediaUpdates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,SocialMediaTypeId")] SocialMediaUpdates socialMediaUpdates)
        {
            if (ModelState.IsValid)
            {
                db.Entry(socialMediaUpdates).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SocialMediaTypeId = new SelectList(db.SocialMediaTypes, "Id", "Name", socialMediaUpdates.SocialMediaTypeId);
            return View(socialMediaUpdates);
        }

        // GET: SocialMediaUpdates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMediaUpdates socialMediaUpdates = db.SocialMediaUpdates.Find(id);
            if (socialMediaUpdates == null)
            {
                return HttpNotFound();
            }
            return View(socialMediaUpdates);
        }

        // POST: SocialMediaUpdates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SocialMediaUpdates socialMediaUpdates = db.SocialMediaUpdates.Find(id);
            db.SocialMediaUpdates.Remove(socialMediaUpdates);
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
