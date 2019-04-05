using CMS2.Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using CMS2.Helpers;

namespace CMS2.Controllers
{
    public class HomeController : Controller
    {
        private LoginHelper loginHelper = new LoginHelper();
        private List<int> roleRequired = new List<int>(new int[] { 1, 2, 3, 4, 5});

        public ActionResult Index()
        {
            if (Session["userRole"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (!loginHelper.isAuthorized(Convert.ToInt32(Session["userRole"]), roleRequired))
            {
                return RedirectToAction("NotAuthorized", "Error");
            }
            CrisisRepository crisisRepository = new CrisisRepository();
            if (Convert.ToInt32(Session["userRole"]) == 4)
            {
                ViewBag.Crises = JsonConvert.SerializeObject(crisisRepository.getCrisesByRoles(4));
            }
            else if (Convert.ToInt32(Session["userRole"]) == 5)
            {
                ViewBag.Crises = JsonConvert.SerializeObject(crisisRepository.getCrisesByRoles(5));
            }
            else
            {
                ViewBag.Crises = JsonConvert.SerializeObject(crisisRepository.getLastestCrisis());
            }
 
            return View();
        }
    }
}