using System.Web.Mvc;

namespace OfficeAvenger.Web.Areas.Knockout
{
    public class KnockoutAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Knockout";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Knockout_default",
                "Knockout/{controller}/{action}",
                new { controller = "Home", action = "Index" }
            );
        }
    }
}
