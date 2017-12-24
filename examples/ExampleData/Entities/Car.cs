namespace ExampleData.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ExampleData.Events;
    using Vanguard.Framework.Core.Repositories;
    using Vanguard.Framework.Data.Entities;

    public class Car : DataEntity, IAuditable
    {
        public Car()
        {
        }

        public Car(string brand, string model, string licensePlate, decimal? newPrice)
        {
            Brand = brand;
            Model = model;
            LicensePlate = licensePlate;
            NewPrice = newPrice;
        }

        public Car(Guid id, string brand, string model, string licensePlate, decimal? newPrice)
        {
            Id = id;
            Brand = brand;
            Model = model;
            LicensePlate = licensePlate;
            NewPrice = newPrice;
        }

        public string EntityId => Id.ToString();

        [Required]
        [MaxLength(20)]
        public string Brand { get; protected set; }

        [Required]
        [MaxLength(20)]
        public string Model { get; protected set; }

        [Required]
        [MaxLength(20)]
        public string LicensePlate { get; protected set; }

        public decimal? NewPrice { get; protected set; }

        [Required]
        public bool IsStolen { get; protected set; }

        public DateTime? ReportedStolenDate { get; protected set; }

        public void ReportStolen()
        {
            IsStolen = true;
            ReportedStolenDate = DateTime.Now;
            Events.Add(new CarStolenEvent(this));
        }
    }
}
