using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OfficeAvenger.Web.Security.Authority
{
    public class ClaimsPrincipal : IPrincipal
    {
        public ClaimsPrincipal() { }
        public ClaimsPrincipal(IIdentity identity, params string[] authorizations)
        {
            this.Identity = identity;
            this.Authorizations = authorizations;
        }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return this.Authorizations.Contains(role, StringComparer.InvariantCultureIgnoreCase);
        }

        public string[] Authorizations { get; private set; }
    }
}
