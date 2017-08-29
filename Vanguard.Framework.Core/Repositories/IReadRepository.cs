using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Vanguard.Framework.Core.Repositories
{
    /// <summary>
    /// The read repository interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IReadRepository<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Finds entities accoording to the supplied find criteria.
        /// </summary>
        /// <param name="findCriteria">The find criteria.</param>
        /// <returns>A collection of entities.</returns>
        IEnumerable<TEntity> Find(FindCriteria findCriteria);

        /// <summary>
        /// Gets the number of items in the database accoording to the supplied find criteria.
        /// </summary>
        /// <param name="findCriteria">The find criteria.</param>
        /// <returns>The total number of items in the database accoording to the supplied find criteria.</returns>
        int GetCount(FindCriteria findCriteria);

        /// <summary>
        /// Gets the number of items in the database accoording to the supplied filter.
        /// </summary>
        /// <param name="filter">The filter predicate.</param>
        /// <returns>The total number of items in the database accoording to the supplied filter.</returns>
        int GetCount(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Gets a entity by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A entity.</returns>
        TEntity GetById(params object[] id);

        /// <summary>
        /// Gets the enties accoording to the specified filter.
        /// </summary>
        /// <param name="filter">The filter predicate.</param>
        /// <param name="orderBy">The order by predicate.</param>
        /// <param name="includeProperties">The properties that should be include in the query.</param>
        /// <returns>A collection of TEntity.</returns>
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params string[] includeProperties);
    }
}
