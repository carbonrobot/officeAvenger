namespace OfficeAvenger.Web.Controllers.Api
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Domain;
    using Security;

    [Authorize]
    public class TeamController : DataApiController
    {
        // api/team
        [HttpGet]
        public IEnumerable<Avenger> Get()
        {
            return this.DataService.GetAvengers(Shield.ActiveAgent.Id).GoBabyGo();
        }

        // api/team
        [HttpPost]
        public Avenger Post(Avenger model)
        {
            return this.DataService.UpdateAvenger(model, Shield.ActiveAgent.Id).GoBabyGo();
        }

        // api/team/delete/16
        [HttpPost]
        public void Delete(int id)
        {
            this.DataService.DeleteAvenger(id, Shield.ActiveAgent.Id).GoBabyGo();
        }
    }
}