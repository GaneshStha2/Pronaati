using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTech.Demo.Areas.Student.Controllers
{
    public class EssayController : Controller
    {
        // GET: Student/Essay
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WriteAnEssay()
        {
            return View();
        }
    }
}