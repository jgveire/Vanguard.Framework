namespace ExampleModels
{
    using System.ComponentModel.DataAnnotations;
    using Vanguard.Framework.Core.Models;

    /// <summary>
    /// The garage model.
    /// </summary>
    /// <seealso cref="Vanguard.Framework.Core.Models.UniqueModel" />
    public class GarageModel : UniqueModel
    {
        /// <summary>
        /// Gets or sets the name of the garage.
        /// </summary>
        /// <value>
        /// The name of the garage.
        /// </value>
        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the address of the garage.
        /// </summary>
        /// <value>
        /// The address of the garage.
        /// </value>
        [Required]
        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;
    }
}
