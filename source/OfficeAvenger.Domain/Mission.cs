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
            this.CreatedOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the created on date time
        /// </summary>
        /// <value>The created on.</value>
        public DateTime CreatedOn
        {
            get { return _createdOn; }
            private set { _createdOn = value; }
        }

        /// <summary>
        /// Gets or sets the duration of this mission in minutes
        /// </summary>
        public int Duration
        {
            get { return _duration; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Mission duration must be at least 1 minute");

                _duration = value;
                OnDurationPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the end date of this mission
        /// </summary>
        public DateTime EndDateTime
        {
            get { return _endDateTime; }
            set { _endDateTime = value; }
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
        /// Called when [duration property changed].
        /// </summary>
        private void OnDurationPropertyChanged()
        {
            _endDateTime = this.CreatedOn.AddMinutes(_duration);
        }

        private DateTime _createdOn;
        private int _duration;
        private DateTime _endDateTime;
        private string _name;
    }
}