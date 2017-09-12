using ExampleBusiness.Commands;
using ExampleData.Entities;
using Vanguard.Framework.Core.Cqrs;
using Vanguard.Framework.Core.Repositories;

namespace ExampleBusiness.CommandHandlers
{
    /// <summary>
    /// The report car stolen command handler.
    /// </summary>
    public sealed class ReportCarStolenCommandHandler : ICommandHandler<ReportCarStolenCommand>
    {
        private readonly IRepository<Car> _carRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportCarStolenCommandHandler"/> class.
        /// </summary>
        /// <param name="carRepository">The car repository.</param>
        public ReportCarStolenCommandHandler(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        /// <inheritdoc />
        public void Execute(ReportCarStolenCommand command)
        {
            Car car = _carRepository.GetById(command.CarId);
            if (car == null)
            {
                return;
            }

            car.ReportStolen();
            _carRepository.Save();
        }
    }
}
