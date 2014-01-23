using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using OfficeAvenger.Web.Security.Authority;

namespace OfficeAvenger.Web.Security.Handlers
{
    public class TokenAuthentication : IAuthentication
    {
        public TokenAuthentication(ISecurityMatrix matrix)
        {
            this.SecurityMatrix = matrix;
        }

        public IPrincipal Authenticate()
        {
            if (SecurityMatrix == null)
                throw new InvalidOperationException("Security matrix must be configured by calling ConfigureWith() prior to use.");

            var token = HttpContext.Current.Request.Headers["Authorization"];
            var result = SecurityMatrix.AuthenticateToken(token);
            if (result.Success)
            {
                var identity = new ClaimsIdentity(result.Agent, "Token");
                return new ClaimsPrincipal(identity);
            }
                
            return null;
        }
        
        private ISecurityMatrix SecurityMatrix;
    }
}
