using ExampleCommon.Entities;
using Vanguard.Framework.Core.DomainEvents;

namespace ExampleCommon.Events
{
    /// <summary>
    /// The car stolen domain event.
    /// </summary>
    public sealed class CarStolenEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarStolenEvent"/> class.
        /// </summary>
        /// <param name="car">The car that has been stolen.</param>
        public CarStolenEvent(ICar car)
        {
            Car = car;
        }

        /// <summary>
        /// Gets the car that has been stolen.
        /// </summary>
        public ICar Car { get; }
    }
}
