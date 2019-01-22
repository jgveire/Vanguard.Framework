namespace Vanguard.Framework.Core
{
    /// <summary>
    /// The search filter.
    /// </summary>
    public class SearchFilter : OrderByFilter
    {
        /// <summary>
        /// Gets or sets the search string.
        /// </summary>
        /// <value>
        /// The search string.
        /// </value>
        public string Search { get; set; }
    }
}
