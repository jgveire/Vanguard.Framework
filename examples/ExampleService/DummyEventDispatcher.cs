namespace ExampleService
{
    using System.Threading.Tasks;
    using Vanguard.Framework.Core.DomainEvents;

    /// <summary>
    /// The dummy event dispatcher.
    /// </summary>
    public class DummyEventDispatcher : IEventDispatcher
    {
        /// <inheritdoc />
        public void Dispatch(IDomainEvent domainEvent)
        {
            // do nothing
        }

        /// <inheritdoc />
        public Task DispatchAsync(IAsyncDomainEvent domainEvent)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public void Dispatch<TEvent>(TEvent domainEvent)
            where TEvent : IDomainEvent
        {
            // do nothing
        }

        /// <inheritdoc />
        public Task DispatchAsync<TEvent>(TEvent domainEvent)
            where TEvent : IAsyncDomainEvent
        {
            return Task.CompletedTask;
        }
    }
}