using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OfficeAvenger.Web.Areas.Razor.Models;
using OfficeAvenger.Web.Controllers;
using OfficeAvenger.Web.Security;

namespace OfficeAvenger.Web.Areas.Knockout.Controllers
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
            var authenticated = Shield.Authenticate(username, password, true);
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
            return RedirectToAction("Index");
        }
    }
}
