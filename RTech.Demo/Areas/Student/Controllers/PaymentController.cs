using Newtonsoft.Json;
using Riddhasoft.MockTest.Entity;
using Riddhasoft.MockTest.Services;
using Riddhasoft.Setup.Entity;
using Riddhasoft.Setup.Services;
using Riddhasoft.Setup.Services.Course;
using Riddhasoft.Setup.Services.Package;
using Riddhasoft.Setup.Services.QuestionPackage;
using Riddhasoft.Setup.Services.QuestionSet;
using RTech.Demo.Areas.Student.Services;
using RTech.Demo.Areas.Student.ViewModels;
using RTech.Demo.Common;
using RTech.Demo.CommWeb;
using RTech.Demo.Models;
using RTech.Demo.SimplifyCommerce;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
namespace RTech.Demo.Areas.Student.Controllers
{
    public class PaymentController : Controller
    {

        PaymentServices paymentServices = null;
        SPackagePaymentDetails _packagePaymentDetailsServices = null;
        SOnlineClassRoomCourse _onlineClassRoomCourseServies = null;
        SOnlineTraining _onlineTrainingServices = null;
        STutorial _tutorialServices = null;
        CommonServices _commonServices = null;
        SQuestionPackageMaster _questionPackageMasterServices = null;
        SQuestionPackageDetail _questionPackageDetailServices = null;
        SMockTestPurchasedPackages _mockTestPurchasedPackagesServices = null;
        SQuestionSetMaster _questionSetMasterServices = null;
        SNaatiMockTest _naatiMockTestServices = null;
        SNaatiPackage _naatiPackageServices = null;


        public PaymentController()
        {
            paymentServices = new PaymentServices();
            _packagePaymentDetailsServices = new SPackagePaymentDetails();
            _onlineClassRoomCourseServies = new SOnlineClassRoomCourse();
            _onlineTrainingServices = new SOnlineTraining();
            _tutorialServices = new STutorial();
            _commonServices = new CommonServices();
            _questionPackageMasterServices = new SQuestionPackageMaster();
            _questionPackageDetailServices = new SQuestionPackageDetail();
            _mockTestPurchasedPackagesServices = new SMockTestPurchasedPackages();
            _questionSetMasterServices = new SQuestionSetMaster();

            _naatiMockTestServices = new SNaatiMockTest();
            _naatiPackageServices = new SNaatiPackage();
        }



        // GET: Student/Payment
        public ActionResult Index(string Code, PaymentFor PaymentFor)
        {
            if (PaymentFor == PaymentFor.OnlineCourse || PaymentFor == PaymentFor.OnlineTraining || PaymentFor == PaymentFor.Tutorial || PaymentFor == PaymentFor.MockTestPackage || PaymentFor == PaymentFor.NaatiPackage || PaymentFor == PaymentFor.NaatiMocktest)
            {

                Common.StudentSession.CourseCode = Code;
                if (Common.StudentSession.LoggedStudent == null)
                {

                    Common.StudentSession.FromUrl = Request.Url.ToString();
                    return RedirectToAction("Index", "LogIn");

                }
                SetViewBagForContries();
                SetViewBagForYear();
                SetViewBagForMonth();
                bool codeExists = paymentServices.checkIfCodeExists(Code, PaymentFor);
                if (codeExists == false)
                {
                    return RedirectToAction("Index", "PageNotFound", new { area = "", @message = "Invalid Code" });
                }
                var vm = paymentServices.getPaymentDetails(Code, PaymentFor);

                bool courseAlreadyBought = _commonServices.checkIfCourseAlreadyBought(Code, PaymentFor);
                if (courseAlreadyBought == true)
                {
                    ModelState.AddModelError("CourseName", "Your have already purchased this course !");

                }
                Session.Remove("FromUrl");
                Session.Remove("CourseCode");
                decimal Amount = 0;
                decimal totalAmount = 0;

                Amount = (vm.Amount * 10 / 100 );

                totalAmount = vm.Amount;
                vm.Total = totalAmount;

                ViewBag.GSTAmount = Amount;
                return View(vm);
            }
            return RedirectToAction("Index", "PageNotFound", new { area = "", @message = "Invalid Payment Type" });

        }


