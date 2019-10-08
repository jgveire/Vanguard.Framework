namespace Vanguard.Framework.Core.DomainEvents
{
    using System.Collections.Generic;

    /// <summary>
    /// The asynchronous domain entity interface.
    /// </summary>
    public interface IAsyncDomainEntity
    {
        /// <summary>
        /// Gets a collection of domain events that should be dispatched
        /// by the domain event dispatcher.
        /// </summary>
        ICollection<IAsyncDomainEvent> Events { get; }
    }
}
