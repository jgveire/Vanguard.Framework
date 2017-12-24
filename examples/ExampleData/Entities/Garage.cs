namespace ExampleData.Entities
{
    using System.ComponentModel.DataAnnotations;
    using Vanguard.Framework.Core.Repositories;
    using Vanguard.Framework.Data.Entities;

    /// <summary>
    /// The garage data entity.
    /// </summary>
    /// <seealso cref="Vanguard.Framework.Data.Entities.DataEntity" />
    /// <seealso cref="Vanguard.Framework.Core.Repositories.IAuditable" />
    public class Garage : DataEntity, IAuditable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Garage"/> class.
        /// </summary>
        public Garage()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Garage"/> class.
        /// </summary>
        /// <param name="name">The name of the garage.</param>
        /// <param name="address">The address of the garage.</param>
        public Garage(string name, string address)
        {
            Name = name;
            Address = address;
        }

        /// <inheritdoc />
        public string EntityId => Id.ToString();

        /// <summary>
        /// Gets or sets the name of the garage.
        /// </summary>
        /// <value>
        /// The name of the garage.
        /// </value>
        [Required]
        [MaxLength(20)]
        public string Name { get; protected set; }

        /// <summary>
        /// Gets or sets the address of the garage.
        /// </summary>
        /// <value>
        /// The address of the garage.
        /// </value>
        [Required]
        [MaxLength(200)]
        public string Address { get; protected set; }
    }
}
