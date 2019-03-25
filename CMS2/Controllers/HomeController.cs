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
            //if (Session["UserId"] == null)
            //{
            //    return Redirect("/account/login");
            //}
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}