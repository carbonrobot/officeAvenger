using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace OfficeAvenger.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/foundation/css").Include(
                       "~/Content/css/foundation/foundation.css",
                       "~/Content/css/foundation/foundation.mvc.css",
                       "~/Content/css/app.css"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                .Include("~/Scripts/vendor/modernizr.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery", "//code.jquery.com/jquery-2.0.3.min.js")
                .Include("~/Scripts/vendor/jquery.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/application").Include(
                        "~/Scripts/vendor/foundation/foundation.js",
                        "~/Scripts/vendor/foundation/foundation.topbar.js",
                        "~/Scripts/vendor/foundation/foundation.alert.js"));
        }
    }
}