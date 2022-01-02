using RTech.Demo.Areas.Student.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTech.Demo.Areas.Student.Controllers
{
    public class ContactUsController : Controller
    {
        Common.CommonServices _commonServices = null;
        public ContactUsController()
        {
            _commonServices = new Common.CommonServices();
        }

        // GET: Student/ContactUs
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            SetViewBagForCountries();
            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(ContactUsViewModel vm)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ContactUs", "ContactUs", new { @Area = "Student" });
            }
            return View(vm);

        }


        private void SetViewBagForCountries()
        {
            var countries = _commonServices.getCountryDropDown();
            ViewBag.Countries = new SelectList(countries, "CountryCode", "Country");
        }
    }
}