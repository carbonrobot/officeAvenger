using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using OfficeAvenger.Services;

namespace OfficeAvenger.Web.Areas.Razor.Controllers
{
    public class UnderworldController : Controller
    {
        [Inject]
        public DataService DataService { get; set; }

        public ActionResult WithError(ActionResult result, string message)
        {
            TempData[UI_ERROR_KEY] = message;
            return result;
        }

        public ActionResult WithInfo(ActionResult result, string message)
        {
            TempData[UI_INFO_KEY] = message;
            return result;
        }

        public ActionResult WithSuccess(ActionResult result, string message)
        {
            TempData[UI_SUCCESS_KEY] = message;
            return result;
        }

        public const string UI_ERROR_KEY = "UI_ERROR";
        public const string UI_INFO_KEY = "UI_INFO";
        public const string UI_SUCCESS_KEY = "UI_SUCCESS";
    }
}
