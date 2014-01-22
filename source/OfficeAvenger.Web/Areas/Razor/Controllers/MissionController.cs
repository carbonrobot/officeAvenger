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

            var avengersResponse = this.DataService.GetAvengers(Shield.ActiveAgent.Id);
            if (avengersResponse.HasError)
                ShowError("Failed to retrieve the list of Avengers");

            var model = new EditMissionViewModel(response.Result, avengersResponse.Result);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditMissionViewModel model)
        {
            var response = this.DataService.UpdateMission(model.Mission, Shield.ActiveAgent.Id);
            if (response.HasError)
                return WithError(View(model), "Do not submit invalid information!");

            return WithSuccess(RedirectToAction("Edit", new { id = response.Result.Id }), "Mission profile has been updated.");
        }

        [HttpPost]
        public ActionResult Assign(int missionId, int avengerId)
        {
            var response = this.DataService.AssignAvenger(missionId, avengerId, Shield.ActiveAgent.Id);
            
            var action = RedirectToAction("Edit", new { id = missionId });
            if (response.HasError)
                return WithError(action, "Do not submit invalid information!");

            return WithSuccess(action, "Avenger has been assigned to support this mission");
        }

        [HttpPost]
        public ActionResult Remove(int missionId, int avengerId)
        {
            var response = this.DataService.RemoveAvenger(missionId, avengerId, Shield.ActiveAgent.Id);

            var action = RedirectToAction("Edit", new { id = missionId });
            if (response.HasError)
                return WithError(action, "Do not submit invalid information!");

            return WithSuccess(action, "Avenger has been removed from this mission");
        }

        [HttpPost]
        public ActionResult Start(int missionId)
        {
            var response = this.DataService.BeginMission(missionId, Shield.ActiveAgent.Id);
            if (response.HasError)
                ShowError("Unable to begin mission!");

            return RedirectToAction("Index", "Home");
        }
    }
}
