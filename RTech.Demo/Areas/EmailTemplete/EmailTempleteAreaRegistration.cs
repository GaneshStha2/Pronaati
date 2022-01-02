using System.Web.Mvc;

namespace RTech.Demo.Areas.EmailTemplete
{
    public class EmailTempleteAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EmailTemplete";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EmailTemplete_default",
                "EmailTemplete/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}