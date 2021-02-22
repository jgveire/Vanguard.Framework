namespace ExampleBusiness.Queries
{
    using System;
    using ExampleModels;
    using Vanguard.Framework.Core.Cqrs;

    /// <summary>
    /// The get car by identitifier query.
    /// </summary>
    public sealed class GetCarByIdQuery : IQuery<CarModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCarByIdQuery"/> class.
        /// </summary>
        /// <param name="carId">The car identifier.</param>
        public GetCarByIdQuery(Guid carId)
        {
            CarId = carId;
        }

        /// <summary>
        /// Gets the car identifier.
        /// </summary>
        public Guid CarId { get; }
    }
}
