using Riddhasoft.MockTest.Entity;
using Riddhasoft.MockTest.Services;
using Riddhasoft.Setup.Entity;
using Riddhasoft.Setup.Entity.Package;
using Riddhasoft.Setup.Services;
using Riddhasoft.Setup.Services.Package;
using RTech.Demo.Areas.Student.Services;
using RTech.Demo.Areas.Student.ViewModels;
using RTech.Demo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTech.Demo.Areas.Student.Controllers
{
    public class CourseController : Controller
    {
        ClassRoomCourseServices _courseServices = null;
        OnlineTrainingServices _onlineTrainigServices = null;
        CommonServices _commonServices = null;
        SPackagePaymentDetails _packagePaymentDetailServices = null;

        SNaatiPackage _naatiPackageServices = null;

        public CourseController()
        {
            _courseServices = new ClassRoomCourseServices();
            _onlineTrainigServices = new OnlineTrainingServices();
            _commonServices = new CommonServices();

            _naatiPackageServices = new SNaatiPackage();
            _packagePaymentDetailServices = new SPackagePaymentDetails();
        }

        // GET: Student/Course
        public ActionResult Index()
        {
            return RedirectToAction("CourseList");
        }

        public ActionResult CourseList()
        {
            var vm = _courseServices.GetOnlineCourseList();
            return View(vm);
        }

        //Online Course
        public ActionResult CourseDetails(int id)
        {

            var vm = _courseServices.GetOnlineCourseList().Where(x => x.CourseId == id).FirstOrDefault();
            return View(vm);
        }

        public ActionResult MyClassRoomCourseList()
        {
            if (Common.StudentSession.LoggedStudent == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var vm = _courseServices.GetCourseList();
            return View(vm);
        }

        public ActionResult MyNaatiPackageList()
        {
            if (Common.StudentSession.LoggedStudent == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var vm = (from c in _naatiPackageServices.List().Data.ToList()
                      join d in _packagePaymentDetailServices.List().Data.Where(x => x.PaymentFor == PaymentFor.NaatiPackage && x.UserId == Common.StudentSession.LoggedStudent.StudentId && x.ExpiryDate >= DateTime.Now) on c.Code.ToLower() equals d.CourseCode.ToLower()
                      select new ClassRoomCourseMasterViewModel()
                      {
                          CourseId = c.Id,
                          CourseCode = c.Code,
                          Amount = c.Price,
                          CourseName = c.Title,
                          ImageUrl = c.ImageURL,
                          Description = c.Description,
                          CreatedDate = c.CreatedDate.ToString("dd-MM-yyyy")
                      }).ToList();


            return View(vm);
        }

        public ActionResult MyNaatiPackageDetails(int id)
        {
            if (Common.StudentSession.LoggedStudent == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Common.NaatiMockTestSession.PackageId = id;
            string code = _commonServices.getCodeFromId(id, PaymentFor.NaatiPackage);
            bool courseBought = _commonServices.checkIfCourseAlreadyBought(code, PaymentFor.NaatiPackage);
            if (courseBought == false)
            {
                return RedirectToAction("Index", "PageNotFound", new { Area = "" });
            }
            var vm = (from c in _naatiPackageServices.List().Data.Where(x => x.Id == id)
                      select new NaatiPackageVm()
                      {
                          Code = c.Code,
                          Description = c.Description,
                          Duration = c.Duration,
                          Id = c.Id,
                          Price = c.Price,
                          Title = c.Title
                      }).FirstOrDefault();
            Common.StudentSession.CourseId = id;
            return View(vm);
        }

        public ActionResult MyClassRoomCourseDetails(int id)
        {
            if (Common.StudentSession.LoggedStudent == null)
            {
                return RedirectToAction("Index", "Login");
            }

            string code = _commonServices.getCodeFromId(id, Riddhasoft.Setup.Entity.PaymentFor.OnlineCourse);
            bool courseBought = _commonServices.checkIfCourseAlreadyBought(code, Riddhasoft.Setup.Entity.PaymentFor.OnlineCourse);
            if (courseBought == false)
            {
                return RedirectToAction("Index", "PageNotFound", new { Area = "" });
            }
            var vm = _courseServices.GetClassRoomCourseDetails(id);
            Common.StudentSession.CourseId = id;
            return View(vm);
        }

        public ActionResult VocabularyAndPronounciation(int courseId)
        {
            if (Common.StudentSession.LoggedStudent == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.CourseId = courseId;
            var vm = _courseServices.GetVocabDetails(courseId);
            return View(vm);
        }

        public ActionResult SynonymBooster(int courseId)
        {
            if (Common.StudentSession.LoggedStudent == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.CourseId = courseId;
            var vm = _courseServices.GetSynonymBoosterDetails(courseId);
            return View(vm);
        }

        public ActionResult BoosterCollocation(int courseId)
        {

            ViewBag.CourseId = courseId;
            var vm = _courseServices.GetBoosterCOllocationdetails(courseId);
            return View(vm);
        }

        public ActionResult MasterTopicSentence(int courseId)
        {

            ViewBag.CourseId = courseId;
            var vm = _courseServices.GetMasterTopicSentenceDetails(courseId);
            return View(vm);
        }

        //Online Training

        public ActionResult OnlineTrainigList()
        {
            var vm = _onlineTrainigServices.GetOnlineTrainingList();
            return View(vm);
        }

        public ActionResult OnlineTrainingDetails(int id)
        {
            var vm = _onlineTrainigServices.GetOnlineTrainingDetails(id);
            return View(vm);
        }

        public ActionResult MyOnlineTrainingList()
        {
            if (Common.StudentSession.LoggedStudent == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var vm = _onlineTrainigServices.GetOnlineTrainingList();
            return View(vm);
        }

        public ActionResult MyOnlineTrainingDetails(int id)
        {
            var vm = _onlineTrainigServices.GetOnlineTrainingDetails(id);
            return View(vm);
        }


        public ActionResult VideoMaterials(int packageId)
        {
            IncludedVideosViewModel vm = new IncludedVideosViewModel();
            vm.PackageId = packageId;
            vm.PackageName = _commonServices.getNaatiPackageNameFromPackageId(packageId);
            vm.Videos = (from c in _naatiPackageServices.ListPackageFile().Data.Where(x => x.NaatiPackageId == packageId && x.FileType == Riddhasoft.Setup.Entity.Course.FileType.Video).ToList()
                         select new VideoMaterialViewModel()
                         {
                             Title = c.FileName,
                             Id = c.Id,
                             link = c.FileUrl
                         }).ToList();
            return View(vm);
        }

        public ActionResult AudioMaterial(int packageId)
        {
            IncludedAudioViewModel vm = new IncludedAudioViewModel();
            vm.PackageId = packageId;
            vm.PackageName = _commonServices.getNaatiPackageNameFromPackageId(packageId);
            vm.AudioMaterials = (from c in _naatiPackageServices.ListPackageFile().Data.Where(x => x.NaatiPackageId == packageId && x.FileType == Riddhasoft.Setup.Entity.Course.FileType.Audio).ToList()
                                 select new AudioMaterialViewModel()
                                 {
                                     Title = c.FileName,
                                     Id = c.Id,
                                     link = c.FileUrl
                                 }).ToList();

            return View(vm);
        }

        public ActionResult PdfMaterial(int packageId)
        {
            IncludedPdfsVm vm = new IncludedPdfsVm();
            vm.PackageId = packageId;
            vm.PackageName = _commonServices.getNaatiPackageNameFromPackageId(packageId);
            vm.Pdfs = (from c in _naatiPackageServices.ListPackageFile().Data.Where(x => x.NaatiPackageId == packageId && x.FileType == Riddhasoft.Setup.Entity.Course.FileType.PDF).ToList()
                       select new PdfMaterialViewModel()
                       {
                           Description = c.FileName,
                           Id = c.Id,
                           link = c.FileUrl,
                       }).ToList();
            return View(vm);
        }

        public ActionResult IncludedMockTests(int packageId, PackageTestType testType)
        {
            IncludedTestListViewModel vm = new IncludedTestListViewModel();
            Common.MockTestSession.SpeakingTime = new TimeSpan(0, 20, 0);
            vm.PackageId = packageId;
            
            vm.PackageName = _commonServices.getNaatiPackageNameFromPackageId(packageId);
            vm.IncludedTests = (from c in _naatiPackageServices.ListPackageMockTest().Data.Where(x => x.NaatiPackageId == packageId && testType == x.PackageTestType).ToList()
                                select new IncludedTestViewModel()
                                {
                                    Id = c.NaatiMockTestId,
                                    Title = _commonServices.getNaatiMockTestFromMockTestId(c.NaatiMockTestId),
                                    PackageTestType = c.PackageTestType,
                                    IsTaken = CheckTestGiven(c.NaatiMockTestId)
                                }).ToList();
            return View(vm);
        }


        private bool CheckTestGiven(int mockTestId)
        {
            SNaatiMockTestTaken _takenMockTestServices = new SNaatiMockTestTaken();
            int packageId = NaatiMockTestSession.PackageId;  

            bool isTaken = false;
            var mockTest = _takenMockTestServices.List().Data.Where(x => x.MockTestId == mockTestId && x.PackageId == Common.NaatiMockTestSession.PackageId ).FirstOrDefault();
            if (mockTest != null)
            {
                isTaken = mockTest.IsTaken == true ? true : false;
            }

            return isTaken;
        }
    }
}