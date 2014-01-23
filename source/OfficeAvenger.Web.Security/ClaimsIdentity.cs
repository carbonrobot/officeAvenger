using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OfficeAvenger.Web.Security
{
    public class ClaimsIdentity : IIdentity
    {
        public ClaimsIdentity() { }
        public ClaimsIdentity(string username, string type)
        {
            this.Name = username;
            this.AuthenticationType = type;
            this.IsAuthenticated = true;
        }

        public string AuthenticationType { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public string Name { get; private set; }
    }
}
