using System.ComponentModel.DataAnnotations;
using Vanguard.Framework.Core.Models;

namespace ExampleService.Models
{
    public class CarModel : UniqueModel
    {
        [Required]
        [MaxLength(100)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(100)]
        public string Model { get; set; }

        [Required]
        [MaxLength(20)]
        public string LicensePlate { get; protected set; }

        [Required]
        public bool IsStolen { get; protected set; }
    }
}
