using System;
using System.Linq;
using System.Security.Principal;

namespace OfficeAvenger.Web.Security
{
    public interface IAuthentication
    {
        IPrincipal Authenticate();
    }
}