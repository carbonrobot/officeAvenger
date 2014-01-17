using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeAvenger.Domain;

namespace OfficeAvenger.Web.Security
{
    public class LazySecurityMatrix : ISecurityMatrix
    {
        public AuthenticationResult Authenticate(string username, string password)
        {
            if (string.Equals(username, "comic", StringComparison.InvariantCulture)
                && string.Equals(password, "superstar", StringComparison.InvariantCulture))
            {
                return new AuthenticationResult(new Agent() { Name = "Agent 87", Username = username });
            }
            else
            {
                return AuthenticationResult.Failure;
            }
        }
    }
}
