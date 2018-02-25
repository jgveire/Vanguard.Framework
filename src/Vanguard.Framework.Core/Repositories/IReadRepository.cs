namespace Vanguard.Framework.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// The read repository interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IReadRepository<TEntity>
        where TEntity : class, IDataEntity
    {
        /// <summary>
        /// Finds entities accoording to the supplied find criteria.
        /// </summary>
        /// <param name="searchCriteria">The find criteria.</param>
        /// <param name="filter">An aditional filter to apply on the result query.</param>
        /// <returns>A collection of entities.</returns>
        IEnumerable<TEntity> Find(
            SearchCriteria searchCriteria,
            Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Finds entities accoording to the supplied find criteria asynchronously.
        /// </summary>
        /// <param name="searchCriteria">The find criteria.</param>
        /// <param name="filter">An aditional filter to apply on the result query.</param>
        /// <returns>A collection of entities.</returns>
        Task<IEnumerable<TEntity>> FindAsync(
            SearchCriteria searchCriteria,
            Expression<Func<TEntity, bool>> filter = null);

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

        /// <summary>
        /// Gets the enties accoording to the specified filter asynchronously.
        /// </summary>
        /// <param name="filter">The filter predicate.</param>
        /// <param name="orderBy">The order by predicate.</param>
        /// <param name="includeProperties">The properties that should be include in the query.</param>
        /// <returns>A collection of TEntity.</returns>
        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params string[] includeProperties);

        /// <summary>
        /// Gets a entity by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A entity.</returns>
        TEntity GetById(params object[] id);

        /// <summary>
        /// Gets a entity by identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A entity.</returns>
        Task<TEntity> GetByIdAsync(params object[] id);

        /// <summary>
        /// Gets the number of items in the database accoording to the supplied find criteria.
        /// </summary>
        /// <param name="searchCriteria">The find criteria.</param>
        /// <returns>The total number of items in the database accoording to the supplied find criteria.</returns>
        int GetCount(SearchCriteria searchCriteria);

        /// <summary>
        /// Gets the number of items in the database accoording to the supplied filter.
        /// </summary>
        /// <param name="filter">The filter predicate.</param>
        /// <returns>The total number of items in the database accoording to the supplied filter.</returns>
        int GetCount(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Gets the number of items in the database accoording to the supplied find criteria asynchronously.
        /// </summary>
        /// <param name="searchCriteria">The find criteria.</param>
        /// <returns>The total number of items in the database accoording to the supplied find criteria.</returns>
        Task<int> GetCountAsync(SearchCriteria searchCriteria);

        /// <summary>
        /// Gets the number of items in the database accoording to the supplied filter asynchronously.
        /// </summary>
        /// <param name="filter">The filter predicate.</param>
        /// <returns>The total number of items in the database accoording to the supplied filter.</returns>
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null);
    }
}
