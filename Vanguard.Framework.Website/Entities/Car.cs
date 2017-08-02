using System;
using System.ComponentModel.DataAnnotations;
using Vanguard.Framework.Website.Repositories;

namespace Vanguard.Framework.Website.Entities
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

        [Key]
        public Guid Id { get; set; } = new Guid();

        [Required]
        [StringLength(20)]
        public string Brand { get; set; }

        [Required]
        [StringLength(20)]
        public string Model { get; set; }
    }
}
