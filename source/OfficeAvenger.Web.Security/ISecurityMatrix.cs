using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeAvenger.Web.Security
{
    public interface ISecurityMatrix
    {
        AuthenticationResult Authenticate(string username, string password);
        AuthenticationResult AuthenticateToken(string token);
    }
}
