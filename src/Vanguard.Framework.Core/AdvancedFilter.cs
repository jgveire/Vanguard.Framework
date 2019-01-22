namespace Vanguard.Framework.Core
{
    /// <summary>
    /// The advanced filter.
    /// </summary>
    public class AdvancedFilter : OrderByFilter
    {
        /// <summary>
        /// Gets or sets the filter to apply.
        /// For example:
        /// - id eq 1
        /// - id eq 0F17B728-E080-4581-B192-DD475C638DCB
        /// - name eq 'Bike'
        /// </summary>
        /// <value>
        /// The filter to apply.
        /// </value>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets a comma separated list of complex properties
        /// that should be include in the result.
        /// For example: category,orders
        /// </summary>
        /// <value>
        /// A comma separated list of properties.
        /// </value>
        public string Include { get; set; }

        /// <summary>
        /// Gets or sets the fields that should be selected.
        /// </summary>
        /// <value>
        /// The fields that should be selected.
        /// </value>
        public string Select { get; set; }
    }
}