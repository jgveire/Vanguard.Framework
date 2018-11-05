namespace Vanguard.Framework.Core.Collections
{
    /// <summary>
    /// The property mapping.
    /// </summary>
    public class PropertyMapping
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMapping"/> class.
        /// </summary>
        /// <param name="source">The source property.</param>
        /// <param name="destination">The destination property.</param>
        public PropertyMapping(string source, string destination)
        {
            Source = source;
            Destination = destination;
            Guard.ArgumentNotNullOrEmpty(source, nameof(source));
            Guard.ArgumentNotNullOrEmpty(destination, nameof(destination));
        }

        /// <summary>
        /// Gets the destination property.
        /// </summary>
        /// <value>
        /// The destination property.
        /// </value>
        public string Destination { get; }

        /// <summary>
        /// Gets the source property.
        /// </summary>
        /// <value>
        /// The source property.
        /// </value>
        public string Source { get; }
    }
}
