using System;
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

        /// <summary>
        /// Dispatches the specified event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="domainEvent">The domain event.</param>
        public void Dispatch<TEvent>(TEvent domainEvent)
            where TEvent : IDomainEvent
        {
            Guard.ArgumentNotNull(domainEvent, nameof(domainEvent));
            var eventHandler = ServiceProvider.GetRequiredService<IEventHandler<TEvent>>();
            eventHandler.Handle(domainEvent);
        }
    }
}