        [HttpPost]
        public ActionResult Index(PaymentViewModel vm)
        {

            SetViewBagForContries();
            SetViewBagForMonth();
            SetViewBagForYear();
            if (Common.StudentSession.LoggedStudent == null)
            {
                Common.StudentSession.FromUrl = Request.Url.ToString();
                return RedirectToAction("Index", "LogIn");
            }

            bool valid = true;

            if (!ModelState.IsValid)
            {
                valid = false;
            }
            #region Validaton For Payment Form Inputs

            bool courseAlreadyBought = _commonServices.checkIfCourseAlreadyBought(vm.Code, vm.PaymentFor);

            if (courseAlreadyBought == true)
            {
                ModelState.AddModelError("CourseName", "Your have already purchased this course !");
                valid = false;
            }

            if (valid == false)
            {
                return View(vm);
            }



            PaymentsApi.PublicApiKey = WebConfigurationManager.AppSettings["PublicApiKey"].ToString();
            PaymentsApi.PrivateApiKey = WebConfigurationManager.AppSettings["PrivateApiKey"].ToString();

            PaymentsApi api = new PaymentsApi();

            Payment payment = new Payment();
            payment.Amount = (long)vm.Total * 100;
            payment.Currency = "AUD";
            Card card = new Card();
            card.Cvc = vm.CardSecurityKey;
            card.ExpMonth = vm.CardExpMonth;
            card.ExpYear = vm.CardExpYear;
            card.Number = vm.CardNumber;
            payment.Card = card;
            payment.Description = "payment description";


            try
            {
                payment = (Payment)api.Create(payment);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            if(payment.PaymentStatus== null)
            {
                return View(vm);
            }

            #endregion
            if (payment.PaymentStatus.ToLower() != "declined")
            {
                int courseDuration = 0;

                if (vm.PaymentFor == PaymentFor.NaatiPackage)
                {
                    courseDuration = _naatiPackageServices.List().Data.Where(x => x.Code.Trim().ToLower() == vm.Code.Trim().ToLower()).FirstOrDefault().Duration;
                }

                else if (vm.PaymentFor == PaymentFor.NaatiMocktest)
                {
                    courseDuration = _naatiMockTestServices.List().Data.Where(x => x.Code.Trim().ToLower() == vm.Code.Trim().ToLower()).FirstOrDefault().Duration;
                }
                //mapping response values into viewmodel
                EPackagePaymentDetails model = new EPackagePaymentDetails()
                {
                    UserId = Common.StudentSession.LoggedStudent.StudentId,
                    PaymentFor = vm.PaymentFor,
                    CourseCode = vm.Code,
                    OrderId =/* payment.Order.Id*/ "" ,
                    TransactionId = /*payment.Id*/ "" ,
                    TransactionDateTime = DateTime.Now,
                    CardType = /*payment.Card.Type*/ "" ,
                    CardNumber = /*payment.Card.Number */ vm.CardNumber,
                    Amount = /*(decimal)payment.Amount*/ vm.Amount ,
                    Currency = /*payment.Currency*/  "AUD",
                    ReceiptNumber = /*payment.Receipt.Id*/ "" ,
                    //acquirerTransactionId = payment.TransactionDetails.Data.,
                    StartDate = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddDays(courseDuration),
                    NameOnCard = /*payment.Card.Name*/ vm.NameOnCard,
                    Country = vm.Country,
                    ZipCode = vm.ZipCode
                };
                _packagePaymentDetailsServices.Add(model);

                if (vm.PaymentFor == PaymentFor.MockTestPackage)
                {
                    int questionPackageId = _questionPackageMasterServices.List().Data.Where(x => x.PackageCode.Trim().ToLower() == vm.Code.Trim().ToLower()).FirstOrDefault().Id;
                    var packageDetails = _questionPackageDetailServices.List().Data.Where(x => x.QuestionPackageMasterId == questionPackageId).ToList();

                    //if the package is already bought, delete previous entries
                    var packageAlreadyExists = _mockTestPurchasedPackagesServices.List().Data.Where(x => x.MockTestPackageId == questionPackageId && x.StudentId == Common.StudentSession.LoggedStudent.StudentId).FirstOrDefault();
                    if (packageAlreadyExists != null)
                    {
                        var alreadyPurchased = _mockTestPurchasedPackagesServices.List().Data.Where(x => x.MockTestPackageId == questionPackageId && x.StudentId == Common.StudentSession.LoggedStudent.StudentId).ToList();
                        _mockTestPurchasedPackagesServices.RemoveRange(alreadyPurchased);
                    }
                    foreach (var item in packageDetails)
                    {
                        EMockTestPurchasedPackages packageData = new EMockTestPurchasedPackages()
                        {
                            StudentId = Common.StudentSession.LoggedStudent.StudentId,
                            MockTestPackageId = questionPackageId,
                            IsUnscored = _questionSetMasterServices.List().Data.Where(x => x.Id == item.QuestionSetId).FirstOrDefault().IsUnscored,
                            PurchaseDate = DateTime.Now,
                            ExpiryDate = DateTime.Now.AddDays(courseDuration),
                            IsTestTaken = false
                        };
                        _mockTestPurchasedPackagesServices.Add(packageData);
                    }
                }
                if (vm.PaymentFor == PaymentFor.NaatiMocktest)
                {
                    int mockTestPackageId = _naatiMockTestServices.List().Data.Where(x => x.Code.Trim().ToLower() == vm.Code.Trim().ToLower()).FirstOrDefault().Id;
                    var mockTestDetails = _naatiMockTestServices.ListDetails().Data.Where(x => x.NaatiMockTestMasterId == mockTestPackageId).ToList();

                    //if the package is already bought, delete previous entries
                    var packageAlreadyExists = _mockTestPurchasedPackagesServices.List().Data.Where(x => x.MockTestPackageId == mockTestPackageId && x.StudentId == Common.StudentSession.LoggedStudent.StudentId).FirstOrDefault();
                    if (packageAlreadyExists != null)
                    {
                        var alreadyPurchased = _mockTestPurchasedPackagesServices.List().Data.Where(x => x.MockTestPackageId == mockTestPackageId && x.StudentId == Common.StudentSession.LoggedStudent.StudentId).ToList();
                        _mockTestPurchasedPackagesServices.RemoveRange(alreadyPurchased);
                    }
                    foreach (var item in mockTestDetails)
                    {
                        EMockTestPurchasedPackages packageData = new EMockTestPurchasedPackages()
                        {
                            StudentId = Common.StudentSession.LoggedStudent.StudentId,
                            MockTestPackageId = mockTestPackageId,
                            IsUnscored = false,
                            PurchaseDate = DateTime.Now,
                            ExpiryDate = DateTime.Now.AddDays(courseDuration),
                            IsTestTaken = false
                        };
                        _mockTestPurchasedPackagesServices.Add(packageData);
                    }
                }
                Common.StudentSession.FromUrl = null;
                return RedirectToAction("PaymentSuccess", "Payment", new { @Area = "Student" });


            }
            return View(vm);
        }

        public ActionResult CoursePage(string code, PaymentFor courseType)
        {
            if (courseType == PaymentFor.OnlineCourse)
            {
                var data = _onlineClassRoomCourseServies.List().Data.Where(x => x.Code.ToLower() == code.ToLower()).FirstOrDefault();
                if (data != null)
                {
                    return RedirectToAction("CourseDetails", "Course", new { @id = data.Id });
                }
            }
            else if (courseType == PaymentFor.OnlineTraining)
            {
                var data = _onlineTrainingServices.List().Data.Where(x => x.Code.ToLower() == code.ToLower()).FirstOrDefault();
                if (data != null)
                {
                    return RedirectToAction("OnlineTrainingDetails", "Course", new { @id = data.Id });
                }
            }
            else if (courseType == PaymentFor.Tutorial)
            {
                var data = _tutorialServices.List().Data.Where(x => x.Code.ToLower() == code.ToLower()).FirstOrDefault();
                if (data != null)
                {
                    //return RedirectToAction("", "");
                }
            }
            string currentUrl = Request.Url.ToString();
            return Redirect(currentUrl);

        }

        public ActionResult PaymentSuccess()
        {
            return View();
        }

        private void SetViewBagForContries()
        {
            var countries = _commonServices.getCountryDropDown();
            ViewBag.Countries = new SelectList(countries, "CountryCode", "Country");
        }


        private void SetViewBagForMonth()
        {
            List<DropdownViewModel> months = new List<DropdownViewModel>();
            months.Add(new DropdownViewModel()
            {
                Id = 01,
                Name = "01"
            });
            months.Add(new DropdownViewModel()
            {
                Id = 02,
                Name = "02"
            });
            months.Add(new DropdownViewModel()
            {
                Id = 03,
                Name = "03"
            });
            months.Add(new DropdownViewModel()
            {
                Id = 04,
                Name = "04"
            });
            months.Add(new DropdownViewModel()
            {
                Id = 05,
                Name = "05"
            });
            months.Add(new DropdownViewModel()
            {
                Id = 06,
                Name = "06"
            });
            months.Add(new DropdownViewModel()
            {
                Id = 07,
                Name = "07"
            });
            months.Add(new DropdownViewModel()
            {
                Id = 08,
                Name = "08"
            });
            months.Add(new DropdownViewModel()
            {
                Id = 09,
                Name = "09"
            });
            months.Add(new DropdownViewModel()
            {
                Id = 10,
                Name = "10"
            });
            months.Add(new DropdownViewModel()
            {
                Id = 11,
                Name = "11"
            });
            months.Add(new DropdownViewModel()
            {
                Id = 12,
                Name = "12"
            });
            ViewBag.Months = new SelectList(months, "Id", "Name");
        }

        private void SetViewBagForYear()
        {
            List<DropdownViewModel> years = new List<DropdownViewModel>();

            for (int i = 20; i <= 50; i++)
            {
                years.Add(new DropdownViewModel()
                {
                    Id = i,
                    Name = i.ToString()
                });
            }
          
            ViewBag.Years = new SelectList(years, "Id", "Name");
        }


        //public void CreatePayment(PaymentViewModel vm)
        //{
        //    //PaymentsApi.PublicApiKey = "lvpb_Y2QzNWY5YWItYzkxMC00MmY3LThiOGMtMjdjZDhkNDc4NWE3";
        //    //PaymentsApi.PrivateApiKey = "Z5GH0biivE+5a7l6itOtXN9id96xaa+LQmLvsbH8PqR5YFFQL0ODSXAOkNtXTToq";

        //    PaymentsApi.PublicApiKey = "lvpb_MGM4YjhhNmEtNzk5Yi00YmEzLTkzOWMtZTFhOWVjNTE2NGE3";
        //    PaymentsApi.PrivateApiKey = "oTCwAp9wA+MYRCBW3fWw7B7y16NgyoYIQ3u6Fq1CHPB5YFFQL0ODSXAOkNtXTToq";

        //    PaymentsApi api = new PaymentsApi();

        //    Payment payment = new Payment();
        //    payment.Amount = (long)vm.Amount * 100;
        //    payment.Currency = "AUD";
        //    Card card = new Card();
        //    card.Cvc = vm.CardSecurityKey;
        //    card.ExpMonth = vm.CardExpMonth;
        //    card.ExpYear = vm.CardExpYear;
        //    card.Number = vm.CardNumber;
        //    payment.Card = card;
        //    payment.Description = "";


        //    try
        //    {
        //        payment = (Payment)api.Create(payment);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //}
    }


}