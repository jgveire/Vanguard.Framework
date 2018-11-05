namespace Vanguard.Framework.Core.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The property mappings.
    /// </summary>
    public class PropertyMappings : List<PropertyMapping>, IPropertyMapper
    {
        /// <summary>
        /// Maps the properties.
        /// </summary>
        /// <param name="properties">The source properties.</param>
        /// <returns>A collection of mapper properties.</returns>
        public IEnumerable<string> MapProperties(IEnumerable<string> properties)
        {
            if (properties == null)
            {
                return null;
            }

            var mappedProperties = new List<string>();
            foreach (var property in properties)
            {
                var mappedProperty = MapProperty(property);
                mappedProperties.Add(mappedProperty);
            }

            return mappedProperties.Distinct();
        }

        /// <summary>
        /// Maps the source property.
        /// </summary>
        /// <param name="property">The source property.</param>
        /// <returns>The destination property if found; otherwise, the supplied property.</returns>
        public string MapProperty(string property)
        {
            foreach (var mapping in this)
            {
                if (string.Equals(property, mapping.Source, StringComparison.InvariantCultureIgnoreCase))
                {
                    return mapping.Destination;
                }
            }

            return property;
        }

        /// <summary>
        /// Adds a new property mapping entry to the collection.
        /// </summary>
        /// <param name="source">The source property.</param>
        /// <param name="destination">The destination property.</param>
        public void Add(string source, string destination)
        {
            Add(new PropertyMapping(source, destination));
        }
    }
}
