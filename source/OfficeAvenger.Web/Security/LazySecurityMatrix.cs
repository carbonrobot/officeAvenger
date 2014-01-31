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
                return CreateFakeAgent();
            }
            else
            {
                return AuthenticationResult.Failure;
            }
        }

        private AuthenticationResult CreateFakeAgent()
        {
            return new AuthenticationResult(new Agent() { Id = 1, Name = "Agent 87", Username = "comic" });
        }


        public AuthenticationResult AuthenticateToken(string token)
        {
            if (token == "1234567890")
            {
                return CreateFakeAgent();
            }
            else
            {
                return AuthenticationResult.Failure;
            }
        }
    }
}
