using System.Web.Mvc;

namespace OfficeAvenger.Web.Areas.Angular
{
    public class AngularAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Angular";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Angular_default",
                "Angular/{controller}/{action}",
                new { controller = "Home", action = "Index" }
            );
        }
    }
}