namespace Vanguard.Framework.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// The filter result.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    public class FilterResult<TItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterResult{TItem}"/> class.
        /// </summary>
        public FilterResult()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterResult{TItem}"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="totalCount">The total count in the database.
        /// The items property will in general contain a subset
        /// of the total collection because of pagination.</param>
        public FilterResult(ICollection<TItem> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public ICollection<TItem> Items { get; set; }

        /// <summary>
        /// Gets or sets the total count in the database.
        /// The items property will in general contain a subset
        /// of the total collection because of pagination.
        /// </summary>
        /// <value>
        /// The total count.
        /// </value>
        public int TotalCount { get; set; }
    }
}
