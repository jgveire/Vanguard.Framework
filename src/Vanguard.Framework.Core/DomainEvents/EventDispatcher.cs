using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Vanguard.Framework.Core.DomainEvents
{
    /// <summary>
    /// The event dispatcher class.
    /// </summary>
    /// <seealso cref="IEventDispatcher" />
    public class EventDispatcher : IEventDispatcher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventDispatcher"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public EventDispatcher(IServiceProvider serviceProvider)
        {
            Guard.ArgumentNotNull(serviceProvider, nameof(serviceProvider));
            ServiceProvider = serviceProvider;
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
            Type genericType = typeof(IEventHandler<>);
            Type[] typeArguments = { domainEvent.GetType() };
            Type eventHandlerType = genericType.MakeGenericType(typeArguments);
            var eventHandlers = ServiceProvider.GetServices(eventHandlerType);

            foreach (var eventHandler in eventHandlers)
            {
                // Invoke handle method.
                MethodInfo retrieveMethod = eventHandlerType.GetMethod("Handle");
                retrieveMethod.Invoke(eventHandler, new object[] { domainEvent });
            }
        }

        /// <inheritdoc />
        public async Task DispatchAsync(IAsyncDomainEvent domainEvent)
        {
            // Retriever query handler.
            Type genericType = typeof(IAsyncEventHandler<>);
            Type[] typeArguments = { domainEvent.GetType() };
            Type eventHandlerType = genericType.MakeGenericType(typeArguments);
            var eventHandlers = ServiceProvider.GetServices(eventHandlerType);

            foreach (var eventHandler in eventHandlers)
            {
                // Invoke handle method.
                MethodInfo retrieveMethod = eventHandlerType.GetMethod("Handle");
                var result = (Task)retrieveMethod.Invoke(eventHandler, new object[] { domainEvent });
                await result;
            }
        }

        /// <inheritdoc />
        public void Dispatch<TEvent>(TEvent domainEvent)
            where TEvent : IDomainEvent
        {
            Guard.ArgumentNotNull(domainEvent, nameof(domainEvent));
            var eventHandlers = ServiceProvider.GetServices<IEventHandler<TEvent>>();

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

            foreach (var eventHandler in eventHandlers)
            {
                await eventHandler.Handle(domainEvent);
            }
        }
    }
}
