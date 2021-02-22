namespace ExampleBusiness.QueryHandlers
{
    using System;
    using ExampleBusiness.Queries;
    using ExampleData.Entities;
    using ExampleModels;
    using Vanguard.Framework.Core;
    using Vanguard.Framework.Core.Cqrs;
    using Vanguard.Framework.Core.Repositories;

    /// <summary>
    /// The get car by identitifier query handler.
    /// </summary>
    public sealed class GetCarByIdQueryHandler : IQueryHandler<CarModel, GetCarByIdQuery>
    {
        private readonly IRepository<Car> _carRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCarByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="carRepository">The car repository.</param>
        /// <param name="mapper">The class mapper.</param>
        public GetCarByIdQueryHandler(
            IRepository<Car> carRepository,
            IMapper mapper)
        {
            _carRepository = carRepository ?? throw new ArgumentNullException(nameof(carRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        public CarModel Retrieve(GetCarByIdQuery query)
        {
            var car = _carRepository.GetById(query.CarId);
            return _mapper.Map<CarModel>(car);
        }
    }
}
