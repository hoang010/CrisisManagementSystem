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
    public class SocialMediaTypesController : Controller
    {
        private CMS2Context db = new CMS2Context();

        // GET: SocialMediaTypes
        public ActionResult Index()
        {
            return View(db.SocialMediaTypes.ToList());
        }

        // GET: SocialMediaTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMediaType socialMediaType = db.SocialMediaTypes.Find(id);
            if (socialMediaType == null)
            {
                return HttpNotFound();
            }
            return View(socialMediaType);
        }

        // GET: SocialMediaTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SocialMediaTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] SocialMediaType socialMediaType)
        {
            if (ModelState.IsValid)
            {
                db.SocialMediaTypes.Add(socialMediaType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(socialMediaType);
        }

        // GET: SocialMediaTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMediaType socialMediaType = db.SocialMediaTypes.Find(id);
            if (socialMediaType == null)
            {
                return HttpNotFound();
            }
            return View(socialMediaType);
        }

        // POST: SocialMediaTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] SocialMediaType socialMediaType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(socialMediaType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(socialMediaType);
        }

        // GET: SocialMediaTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMediaType socialMediaType = db.SocialMediaTypes.Find(id);
            if (socialMediaType == null)
            {
                return HttpNotFound();
            }
            return View(socialMediaType);
        }

        // POST: SocialMediaTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SocialMediaType socialMediaType = db.SocialMediaTypes.Find(id);
            db.SocialMediaTypes.Remove(socialMediaType);
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
