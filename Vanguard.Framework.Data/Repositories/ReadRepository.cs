using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Vanguard.Framework.Core;
using Vanguard.Framework.Core.Repositories;

namespace Vanguard.Framework.Data.Repositories
{
    /// <summary>
    /// The read repository class.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="IReadRepository{TEntity}" />
    public class ReadRepository<TEntity> : IReadRepository<TEntity>
        where TEntity : class, IEntity
    {
        private static ICollection<string> _entityProperties;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public ReadRepository(DbContext dbContext)
        {
            Guard.ArgumentNotNull(dbContext, nameof(dbContext));
            DbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
            DbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        /// <summary>
        /// Gets a collection of entity properties.
        /// </summary>
        /// <value>
        /// A collection of entity properties.
        /// </value>
        protected static ICollection<string> EntityProperties => GetEntityProperties();

        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        protected DbContext DbContext { get; }

        /// <summary>
        /// Gets the database set.
        /// </summary>
        /// <value>
        /// The database set.
        /// </value>
        protected DbSet<TEntity> DbSet { get; }

        /// <summary>
        /// Finds entities accoording to the supplied find data.
        /// </summary>
        /// <param name="findData">The find data.</param>
        /// <returns>A collection of entities.</returns>
        public IEnumerable<TEntity> Find(FindData findData = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (findData == null)
            {
                return query.ToList();
            }

            Validate(findData);

            if (!string.IsNullOrEmpty(findData.Filter))
            {
                query = query.Where(findData.Filter);
            }

            if (string.IsNullOrEmpty(findData.Select))
            {
                string select = CleanSelect(findData.Select);
                if (string.IsNullOrEmpty(select))
                {
                    query = query.Select<TEntity>(select);
                }
            }

            if (!string.IsNullOrEmpty(findData.OrderBy) && findData.SortOrder == SortOrder.Asc)
            {
                query = query.OrderBy(findData.OrderBy);
            }
            else if (!string.IsNullOrEmpty(findData.OrderBy) && findData.SortOrder == SortOrder.Desc)
            {
                query = query.OrderBy(findData.OrderBy);
            }

            query = query.GetPage(findData.Page, findData.PageSize);

            return query.ToList();
        }

        /// <summary>
        /// Gets a entity by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A entity.</returns>
        public virtual TEntity GetById(params object[] id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Gets the enties accoording to the specified filter.
        /// </summary>
        /// <param name="filter">The filter predicate.</param>
        /// <param name="orderBy">The order by predicate.</param>
        /// <param name="includeProperties">The properties that should be include in the query.</param>
        /// <returns>A collection of TEntity.</returns>
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params string[] includeProperties)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        private static ICollection<string> GetEntityProperties()
        {
            if (_entityProperties == null)
            {
                _entityProperties = new List<string>();

                Type type = typeof(TEntity);
                var properties = type
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => x.PropertyType.IsPrimitive || x.PropertyType.IsEnum || x.PropertyType == typeof(string));
            }

            return _entityProperties;
        }

        private void Validate(FindData findData)
        {
        }

        private string CleanSelect(string select)
        {
            IEnumerable<string> items = select
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(item => item.Trim());
            items = items.Where(item => EntityProperties.Contains(item));

            return string.Join(',', items);
        }
    }
}
