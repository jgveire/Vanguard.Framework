using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Vanguard.Framework.Data.Repositories
{
    /// <summary>
    /// The queryable extension method class.
    /// </summary>
    internal static class QueryableExtensions
    {
        /// <summary>
        /// Gets the specified page of a sequence of elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="page">The page to retrieve.</param>
        /// <param name="pageSize">Size of an page.</param>
        /// <returns>A <see cref="IQueryable{T}"/> whose elements are restricted to the specified page.</returns>
        public static IQueryable<TSource> GetPage<TSource>(this IQueryable<TSource> source, int page, int pageSize)
        {
            int index = Math.Max(1, page) - 1;
            int skip = index * pageSize;
            return source.Skip(skip).Take(Math.Max(1, pageSize));
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order according to a key.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="ordering">The ordering.</param>
        /// <returns>A <see cref="IQueryable{T}"/> whose elements are sorted according to the specified ordering.</returns>
        public static IOrderedQueryable<TSource> OrderByDescending<TSource>(this IQueryable<TSource> source, string ordering)
        {
            return source.OrderBy(ordering + " DESC");
        }
    }
}
