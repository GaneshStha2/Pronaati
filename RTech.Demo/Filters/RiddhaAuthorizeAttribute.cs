using Riddhasoft.Entity.User;
using Riddhasoft.Services.User;
using Riddhasoft.User.Entity;
using RTech.Demo.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RTech.Demo.Filters
{
    public class RiddhaMVCAuthorizeAttribute : AuthorizeAttribute
    {
        bool IsStudent = false;
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            bool authorize = false;
            string controllerName = httpContext.Request.RequestContext.RouteData.GetRequiredString("controller");
            string actionName = httpContext.Request.RequestContext.RouteData.GetRequiredString("action");
            string areaName = (httpContext.Request.RequestContext.RouteData.DataTokens["area"] ?? "").ToString();


            if (controllerName.ToLower() == "user" && (actionName.ToLower() == "login" || actionName.ToLower() == "logout" || actionName.ToLower() == "changepassword" || actionName.ToLower() == "lockscreen" || actionName.ToLower() == "owner" || actionName.ToLower() == "captcha"))
            {
                return true;
            }
            if (controllerName.ToLower() == "home" && actionName.ToLower() == "nopermission" || actionName == "pteindex" || controllerName.ToLower() == "fileupload" || (areaName.ToLower() == "mobile" && controllerName.ToLower() == "initialize") || controllerName.ToLower() == "qrcode" || (controllerName.ToLower() == "home" && actionName.ToLower() == "student"))
            {
                return true;
            }
            if ((controllerName.ToLower() == "home" && actionName.ToLower() == "pteindex") || (controllerName.ToLower() == "pagenotfound"))
            {

                return true;
            }
            if ((controllerName.ToLower() == "payment" && actionName.ToLower() == "index"))
            {

                return true;
            }
            if ((areaName.ToLower() == "emailtemplete"))
            {

                return true;
            }
            if ((controllerName.ToLower() == "topbar"))
            {

                return true;
            }


            if (areaName.ToLower() == "mocktest" || areaName.ToLower() == "student")
            {

                if ((controllerName.ToLower() == "login" && actionName.ToLower() == "index" || actionName.ToLower() == "forgotpassword") || (controllerName.ToLower() == "signup" && actionName.ToLower() == "index") || controllerName.ToLower() == "dashboard")
                {

                    return true;
                }

                if ((controllerName.ToLower() == "course" && actionName.ToLower() == "courselist" || actionName.ToLower() == "onlinetrainiglist")
                    || (controllerName.ToLower() == "dashboard" && actionName.ToLower() == "moctTestpackages" || actionName.ToLower() == "logout") || (controllerName.ToLower() == "blog" && actionName.ToLower() == "blog")
                     || (controllerName.ToLower() == "contactus" && actionName.ToLower() == "contactus") || (controllerName.ToLower() == "login" && actionName.ToLower() == "forgotpassword" || actionName.ToLower() == "securitycode" || actionName.ToLower() == "passwordreset"))
                {
                    return true;
                }
                if (Common.StudentSession.LoggedStudent == null)
                {

                    IsStudent = true;
                    return false;
                }

                if (Common.StudentSession.LoggedStudent != null)
                {

                    return true;
                }

            }
            EUser user = RiddhaSession.CurrentUser;
            if (user == null)
            {
                return false;
            }
            if (user.UserType == UserType.Owner)
            {
                return true;
            }
            else
            {
                authorize = userAuth(httpContext, controllerName, areaName);
            }
            SetCulture();
            return authorize;
        }
        private void SetCulture()
        {
            string language = CultureHelper.GetImplementedCulture(RiddhaSession.Language);
            CultureInfo ci = CultureInfo.GetCultureInfo(language);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }
        private bool userAuth(HttpContextBase httpContext, string controllerName, string areaName)
        {
            SUserRole userRoleServices = new SUserRole();
            int roleId = RiddhaSession.RoleId;
            if (roleId == 0)
            {
                return true;
            }
            var menuRights = userRoleServices.ListMenuRights().Data;
            string url = "/" + areaName.ToLower() + "/" + controllerName.ToLower();
            var requestedMenu = new SUserRole().ListMenus().Data.Where(x => x.MenuUrl.ToLower() == url).FirstOrDefault();
            return menuRights.Where(x => x.MenuId == requestedMenu.Id && x.RoleId == roleId).Any();
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (IsStudent)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "LogIn", area = "student" }));
            }
            else
            {
                var currentUser = RiddhaSession.CurrentUser;
                if (currentUser != null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "NoPermission", controller = "home", area = "" }));
                }
                else
                {

                    //request uri is not login 
                    if ((filterContext.HttpContext.Request.Url.LocalPath == "/User/User/Login" || filterContext.HttpContext.Request.Url.LocalPath == "/") || RiddhaSession.UserId == 0)
                    //then lock screen
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "login", controller = "user", area = "User" }));


                    }
                    //else
                    //lock screen
                    else
                    {
                        RiddhaSession.RequestUrl = filterContext.HttpContext.Request.Url.LocalPath;
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "lockscreen", controller = "user", area = "User" }));
                    }

                }
            }
        }
    }

}