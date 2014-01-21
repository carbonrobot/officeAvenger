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
    public class MissionController : UnderworldController
    {
        [HttpGet]
        public ActionResult Add()
        {
            var model = new EditMissionViewModel(new Mission()
            {
                AgentId = Shield.ActiveAgent.Id
            });
            return View("Edit", model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var response = this.DataService.GetMission(id, Shield.ActiveAgent.Id);
            if (response.HasError)
                return WithError(this.RedirectToAction("Index", "Home"), "Failed to retrieve the specified Avenger");

            var model = new EditMissionViewModel(response.Result);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditMissionViewModel model)
        {
            var response = this.DataService.UpdateMission(model.Mission, Shield.ActiveAgent.Id);
            if (response.HasError)
                return WithError(View(model), "Do not submit invalid information!");

            return WithSuccess(RedirectToAction("Edit", new { id = response.Result.Id }), "Mission profile has been updated.");
            //return WithSuccess(RedirectToAction("Index", "Home"), "Ready to {0}".FormatWith(model.Mission.Name));
        }

    }
}
