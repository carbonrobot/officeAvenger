using System;

namespace OfficeAvenger.Domain
{
    public class Avenger
    {
        /// <summary>
        /// Gets or sets the path to the avatar
        /// </summary>
        public string Avatar
        {
            get
            {
                return _avatar;
            }
            set
            {
                _avatar = value;
            }
        }

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

        private string _avatar;
        private string _name;
    }
}