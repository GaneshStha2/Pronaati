using RTech.Demo.Areas.Student.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Common
{
    public class StudentSession
    {
        public static LoggedStudentViewModel LoggedStudent
        {
            get
            {
                return GetSession("LoggedStudent") as LoggedStudentViewModel;
            }
            set
            {
                SetSession("LoggedStudent", value);
            }
        }

        public static string FromUrl
        {
            get
            {
                return (GetSession("FromUrl") ?? "").ToString();
            }
            set
            {
                SetSession("FromUrl", value);
            }
        }

        public static string CourseCode
        {

            get
            {
                return (GetSession("CourseCode") ?? "").ToString();
            }
            set
            {
                SetSession("CourseCode", value);
            }
        }

        public static string PasswordResetEmail
        {
            get
            {
                return (GetSession("PasswordResetEmail") ?? "").ToString();
            }
            set
            {
                SetSession("PasswordResetEmail", value);
            }
        }

        public static string PasswordResetSecurityCode
        {
            get
            {
                return (GetSession("PasswordResetSecurityCode") ?? "").ToString();
            }
            set
            {
                SetSession("PasswordResetSecurityCode", value);
            }
        }
        public static int CourseId
        {
            get
            {
                return (int)GetSession("CourseId");
            }
            set
            {
                SetSession("CourseId", value);
            }
        }

        private static object GetSession(string key)
        {
            return HttpContext.Current.Session[key];
        }
        private static void SetSession(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }


    }
}