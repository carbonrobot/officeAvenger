using System.Web.Mvc;

namespace OfficeAvenger.Web.Areas.Razor
{
    public class RazorAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Razor";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Razor_default",
                "Razor/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
