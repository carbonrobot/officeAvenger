using System.Web.Mvc;

namespace OfficeAvenger.Web.Areas.Vue
{
    public class VueAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Vue";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Vue_default",
                "Vue/{controller}/{action}",
                new { controller = "Home", action = "Index" }
            );
        }
    }
}
