using System;
using System.ComponentModel.DataAnnotations;
using Vanguard.Framework.Core.Repositories;

namespace Example.WebApi.Entities
{
    public class Car : IEntity
    {
        public Car()
        {
        }

        public Car(string brand, string model)
        {
            Brand = brand;
            Model = model;
        }

        public Car(Guid id, string brand, string model)
        {
            Id = id;
            Brand = brand;
            Model = model;
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(20)]
        public string Brand { get; set; }

        [Required]
        [StringLength(20)]
        public string Model { get; set; }
    }
}
