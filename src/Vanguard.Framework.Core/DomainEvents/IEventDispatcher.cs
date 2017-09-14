namespace Vanguard.Framework.Core.DomainEvents
{
    /// <summary>
    /// The event dispatcher interface.
    /// </summary>
    public interface IEventDispatcher
    {
        /// <summary>
        /// Dispatches the specified event.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        void Dispatch(IDomainEvent domainEvent);

        /// <summary>
        /// Dispatches the specified event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="domainEvent">The domain event.</param>
        void Dispatch<TEvent>(TEvent domainEvent)
            where TEvent : IDomainEvent;
    }
}