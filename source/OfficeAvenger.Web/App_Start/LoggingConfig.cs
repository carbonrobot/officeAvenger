using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using StackExchange.Profiling;
using StackExchange.Profiling.MVCHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeAvenger.Services.Logging;

namespace OfficeAvenger.Web
{
    public static class LoggingConfig
    {
        public static void InitializeLogger()
        {
            // start ef logging to miniprofiler
            //MiniProfilerEF.InitializeEF42();

            // configure global logging to use nlog
            Log.InitializeWith<NLogLog>();
        }
    }
}