using System;
using System.ComponentModel.DataAnnotations;
using Vanguard.Framework.Core.Repositories;

namespace Vanguard.Framework.Website.Models
{
    public class CarModel : IUniqueEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(100)]
        public string Model { get; set; }
    }
}
