namespace ExampleModels
{
    using System.ComponentModel.DataAnnotations;
    using Vanguard.Framework.Core.Models;

    /// <summary>
    /// The car model.
    /// </summary>
    /// <seealso cref="Vanguard.Framework.Core.Models.UniqueModel" />
    public class CarModel : UniqueModel
    {
        /// <summary>
        /// Gets or sets the brand name of the car.
        /// </summary>
        /// <value>
        /// The brand name of the car.
        /// </value>
        [Required]
        [MaxLength(100)]
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the model name of the car.
        /// </summary>
        /// <value>
        /// The model name of the car.
        /// </value>
        [Required]
        [MaxLength(100)]
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the license plate of the car.
        /// </summary>
        /// <value>
        /// The license plate of the car.
        /// </value>
        [Required]
        [MaxLength(20)]
        public string LicensePlate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the car is stolen.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the car is stolen; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool IsStolen { get; set; }
    }
}
