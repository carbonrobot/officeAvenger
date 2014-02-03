﻿namespace OfficeAvenger.Web.Controllers.Api
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
            var response = this.DataService.GetAvengers(Shield.ActiveAgent.Id);
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