namespace Vanguard.Framework.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Vanguard.Framework.Core.DomainEvents;
    using Vanguard.Framework.Core.Repositories;

    /// <summary>
    /// The data entity class.
    /// </summary>
    public class DataEntity : IUniqueEntity, IDomainEntity
    {
        /// <summary>
        /// Gets or sets the identifier of the entity.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets a collection of domain events.
        /// </summary>
        [NotMapped]
        public ICollection<IDomainEvent> Events { get; } = new List<IDomainEvent>();
    }
}
