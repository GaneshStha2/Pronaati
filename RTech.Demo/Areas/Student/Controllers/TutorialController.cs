using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTech.Demo.Areas.Student.Controllers
{
    public class TutorialController : Controller
    {
        // GET: Student/Tutorial
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VideoTutorial()
        {
            return View();
        }

        public ActionResult PDFTutorial()
        {
            return View();
        }

    }
}