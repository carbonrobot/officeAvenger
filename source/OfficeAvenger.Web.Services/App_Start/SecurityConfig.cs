using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeAvenger.Web.Services
{
    public static class SecurityConfig
    {
        public static void Register()
        {
            OfficeAvenger.Web.Security.Shield.ConfigureWith(new OfficeAvenger.Web.Security.LazySecurityMatrix());
        }
    }
}