using System;
using System.Threading;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using OfficeAvenger.Domain;

namespace OfficeAvenger.Web.Security
{
    /// <summary>
    /// Security Helpers
    /// </summary>
    public static class Shield
    {
        /// <summary>
        /// Gets the current authorized agent
        /// </summary>
        public static Agent ActiveAgent
        {
            get
            {
                if (HttpContext.Current.User == null)
                    return null;

                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return null;

                return ((ClaimsIdentity)HttpContext.Current.User.Identity).Agent;
            }
        }

        /// <summary>
        /// Authenticates the current agent. If successful, creates an encrypted security token storing the agents basic information.
        /// </summary>
        public static bool Authenticate(string username, string password, bool persistent = false)
        {
            if (SecurityMatrix == null)
                throw new InvalidOperationException("Security matrix must be configured by calling ConfigureWith() prior to use.");

            var result = SecurityMatrix.Authenticate(username, password);
            if (result.Success)
            {
                // get an authentication cookie we can use as a template
                var cookie = FormsAuthentication.GetAuthCookie(username, persistent);
                var ticket = FormsAuthentication.Decrypt(cookie.Value);

                // create a custom ticket so we can put our agent data on it
                var newTicket = new FormsAuthenticationTicket(
                    ticket.Version,
                    ticket.Name,
                    ticket.IssueDate,
                    ticket.Expiration,
                    ticket.IsPersistent,
                    JsonConvert.SerializeObject(result.Agent),
                    ticket.CookiePath);

                // encrypt it up
                var encTicket = FormsAuthentication.Encrypt(newTicket);
                cookie.Value = encTicket;
                HttpContext.Current.Response.Cookies.Add(cookie);

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Authenticates the request using API tokens
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Security matrix must be configured by calling ConfigureWith() prior to use.</exception>
        public static void AuthenticateRequest()
        {
            if (SecurityMatrix == null)
                throw new InvalidOperationException("Security matrix must be configured by calling ConfigureWith() prior to use.");

            var agent = ResolveFormsAuthentication();
            if (agent == null)
                agent = ResolveTokenAuthentication();

            var identity = new ClaimsIdentity(agent, "Token");
            var principal = new ClaimsPrincipal(identity);
            Thread.CurrentPrincipal = principal;
            HttpContext.Current.User = principal;
        }

        /// <summary>
        /// Configure the shield with the specified matrix
        /// </summary>
        public static void ConfigureWith(ISecurityMatrix matrix)
        {
            Shield.SecurityMatrix = matrix;
        }

        /// <summary>
        /// Sign the agent out of the matrix
        /// </summary>
        public static void Signout()
        {
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// Resolves the authenticated agent using forms authentication
        /// </summary>
        private static Agent ResolveFormsAuthentication()
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    // Resolve the agent from the forms authentication cookie
                    // TODO: Should we really store it inside the cookie, is it safe??
                    var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                    if (cookie != null)
                    {
                        var ticket = FormsAuthentication.Decrypt(cookie.Value);
                        if (ticket != null)
                        {
                            return JsonConvert.DeserializeObject<Agent>(ticket.UserData);
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Resolves the authentication agent using basic auth tokens
        /// </summary>
        /// <returns></returns>
        private static Agent ResolveTokenAuthentication()
        {
            var token = HttpContext.Current.Request.Headers["Authorization"];
            var result = SecurityMatrix.AuthenticateToken(token);
            if (result.Success)
                return result.Agent;
            else
                return null;
        }

        private static ISecurityMatrix SecurityMatrix;
    }
}