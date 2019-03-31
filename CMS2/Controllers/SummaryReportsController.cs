using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS2.Models;
using CMS2.ReportAndSocialMedia_Module;
using Hangfire;

namespace CMS2.Controllers
{
    public class SummaryReportsController : Controller
    {
        private CMS2Context db = new CMS2Context();

        // GET: SummaryReports
        public ActionResult Index()
        {
            if (Session["userId"] == null)
            {
                return Redirect("/login/index");
            }
            return View(db.SummaryReports.ToList());
        }

        // GET: SummaryReports/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["userId"] == null)
            {
                return Redirect("/login/index");
            }
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


        // GET: SummaryReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["userId"] == null)
            {
                return Redirect("/login/index");
            }
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
        public ActionResult Edit([Bind(Include = "Id,ReportDetails,Approved")] SummaryReport summaryReport)
        {
            if (Session["userId"] == null)
            {
                return Redirect("/login/index");
            }
            summaryReport.TimeStamp = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (summaryReport.Approved == true)
                {
                    ReportJobs reportJobs = new ReportJobs();
                    BackgroundJob.Enqueue(() => reportJobs.sendReport(summaryReport));
                }
                db.Entry(summaryReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(summaryReport);
        }

        // GET: SummaryReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["userId"] == null)
            {
                return Redirect("/login/index");
            }
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
            if (Session["userId"] == null)
            {
                return Redirect("/login/index");
            }
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
