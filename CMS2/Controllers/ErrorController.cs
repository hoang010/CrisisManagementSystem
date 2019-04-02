using CMS2.Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS2.Controllers
{
    public class ErrorController : Controller
    {
        private UserRoleRepository userRoleRepository = new UserRoleRepository();

        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult NotAuthorized()
        {
            ViewBag.Roles = userRoleRepository.getAllRoles();
            return View();
        }
    }
}