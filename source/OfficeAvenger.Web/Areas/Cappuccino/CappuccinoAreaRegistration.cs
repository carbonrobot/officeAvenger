using System.Web.Mvc;

namespace OfficeAvenger.Web.Areas.Cappuccino
{
    public class CappuccinoAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Cappuccino";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Cappuccino_default",
                "Cappuccino/{controller}/{action}",
                new { controller = "Home", action = "Index" }
            );
        }
    }
}
