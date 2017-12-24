using Vanguard.Framework.Core.Models;

namespace ExampleModels
{
    using System.ComponentModel.DataAnnotations;

    public class GarageModel : UniqueModel
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }
    }
}
