namespace ExampleData.Entities
{
    using System.ComponentModel.DataAnnotations;
    using Vanguard.Framework.Core.Repositories;
    using Vanguard.Framework.Data.Entities;

    public class Garage : DataEntity, IAuditable
    {
        public Garage()
        {
        }

        public Garage(string name, string address)
        {
            Name = name;
            Address = address;
        }

        public string EntityId => Id.ToString();

        [Required]
        [MaxLength(20)]
        public string Name { get; protected set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; protected set; }
    }
}
