namespace Vanguard.Framework.Core
{
    using System;

    /// <summary>
    /// The filter query.
    /// Used for filtering search results.
    /// </summary>
    [Obsolete("Make use of the AdvancedFilter class.")]
    public class FilterQuery : AdvancedFilter
    {
        /// <summary>
        /// Gets or sets the search string.
        /// </summary>
        /// <value>
        /// The search string.
        /// </value>
        public string? Search { get; set; }
    }
}
