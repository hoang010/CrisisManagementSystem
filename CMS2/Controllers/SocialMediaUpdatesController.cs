﻿using System;
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

                //api usage
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.1.108:5000/tweet");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"tweet\":" + "\""+ socialMediaUpdates.Description.ToString()+"\"}";
                    System.Diagnostics.Debug.WriteLine(json);
                    System.Diagnostics.Debug.WriteLine("===============================================================");
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }

                return RedirectToAction("Index");
            }

            ViewBag.SocialMediaTypeId = new SelectList(db.SocialMediaTypes, "Id", "Name", socialMediaUpdates.SocialMediaTypeId);

            return View(socialMediaUpdates);
        }

        //should not be able to update it
        // GET: SocialMediaUpdates/Edit/5
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
        //should not be able to delete also
        // GET: SocialMediaUpdates/Delete/5
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
            if (Session["userId"] == null)
            {
                return Redirect("/login/index");
            }
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
