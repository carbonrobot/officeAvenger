using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using OfficeAvenger.Services.Logging;
using OfficeAvenger.Web.Security;
using StackExchange.Profiling;

namespace OfficeAvenger.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_AuthenticateRequest()
        {
            this.Log().Info(() => "Application_AuthenticateRequest");
            Shield.AuthenticateRequest();
        }

        protected void Application_Start()
        {
            LoggingConfig.InitializeLogger();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
			JsonConfig.Register();
            SecurityConfig.Register();
        }

        protected void Application_BeginRequest()
        {
            this.Log().Info(() => "Begin Request");
            MiniProfiler.Start();
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
            this.Log().Info(() => "End Request");
        }
    }
}
