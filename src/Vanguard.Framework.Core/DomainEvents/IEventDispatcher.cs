using System.Threading.Tasks;

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
        /// Dispatches the specified event asynchronously.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DispatchAsync(IAsyncDomainEvent domainEvent);

        /// <summary>
        /// Dispatches the specified event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="domainEvent">The domain event.</param>
        void Dispatch<TEvent>(TEvent domainEvent)
            where TEvent : IDomainEvent;

        /// <summary>
        /// Dispatches the specified event asynchronously.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="domainEvent">The domain event.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DispatchAsync<TEvent>(TEvent domainEvent)
            where TEvent : IAsyncDomainEvent;
    }
}