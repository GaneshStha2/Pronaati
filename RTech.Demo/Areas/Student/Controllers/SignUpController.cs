using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity;
using Riddhasoft.Setup.Services;
using Riddhasoft.Student.Entity;
using Riddhasoft.Student.Services;
using RTech.Demo.Areas.EmailTemplete.ViewModels;
using RTech.Demo.Areas.Student.ViewModels;
using RTech.Demo.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Mvc.Html;


namespace RTech.Demo.Areas.Student.Controllers
{
    public class SignUpController : Controller
    {
        SStudentInformation _studentInformationService = null;
        SStudentLogin _studentLoginService = new SStudentLogin();
        CommonServices _commonService = new CommonServices();

        public SignUpController()
        {
            _studentInformationService = new SStudentInformation();

        }
        // GET: Student/SignUp
        public ActionResult Index()
        {
            if (Common.StudentSession.LoggedStudent != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            SetViewBagForCountries();
            return View();
        }

        [HttpPost]
        public ActionResult Index(SignUpViewModel model)
        {
            var countries = _commonService.getCountryDropDown();
            ViewBag.Countries = new SelectList(countries, "CountryCode", "Country");
            if (ModelState.IsValid)
            {
                bool valid = true;

                ServiceResult<bool> isUiqueEmail = _studentInformationService.IsEmailExist(model.Email);
                if (isUiqueEmail.Data == false)
                {
                    ModelState.AddModelError("Email", isUiqueEmail.Message);
                    valid = false;
                }

                if (valid == true)
                {

                    if (Request.Files.Count > 0)
                    {
                        string fileName = "";
                        string directory = Server.MapPath("/Images");
                        var image = Request.Files["image"];

                        if (image != null && image.ContentLength > 0)
                        {
                            fileName = Guid.NewGuid() + "." + image.FileName.Split('.')[1];
                            image.SaveAs(Path.Combine(directory, fileName));
                            model.ImageUrl = "/Images/" + fileName;
                        }
                    }

                    EStudentInformation data = new EStudentInformation()
                    {
                        Institute = model.Institute,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        Gender = model.Gender,
                        BirthCountry = model.BirthCountry,
                        Occupation = model.Occupation,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        Address = model.Address,
                        Code = _commonService.generateUserCode(model.Email)
                    };
                    var result = _studentInformationService.Add(data);

                    string guId = Guid.NewGuid().ToString();
                    EStudentLogin data1 = new EStudentLogin()
                    {
                        StudentId = result.Data.Id,
                        Email = model.Email,
                        Password = model.Password,
                        IsActive = false,
                        IsDeleted = false,
                        VerificationCode = guId,
                        LogInFailedCount = 0
                    };
                    var result1 = _studentLoginService.Add(data1);

                    ModelState.Clear();
                    MailCommon mail = new MailCommon();

                    var baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
                    var url = baseUrl + "/Student/LogIn/Index?guId=" + guId;

                    string mailLink = baseUrl + "/EmailTemplete/StudentSignUpEmailTemplete/Index?NewSignedUpEmail=" + data1.Email + "&link=" + url;
                    string mailBody = Common.CommonServices.GetTemplate(mailLink);

                    string senderEmail = WebConfigurationManager.AppSettings["GmailUserName"].ToString();
                    string senderPassword = WebConfigurationManager.AppSettings["GmailPassword"].ToString();

                    new Thread(() =>
                    {
                        mail.SendMail(data1.Email, "New Account Register", mailBody, senderEmail, senderPassword);
                        mail.SendMail("admin@pronaati.com.au", "New Account Register", mailBody, senderEmail, senderPassword);
                    }).Start();

                    ViewBag.Message = "An activation link has been sent to your email address.  Please click on it to activate your account !";

                    //string body = Common.CommonServices.GetTemplate(baseUrl + "/EmailTemplete/StudentSignUpEmailTemplete/Index?NewSignedUpEmail=" + data1.Email);

                    SetViewBagForCountries();
                    return View();
                }
            }
            SetViewBagForCountries();
            return View(model);
        }

        public static string GetTemplate(string link)
        {
            using (var myWebClient = new WebClient())
            {
                myWebClient.Headers["User-Agent"] = "MOZILLA/5.0 (WINDOWS NT 6.1; WOW64) APPLEWEBKIT/537.1 (KHTML, LIKE GECKO) CHROME/21.0.1180.75 SAFARI/537.1";

                string page = myWebClient.DownloadString(link);

                return page;
            }
        }
        private void SetViewBagForCountries()
        {
            var countries = _commonService.getCountryDropDown();
            ViewBag.Countries = new SelectList(countries, "CountryCode", "Country");
        }

    }
}