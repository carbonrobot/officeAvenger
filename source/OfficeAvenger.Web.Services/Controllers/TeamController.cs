using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ninject;
using OfficeAvenger.Domain;
using OfficeAvenger.Services;
using OfficeAvenger.Web.Security;

namespace OfficeAvenger.Web.Services.Controllers
{
    public class TeamController : ApiController
    {
        
        public IEnumerable<Avenger> Get()
        {
            var response = this.DataService.GetAvengers(1);
            if (response.HasError)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(response.Exception.ToString())
                });
            }

            return response.Result;
        }

        [Inject]
        public DataService DataService { get; set; }
    }
}
