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
                "Razor_action",
                "Razor/{controller}/{id}/{action}",
                null,
                new { id = @"\d+" }
            );

            context.MapRoute(
                "Razor_default",
                "Razor/{controller}/{action}",
                new { controller = "Home", action = "Index" }
            );
        }
    }
}
