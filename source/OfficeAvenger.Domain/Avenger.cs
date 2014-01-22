using System;
using System.Collections.Generic;

namespace OfficeAvenger.Domain
{
    /// <summary>
    /// An instance of the mighty Avengers team
    /// </summary>
    public class Avenger : AgentOwnedEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Avenger"/> class.
        /// </summary>
        public Avenger()
        {
            this.Missions = new List<Mission>();
        }

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

        /// <summary>
        /// Gets the missions.
        /// </summary>
        public IList<Mission> Missions { get; private set; }
    }
}