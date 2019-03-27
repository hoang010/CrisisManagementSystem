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
    public class SummaryReportsController : Controller
    {
        private CMS2Context db = new CMS2Context();

        // GET: SummaryReports
        public ActionResult Index()
        {
            return View(db.SummaryReports.ToList());
        }

        // GET: SummaryReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SummaryReport summaryReport = db.SummaryReports.Find(id);
            if (summaryReport == null)
            {
                return HttpNotFound();
            }
            return View(summaryReport);
        }

        // GET: SummaryReports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SummaryReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ReportDetails")] SummaryReport summaryReport)
        {
            if (ModelState.IsValid)
            {
                db.SummaryReports.Add(summaryReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(summaryReport);
        }

        // GET: SummaryReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SummaryReport summaryReport = db.SummaryReports.Find(id);
            if (summaryReport == null)
            {
                return HttpNotFound();
            }
            return View(summaryReport);
        }

        // POST: SummaryReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ReportDetails")] SummaryReport summaryReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(summaryReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(summaryReport);
        }

        // GET: SummaryReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SummaryReport summaryReport = db.SummaryReports.Find(id);
            if (summaryReport == null)
            {
                return HttpNotFound();
            }
            return View(summaryReport);
        }

        // POST: SummaryReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SummaryReport summaryReport = db.SummaryReports.Find(id);
            db.SummaryReports.Remove(summaryReport);
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
