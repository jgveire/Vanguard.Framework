using System.Collections.Generic;

namespace Vanguard.Framework.Core.DomainEvents
{
    /// <summary>
    /// The domain entity interface.
    /// </summary>
    public interface IDomainEntity
    {
        /// <summary>
        /// Gets a collection of domain events that should be dispatched
        /// by the domain event dispatcher.
        /// </summary>
        ICollection<IDomainEvent> Events { get; }
    }
}
