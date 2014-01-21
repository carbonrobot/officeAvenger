using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace OfficeAvenger.Domain
{
    public abstract class AgentOwnedEntity : Entity
    {
        /// <summary>
        /// Gets or sets the agent id
        /// </summary>
        public int AgentId { get; set; }
    }
}