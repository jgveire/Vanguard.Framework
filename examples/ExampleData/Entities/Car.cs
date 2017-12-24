namespace ExampleData.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ExampleData.Events;
    using Vanguard.Framework.Core.Repositories;
    using Vanguard.Framework.Data.Entities;

    /// <summary>
    /// The car data entity.
    /// </summary>
    /// <seealso cref="Vanguard.Framework.Data.Entities.DataEntity" />
    /// <seealso cref="Vanguard.Framework.Core.Repositories.IAuditable" />
    public class Car : DataEntity, IAuditable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Car"/> class.
        /// </summary>
        public Car()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Car"/> class.
        /// </summary>
        /// <param name="brand">The car brand name.</param>
        /// <param name="model">The car model name.</param>
        /// <param name="licensePlate">The car license plate.</param>
        /// <param name="newPrice">The new price of the car.</param>
        public Car(string brand, string model, string licensePlate, decimal? newPrice)
        {
            Brand = brand;
            Model = model;
            LicensePlate = licensePlate;
            NewPrice = newPrice;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Car"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="brand">The car brand name.</param>
        /// <param name="model">The car model.</param>
        /// <param name="licensePlate">The car license plate.</param>
        /// <param name="newPrice">The new price of the car.</param>
        public Car(Guid id, string brand, string model, string licensePlate, decimal? newPrice)
        {
            Id = id;
            Brand = brand;
            Model = model;
            LicensePlate = licensePlate;
            NewPrice = newPrice;
        }

        /// <inheritdoc />
        public string EntityId => Id.ToString();

        /// <summary>
        /// Gets or sets the car brand name.
        /// </summary>
        /// <value>
        /// The brand.
        /// </value>
        [Required]
        [MaxLength(20)]
        public string Brand { get; protected set; }

        /// <summary>
        /// Gets or sets the car model name.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [Required]
        [MaxLength(20)]
        public string Model { get; protected set; }

        /// <summary>
        /// Gets or sets the license plate of the car.
        /// </summary>
        /// <value>
        /// The license plate of the car.
        /// </value>
        [Required]
        [MaxLength(20)]
        public string LicensePlate { get; protected set; }

        /// <summary>
        /// Gets or sets the new price of the car.
        /// </summary>
        /// <value>
        /// The new price of the car.
        /// </value>
        public decimal? NewPrice { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether the car is stolen.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the car is stolen; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool IsStolen { get; protected set; }

        /// <summary>
        /// Gets or sets the date the car has been reported stolen.
        /// </summary>
        /// <value>
        /// The date the car has been reported stolen.
        /// </value>
        public DateTime? ReportedStolenDate { get; protected set; }

        /// <summary>
        /// Reports the car as stolen.
        /// </summary>
        public void ReportStolen()
        {
            IsStolen = true;
            ReportedStolenDate = DateTime.Now;
            Events.Add(new CarStolenEvent(this));
        }
    }
}
