namespace Vanguard.Framework.Core.Cqrs
{
    /// <summary>
    /// The total count query.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="Vanguard.Framework.Core.Cqrs.ICommand" />
    public class TotalCountQuery<TModel> : IQuery<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TotalCountQuery{TModel}"/> class.
        /// </summary>
        /// <param name="filter">The search filter.</param>
        public TotalCountQuery(SearchFilter filter)
        {
            Filter = Guard.ArgumentNotNull(filter, nameof(filter));
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
