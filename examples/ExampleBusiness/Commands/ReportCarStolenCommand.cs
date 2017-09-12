using System;
using Vanguard.Framework.Core.Cqrs;

namespace ExampleBusiness.Commands
{
    /// <summary>
    /// The report car stolen command.
    /// </summary>
    public sealed class ReportCarStolenCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportCarStolenCommand"/> class.
        /// </summary>
        /// <param name="carId">The car identifier.</param>
        public ReportCarStolenCommand(Guid carId)
        {
            CarId = carId;
        }

        /// <summary>
        /// Gets the car identifier.
        /// </summary>
        public Guid CarId { get; }
    }
}
