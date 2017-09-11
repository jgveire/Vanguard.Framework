namespace Vanguard.Framework.Core.DomainEvents
{
    /// <summary>
    /// The domain event handler class.
    /// </summary>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public interface IEventHandler<TEvent>
        where TEvent : IDomainEvent
    {
        /// <summary>
        /// Handles an event.
        /// </summary>
        /// <param name="domainEvent">The event that needs to be handled.</param>
        void Handle(TEvent domainEvent);
    }
}
