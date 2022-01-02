using Riddhasoft.Setup.Entity.Package;
using Riddhasoft.Setup.Services.Package;
using Riddhasoft.Setup.Services.QuestionPackage;
using RTech.Demo.Areas.MockTest.Services;
using RTech.Demo.Areas.MockTest.ViewModels;
using RTech.Demo.Areas.Student.Services;
using RTech.Demo.Areas.Student.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTech.Demo.Areas.Student.Controllers
{
    public class TopBarController : Controller
    {

        MockTestHomeServices _mockTestHomeServices = null;
        SNaatiPackage _naatiPackageServices = null;
        SNaatiMockTest _naatiMocktestServices = null;

        public TopBarController()
        {
            _mockTestHomeServices = new MockTestHomeServices();
            _naatiPackageServices = new SNaatiPackage();
            _naatiMocktestServices = new SNaatiMockTest();
        }


        // GET: Student/TopBar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            HomeViewModel vm = new HomeViewModel();
            vm.Scorers = new List<ScorerViewModel>();
            vm.Blogs = new List<BlogViewModel>();
            vm.Blogs = new List<BlogViewModel>();
            vm.Testimonials = new List<TestimonialViewModel>();

            vm.Packages = (from c in _naatiMocktestServices.List().Data.ToList()
                                                 select new NaatiMocktestPackageVm()
                                                 {
                                                     Code = c.Code,
                                                     Description = c.Description,
                                                     Duration = c.Duration,
                                                     Id = c.Id,
                                                     Price = c.Price,
                                                     Title = c.Title,

                                                 }).ToList();
            return View(vm);
        }

        public ActionResult AboutUs()
        {
            return View();
        }


        public ActionResult NaatiLanguages()
        {
            return View();
        }

        public ActionResult Testimonials()
        {
            List<TestimonialViewModel> list = new List<TestimonialViewModel>();

            return View(list);
        }

        public ActionResult ScorerList()
        {
            List<ScorerViewModel> list = new List<ScorerViewModel>();
            return View(list);
        }

        public ActionResult AllPAckages(string lang)
        {

            List<ENaatiPackage> packages = _naatiPackageServices.List().Data.ToList();

            if (!string.IsNullOrEmpty(lang))
            {
                packages = packages.Where(x => x.LanguageType.Code.Trim().ToLower() == lang.Trim().ToLower()).ToList();
            }

            List<NaatiPackageVm> vm = (from c in packages
                                       select new NaatiPackageVm()
                                       {
                                           Code = c.Code,
                                           Description = c.Description,
                                           Duration = c.Duration,
                                           Id = c.Id,
                                           PackageType = c.PackageType,
                                           PackageTypeName = Enum.GetName(typeof(PackageType), c.PackageType),
                                           Price = c.Price,
                                           Title = c.Title,
                                           MocktestCount = _naatiPackageServices.ListPackageMockTest().Data.Where(x => x.NaatiPackageId == c.Id).Count()
                                       }).ToList();
            return View(vm);
        }

        public ActionResult AllMockTests()
        {
            List<NaatiMocktestPackageVm> list = (from c in _naatiMocktestServices.List().Data.ToList()
                                                 select new NaatiMocktestPackageVm()
                                                 {
                                                     Code = c.Code,
                                                     Description = c.Description,
                                                     Duration = c.Duration,
                                                     Id = c.Id,
                                                     Price = c.Price,
                                                     Title = c.Title,

                                                 }).ToList();
            return View(list);
        }

    }
}