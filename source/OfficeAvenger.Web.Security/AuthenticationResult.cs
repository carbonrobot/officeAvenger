using OfficeAvenger.Domain;

namespace OfficeAvenger.Web.Security
{
    public class AuthenticationResult
    {
        public AuthenticationResult(Agent authenticatedAgent)
        {
            this.Agent = authenticatedAgent;
            this.Success = true;
        }

        /// <summary>
        /// Creates a failed authentication result
        /// </summary>
        public static AuthenticationResult Failure
        {
            get
            {
                return new AuthenticationResult(null) { Success = false };
            }
        }

        /// <summary>
        /// Gets the authenticated agent information
        /// </summary>
        public Agent Agent { get; private set; }

        /// <summary>
        /// Gets a value indicating if authentication was successful
        /// </summary>
        public bool Success { get; private set; }
    }
}