namespace Vanguard.Framework.Core.Collections
{
    using System.Collections.Generic;

    /// <summary>
    /// The property mapper.
    /// </summary>
    public interface IPropertyMapper
    {
        /// <summary>
        /// Maps the properties.
        /// </summary>
        /// <param name="properties">The source properties.</param>
        /// <returns>A collection of mapper properties.</returns>
        IEnumerable<string> MapProperties(IEnumerable<string> properties);

        /// <summary>
        /// Maps the source property.
        /// </summary>
        /// <param name="property">The source property.</param>
        /// <returns>The destination property if found; otherwise, the supplied property.</returns>
        string MapProperty(string property);
    }
}