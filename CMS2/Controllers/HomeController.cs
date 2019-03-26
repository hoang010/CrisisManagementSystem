using CMS2.Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["userId"] == null)
            {
                return Redirect("/login/index");
            }
            CrisisRepository crisisRepository = new CrisisRepository();
            ViewBag.Crises = crisisRepository.getAllCrises();
            return View();
        }
    }
}