using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OfficeAvenger.Web.Areas.Razor.Models;
using OfficeAvenger.Web.Security;

namespace OfficeAvenger.Web.Areas.Razor.Controllers
{
    public class HomeController : UnderworldController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = new HomeViewModel();
            if (User.Identity.IsAuthenticated)
            {
                var response = this.DataService.GetAvengers(Shield.ActiveAgent.Id);
                if (response.HasError)
                {
                    return WithError(View(model), "Unable to access agent profile");
                }
                else
                {
                    model.Avengers = response.Result;
                }
            }
            return View(model);
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
            return new RedirectResult("/Razor");
        }

    }
}
