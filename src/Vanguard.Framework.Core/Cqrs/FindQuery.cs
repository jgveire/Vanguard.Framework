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
        /// <param name="criteria">The find criteria.</param>
        public FindQuery(FilterQuery criteria)
        {
            Guard.ArgumentNotNull(criteria, nameof(criteria));
            Criteria = criteria;
        }

        /// <summary>
        /// Gets the find criteria.
        /// </summary>
        /// <value>
        /// The find criteria.
        /// </value>
        public FilterQuery Criteria { get; }
    }
}
