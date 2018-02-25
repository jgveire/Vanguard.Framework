namespace Vanguard.Framework.Data.Repositories
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Vanguard.Framework.Core;
    using Vanguard.Framework.Core.Repositories;

    /// <summary>
    /// The repository base class.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    public class RepositoryBase<TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{TDbContext}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public RepositoryBase(TDbContext dbContext)
        {
            Guard.ArgumentNotNull(dbContext, nameof(dbContext));
            DbContext = dbContext;
        }

        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        protected TDbContext DbContext { get; }

        /// <summary>
        /// Applies the find criteria to the querable.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The queryable.</param>
        /// <param name="searchCriteria">The find criteria.</param>
        /// <returns>A collection of entities.</returns>
        [Obsolete("Make use of the Vanguard.Framework.Data.Repositories.QueryableExtensions.ApplySearch instead.")]
        protected IQueryable<TEntity> ApplyFindCriteria<TEntity>(
            IQueryable<TEntity> query,
            SearchCriteria searchCriteria)
            where TEntity : class, IDataEntity
        {
            return query.ApplySearch(searchCriteria);
        }
    }
}
