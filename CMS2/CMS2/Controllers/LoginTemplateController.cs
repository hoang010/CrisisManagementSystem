using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS2.Controllers
{
    public class LoginTemplateController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authenticate(string button)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}