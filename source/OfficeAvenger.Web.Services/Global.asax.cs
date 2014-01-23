using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using OfficeAvenger.Services.Logging;
using OfficeAvenger.Web.Security;

namespace OfficeAvenger.Web.Services
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_AuthenticateRequest()
        {
            this.Log().Info(() => "Authenticating auth token");
            Shield.AuthenticateRequest();
        }

        protected void Application_BeginRequest()
        {
            this.Log().Info(() => "Begin Request");
        }

        protected void Application_EndRequest()
        {
            this.Log().Info(() => "End Request");
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            NinjectHttpContainer.RegisterModules(NinjectHttpModules.Modules);
            SecurityConfig.Register();
            JsonConfig.Register();
        }
    }
}