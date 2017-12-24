namespace ExampleData.Events
{
    using ExampleData.Entities;
    using Vanguard.Framework.Core.DomainEvents;

    /// <summary>
    /// The car stolen domain event.
    /// </summary>
    public sealed class CarStolenEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarStolenEvent"/> class.
        /// </summary>
        /// <param name="car">The car that has been stolen.</param>
        public CarStolenEvent(Car car)
        {
            Car = car;
        }

        /// <summary>
        /// Gets the car that has been stolen.
        /// </summary>
        public Car Car { get; }
    }
}
