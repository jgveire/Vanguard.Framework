namespace Vanguard.Framework.Core
{
    /// <summary>
    /// The paging filter.
    /// Used for filtering search results.
    /// </summary>
    public class PagingFilter
    {
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
    }
}
