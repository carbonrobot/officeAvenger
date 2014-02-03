namespace OfficeAvenger.Web.Controllers.Api
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Net;
    using System.Net.Http;
    using Domain;
    using Security;

    [Authorize]
    public class MissionController : DataApiController
    {
        public IEnumerable<Mission> Get()
        {
            var response = this.DataService.GetActiveMissions(Shield.ActiveAgent.Id);
            if (response.HasError)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(response.Exception.ToString())
                });
            }

            return response.Result;
        }
    }
}