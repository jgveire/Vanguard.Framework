namespace Vanguard.Framework.Core.Extensions
{
    using System.Collections.Generic;

    /// <summary>
    /// The collection extensions.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds range of items to the collection.
        /// </summary>
        /// <typeparam name="T">The type of the items in the collection.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="items">The items to add.</param>
        public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> items)
        {
            Guard.ArgumentNotNull(source, nameof(source));
            Guard.ArgumentNotNull(items, nameof(items));

            foreach (var item in items)
            {
                source.Add(item);
            }
        }
    }
}
