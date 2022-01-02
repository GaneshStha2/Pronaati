using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity.Migrations;
using RTech.Demo.Migrations;
using System.Web.SessionState;
using System.Globalization;
using System.Threading;
using RTech.Demo.Utilities;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using RTech.Demo.Common;
namespace RTech.Demo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }
        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/api");
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DbMigration();
            configureCulture();
        }
        private static void DbMigration()
        {
            var configuration = new Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }
        public static void configureCulture()
        {
            CultureInfo newCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            newCulture.DateTimeFormat.DateSeparator = "/";
            newCulture.DateTimeFormat.TimeSeparator = ":";
            Thread.CurrentThread.CurrentCulture = newCulture;
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            var lastErrorWrapper = Server.GetLastError();
            if (lastErrorWrapper != null)
                RTech.Demo.Common.Log.SytemLog("Message=" + lastErrorWrapper.Message + ", Stack Trace= " + lastErrorWrapper.StackTrace);
        }
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            configureCulture();
        }
    }
}
