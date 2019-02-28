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
        /// Determines whether the repository contains an entity with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns><c>true</c> if the repository contains an entity with the specified identifier; otherwise, <c>false</c>.</returns>
        bool Contains(object id);

        /// <summary>
        /// Determines whether the repository contains an entity with the specified identifiers.
        /// </summary>
        /// <param name="ids">The identifiers.</param>
        /// <returns><c>true</c> if the repository contains an entity with the specified identifiers; otherwise, <c>false</c>.</returns>
        bool Contains(object[] ids);

        /// <summary>
        /// Determines whether the repository contains an entity with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A task that return <c>true</c> if the repository contains an entity with the specified identifier; otherwise, <c>false</c>.</returns>
        Task<bool> ContainsAsync(object id);

        /// <summary>
        /// Determines whether the repository contains an entity with the specified identifiers.
        /// </summary>
        /// <param name="ids">The identifiers.</param>
        /// <returns>A task that return <c>true</c> if the repository contains an entity with the specified identifiers; otherwise, <c>false</c>.</returns>
        Task<bool> ContainsAsync(object[] ids);

        /// <summary>
        /// Finds entities according to the supplied find criteria.
        /// </summary>
        /// <param name="filterQuery">The find criteria.</param>
        /// <param name="filter">An additional filter to apply on the result query.</param>
        /// <returns>A collection of entities.</returns>
        [Obsolete("Use one of the following filters: AdvancedFilter, OrderByFilter, PagingFilter or SearchFilter")]
        IEnumerable<TEntity> Find(
            FilterQuery filterQuery,
            Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Finds entities according to the supplied find criteria asynchronously.
        /// </summary>
        /// <param name="filterQuery">The find criteria.</param>
        /// <param name="filter">An additional filter to apply on the result query.</param>
        /// <returns>A collection of entities.</returns>
        [Obsolete("Use one of the following filters: AdvancedFilter, OrderByFilter, PagingFilter or SearchFilter")]
        Task<IEnumerable<TEntity>> FindAsync(
            FilterQuery filterQuery,
            Expression<Func<TEntity, bool>> filter = null);

        /// <summary>Finds entities according to the supplied find criteria.</summary>
        /// <param name="advancedFilter">The advanced filter.</param>
        /// <param name="filter">An additional filter to apply on the result query.</param>
        /// <returns>A collection of entities.</returns>
        IEnumerable<TEntity> Find(
            AdvancedFilter advancedFilter,
            Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Finds entities according to the supplied find criteria asynchronously.
        /// </summary>
        /// <param name="advancedFilter">The advanced filter.</param>
        /// <param name="filter">An additional filter to apply on the result query.</param>
        /// <returns>A collection of entities.</returns>
        Task<IEnumerable<TEntity>> FindAsync(
            AdvancedFilter advancedFilter,
            Expression<Func<TEntity, bool>> filter = null);

        /// <summary>Finds entities according to the supplied find criteria.</summary>
        /// <param name="orderByFilter">The order by filter.</param>
        /// <param name="filter">An additional filter to apply on the result query.</param>
        /// <returns>A collection of entities.</returns>
        IEnumerable<TEntity> Find(
            OrderByFilter orderByFilter,
            Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Finds entities according to the supplied find criteria asynchronously.
        /// </summary>
        /// <param name="orderByFilter">The order by filter.</param>
        /// <param name="filter">An additional filter to apply on the result query.</param>
        /// <returns>A collection of entities.</returns>
        Task<IEnumerable<TEntity>> FindAsync(
            OrderByFilter orderByFilter,
            Expression<Func<TEntity, bool>> filter = null);

        /// <summary>Finds entities according to the supplied find criteria.</summary>
        /// <param name="pagingFilter">The paging filter.</param>
        /// <param name="filter">An additional filter to apply on the result query.</param>
        /// <returns>A collection of entities.</returns>
        IEnumerable<TEntity> Find(
            PagingFilter pagingFilter,
            Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Finds entities according to the supplied find criteria asynchronously.
        /// </summary>
        /// <param name="pagingFilter">The paging filter.</param>
        /// <param name="filter">An additional filter to apply on the result query.</param>
        /// <returns>A collection of entities.</returns>
        Task<IEnumerable<TEntity>> FindAsync(
            PagingFilter pagingFilter,
            Expression<Func<TEntity, bool>> filter = null);

        /// <summary>Finds entities according to the supplied find criteria.</summary>
        /// <param name="searchFilter">The search filter.</param>
        /// <param name="filter">An additional filter to apply on the result query.</param>
        /// <returns>A collection of entities.</returns>
        IEnumerable<TEntity> Find(
            SearchFilter searchFilter,
            Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Finds entities according to the supplied find criteria asynchronously.
        /// </summary>
        /// <param name="searchFilter">The search filter.</param>
        /// <param name="filter">An additional filter to apply on the result query.</param>
        /// <returns>A collection of entities.</returns>
        Task<IEnumerable<TEntity>> FindAsync(
            SearchFilter searchFilter,
            Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Gets the entities according to the specified filter.
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
        /// Gets the entities according to the specified filter asynchronously.
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
        /// <param name="includeProperties">The properties that should be include in the query.</param>
        /// <returns>An entity.</returns>
        TEntity GetById(object id, params string[] includeProperties);

        /// <summary>
        /// Gets a entity by identifier.
        /// </summary>
        /// <param name="ids">The identifier.</param>
        /// <param name="includeProperties">The properties that should be include in the query.</param>
        /// <returns>An entity.</returns>
        TEntity GetById(object[] ids, params string[] includeProperties);

        /// <summary>
        /// Gets a entity by identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeProperties">The properties that should be include in the query.</param>
        /// <returns>An entity.</returns>
        Task<TEntity> GetByIdAsync(object id, params string[] includeProperties);

        /// <summary>
        /// Gets a entity by identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeProperties">The properties that should be include in the query.</param>
        /// <returns>An entity.</returns>
        Task<TEntity> GetByIdAsync(object[] id, params string[] includeProperties);

        /// <summary>
        /// Gets the number of items in the database according to the supplied find criteria.
        /// </summary>
        /// <param name="filterQuery">The find criteria.</param>
        /// <returns>The total number of items in the database according to the supplied find criteria.</returns>
        [Obsolete("Make use of the AdvancedFilter, SearchFilter, OrderByFilter or PagingFilter class.")]
        int GetCount(FilterQuery filterQuery);

        /// <summary>
        /// Gets the number of items in the database according to the supplied find criteria.
        /// </summary>
        /// <param name="filterQuery">The find criteria.</param>
        /// <param name="filter">The filter predicate.</param>
        /// <returns>The total number of items in the database according to the supplied find criteria.</returns>
        [Obsolete("Make use of the AdvancedFilter, SearchFilter, OrderByFilter or PagingFilter class.")]
        int GetCount(FilterQuery filterQuery, Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Gets the number of items in the database according to the supplied filter.
        /// </summary>
        /// <param name="filter">The filter predicate.</param>
        /// <returns>The total number of items in the database according to the supplied filter.</returns>
        int GetCount(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Gets the number of items in the database according to the supplied find criteria asynchronously.
        /// </summary>
        /// <param name="filterQuery">The find criteria.</param>
        /// <returns>The total number of items in the database according to the supplied find criteria.</returns>
        [Obsolete("Make use of the AdvancedFilter, SearchFilter, OrderByFilter or PagingFilter class.")]
        Task<int> GetCountAsync(FilterQuery filterQuery);

        /// <summary>
        /// Gets the number of items in the database according to the supplied find criteria asynchronously.
        /// </summary>
        /// <param name="filterQuery">The find criteria.</param>
        /// <param name="filter">The filter predicate.</param>
        /// <returns>The total number of items in the database according to the supplied find criteria.</returns>
        [Obsolete("Make use of the AdvancedFilter, SearchFilter, OrderByFilter or PagingFilter class.")]
        Task<int> GetCountAsync(FilterQuery filterQuery, Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Gets the number of items in the database according to the supplied filter asynchronously.
        /// </summary>
        /// <param name="filter">The filter predicate.</param>
        /// <returns>The total number of items in the database according to the supplied filter.</returns>
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>Gets the number of items in the database according to the supplied find criteria.</summary>
        /// <param name="advancedFilter">The advanced filter.</param>
        /// <param name="filter">An additional filter to apply on the result query.</param>
        /// <returns>The total number of items in the database according to the supplied filter.</returns>
        int GetCount(
            AdvancedFilter advancedFilter,
            Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Gets the number of items in the database according to the supplied find criteria asynchronously.
        /// </summary>
        /// <param name="advancedFilter">The advanced filter.</param>
        /// <param name="filter">An additional filter to apply on the result query.</param>
        /// <returns>The total number of items in the database according to the supplied filter.</returns>
        Task<int> GetCountAsync(
            AdvancedFilter advancedFilter,
            Expression<Func<TEntity, bool>> filter = null);

        /// <summary>Gets the number of items in the database according to the supplied find criteria.</summary>
        /// <param name="orderByFilter">The order by filter.</param>
        /// <param name="filter">An additional filter to apply on the result query.</param>
        /// <returns>The total number of items in the database according to the supplied filter.</returns>
        int GetCount(
            OrderByFilter orderByFilter,
            Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Gets the number of items in the database according to the supplied find criteria asynchronously.
        /// </summary>
        /// <param name="orderByFilter">The order by filter.</param>
        /// <param name="filter">An additional filter to apply on the result query.</param>
        /// <returns>The total number of items in the database according to the supplied filter.</returns>
        Task<int> GetCountAsync(
            OrderByFilter orderByFilter,
            Expression<Func<TEntity, bool>> filter = null);

        /// <summary>Gets the number of items in the database according to the supplied find criteria.</summary>
        /// <param name="pagingFilter">The paging filter.</param>
        /// <param name="filter">An additional filter to apply on the result query.</param>
        /// <returns>The total number of items in the database according to the supplied filter.</returns>
        int GetCount(
            PagingFilter pagingFilter,
            Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Gets the number of items in the database according to the supplied find criteria asynchronously.
        /// </summary>
        /// <param name="pagingFilter">The paging filter.</param>
        /// <param name="filter">An additional filter to apply on the result query.</param>
        /// <returns>The total number of items in the database according to the supplied filter.</returns>
        Task<int> GetCountAsync(
            PagingFilter pagingFilter,
            Expression<Func<TEntity, bool>> filter = null);

        /// <summary>Gets the number of items in the database according to the supplied find criteria.</summary>
        /// <param name="searchFilter">The search filter.</param>
        /// <param name="filter">An additional filter to apply on the result query.</param>
        /// <returns>The total number of items in the database according to the supplied filter.</returns>
        int GetCount(
            SearchFilter searchFilter,
            Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Gets the number of items in the database according to the supplied find criteria asynchronously.
        /// </summary>
        /// <param name="searchFilter">The search filter.</param>
        /// <param name="filter">An additional filter to apply on the result query.</param>
        /// <returns>The total number of items in the database according to the supplied filter.</returns>
        Task<int> GetCountAsync(
            SearchFilter searchFilter,
            Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Gets a single entity.
        /// </summary>
        /// <param name="filter">A filter to retrieve the entity.</param>
        /// <param name="includeProperties">The properties that should be include in the query.</param>
        /// <returns>An entity.</returns>
        TEntity GetSingle(
            Expression<Func<TEntity, bool>> filter,
            params string[] includeProperties);

        /// <summary>
        /// Gets a single entity asynchronously.
        /// </summary>
        /// <param name="filter">A filter to retrieve the entity.</param>
        /// <param name="includeProperties">The properties that should be include in the query.</param>
        /// <returns>A task that returns an entity.</returns>
        Task<TEntity> GetSingleAsync(
            Expression<Func<TEntity, bool>> filter,
            params string[] includeProperties);
    }
}
