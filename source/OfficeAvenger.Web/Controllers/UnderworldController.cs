using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using OfficeAvenger.Services;

namespace OfficeAvenger.Web.Controllers
{
    public class UnderworldController : Controller
    {
        [Inject]
        public DataService DataService { get; set; }

        public ActionResult WithError(ActionResult result, string message)
        {
            ShowError(message);
            return result;
        }

        public ActionResult WithInfo(ActionResult result, string message)
        {
            ShowInfo(message);
            return result;
        }

        public ActionResult WithSuccess(ActionResult result, string message)
        {
            ShowSuccess(message);
            return result;
        }

        public void ShowError(string message)
        {
            TempData[UI_ERROR_KEY] = message;
        }

        public void ShowInfo(string message)
        {
            TempData[UI_INFO_KEY] = message;
        }

        public void ShowSuccess(string message)
        {
            TempData[UI_SUCCESS_KEY] = message;
        }

        public const string UI_ERROR_KEY = "UI_ERROR";
        public const string UI_INFO_KEY = "UI_INFO";
        public const string UI_SUCCESS_KEY = "UI_SUCCESS";
    }
}
