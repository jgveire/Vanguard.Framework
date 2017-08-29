using Vanguard.Framework.Core;
using Vanguard.Framework.Core.Cqrs;

namespace Vanguard.Framework.Data.Cqrs
{
    /// <summary>
    /// The total count query class.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="Vanguard.Framework.Core.Cqrs.ICommand" />
    public class CountQuery<TModel> : IQuery<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountQuery{TModel}"/> class.
        /// </summary>
        /// <param name="criteria">The find criteria.</param>
        public CountQuery(FindCriteria criteria)
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
        public FindCriteria Criteria { get; }
    }
}
