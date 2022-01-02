using Riddhasoft.Entity.User;
using Riddhasoft.Services.Common;
using Riddhasoft.Services.User;
using Riddhasoft.Setup.Entity;
using Riddhasoft.Setup.Services;
using Riddhasoft.User.Entity;
using RTech.Demo.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace RTech.Demo.Areas.User.Controllers
{
    public class UserController : Controller
    {
        EUser userData = new EUser();
        SUser userServices = null;
        SUserRole roleServices = new SUserRole();
        SSessionDetail sessionServices = new SSessionDetail();
        SContext contextServices = new SContext();
        SCompany _companyServices = null;

        public UserController()
        {
            _companyServices = new SCompany();
            userServices = new SUser();
        }
        //
        // GET: /User/User/
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Message = "";
            ViewBag.LoginAs = 2;
            ViewBag.IpAddress = GetIPAddress();
            ViewBag.CompanyCode = "";


            return PartialView("_Login");
        }
        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        [HttpPost]
        public ActionResult Login(UserViewModel user)
        {
            if (RiddhaSession.OperationDate == "" || RiddhaSession.Language == "")
            {
                RiddhaSession.Language = "en";
                RiddhaSession.OperationDate = "en";
            }
            RiddhaSession.CurDate = DateTime.Now.ToFormatedString();
            var branch = _companyServices.ListBranch().Data.Where(x => x.Code.ToLower() == user.CompanyCode.ToLower() && x.IsHeadOffice && x.IsDeleted == false).FirstOrDefault();
            var userQuery = userServices.List().Data;
            if (branch == null)
            {
                ViewBag.Message = "Invalid Company Code";
                return PartialView("_Login");
            }
            else
            {
                var userData = userQuery.Where(x => x.BranchId == branch.Id && x.Name.ToLower() == user.UserName.ToLower() && x.Password == user.Password).FirstOrDefault();
                if (userData == null)
                {
                    ViewBag.Message = "Invalid Username or Password";
                    return PartialView("_Login");
                }
                else
                {
                    if (userData.IsSuspended)
                    {
                        ViewBag.Message = "Your company account is suspended.Please contact admin";
                        return PartialView("_Login");
                    }
                    else
                    {
                        var companyLicense = _companyServices.ListCompanyLicense().Data.Where(x => x.CompanyId == branch.CompanyId).FirstOrDefault();
                        if (companyLicense == null)
                        {
                            ViewBag.Message = "License is not issued. Please contact admin";
                            return PartialView("_Login");
                        }
                        else if (companyLicense.ExpiryDate.Date < DateTime.Now.Date)
                        {
                            ViewBag.Message = "The Company license is expired.Please contact admin";
                            return PartialView("_Login");
                        }
                        else
                        {
                            
                            var context = UpdateSession(userData);
                            RiddhaSession.CurrentToken = context.Token;
                            RiddhaSession.CurDate = DateTime.Now.ToFormatedString();
                            return RedirectToAction("Index", "Home", new { area = "" });
                        }
                    }
                }
            }
        }
        private EContext UpdateSession(EUser User)
        {
            var newContext = new EContext() { Id = 0, LastLogin = DateTime.Now, TimeOut = TimeSpan.FromMinutes(20), UserId = User.Id, Token = getToken() };
            var contextResult = contextServices.Add(newContext);
            sessionServices.Add(new ESessionDetail() { Id = 0, Key = "User", Value = User.Id.ToString(), ContextId = contextResult.Data.Id });
            return newContext;
        }
        private string getToken()
        {
            return Guid.NewGuid().ToString();
        }
        public bool IsFirstLogin()
        {
            return (userServices.List().Data.Count() == 0 && roleServices.List().Data.Count() == 0);
        }
        [HttpGet]
        public ActionResult Logout()
        {
            var curuser = RiddhaSession.CurrentUser;
            RiddhaSession.Logout();
            ViewBag.Message = "";
            return RedirectToAction("login", "User", new { area = "User" });
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            ChangePasswordViewModel vm = new ChangePasswordViewModel();
            var user = RiddhaSession.CurrentUser;
            vm.UserName = user.Name;
            return View(vm);
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel vm)
        {
            ServiceResult<int> result = new ServiceResult<int>();
            EUser user = userServices.List().Data.Where(x => x.Id == RiddhaSession.CurrentUser.Id).FirstOrDefault();
            if (user.Name == vm.UserName && user.Password == vm.CurrentPassword)
            {
                user.Name = vm.UserName;
                user.Password = vm.NewPassword;
                userServices.Update(user);
                result = new ServiceResult<int>()
                {
                    Data = 1,
                    Message = "Changed Successfully",
                    Status = ResultStatus.Ok
                };
            }
            else
            {
                result = new ServiceResult<int>()
                {
                    Data = 0,
                    Message = "Invalid User or Password",
                    Status = ResultStatus.processError
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            if (user.Password == vm.NewPassword && user.Password == vm.ConfirmPassword)
            {
                user.Password = vm.NewPassword;
                user.Password = vm.ConfirmPassword;
            }
            else
            {
                result = new ServiceResult<int>()
                {
                    Data = 0,
                    Message = "Password do not match!!!",
                    Status = ResultStatus.processError
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        private void SetCulture(string culture)
        {
            string language = CultureHelper.GetImplementedCulture(culture);
            CultureInfo ci = CultureInfo.GetCultureInfo(language);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }
        public ActionResult Captcha()
        {
            CaptchaHelper captchaHelper = new CaptchaHelper();
            return File(captchaHelper.DrawByte(), "image/jpeg");
        }
        [HttpGet]
        public ActionResult Owner()
        {
            ViewBag.Message = "";
            return PartialView("_OwnerLogin");
        }
        [HttpPost]
        public ActionResult Owner(string UserName, string Password)
        {
            var user = userServices.List().Data;
            if (user.FirstOrDefault() == null)
            {
                userServices.Add(new EUser() { BranchId = null, FullName = "Admin", IsDeleted = false, IsSuspended = false, Name = "Admin", Password = "Adm!n123", RoleId = null, UserType = UserType.Owner });
                ViewBag.Message = "First Login Success Username is Admin and Password is Adm!n123";
                return PartialView("_OwnerLogin");
            }
            else
            {
                var userData = user.Where(x => x.UserType == UserType.Owner && x.Name.ToLower() == UserName.Trim().ToLower() && x.Password == Password).FirstOrDefault();
                if (userData != null)
                {
                    var context = UpdateSession(userData);
                    RiddhaSession.CurrentToken = context.Token;
                    RiddhaSession.CurDate = DateTime.Now.ToFormatedString();
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                else
                {
                    ViewBag.Message = "Invalid Username or Password";
                    return PartialView("_OwnerLogin");
                }
            }
        }
        //[HttpGet]
        //public ActionResult LockScreen()
        //{
        //    RiddhaSession.CurrentToken = "";
        //    var user = new SUser().List().Data.Where(x => x.Id == RiddhaSession.UserId).FirstOrDefault() ?? new EUser();
        //    LockScreenViewModel vm = new LockScreenViewModel()
        //    {
        //        img = user.PhotoURL,
        //        UserName = user.Name,
        //        FullName = user.FullName
        //    };

        //    return View("_LockScreen", vm);
        //}
        //[HttpPost]
        //public ActionResult LockScreen(int UserId, string Password)
        //{
        //    var user = userServices.List().Data.Where(x => x.Id == UserId).FirstOrDefault();
        //    if (user != null)
        //    {
        //        if (user.Password == Password)
        //        {
        //            var requestedUrl = RiddhaSession.RequestUrl;

        //            var context = UpdateSession(user);
        //            RiddhaSession.CurrentToken = context.Token;
        //            var url = requestedUrl.Split('/');
        //            if (url.Length >= 3)
        //            {
        //                string area = url[1];
        //                string controller = url[2];
        //                string action = "Index";

        //                if (url.Count() == 4)
        //                {
        //                    action = url[3];
        //                }
        //                return RedirectToAction(action, controller, new { area = area });
        //            }
        //            else
        //            {
        //                return RedirectToAction("index", "home", new { area = "" });
        //            }
        //        }
        //    }
        //    return RedirectToAction("LockScreen");
        //}
        public class UserViewModel
        {
            public string CompanyCode { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Captcha { get; set; }
        }
        public class ChangePasswordViewModel
        {
            public string UserName { get; set; }
            public string CurrentPassword { get; set; }
            public string NewPassword { get; set; }
            public string ConfirmPassword { get; set; }
        }
    }
    public class LockScreenViewModel
    {
        /* preserve*/
        #region for preserve
        public int UserId { get { return RiddhaSession.UserId; } }
        public string RequestUrl { get { return RiddhaSession.RequestUrl; } }

        #endregion


        public string img { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
    }
}