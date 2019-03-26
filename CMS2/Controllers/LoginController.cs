using CMS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS2.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Authorize(CMS2.Models.User userModel)
        {
            CMS2Context db = new CMS2Context();
            var users = db.Users.ToList();
            User userDetails = null;
            foreach (User item in users){
                if (item.UserName == userModel.UserName)
                {
                    userDetails = item;
                }
            }
            if (userDetails == null || userModel.Password != userDetails.Password)
            {
                userModel.LoginError = "Wrong username or password";
                return View("Index", userModel);
            }
            else
            {
                Session["userId"] = userDetails.Id;
                Session["userRole"] = userDetails.RoleId;
            }

            return RedirectToAction("Index", "Home");
        }
    }
}