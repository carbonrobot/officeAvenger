using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeAvenger.Domain;
using OfficeAvenger.Web.Areas.Razor.Models;
using OfficeAvenger.Web.Security;
using OfficeAvenger.Services.Logging;

namespace OfficeAvenger.Web.Areas.Razor.Controllers
{
    [Authorize]
    public class TeamController : UnderworldController
    {
        [HttpGet]
        public ActionResult Add()
        {
            var model = new EditTeamViewModel(new Avenger()
            {
                AgentId = Shield.ActiveAgent.Id
            });
            return View("Edit", model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var response = this.DataService.GetAvenger(id, Shield.ActiveAgent.Id);
            if (response.HasError)
                return WithError(this.RedirectToAction("Index", "Home"), "Failed to retrieve the specified Avenger");

            var model = new EditTeamViewModel(response.Result);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditTeamViewModel model)
        {
            var response = this.DataService.UpdateAvenger(model.Avenger, Shield.ActiveAgent.Id);
            if (response.HasError)
                return WithError(View(model), "Do not submit invalid information!");

            return WithSuccess(RedirectToAction("Index", "Home"), "{0} is now available for missions".FormatWith(model.Avenger.Name));
        }

    }
}
