using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS2.Data_Access_Layer;
using CMS2.Models;
using CMS2.ReportAndSocialMedia_Module;
using Newtonsoft.Json;

namespace CMS2.Controllers
{
    public class SocialMediaUpdatesController : Controller
    {
        private CMS2Context db = new CMS2Context();
        private SocialMediaUpdatesRepository SocialMediaUpdatesRepository = new SocialMediaUpdatesRepository();

        // GET: SocialMediaUpdates
        public ActionResult Index()
        {
            if (Session["userId"] == null)
            {
                return Redirect("/login/index");
            }
            //var socialMediaUpdates = db.SocialMediaUpdates.Include(s => s.SocialMediaType);
            var socialMediaUpdates = SocialMediaUpdatesRepository.getAllUpdates();
            return View(socialMediaUpdates);
        }

        // GET: SocialMediaUpdates/Details/5
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
            var socialMediaUpdates = SocialMediaUpdatesRepository.getUpdateById(id);
            //SocialMediaUpdates socialMediaUpdates = db.SocialMediaUpdates.Find(id);
            if (socialMediaUpdates == null)
            {
                return HttpNotFound();
            }
            return View(socialMediaUpdates);
        }

        // GET: SocialMediaUpdates/Create
        public ActionResult Create()
        {
            if (Session["userId"] == null)
            {
                return Redirect("/login/index");
            }
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
                socialMediaUpdates.TimeStamp = DateTime.Now;
                db.SocialMediaUpdates.Add(socialMediaUpdates);
                db.SaveChanges();
                SocialMediaJobs socialMediaJobs = new SocialMediaJobs();
                socialMediaJobs.tweetMessage(socialMediaUpdates);
                return RedirectToAction("Index");
            }

            ViewBag.SocialMediaTypeId = new SelectList(db.SocialMediaTypes, "Id", "Name", socialMediaUpdates.SocialMediaTypeId);

            return View(socialMediaUpdates);
        }
    }
}
