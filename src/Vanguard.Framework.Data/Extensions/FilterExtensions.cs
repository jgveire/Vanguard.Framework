namespace Vanguard.Framework.Data.Extensions
{
    using Vanguard.Framework.Core;

    /// <summary>
    /// The filter extensions.
    /// </summary>
    internal static class FilterExtensions
    {
        /// <summary>
        /// Gets a copy of the filter to retrieve the total count.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>A copy of the filter to retrieve the total count.</returns>
        public static SearchFilter? GetTotalCountCopy(this SearchFilter? filter)
        {
            if (filter == null)
            {
                return filter;
            }

            return new SearchFilter
            {
                Page = 1,
                PageSize = int.MaxValue,
                Search = filter.Search,
            };
        }

        /// <summary>
        /// Gets a copy of the filter to retrieve the total count.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>A copy of the filter to retrieve the total count.</returns>
        public static OrderByFilter? GetTotalCountCopy(this OrderByFilter? filter)
        {
            if (filter == null)
            {
                return filter;
            }

            return new SearchFilter
            {
                Page = 1,
                PageSize = int.MaxValue,
            };
        }

        /// <summary>
        /// Gets a copy of the filter to retrieve the total count.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>A copy of the filter to retrieve the total count.</returns>
        public static PagingFilter? GetTotalCountCopy(this PagingFilter? filter)
        {
            if (filter == null)
            {
                return filter;
            }

            return new PagingFilter
            {
                Page = 1,
                PageSize = int.MaxValue,
            };
        }

        /// <summary>
        /// Gets a copy of the filter to retrieve the total count.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>A copy of the filter to retrieve the total count.</returns>
        public static AdvancedFilter? GetTotalCountCopy(this AdvancedFilter? filter)
        {
            if (filter == null)
            {
                return filter;
            }

            return new AdvancedFilter
            {
                Page = 1,
                PageSize = int.MaxValue,
                Filter = filter.Filter,
            };
        }
    }
}
