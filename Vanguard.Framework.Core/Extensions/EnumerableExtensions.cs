using System;
using System.Collections.Generic;

namespace Vanguard.Framework.Core.Extensions
{
    /// <summary>
    /// The enumerable extension method class.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Executes an action for each item in the collection.
        /// </summary>
        /// <typeparam name="T">The type of the enumerator.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="action">The action to execute.</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            Guard.ArgumentNotNull(source, nameof(source));
            Guard.ArgumentNotNull(action, nameof(action));

            foreach (var item in source)
            {
                action(item);
            }
        }
    }
}
