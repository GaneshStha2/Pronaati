using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTech.Demo.Areas.EmailTemplete.Controllers
{
    public class StudentSignUpEmailTempleteController : Controller
    {
        // GET: EmailTemplete/StudentSignUpEmailTemplete
        public ActionResult Index( string NewSignedUpEmail,string link)
        {
            ViewBag.NewSignedUpEmail = NewSignedUpEmail ;
            ViewBag.Link = link;
            return View();
        }
    }
}