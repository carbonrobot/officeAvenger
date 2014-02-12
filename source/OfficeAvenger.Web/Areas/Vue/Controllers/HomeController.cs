using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OfficeAvenger.Web.Areas.Razor.Models;
using OfficeAvenger.Web.Controllers;
using OfficeAvenger.Web.Security;

namespace OfficeAvenger.Web.Areas.Vue.Controllers
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
            var agent = OfficeAvenger.Web.Security.Handlers.FormsAuthentication.Authenticate(username, password, true);
            if (agent == null)
            {
                return WithError(this.RedirectToAction("Index"), "Incorrect. Do not fail again.");
            }
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            OfficeAvenger.Web.Security.Handlers.FormsAuthentication.Signout();
            return RedirectToAction("Index");
        }
    }
}
