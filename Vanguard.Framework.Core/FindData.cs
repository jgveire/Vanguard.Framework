using System;

namespace Vanguard.Framework.Core
{
    /// <summary>
    /// The find data class.
    /// </summary>
    public class FindData
    {
        /// <summary>
        /// Gets or sets the search filter.
        /// </summary>
        /// <value>
        /// The search filter.
        /// </value>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the order by.
        /// </summary>
        /// <value>
        /// The order by.
        /// </value>
        public string OrderBy { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// The default value is ascending.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        public SortOrder SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the page.
        /// The default value is 1.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Gets or sets the size of the page.
        /// The default value is 20.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// Gets or sets the fields that should be selected.
        /// </summary>
        /// <value>
        /// The fields that should be selected.
        /// </value>
        public string Select { get; set; }
    }
}
