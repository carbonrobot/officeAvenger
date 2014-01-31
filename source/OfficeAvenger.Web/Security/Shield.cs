using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using OfficeAvenger.Domain;
using OfficeAvenger.Web.Security.Authority;
using OfficeAvenger.Services.Logging;

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
        /// Authenticates the request using API tokens
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Security matrix must be configured by calling ConfigureWith() prior to use.</exception>
        public static void AuthenticateRequest()
        {
            if (handlers.Count == 0)
            {
                "Shield".Log().Error(() => "No authentication handlers registered");
                throw new InvalidOperationException("You must register one or more authentication handlers");
            }

            foreach (var handler in handlers)
            {
                var principal = handler.Authenticate();
                if (principal != null)
                {
                    Thread.CurrentPrincipal = principal;
                    HttpContext.Current.User = principal;
                    break;
                }
            }
        }

        /// <summary>
        /// Registers an authentication handler
        /// </summary>
        public static void Register(IAuthentication handler)
        {
            handlers.Add(handler);
        }

        private static readonly List<IAuthentication> handlers = new List<IAuthentication>();
    }
}