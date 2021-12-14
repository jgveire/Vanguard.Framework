namespace Vanguard.Framework.Http.Extensions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The source extensions.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets all base types and the class type.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <returns>A collection of type where the deepest base type is returned first.</returns>
        public static IEnumerable<Type> BaseTypesAndSelf(this Type? source)
        {
            while (source != null)
            {
                yield return source;
                source = source.BaseType;
            }
        }
    }
}