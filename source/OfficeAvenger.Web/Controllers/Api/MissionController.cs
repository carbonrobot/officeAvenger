namespace OfficeAvenger.Web.Controllers.Api
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Net;
    using System.Net.Http;
    using Domain;
    using Models;
    using Security;
    using Services;

    [Authorize]
    public class MissionController : DataApiController
    {
        // api/mission
        [HttpGet]
        public IEnumerable<Mission> Get()
        {
            return this.DataService.GetActiveMissions(Shield.ActiveAgent.Id).GoBabyGo();
        }

        // api/mission/12
        [HttpGet]
        public Mission Get(int id)
        {
            return this.DataService.GetMission(id, Shield.ActiveAgent.Id).GoBabyGo();
        }

        // api/mission/update
        [HttpPost]
        public Mission Update(Mission model)
        {
            return this.DataService.UpdateMission(model, Shield.ActiveAgent.Id).GoBabyGo();
        }

        // api/mission/assign
        [HttpPost]
        public void Assign(AssignAvengerRequest request)
        {
            this.DataService.AssignAvenger(request.MissionId, request.AvengerId, Shield.ActiveAgent.Id).GoBabyGo();
        }

        // api/mission/remove
        [HttpPost]
        public void Remove(AssignAvengerRequest request)
        {
            this.DataService.RemoveAvenger(request.MissionId, request.AvengerId, Shield.ActiveAgent.Id).GoBabyGo();
        }

        // api/mission/delete/12
        [HttpDelete]
        public void Delete(int id)
        {
            this.DataService.DeleteMission(id, Shield.ActiveAgent.Id).GoBabyGo();
        }

        // api/mission/start/17
        [HttpPost]
        public Mission Start(int id)
        {
            return this.DataService.BeginMission(id, Shield.ActiveAgent.Id).GoBabyGo();
        }
    }

}