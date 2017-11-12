using System;
using System.ComponentModel.DataAnnotations;
using ExampleCommon.Entities;
using ExampleCommon.Events;
using Vanguard.Framework.Data.Entities;

namespace ExampleData.Entities
{
    public class Car : DataEntity, ICar
    {
        public Car()
        {
        }

        public Car(string brand, string model, string licensePlate)
        {
            Brand = brand;
            Model = model;
            LicensePlate = licensePlate;
        }

        public Car(Guid id, string brand, string model, string licensePlate)
        {
            Id = id;
            Brand = brand;
            Model = model;
            LicensePlate = licensePlate;
        }

        [Required]
        [MaxLength(20)]
        public string Brand { get; protected set; }

        [Required]
        [MaxLength(20)]
        public string Model { get; protected set; }

        [Required]
        [MaxLength(20)]
        public string LicensePlate { get; protected set; }

        [Required]
        public bool IsStolen { get; protected set; }

        public void ReportStolen()
        {
            IsStolen = true;
            Events.Add(new CarStolenEvent(this));
        }
    }
}
