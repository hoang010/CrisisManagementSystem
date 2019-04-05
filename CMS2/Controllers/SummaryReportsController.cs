using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS2.Data_Access_Layer;
using CMS2.Helpers;
using CMS2.Models;
using CMS2.ReportAndSocialMedia_Module;
using Hangfire;

namespace CMS2.Controllers
{
    public class SummaryReportsController : Controller
    {
        private CMS2Context db = new CMS2Context();
        private SummaryReportRepository summaryReportRepository = new SummaryReportRepository();
        private LoginHelper loginHelper = new LoginHelper();
        private List<int> roleRequired = new List<int>(new int[] { 1, 2 }); //admin and crisis manager

        // GET: SummaryReports
        public ActionResult Index()
        {
            if (!loginHelper.isAuthorized(Convert.ToInt32(Session["userRole"]), roleRequired))
            {
                return RedirectToAction("NotAuthorized", "Error");
            }

            return View(summaryReportRepository.getAllReports());
        }

        // GET: SummaryReports/Details/5
        public ActionResult Details(int? id)
        {
            if (!loginHelper.isAuthorized(Convert.ToInt32(Session["userRole"]), roleRequired))
            {
                return RedirectToAction("NotAuthorized", "Error");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SummaryReport summaryReport = summaryReportRepository.getReportById(id);
            if (summaryReport == null)
            {
                return HttpNotFound();
            }
            return View(summaryReport);
        }


        // GET: SummaryReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!loginHelper.isAuthorized(Convert.ToInt32(Session["userRole"]), roleRequired))
            {
                return RedirectToAction("NotAuthorized", "Error");
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
            if (!loginHelper.isAuthorized(Convert.ToInt32(Session["userRole"]), roleRequired))
            {
                return RedirectToAction("NotAuthorized", "Error");
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
