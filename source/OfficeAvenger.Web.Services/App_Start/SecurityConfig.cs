using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeAvenger.Web.Security;
using OfficeAvenger.Web.Security.Handlers;

namespace OfficeAvenger.Web.Services
{
    public static class SecurityConfig
    {
        public static void Register()
        {
            var matrix = new LazySecurityMatrix();
            Shield.Register(new FormsAuthentication(matrix));
            Shield.Register(new TokenAuthentication(matrix));
        }
    }
}