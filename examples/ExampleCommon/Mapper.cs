namespace ExampleCommon
{
    using Vanguard.Framework.Core;

    /// <inheritdoc />
    public class Mapper : IMapper
    {
        /// <inheritdoc />
        public TDestination Map<TDestination>(object source)
        {
            return AutoMapper.Mapper.Map<TDestination>(source);
        }

        /// <inheritdoc />
        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
