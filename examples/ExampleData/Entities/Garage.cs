using System.ComponentModel.DataAnnotations;
using Vanguard.Framework.Data.Entities;

namespace ExampleData.Entities
{
    public class Garage : DataEntity
    {
        public Garage()
        {
        }

        public Garage(string name, string address)
        {
            Name = name;
            Address = address;
        }

        [Required]
        [MaxLength(20)]
        public string Name { get; protected set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; protected set; }
    }
}
