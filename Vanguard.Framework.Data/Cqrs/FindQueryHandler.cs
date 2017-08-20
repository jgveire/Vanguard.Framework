using System;
using System.Collections.Generic;
using Vanguard.Framework.Core.Cqrs;

namespace Vanguard.Framework.Data.Cqrs
{
    /// <summary>
    /// The find query handler class.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="IQueryHandler{TResult, TQuery}" />
    public class FindQueryHandler<TModel> : IQueryHandler<IEnumerable<TModel>, FindQuery<TModel>>
    {
        /// <summary>
        /// Retrieves the result for the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The query result.</returns>
        public IEnumerable<TModel> Retrieve(FindQuery<TModel> query)
        {
            throw new NotImplementedException();
        }
    }
}