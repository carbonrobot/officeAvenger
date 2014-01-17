using System;
namespace OfficeAvenger.Domain
{
    /// <summary>
    /// Typical cubical bound office prisoner
    /// </summary>
    public class Agent
    {
        /// <summary>
        /// Gets or sets the name of this prisoner
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Name");

                _name = value;
            }
        }

        /// <summary>
        /// Gets or sets the password hash
        /// </summary>
        public string PasswordHash
        {
            get { return _passwordHash; }
            set
            {
                _passwordHash = value;
            }
        }

        /// <summary>
        /// Gets or sets the username
        /// </summary>
        public string Username
        {
            get { return _username; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Username");

                _username = value;
            }
        }

        private string _name;
        private string _passwordHash;
        private string _username;
    }
}