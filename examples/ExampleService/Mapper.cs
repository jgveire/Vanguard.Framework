namespace ExampleService
{
    using ExampleData.Entities;
    using ExampleModels;
    using Vanguard.Framework.Core;

    /// <inheritdoc />
    public class Mapper : IMapper
    {
        private AutoMapper.IMapper _mapper = CreateMapper();

        /// <inheritdoc />
        public TDestination Map<TDestination>(object source)
        {
            return _mapper.Map<TDestination>(source);
        }

        /// <inheritdoc />
        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return _mapper.Map(source, destination);
        }

        private static AutoMapper.IMapper CreateMapper()
        {

            return new AutoMapper.MapperConfiguration(config =>
            {
                config.CreateMap<Car, CarModel>();
                config.CreateMap<CarModel, Car>();
                config.CreateMap<Garage, GarageModel>();
                config.CreateMap<GarageModel, Garage>();
            }).CreateMapper();
        }
    }
}