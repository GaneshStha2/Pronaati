using RTech.Demo.Areas.Student.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTech.Demo.Areas.Student.Controllers
{
    public class BlogController : Controller
    {
        // GET: Student/Blog
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Blog()
        {
           List<BlogViewModel>  list = new List<BlogViewModel>();
            return View(list);
        }

        public ActionResult BloogDetail(int id)
        {
            return View();
        }


    }
}