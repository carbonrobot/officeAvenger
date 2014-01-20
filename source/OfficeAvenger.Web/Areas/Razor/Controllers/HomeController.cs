using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OfficeAvenger.Web.Areas.Razor.Controllers
{
    public class HomeController : UnderworldController
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
                return WithError(this.RedirectToAction("Index"), "Incorrect. Do not fail again.");
            }
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            OfficeAvenger.Web.Security.Shield.Signout();
            return new RedirectResult("/Razor");
        }

    }
}
