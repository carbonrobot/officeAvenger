using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OfficeAvenger.Web.Areas.Razor.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var authenticated = OfficeAvenger.Web.Security.Shield.Authenticate(username, password, true);
            if (!authenticated)
            {
                TempData["LOGIN_ERROR"] = "Invalid. Do not fail again.";
            }
            return new RedirectResult("/Razor");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            OfficeAvenger.Web.Security.Shield.Signout();
            return new RedirectResult("/Razor");
        }

    }
}
