using System.Web.Mvc;

namespace RTech.Demo.Areas.MockTest
{
    public class MockTestAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MockTest";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MockTest_default",
                "MockTest/{controller}/{action}/{id}",
                new {AreaName="MockTest", Controller= "MockTestHome", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}