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
        public IEnumerable<Avenger> Get()
        {
            return this.DataService.GetAvengers(Shield.ActiveAgent.Id).GoBabyGo();
        }

        public Avenger Post(Avenger model)
        {
            return this.DataService.UpdateAvenger(model, Shield.ActiveAgent.Id).GoBabyGo();
        }
    }
}