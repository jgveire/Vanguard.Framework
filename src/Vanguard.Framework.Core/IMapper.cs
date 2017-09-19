namespace Vanguard.Framework.Core
{
    /// <summary>
    /// The entity mapper interface.
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// Execute a mapping from the source object to a new destination object. The source
        /// type is inferred from the source object.
        /// </summary>
        /// <typeparam name="TDestination">The destination type to create.</typeparam>
        /// <param name="source">The source object to map from.</param>
        /// <returns>Mapped destination object.</returns>
        TDestination Map<TDestination>(object source);

        /// <summary>
        /// Execute a mapping from the source object to a new destination object. The source
        /// type is inferred from the source object.
        /// </summary>
        /// <typeparam name="TSource">The source type to use.</typeparam>
        /// <typeparam name="TDestination">The destination type to create.</typeparam>
        /// <param name="source">The source object to map from.</param>
        /// <param name="destination">Destination object to map into.</param>
        /// <returns>The mapped destination object, same instance as the destination object.</returns>
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
