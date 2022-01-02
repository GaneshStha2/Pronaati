using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTech.Demo.Controllers
{
    public class PageNotFoundController : Controller
    {
        // GET: PageNotFound
        public ActionResult Index(string message="")
        {
            if (!string.IsNullOrEmpty(message) )
            {
                ViewBag.Message = message;
            }
            return View();
        }
    }
}