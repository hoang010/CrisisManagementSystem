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
        private List<int> roleRequired = new List<int>(new int[] { 1, 2, 3});

        public ActionResult Index()
        {
            if (!loginHelper.isAuthorized(Convert.ToInt32(Session["userRole"]), roleRequired))
            {
                return Redirect("/error/notauthorized");
            }
            CrisisRepository crisisRepository = new CrisisRepository();
            ViewBag.Crises = JsonConvert.SerializeObject(crisisRepository.getAllCrises());
            return View();
        }
    }
}