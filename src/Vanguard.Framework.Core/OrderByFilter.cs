namespace Vanguard.Framework.Core
{
    using Collections;

    /// <summary>
    /// The order by filter.
    /// </summary>
    public class OrderByFilter : PagingFilter
    {
        /// <summary>
        /// Gets or sets the order by.
        /// </summary>
        /// <value>
        /// The order by.
        /// </value>
        public string OrderBy { get; set; }

        /// <summary>
        /// Gets the property mappings. These mappings are used
        /// to map the model properties to the entity properties.
        /// </summary>
        /// <value>
        /// The property mappings.
        /// </value>
        public PropertyMappings PropertyMappings { get; } = new PropertyMappings();

        /// <summary>
        /// Gets or sets the sort order.
        /// The default value is ascending.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        public SortOrder SortOrder { get; set; }
    }
}
