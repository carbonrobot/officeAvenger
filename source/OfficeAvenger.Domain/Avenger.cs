using System;

namespace OfficeAvenger.Domain
{
    /// <summary>
    /// An instance of the mighty Avengers team
    /// </summary>
    public class Avenger : AgentOwnedEntity
    {   
        /// <summary>
        /// Gets or sets the path to the avatar
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentOutOfRangeException();

                _name = value;
            }
        }

        private string _name;
    }
}