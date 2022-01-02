using Riddhasoft.Services.Common;
using Riddhasoft.Student.Services;
using RTech.Demo.Areas.Student.ViewModels;
using RTech.Demo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTech.Demo.Areas.Student.Controllers
{
    public class LogInController : Controller
    {
        SStudentLogin _studentLoginService = new SStudentLogin();
        CommonServices CommonService = new CommonServices();
        // GET: Student/LogIn
        public ActionResult Index(string guId = "", string @message = "")
        {

            if (!string.IsNullOrEmpty(guId))
            {
                bool activation = CommonService.checkGuid(guId);
                if (activation)
                {
                    ViewBag.ActivationMessage = "Account activated. You can Log In now !";
                }
            }
            if (!string.IsNullOrEmpty(message))
            {
                if (message == "resetSuccess")
                {
                    ViewBag.Message = "Password Reset Successful !";
                    ViewBag.ToastrVal = 4;
                    message = "";
                }
            }
            if (Common.StudentSession.LoggedStudent != null)
            {
                
                return RedirectToAction("Index","Dashboard");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            if (model != null)
            {
                bool valid = true;
                if (string.IsNullOrEmpty(model.EmailUserName))
                {
                    ModelState.AddModelError("EmailUserName", "This field can't be blank !");
                    valid = false;
                }
                if (string.IsNullOrEmpty(model.Password))
                {
                    ModelState.AddModelError("Password", "This field can't be blank !");
                    valid = false;
                }
                if (!string.IsNullOrEmpty(model.EmailUserName))
                {
                    ServiceResult<bool> isEmailUsernameExist = _studentLoginService.IsUserNameEmailExist(model.EmailUserName);
                    if (isEmailUsernameExist.Data == false)
                    {
                        ModelState.AddModelError("EmailUserName", isEmailUsernameExist.Message);
                        valid = false;
                    }
                }
                if (valid == true)
                {
                    var login = _studentLoginService.Login(model.EmailUserName, model.Password);
                    if (login.Status == ResultStatus.Ok)
                    {
                        if (login.Data.IsActive == false) {
                            ModelState.AddModelError("EmailUserName", "Your Account Has Been Deactivated");
                            return View(model);
                        }
                        LoggedStudentViewModel loggedStudent = new LoggedStudentViewModel()
                        {
                            StudentId = login.Data.StudentId,
                            StudentName = CommonService.getStudentNameFromId(login.Data.StudentId),
                            StudentUserName = CommonService.getStudentEmailFromId(login.Data.StudentId),
                            StudentImageUrl = CommonService.getStudentImage(login.Data.StudentId),
                            StudentNameForMockTest = CommonService.getStudentFullNameFromId(login.Data.StudentId),
                        };
                        Common.StudentSession.LoggedStudent = loggedStudent;

                        if (!string.IsNullOrEmpty(Common.StudentSession.FromUrl)) 
                        {
                            return Redirect(Common.StudentSession.FromUrl);
                        }
                        return RedirectToAction("Index", "Dashboard");
                    }
                    ModelState.AddModelError("Password", login.Message);
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewModel vm)
        {
            if (string.IsNullOrEmpty(vm.Email))
            {
                ModelState.AddModelError("Email", "Please provide your email address ! ");
                return View(vm);
            }

            var data = _studentLoginService.List().Data.Where(x => x.Email.ToLower() == vm.Email.ToLower()).FirstOrDefault();
            if (data == null)
            {
                ModelState.AddModelError("Email", "The provided email address doesn't exist in the database !");
                return View(vm);
            }

            
            MailCommon mail = new MailCommon();
            string guId = Guid.NewGuid().ToString();
            var emailBody = "Password Reset Security Code : " + guId;            
            mail.SendMail(data.Email, "Password Reset", emailBody);
            StudentSession.PasswordResetEmail = data.Email;
            data.TemporaryResetCode = guId;
            _studentLoginService.Update(data);

            return RedirectToAction("SecurityCode");



            
        }

        [HttpGet]
        public ActionResult SecurityCode()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SecurityCode(PasswordResetSecurityCodeViewModel vm)
        {
            if (string.IsNullOrEmpty(vm.SecurityCode))
            {
                ModelState.AddModelError("SecurityCode", "Please enter a non-empty value !");
                return View(vm);
            }
            var data = _studentLoginService.List().Data.Where(x => x.TemporaryResetCode == vm.SecurityCode && x.Email.ToLower() == StudentSession.PasswordResetEmail.ToLower()).FirstOrDefault();
            if (data == null)
            {
                ModelState.AddModelError("SecurityCode", "Security Code mismatch !");
                return View(vm);
            }
            
            StudentSession.PasswordResetSecurityCode = vm.SecurityCode;

            return RedirectToAction("PasswordReset");

            
        }

        [HttpGet]
        public ActionResult PasswordReset()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PasswordReset(PasswordResetViewModel vm)
        {
            if (string.IsNullOrEmpty(vm.NewPassword))
            {
                ModelState.AddModelError("NewPassword", "This field can't be empty !");
                return View(vm);
            }
            if (string.IsNullOrEmpty(vm.ConfirmNewPassword))
            {
                ModelState.AddModelError("ConfirmNewPassword", "This field can't be empty !");
                return View(vm);
            }
            if (vm.NewPassword != vm.ConfirmNewPassword)
            {
                ModelState.AddModelError("ConfirmNewPassword", "Passwords don't match !");
                return View(vm);                     
            }
            var data = _studentLoginService.List().Data.Where(x => x.TemporaryResetCode == StudentSession.PasswordResetSecurityCode && x.Email.ToLower() == StudentSession.PasswordResetEmail.ToLower()).FirstOrDefault();
            if (data == null)
            {
                ModelState.AddModelError("ConfirmNewPassword", "Something went wrong. Please try again later !");
                return View();
            }
            data.Password = vm.NewPassword;
            data.TemporaryResetCode = "";
            _studentLoginService.Update(data);



            return RedirectToAction("PasswordResetSuccess", "Index");
        }

        public ActionResult PasswordResetSuccess()
        {
            return View();
        }
    }
}