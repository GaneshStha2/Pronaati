﻿using Riddhasoft.Entity.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace RTech.Demo.Utilities
{
    public class RiddhaSession
    {
        public static Riddhasoft.User.Entity.EUser CurrentUser
        {
            get
            {
                Riddhasoft.DB.RiddhaDBContext db = new Riddhasoft.DB.RiddhaDBContext();
                var data = db.Context.Where(x => x.Token == CurrentToken).FirstOrDefault() ?? new EContext();

                if (data.User != null)
                {
                    UserId = data.UserId;
                    DataVisibilityLevel = 0;
                    UserType = (int)data.User.UserType;
                    if (data.User.UserType == Riddhasoft.User.Entity.UserType.User)
                    {
                        BranchId = data.User.BranchId;
                        CompanyId = data.User.Branch.CompanyId;
                        RoleId = data.User.RoleId.ToInt();
                    }
                }
                return data.User;
            }
        }
        public static Riddhasoft.User.Entity.EUser GetUser()
        {

            Riddhasoft.DB.RiddhaDBContext db = new Riddhasoft.DB.RiddhaDBContext();
            var data = db.Context.Where(x => x.UserId == UserId).FirstOrDefault() ?? new EContext();
            return data.User;

        }
        private static HttpSessionState session { get { return HttpContext.Current.Session; } }
        public static string CurrentToken
        {
            get
            {
                return RiddhaSession.GetContextCookie("Token");
            }
            set { RiddhaSession.SetContextCookie("Token", value); }
        }

        /// <summary>
        /// cookie expires never
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        private static void SetCookie(string Key, string Value)
        {
            //int CookieLife =min==0? ConfigurationManager.AppSettings.Get("CookieLife").ToInt():min;
            HttpCookie myCookie = HttpContext.Current.Request.Cookies["Ehajiri"] ?? new HttpCookie("Ehajiri");
            myCookie.Values[Key] = Value;
            myCookie.Expires = System.DateTime.Now.AddYears(1);
            HttpContext.Current.Response.Cookies.Add(myCookie);
        }
        /// <summary>
        /// save context information, expire as per web setting
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        private static void SetContextCookie(string Key, string Value)
        {
            int CookieLife = ConfigurationManager.AppSettings.Get("CookieLife").ToInt();
            HttpCookie myCookie = HttpContext.Current.Request.Cookies["Context"] ?? new HttpCookie("Context");
            myCookie.Values[Key] = Value;
            myCookie.Expires = DateTime.Now.AddMinutes(CookieLife);
            HttpContext.Current.Response.Cookies.Add(myCookie);
        }
        public static int UserId
        {
            get
            {
                return RiddhaSession.GetCookie("UserId").ToInt();
            }
            set { RiddhaSession.SetCookie("UserId", value.ToString()); }
        }
        public static int? BranchId
        {
            get
            {
                return RiddhaSession.GetCookie("BranchId").ToInt();
            }
            set { RiddhaSession.SetCookie("BranchId", value.ToString()); }
        }
        public static int DepartmentId
        {
            get
            {
                return RiddhaSession.GetCookie("DepartmentId").ToInt();
            }
            set { RiddhaSession.SetCookie("DepartmentId", value.ToString()); }
        }
        public static int SectionId
        {
            get
            {
                return RiddhaSession.GetCookie("SectionId").ToInt();
            }
            set { RiddhaSession.SetCookie("SectionId", value.ToString()); }
        }
        public static int EmployeeId
        {
            get
            {
                return RiddhaSession.GetCookie("EmployeeId").ToInt();
            }
            set { RiddhaSession.SetCookie("EmployeeId", value.ToString()); }
        }
        public static int RoleId
        {
            get
            {
                return RiddhaSession.GetCookie("RoleId").ToInt();
            }
            set { RiddhaSession.SetCookie("RoleId", value.ToString()); }
        }
        public static int DataVisibilityLevel
        {
            get
            {
                return RiddhaSession.GetCookie("DataVisibilityLevel").ToInt();
            }
            set { RiddhaSession.SetCookie("DataVisibilityLevel", value.ToString()); }
        }
        public static int CompanyId
        {
            get
            {
                return RiddhaSession.GetCookie("CompanyId").ToInt();
            }
            set { RiddhaSession.SetCookie("CompanyId", value.ToString()); }
        }
        public static int FYId
        {
            get
            {
                return RiddhaSession.GetCookie("FYId").ToInt();
            }
            set { RiddhaSession.SetCookie("FYId", value.ToString()); }
        }
        public static int UserType
        {
            get
            {
                return RiddhaSession.GetCookie("UserType").ToInt();
            }
            set { RiddhaSession.SetCookie("UserType", value.ToString()); }
        }
        public static string CurDate
        {
            get
            {
                return (RiddhaSession.GetCookie("CurDate"));
            }
            set { RiddhaSession.SetCookie("CurDate", value.ToDateTime().ToFormatedString()); }
        }
        public static string RequestUrl
        {
            get
            {
                return RiddhaSession.GetCookie("RequestUrl") ?? "/";
            }
            set { RiddhaSession.SetCookie("RequestUrl", value); }
        }
        private static string GetCookie(string Key, int min = 0)
        {
            HttpCookie myCookie = HttpContext.Current.Request.Cookies["Ehajiri"];

            if (myCookie != null)
            {
                string value = (myCookie.Values[Key]);
                return value;
            }
            return null;
        }
        private static string GetContextCookie(string Key, int min = 0)
        {
            HttpCookie myCookie = HttpContext.Current.Request.Cookies["Context"];

            if (myCookie != null)
            {

                string value = (myCookie.Values[Key]);
                SetContextCookie(Key, value);
                return value;
            }
            return null;
        }
        public static string Language
        {
            get
            {
                var lang = RiddhaSession.GetCookie("Language");
                if (lang == null)
                {
                    RiddhaSession.SetCookie("Language", "en");
                    lang = "en";
                }
                return lang;
            }

            set { RiddhaSession.SetCookie("Language", value); }
        }
        public static string OperationDate
        {
            get
            {

                return RiddhaSession.GetCookie("OperationDate") ?? "";
            }

            set { RiddhaSession.SetCookie("OperationDate", value); }
        }
        internal static void Logout()
        {



            Riddhasoft.DB.RiddhaDBContext db = new Riddhasoft.DB.RiddhaDBContext();
            var data = db.Context.Where(x => x.UserId == UserId).FirstOrDefault() ?? new EContext();
            db.Context.Attach(data);
            db.Context.Remove(data);
            db.Entry(data).State = EntityState.Deleted;
            db.SaveChanges();
            CurrentToken = null;
            UserId = 0;
            BranchId = 0;
            UserType = 0;
            CompanyId = 0;


        }

        public static string Captcha
        {
            get
            {
                return RiddhaSession.GetCookie("Captcha");
            }

            set { RiddhaSession.SetCookie("Captcha", value); }
        }
        public static bool HasProRight { get { return true; } }

        public static int PackageId
        {
            get
            {
                return RiddhaSession.GetCookie("PackageId").ToInt();
            }

            set { RiddhaSession.SetCookie("PackageId", value.ToString()); }
        }
    }


}