using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using OfficeAvenger.Domain;

namespace OfficeAvenger.Web.Security
{
    public class ClaimsIdentity : IIdentity
    {
        public ClaimsIdentity() { }
        public ClaimsIdentity(Agent agent, string type)
        {
            this.Agent = agent;
            this.AuthenticationType = type;
            this.IsAuthenticated = agent != null;
        }

        public string AuthenticationType { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public string Name
        {
            get
            {
                return this.Agent != null ? this.Agent.Name : "";
            }
        }

        public Agent Agent { get; private set; }
    }
}
