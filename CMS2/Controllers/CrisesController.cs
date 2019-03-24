using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS2.Data_Access_Layer;
using CMS2.Models;

namespace CMS2.Controllers
{
    public class CrisesController : Controller
    {
        private CMS2Context db = new CMS2Context();
        public CrisisRepository CrisisRepository = new CrisisRepository();

        // GET: Crises
        public ActionResult Index()
        {
            //old code
            //var crises = db.Crises.Include(c => c.AssistanceRequired).Include(c => c.Category).Include(c => c.Emergency);

            //with data access layer
            var crisis = CrisisRepository.getAllCrises();
            return View(crisis);
        }

        // GET: Crises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crisis crisis = CrisisRepository.getCrisisById(id);
            if (crisis == null)
            {
                return HttpNotFound();
            }
            return View(crisis);
        }

        // GET: Crises/Create
        public ActionResult Create()
        {
            //pass in data to view the assistance, categories, emergencies
            ViewBag.AssistanceRequiredId = new SelectList(CrisisRepository.GetAssistanceRequired(), "Id", "Assistance");
            ViewBag.CategoryId = new SelectList(CrisisRepository.GetCategories(), "Id", "Description");
            ViewBag.EmergencyId = new SelectList(CrisisRepository.GetEmergencies(), "Id", "Level");

            return View();
        }

        // POST: Crises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CallerName,CallerNumber,Location,Description,EmergencyId,AssistanceRequiredId,CategoryId")] Crisis crisis)
        {
            if (ModelState.IsValid)
            {
                db.Crises.Add(crisis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //generated code
            //ViewBag.AssistanceRequiredId = new SelectList(db.AssistanceRequireds, "Id", "Assistance", crisis.AssistanceRequiredId);
            //ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Description", crisis.CategoryId);
            //ViewBag.EmergencyId = new SelectList(db.Emergencies, "Id", "Level", crisis.EmergencyId);

            //new code
            ViewBag.AssistanceRequiredId = new SelectList(CrisisRepository.GetAssistanceRequired(), "Id", "Assistance", crisis.AssistanceRequiredId);
            ViewBag.CategoryId = new SelectList(CrisisRepository.GetCategories(), "Id", "Description", crisis.CategoryId);
            ViewBag.EmergencyId = new SelectList(CrisisRepository.GetEmergencies(), "Id", "Level", crisis.EmergencyId);

            return View(crisis);
        }

        // GET: Crises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Crisis crisis = db.Crises.Find(id);
            Crisis crisis = CrisisRepository.getCrisisById(id);
            if (crisis == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssistanceRequiredId = new SelectList(db.AssistanceRequireds, "Id", "Assistance", crisis.AssistanceRequiredId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Description", crisis.CategoryId);
            ViewBag.EmergencyId = new SelectList(db.Emergencies, "Id", "Level", crisis.EmergencyId);
            return View(crisis);
        }

        // POST: Crises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CallerName,CallerNumber,Location,Description,EmergencyId,AssistanceRequiredId,CategoryId")] Crisis crisis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crisis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssistanceRequiredId = new SelectList(db.AssistanceRequireds, "Id", "Assistance", crisis.AssistanceRequiredId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Description", crisis.CategoryId);
            ViewBag.EmergencyId = new SelectList(db.Emergencies, "Id", "Level", crisis.EmergencyId);
            return View(crisis);
        }

        // GET: Crises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Crisis crisis = db.Crises.Find(id);
            Crisis crisis = CrisisRepository.getCrisisById(id);
            if (crisis == null)
            {
                return HttpNotFound();
            }
            return View(crisis);
        }

        // POST: Crises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Crisis crisis = db.Crises.Find(id);
            Crisis crisis = CrisisRepository.getCrisisById(id);
            db.Crises.Remove(crisis);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //to be removed after testing
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
