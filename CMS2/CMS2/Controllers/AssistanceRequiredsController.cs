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
    public class AssistanceRequiredsController : Controller
    {
        private CMS2Context db = new CMS2Context();

        // GET: AssistanceRequireds
        public ActionResult Index()
        {
            return View(db.AssistanceRequireds.ToList());
        }

        // GET: AssistanceRequireds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssistanceRequired assistanceRequired = db.AssistanceRequireds.Find(id);
            if (assistanceRequired == null)
            {
                return HttpNotFound();
            }
            return View(assistanceRequired);
        }

        // GET: AssistanceRequireds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AssistanceRequireds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Assistance")] AssistanceRequired assistanceRequired)
        {
            if (ModelState.IsValid)
            {
                db.AssistanceRequireds.Add(assistanceRequired);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assistanceRequired);
        }

        // GET: AssistanceRequireds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssistanceRequired assistanceRequired = db.AssistanceRequireds.Find(id);
            if (assistanceRequired == null)
            {
                return HttpNotFound();
            }
            return View(assistanceRequired);
        }

        // POST: AssistanceRequireds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Assistance")] AssistanceRequired assistanceRequired)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assistanceRequired).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assistanceRequired);
        }

        // GET: AssistanceRequireds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssistanceRequired assistanceRequired = db.AssistanceRequireds.Find(id);
            if (assistanceRequired == null)
            {
                return HttpNotFound();
            }
            return View(assistanceRequired);
        }

        // POST: AssistanceRequireds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssistanceRequired assistanceRequired = db.AssistanceRequireds.Find(id);
            db.AssistanceRequireds.Remove(assistanceRequired);
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
