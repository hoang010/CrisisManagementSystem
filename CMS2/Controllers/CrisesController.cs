using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using CMS2.Data_Access_Layer;
using CMS2.Helpers;
using CMS2.Models;
using CMS2.ReportAndSocialMedia_Module;
using Hangfire;

namespace CMS2.Controllers
{
    public class CrisesController : Controller
    {
        private LoginHelper loginHelper = new LoginHelper();

        //all data access elements
        private CrisisRepository CrisisRepository = new CrisisRepository();
        private AssistanceRequiredRepository assistanceRequiredRepository = new AssistanceRequiredRepository();
        private CategoriesRepository categoriesRepository = new CategoriesRepository();
        private EmergencyRepository emergencyRepository = new EmergencyRepository();

        private List<int> roleRequired = new List<int>(new int[] {1, 2});

        // GET: Crises
        public ActionResult Index()
        {
            if (!loginHelper.isAuthorized(Convert.ToInt32(Session["userRole"]), roleRequired))
            {
                return Redirect("/error/notauthorized");
            }

            var crisis = CrisisRepository.getAllCrises();
            return View(crisis);
        }

        // GET: Crises/Details/5
        public ActionResult Details(int? id)
        {
            if (!loginHelper.isAuthorized(Convert.ToInt32(Session["userRole"]), roleRequired))
            {
                return Redirect("/error/notauthorized");
            }
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
            if (!loginHelper.isAuthorized(Convert.ToInt32(Session["userRole"]), roleRequired))
            {
                //allow this action if the user is a call center operator
                if (!(Convert.ToInt32(Session["userRole"]) == 3)) {
                    return Redirect("/error/notauthorized");
                }
            }
            //pass in data to view the assistance, categories, emergencies
            ViewBag.AssistanceRequiredId = new SelectList(assistanceRequiredRepository.getAssistanceRequired(), "Id", "Assistance");
            ViewBag.CategoryId = new SelectList(categoriesRepository.getCategories(), "Id", "Description");
            ViewBag.EmergencyId = new SelectList(emergencyRepository.getEmergencies(), "Id", "Level");

            return View();
        }

        // POST: Crises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CallerName,CallerNumber,Location,Description,EmergencyId,AssistanceRequiredId,CategoryId")] Crisis crisis)
        {
            if (!loginHelper.isAuthorized(Convert.ToInt32(Session["userRole"]), roleRequired))
            {
                return Redirect("/error/notauthorized");
            }
            if (ModelState.IsValid)
            {
                crisis.TimeStamp = DateTime.Now;
                CrisisRepository.addCrisis(crisis);
                if (crisis.EmergencyId== 3)
                {
                    crisis.Category = categoriesRepository.getCategoryById(crisis.CategoryId);
                    crisis.AssistanceRequired = assistanceRequiredRepository.getAssistanceRequiredById(crisis.AssistanceRequiredId);
                    crisis.Emergency = emergencyRepository.getEmergencyById(crisis.EmergencyId);
                    Console.WriteLine("Level 3 Report detected!");
                    ReportJobs reportJobs = new ReportJobs();
                    //add back ground job to send crisis for approval
                    BackgroundJob.Enqueue(() => reportJobs.sendCrisis(crisis));
                }

                return RedirectToAction("Index");
            }

            //new code
            ViewBag.AssistanceRequiredId = new SelectList(assistanceRequiredRepository.getAssistanceRequired(), "Id", "Assistance", crisis.AssistanceRequiredId);
            ViewBag.CategoryId = new SelectList(categoriesRepository.getCategories(), "Id", "Description", crisis.CategoryId);
            ViewBag.EmergencyId = new SelectList(emergencyRepository.getEmergencies(), "Id", "Level", crisis.EmergencyId);

            return View(crisis);
        }

        // GET: Crises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!loginHelper.isAuthorized(Convert.ToInt32(Session["userRole"]), roleRequired))
            {
                return Redirect("/error/notauthorized");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crisis crisis = CrisisRepository.getCrisisById(id);
            if (crisis == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssistanceRequiredId = new SelectList(assistanceRequiredRepository.getAssistanceRequired(), "Id", "Assistance", crisis.AssistanceRequiredId);
            ViewBag.CategoryId = new SelectList(categoriesRepository.getCategories(), "Id", "Description", crisis.CategoryId);
            ViewBag.EmergencyId = new SelectList(emergencyRepository.getEmergencies(), "Id", "Level", crisis.EmergencyId);
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
                CrisisRepository.editCrisis(crisis);
                return RedirectToAction("Index");
            }
            ViewBag.AssistanceRequiredId = new SelectList(assistanceRequiredRepository.getAssistanceRequired(), "Id", "Assistance", crisis.AssistanceRequiredId);
            ViewBag.CategoryId = new SelectList(categoriesRepository.getCategories(), "Id", "Description", crisis.CategoryId);
            ViewBag.EmergencyId = new SelectList(emergencyRepository.getEmergencies(), "Id", "Level", crisis.EmergencyId);
            return View(crisis);
        }

        // GET: Crises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!loginHelper.isAuthorized(Convert.ToInt32(Session["userRole"]), roleRequired))
            {
                return Redirect("/error/notauthorized");
            }
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

        // POST: Crises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Crisis crisis = CrisisRepository.removeCrisis(id);

            return RedirectToAction("Index");
        }
    }
}
