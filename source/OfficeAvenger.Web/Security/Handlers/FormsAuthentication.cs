using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using OfficeAvenger.Domain;
using OfficeAvenger.Web.Security.Authority;

namespace OfficeAvenger.Web.Security.Handlers
{
    public class FormsAuthentication : IAuthentication
    {
        private static ISecurityMatrix SecurityMatrix;
        
        public FormsAuthentication(ISecurityMatrix matrix)
        {
            SecurityMatrix = matrix;
        }

        public IPrincipal Authenticate()
        {
            if (SecurityMatrix == null)
                throw new InvalidOperationException("Security matrix must be configured by calling ConfigureWith() prior to use.");

            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    // Resolve the agent from the forms authentication cookie
                    // TODO: Should we really store it inside the cookie, is it safe??
                    var cookie = HttpContext.Current.Request.Cookies[System.Web.Security.FormsAuthentication.FormsCookieName];
                    if (cookie != null)
                    {
                        var ticket = System.Web.Security.FormsAuthentication.Decrypt(cookie.Value);
                        if (ticket != null)
                        {
                            var agent = JsonConvert.DeserializeObject<Agent>(ticket.UserData);
                            var identity = new ClaimsIdentity(agent, "Forms");
                            return new ClaimsPrincipal(identity);
                        }
                    }
                }
            }

            return null;
        }
        
        /// <summary>
        /// Sign the agent out of the matrix
        /// </summary>
        public static void Signout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
        }

        /// <summary>
        /// Authenticates the current agent. If successful, creates an encrypted security token storing the agents basic information.
        /// </summary>
        public static Agent Authenticate(string username, string password, bool persistent = false)
        {
            if (SecurityMatrix == null)
                throw new InvalidOperationException("Security matrix must be configured first");

            var result = SecurityMatrix.Authenticate(username, password);
            if (result.Success)
            {
                // get an authentication cookie we can use as a template
                var cookie = System.Web.Security.FormsAuthentication.GetAuthCookie(username, persistent);
                var ticket = System.Web.Security.FormsAuthentication.Decrypt(cookie.Value);

                // create a custom ticket so we can put our agent data on it
                var newTicket = new System.Web.Security.FormsAuthenticationTicket(
                    ticket.Version,
                    ticket.Name,
                    ticket.IssueDate,
                    ticket.Expiration,
                    ticket.IsPersistent,
                    JsonConvert.SerializeObject(result.Agent),
                    ticket.CookiePath);

                // encrypt it up
                var encTicket = System.Web.Security.FormsAuthentication.Encrypt(newTicket);
                cookie.Value = encTicket;
                HttpContext.Current.Response.Cookies.Add(cookie);

                // wire up the bizness
                var user = new ClaimsPrincipal(new ClaimsIdentity(result.Agent, "Forms"));
                HttpContext.Current.User = user;
                System.Threading.Thread.CurrentPrincipal = user;

                return result.Agent;
            }
            else
            {
                return null;
            }
        }


    }
}
