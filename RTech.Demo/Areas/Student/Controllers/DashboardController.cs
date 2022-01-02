using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Services;
using Riddhasoft.Student.Entity;
using Riddhasoft.Student.Services;
using RTech.Demo.Areas.MockTest.Services;
using RTech.Demo.Areas.Student.Services;
using RTech.Demo.Areas.Student.ViewModels;
using RTech.Demo.Common;
using RTech.Demo.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace RTech.Demo.Areas.Student.Controllers
{
    public class DashboardController : Controller
    {
        SStudentInformation _studentInformationService = null;
        SStudentLogin _studentLoginService = null;
        CommonServices _commonServices = null;
        DashboardServices dashboardServices = null;
        ReportServices _repostServices = null;
        ClassRoomCourseServices _courseServices = null;
        MockTestHomeServices _mockTestHomeServices = null;


        SPackagePaymentDetails _paymentDetailServices = null;

        public DashboardController()
        {
            _studentInformationService = new SStudentInformation();
            _studentLoginService = new SStudentLogin();
            _commonServices = new CommonServices();
            dashboardServices = new DashboardServices();
            _repostServices = new ReportServices();
            _courseServices = new ClassRoomCourseServices();
            _mockTestHomeServices = new MockTestHomeServices();

            _paymentDetailServices = new SPackagePaymentDetails();
        }


        // GET: Student/Dashboard
        public ActionResult Index(string message = "")
        {
            if (Common.StudentSession.LoggedStudent == null)
            {
                return RedirectToAction("Index", "LogIn");
            }
            if (message == "profileUpdated")
            {
                ViewBag.Message = "Profile Updated Successfully !";
                ViewBag.ToastrVal = 4;
                message = "";
            }
            else if (message == "passwordChangedSuccess")
            {
                ViewBag.Message = "Password Updated Successfully !";
                ViewBag.ToastrVal = 4;
                message = "";
            }
            else if (message == "paymentSuccess")
            {
                ViewBag.Message = "Payment Completed Successfully !";
                ViewBag.ToastrVal = 4;
                message = "";
            }
            ViewBag.ClassRoomTrainingCount = _paymentDetailServices.List().Data.Where(x => x.PaymentFor == Riddhasoft.Setup.Entity.PaymentFor.NaatiPackage && x.UserId == Common.StudentSession.LoggedStudent.StudentId).Count();
            ViewBag.MockTestCount = _paymentDetailServices.List().Data.Where(x => x.PaymentFor == Riddhasoft.Setup.Entity.PaymentFor.NaatiMocktest && x.UserId == Common.StudentSession.LoggedStudent.StudentId).Count();
            return View();
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Common.StudentSession.LoggedStudent == null)
            {
                return RedirectToAction("Index", "LogIn");
            }
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (Common.StudentSession.LoggedStudent == null)
            {
                return RedirectToAction("Index", "LogIn");
            }
            model.Id = Common.StudentSession.LoggedStudent.StudentId;

            if (ModelState.IsValid)
            {

                var data = _studentLoginService.List().Data.Where(x => x.StudentId == Common.StudentSession.LoggedStudent.StudentId).FirstOrDefault();
                data.Password = model.NewPassword;
                _studentLoginService.Update(data);

                MailCommon mail = new MailCommon();
                string body = "Your password has been changed on " + DateTime.Now.Date + " !";
                mail.SendMail(data.Email, "Password Successfully Updated.", body);
                return RedirectToAction("Index", new { @message = "passwordChangedSuccess" });
            }

            return View(model);
        }

        public ActionResult CoursesList()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            if (Common.StudentSession.LoggedStudent == null)
            {
                return RedirectToAction("Index", "LogIn");
            }
            SetViewBagForCountries();

            var student = _studentInformationService.List().Data.Where(x => x.Id == Common.StudentSession.LoggedStudent.StudentId).FirstOrDefault();
            EditProfileViewModel vm = new EditProfileViewModel()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                MiddleName = student.MiddleName,
                LastName = student.LastName,
                Address = student.Address,
                Phone = student.PhoneNumber,
                Email = student.Email,
                Gender = student.Gender,
                BirthCountry = student.BirthCountry,
                Occupation = student.Occupation,
                PhotoUrl = student.PhotoUrl,
                WebsiteUrl = student.WebsiteUrl,
                FacebookUrl = student.FacebookUrl,
                LinkedInUrl = student.LinkedInUrl,
                TwitterUrl = student.TwitterUrl
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult EditProfile(EditProfileViewModel model)
        {
            if (Common.StudentSession.LoggedStudent == null)
            {
                return RedirectToAction("Index", "LogIn");
            }
            if (model != null)
            {
                bool valid = true;
                if (string.IsNullOrEmpty(model.FirstName))
                {
                    ModelState.AddModelError("FirstName", "This field can't be blank !");
                    valid = false;
                }
                if (string.IsNullOrEmpty(model.LastName))
                {
                    ModelState.AddModelError("LastName", "This field can't be blank !");
                    valid = false;
                }
                if (string.IsNullOrEmpty(model.Address))
                {
                    ModelState.AddModelError("Address", "This field can't be blank !");
                    valid = false;
                }
                if (string.IsNullOrEmpty(model.Phone))
                {
                    ModelState.AddModelError("Phone", "This field can't be blank !");
                    valid = false;
                }
                if (string.IsNullOrEmpty(model.Email))
                {
                    ModelState.AddModelError("Email", "This field can't be blank !");
                    valid = false;
                }
                if (!string.IsNullOrEmpty(model.Email))
                {
                    bool isEmail = Regex.IsMatch(model.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);
                    if (isEmail == false)
                    {
                        valid = false;
                        ModelState.AddModelError("Email", "Invalid Email");
                    }
                    ServiceResult<bool> isEmailExitWhileUpdating = _studentInformationService.IsEmailExistWhileUpdating(Common.StudentSession.LoggedStudent.StudentId, model.Email);
                    if (isEmailExitWhileUpdating.Data == false)
                    {
                        ModelState.AddModelError("Email", isEmailExitWhileUpdating.Message);
                        valid = false;
                    }
                }
                if (model.Gender == (Gender)0)
                {
                    ModelState.AddModelError("Gender", "This field can't be blank !");
                    valid = false;
                }
                if (string.IsNullOrEmpty(model.BirthCountry))
                {
                    ModelState.AddModelError("BirthCountry", "This field can't be blank !");
                    valid = false;
                }
                if (string.IsNullOrEmpty(model.Occupation))
                {
                    ModelState.AddModelError("Occupation", "This field can't be blank !");
                    valid = false;
                }
                if (valid == true)
                {
                    if (Request.Files.Count > 0)
                    {
                        string fileName = "";
                        string directory = Server.MapPath("/StudentPhotos");
                        var profileImage = Request.Files["profileImage"];

                        if (profileImage != null && profileImage.ContentLength > 0)
                        {
                            fileName = Guid.NewGuid() + "." + profileImage.FileName.Split('.')[1];
                            profileImage.SaveAs(Path.Combine(directory, fileName));
                            model.PhotoUrl = "/StudentPhotos/" + fileName;
                        }
                    }


                    var data = _studentInformationService.List().Data.Where(x => x.Id == Common.StudentSession.LoggedStudent.StudentId).FirstOrDefault();
                    if (data != null)
                    {
                        data.FirstName = model.FirstName;
                        data.MiddleName = model.MiddleName;
                        data.LastName = model.LastName;
                        data.Address = model.Address;
                        data.PhoneNumber = model.Phone;
                        data.Email = model.Email;
                        data.Gender = model.Gender;
                        data.BirthCountry = model.BirthCountry;
                        data.Occupation = model.Occupation;
                        data.PhotoUrl = model.PhotoUrl;
                        data.WebsiteUrl = model.WebsiteUrl;
                        data.FacebookUrl = model.FacebookUrl;
                        data.TwitterUrl = model.TwitterUrl;
                        data.LinkedInUrl = model.LinkedInUrl;
                        data.LastModifiedDate = DateTime.Now;
                        _studentInformationService.Update(data);

                        //for studentLoginTable
                        var data1 = _studentLoginService.List().Data.Where(x => x.StudentId == Common.StudentSession.LoggedStudent.StudentId).FirstOrDefault();
                        if (data1 != null)
                        {
                            data1.Email = model.Email;
                            _studentLoginService.Update(data1);
                        }
                        return RedirectToAction("Index", new { @message = "profileUpdated" });
                    }

                }
            }
            SetViewBagForCountries();
            return View(model);
        }

        [HttpGet]
        public ActionResult Profile()
        {
            if (Common.StudentSession.LoggedStudent == null)
            {
                return RedirectToAction("Index", "LogIn");
            }
            var data = _studentInformationService.List().Data.Where(x => x.Id == Common.StudentSession.LoggedStudent.StudentId).FirstOrDefault();
            var profile = new ProfileViewModel()
            {
                Phone = data.PhoneNumber,
                Address = data.Address,
                Email = data.Email,
                Gender = data.Gender,
                Country = _commonServices.getCountryNameFromCode(data.BirthCountry),
                Occupation = data.Occupation
            };
            return View(profile);
        }

        public ActionResult Reports()
        {
            if (Common.StudentSession.LoggedStudent == null)
            {
                return RedirectToAction("Index", "LogIn");
            }
            var vm = _repostServices.getReportLists();
            return View(vm);
        }

        public ActionResult ViewMockTestReport(int mockTestId, int studentId = 0)
        {
            //if (Common.StudentSession.LoggedStudent == null)
            //{
            //    return RedirectToAction("Index", "LogIn");
            //}
            if (studentId == 0)
            {
                studentId = Common.StudentSession.LoggedStudent.StudentId;
            }
            var vm = _repostServices.getStudentMockTestReport(mockTestId, studentId);
            return View(vm);
        }

        public ActionResult TransactionHistory()
        {
            if (Common.StudentSession.LoggedStudent == null)
            {
                return RedirectToAction("Index", "LogIn");
            }
            var vm = dashboardServices.getTransactionHistory();
            return View(vm);
        }

        public ActionResult TransactionInvoice(int paymentId)
        {

            InvoiceViewModel vm = new InvoiceViewModel();
            var paymentInfo = _paymentDetailServices.List().Data.Where(x => x.Id == paymentId).FirstOrDefault();
            int studentId =  Common.StudentSession.LoggedStudent == null ? 0 :  Common.StudentSession.LoggedStudent.StudentId;

            vm = (from c in _studentInformationService.List().Data.Where(x => x.Id == studentId).ToList()
                  select new InvoiceViewModel()
                  {
                      Address = c.Address,
                      Country = _commonServices.getCountryNameFromCode(c.BirthCountry),
                      CustomerEmail = c.Email,
                      CustomerName = c.FirstName + " " + c.LastName,
                  }).FirstOrDefault();

            vm.ProductName = dashboardServices.getPackageTitle(paymentInfo.CourseCode, paymentInfo.PaymentFor);
            vm.InvoiceNumber = paymentInfo.TransactionId;
            vm.Price = paymentInfo.Amount;
            vm.TotalAmount = paymentInfo.Amount;
            vm.CreatedDate = paymentInfo.TransactionDateTime.ToFormatedString();

            return View(vm);
        }

        public ActionResult LogOut()
        {
            Common.StudentSession.LoggedStudent = null;
            return RedirectToAction("Home", "TopBar", new { Area = "Student" });
        }
        private void SetViewBagForCountries()
        {
            var countries = _commonServices.getCountryDropDown();
            ViewBag.Countries = new SelectList(countries, "CountryCode", "Country");
        }

        public ActionResult MockTestPackages()
        {

            var result = _commonServices.GetMockTestPackages();
            return View(result);
        }


    }


}