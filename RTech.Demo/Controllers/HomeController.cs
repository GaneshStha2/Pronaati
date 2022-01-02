using Riddhasoft.Services.User;
using Riddhasoft.User.Entity;
using RTech.Demo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RTech.Demo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (RiddhaSession.CurrentToken != null)
            {
                int userType = RiddhaSession.UserType;
                if (userType == 0)
                {
                    return View("_OwnerHome");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("login", "User", new { area = "User" });
            }
        }
        public ActionResult InitialPage()
        {
            return View("_landingpage");
        }
        public ActionResult NoPermission()
        {
            return View("_NoPermissionPage");
        }

        public ActionResult PTEIndex()
        {

            return View();
        }

        public ActionResult Student()
        {

            return RedirectToAction("Index", "LogIn", new { area = "Student" });
        }
    }
}
