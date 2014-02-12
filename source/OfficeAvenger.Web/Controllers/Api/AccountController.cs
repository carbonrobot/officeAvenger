using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OfficeAvenger.Web.Models;

namespace OfficeAvenger.Web.Controllers.Api
{
    public class AccountController : DataApiController
    {
        [HttpPost]
        [Route("api/login")]
        public HttpResponseMessage Login(LoginCredentials creds)
        {
            var agent = OfficeAvenger.Web.Security.Handlers.FormsAuthentication.Authenticate(creds.Username, creds.Password, true);
            if (agent == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.OK, new
                {
                    HasError = true,
                    Error = "Incorrect username or password"
                });
            }

            // success
            return CreateAuthResponse();
        }

        [HttpGet]
        [Route("api/logout")]
        public void Logout()
        {
            OfficeAvenger.Web.Security.Handlers.FormsAuthentication.Signout();
        }
        
        [HttpGet]
        [Authorize]
        [Route("api/activeagent")]
        public HttpResponseMessage Login()
        {
            return CreateAuthResponse();
        }

        private HttpResponseMessage CreateAuthResponse()
        {
            return this.Request.CreateResponse(HttpStatusCode.OK, new { 
                Name = OfficeAvenger.Web.Security.Shield.ActiveAgent.Name, 
                Username = OfficeAvenger.Web.Security.Shield.ActiveAgent.Username
            });
        }
    }
}
