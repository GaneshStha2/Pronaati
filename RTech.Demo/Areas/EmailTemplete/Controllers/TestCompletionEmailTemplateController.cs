using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTech.Demo.Areas.EmailTemplete.Controllers
{
    public class TestCompletionEmailTemplateController : Controller
    {
        // GET: EmailTemplete/TestCompletionEmailTemplate
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ScoreAvailable()
        {
            return View();
        }
    }
}