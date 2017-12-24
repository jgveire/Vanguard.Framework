namespace ExampleBusiness.EventHandlers
{
    using System.Diagnostics;
    using ExampleCommon.Events;
    using Vanguard.Framework.Core.DomainEvents;

    /// <summary>
    /// The car stolen event handler.
    /// </summary>
    public sealed class CarStolenEventHandler : IEventHandler<CarStolenEvent>
    {
        /// <inheritdoc />
        public void Handle(CarStolenEvent domainEvent)
        {
            // Create your custom implementation of what to do when
            // a car is reported stolen. For example send an email to the police.
            Debug.WriteLine($"The car with license {domainEvent.Car.LicensePlate} has been reported stolen.");
        }
    }
}
