using CMS2.Data_Access_Layer;
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
        //Data Access Layer
        private UserRepository userRepository= new UserRepository();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Authorize(CMS2.Models.User userModel)
        {
            User userDetails = userRepository.getUserByName(userModel.UserName);
            
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