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
                        "~/Scripts/vendor/foundation/foundation.reveal.js",
                        "~/Scripts/vendor/foundation/foundation.alert.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                        "~/Scripts/vendor/knockout-3.0.0.js",
                        "~/Scripts/vendor/knockout.mapping.js",
                        "~/Scripts/vendor/moment.js",
                        "~/Scripts/areas/knockout/app.js",
                        "~/Scripts/areas/knockout/app-common.js",
                        "~/Scripts/areas/knockout/app-team.js",
                        "~/Scripts/areas/knockout/app-mission.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/vendor/angular.js",
                        "~/Scripts/vendor/moment.js",
                        "~/Scripts/areas/angular/app.js"
                        ));
        }
    }
}