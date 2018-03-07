namespace Vanguard.Framework.Core.DomainEvents
{
    using System.Threading.Tasks;

    /// <summary>
    /// The asynchronous domain event handler class.
    /// </summary>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public interface IAsyncEventHandler<in TEvent>
        where TEvent : IAsyncDomainEvent
    {
        /// <summary>
        /// Handles an event asynchronously.
        /// </summary>
        /// <param name="domainEvent">The event that needs to be handled.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task HandleAsync(TEvent domainEvent);
    }
}
