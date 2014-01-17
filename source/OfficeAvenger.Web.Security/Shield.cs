using System;
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
        public static Agent ActiveAgent
        {
            get
            {
                // TODO: add caching support
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return null;
                
                var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie == null)
                    return null;

                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                if (ticket == null)
                    return null;

                var agent = JsonConvert.DeserializeObject<Agent>(ticket.UserData);
                if (agent == null)
                    return null;

                return agent;
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

        private static ISecurityMatrix SecurityMatrix;
    }
}