using System;
using System.ComponentModel.DataAnnotations;

namespace Vanguard.Framework.Data.Entities
{
    /// <summary>
    /// The audit data entity keeps track of changes that are
    /// made on other data entities.
    /// </summary>
    public class AuditEntry
    {
        /// <summary>
        /// Gets or sets the audit identifier.
        /// </summary>
        /// <value>
        /// The audit identifier.
        /// </value>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the type of the audit.
        /// </summary>
        /// <value>
        /// The type of the audit.
        /// </value>
        public AuditType AuditType { get; set; }

        /// <summary>
        /// Gets or sets the name of the entity.
        /// </summary>
        /// <value>
        /// The name of the entity.
        /// </value>
        [Required]
        [MaxLength(50)]
        public string EntityName { get; set; }

        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        /// <value>
        /// The entity identifier.
        /// </value>
        [Required]
        [MaxLength(100)]
        public string EntityId { get; set; }

        /// <summary>
        /// Gets or sets a string representation of the entity.
        /// </summary>
        /// <value>
        /// A string representation of the entity.
        /// </value>
        [Required]
        public string Entity { get; set; }

        /// <summary>
        /// Gets or sets the user identifier who made the change.
        /// </summary>
        /// <value>
        /// The user identifier who made the change.
        /// </value>
        [Required]
        [MaxLength(50)]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the UTC date and time the user made the change.
        /// </summary>
        /// <value>
        /// The UTC date and time the user made the change.
        /// </value>
        [Required]
        public DateTime UtcDate { get; set; }
    }
}
