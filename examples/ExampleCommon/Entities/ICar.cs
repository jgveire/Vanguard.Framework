namespace ExampleCommon.Entities
{
    using Vanguard.Framework.Core.Repositories;

    /// <summary>
    /// The car interface.
    /// </summary>
    public interface ICar : IUniqueEntity
    {
        /// <summary>
        /// Gets the car brand name.
        /// </summary>
        string Brand { get; }

        /// <summary>
        /// Gets a value indicating whether the car is stolen.
        /// </summary>
        bool IsStolen { get; }

        /// <summary>
        /// Gets the car license plate.
        /// </summary>
        string LicensePlate { get; }

        /// <summary>
        /// Gets the car model name.
        /// </summary>
        string Model { get; }
    }
}
