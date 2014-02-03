namespace OfficeAvenger.Web.Controllers.Api
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Net;
    using System.Net.Http;
    using Domain;
    using Security;
    using Services;

    [Authorize]
    public class MissionController : DataApiController
    {
        public IEnumerable<Mission> Get()
        {
            return this.DataService.GetActiveMissions(Shield.ActiveAgent.Id).GoBabyGo();
        }

        public Mission Post(Mission model)
        {
            return this.DataService.UpdateMission(model, Shield.ActiveAgent.Id).GoBabyGo();
        }

        [HttpPost]
        public Mission Start(int id)
        {
            return this.DataService.BeginMission(id, Shield.ActiveAgent.Id).GoBabyGo();
        }

        
    }

}