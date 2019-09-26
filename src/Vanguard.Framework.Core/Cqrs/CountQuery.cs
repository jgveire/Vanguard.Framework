namespace Vanguard.Framework.Core.Cqrs
{
    /// <summary>
    /// The total count query.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="Vanguard.Framework.Core.Cqrs.ICommand" />
    public class CountQuery<TModel> : IQuery<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountQuery{TModel}"/> class.
        /// </summary>
        /// <param name="filter">The search filter.</param>
        public CountQuery(SearchFilter filter)
        {
            Guard.ArgumentNotNull(filter, nameof(filter));
            Filter = filter;
        }

        /// <summary>
        /// Gets the search filter.
        /// </summary>
        /// <value>
        /// The search filter.
        /// </value>
        public SearchFilter Filter { get; }
    }
}
