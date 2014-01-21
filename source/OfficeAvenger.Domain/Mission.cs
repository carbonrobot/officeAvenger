using System;
using System.Collections.Generic;
using System.Linq;

namespace OfficeAvenger.Domain
{
    public class Mission : AgentOwnedEntity
    {
        public Mission()
        {
            this.Team = new List<Avenger>();
        }

        /// <summary>
        /// Gets or sets the duration of this mission in minutes
        /// </summary>
        public int Duration
        {
            get { return _duration; }
            set
            {
                if (MissionStart.HasValue)
                    return; // mission already started

                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Mission duration must be at least 1 minute");

                _duration = value;
            }
        }

        /// <summary>
        /// Gets or sets the end date of this mission
        /// </summary>
        public DateTime? MissionEnd
        {
            get { return _missionEnd; }
            set { _missionEnd = value; }
        }

        /// <summary>
        /// Gets or sets the created on date time
        /// </summary>
        /// <value>The created on.</value>
        public DateTime? MissionStart
        {
            get { return _missionStart; }
            private set { _missionStart = value; }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        /// <exception cref="System.ArgumentNullException">name;You must provide a name for a mission</exception>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("name", "You must provide a name for a mission");

                _name = value;
            }
        }

        /// <summary>
        /// Gets or sets the team.
        /// </summary>
        /// <value>The team.</value>
        public IList<Avenger> Team { get; protected set; }

        /// <summary>
        /// Begins the mission
        /// </summary>
        public void Engage()
        {
            this.MissionStart = DateTime.Now;
            this.MissionEnd = this.MissionStart.Value.AddMinutes(this.Duration);
        }

        private int _duration;
        private DateTime? _missionEnd;
        private DateTime? _missionStart;
        private string _name;
    }
}