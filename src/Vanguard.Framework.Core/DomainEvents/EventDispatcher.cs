namespace Vanguard.Framework.Core.DomainEvents
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The event dispatcher.
    /// </summary>
    /// <seealso cref="IEventDispatcher" />
    public class EventDispatcher : IEventDispatcher
    {
        private readonly ILogger<EventDispatcher>? _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventDispatcher"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public EventDispatcher(IServiceProvider serviceProvider)
        {
            ServiceProvider = Guard.ArgumentNotNull(serviceProvider, nameof(serviceProvider));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventDispatcher" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="logger">The logger.</param>
        public EventDispatcher(IServiceProvider serviceProvider, ILogger<EventDispatcher> logger)
        {
            ServiceProvider = Guard.ArgumentNotNull(serviceProvider, nameof(serviceProvider));
            _logger = Guard.ArgumentNotNull(logger, nameof(logger));
        }

        /// <summary>
        /// Gets the service provider.
        /// </summary>
        /// <value>
        /// The service provider.
        /// </value>
        protected IServiceProvider ServiceProvider { get; }

        /// <inheritdoc />
        public void Dispatch(IDomainEvent domainEvent)
        {
            Guard.ArgumentNotNull(domainEvent, nameof(domainEvent));

            // Retriever query handler.
            var genericType = typeof(IEventHandler<>);
            var eventType = domainEvent.GetType();
            Type[] typeArguments = { eventType };
            var eventHandlerType = genericType.MakeGenericType(typeArguments);
            var eventHandlers = ServiceProvider.GetServices(eventHandlerType);

            _logger?.LogDebug($"Dispatch event: {eventType.Name}");
            foreach (var eventHandler in eventHandlers)
            {
                // Invoke handle method.
                var retrieveMethod = eventHandlerType.GetMethod("Handle");
                retrieveMethod.Invoke(eventHandler, new object[] { domainEvent });
            }
        }

        /// <inheritdoc />
        public async Task DispatchAsync(IAsyncDomainEvent domainEvent)
        {
            // Retriever query handler.
            var genericType = typeof(IAsyncEventHandler<>);
            var eventType = domainEvent.GetType();
            Type[] typeArguments = { eventType };
            var eventHandlerType = genericType.MakeGenericType(typeArguments);
            var eventHandlers = ServiceProvider.GetServices(eventHandlerType);

            _logger?.LogDebug($"Dispatch event: {eventType.Name}");
            foreach (var eventHandler in eventHandlers)
            {
                // Invoke handle method.
                var retrieveMethod = eventHandlerType.GetMethod("HandleAsync");
                var result = (Task)retrieveMethod.Invoke(eventHandler, new object[] { domainEvent });
                await result.ConfigureAwait(false);
            }
        }

        /// <inheritdoc />
        public void Dispatch<TEvent>(TEvent domainEvent)
            where TEvent : IDomainEvent
        {
            Guard.ArgumentNotNull(domainEvent, nameof(domainEvent));
            var eventHandlers = ServiceProvider.GetServices<IEventHandler<TEvent>>();
            var eventType = typeof(TEvent);

            _logger?.LogDebug($"Dispatch event: {eventType.Name}");
            foreach (var eventHandler in eventHandlers)
            {
                eventHandler.Handle(domainEvent);
            }
        }

        /// <inheritdoc />
        public async Task DispatchAsync<TEvent>(TEvent domainEvent)
            where TEvent : IAsyncDomainEvent
        {
            Guard.ArgumentNotNull(domainEvent, nameof(domainEvent));
            var eventHandlers = ServiceProvider.GetServices<IAsyncEventHandler<TEvent>>();
            var eventType = typeof(TEvent);

            _logger?.LogDebug($"Dispatch event: {eventType.Name}");
            foreach (var eventHandler in eventHandlers)
            {
                await eventHandler.HandleAsync(domainEvent).ConfigureAwait(false);
            }
        }
    }
}
