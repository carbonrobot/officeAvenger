using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OfficeAvenger.Web.Areas.Razor.Models;
using OfficeAvenger.Web.Controllers;
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
                // active avengers for this agent
                var heroResponse = this.DataService.GetAvengers(Shield.ActiveAgent.Id);
                if (heroResponse.HasError)
                {
                    return WithError(View(model), "Unable to access team member information");
                }
                else
                {
                    model.Avengers = heroResponse.Result;
                }

                // active missions for this agent
                var missionResponse = this.DataService.GetActiveMissions(Shield.ActiveAgent.Id);
                if (missionResponse.HasError)
                {
                    return WithError(View(model), "Unable to access mission portfolio");
                }
                else
                {
                    model.Missions = missionResponse.Result;
                }
            }
            return View(model);
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
