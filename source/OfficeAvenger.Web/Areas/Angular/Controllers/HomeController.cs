using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeAvenger.Web.Controllers;

namespace OfficeAvenger.Web.Areas.Angular.Controllers
{
    public class HomeController : UnderworldController
    {
        // GET: /Angular/Home/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var authenticated = OfficeAvenger.Web.Security.Handlers.FormsAuthentication.Authenticate(username, password, true);
            if (!authenticated)
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