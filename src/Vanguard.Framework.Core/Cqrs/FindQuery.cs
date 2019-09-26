namespace Vanguard.Framework.Core.Cqrs
{
    using System.Collections.Generic;

    /// <summary>
    /// The find query.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="IQuery{TResult}" />
    public class FindQuery<TModel> : IQuery<IEnumerable<TModel>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindQuery{TModel}"/> class.
        /// </summary>
        /// <param name="filter">The find criteria.</param>
        public FindQuery(SearchFilter filter)
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
