using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTech.Demo.Areas.Student.Controllers
{
    public class PackageController : Controller
    {

    
        // GET: Student/Package
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult PackageList()
        {
            return View();
        }

        public ActionResult PackageDetails(int id)
        {
          
            return View();
        }
    }
}