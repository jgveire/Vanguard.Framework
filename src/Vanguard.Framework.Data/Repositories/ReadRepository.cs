using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Vanguard.Framework.Core;
using Vanguard.Framework.Core.Exceptions;
using Vanguard.Framework.Core.Extensions;
using Vanguard.Framework.Core.Repositories;
using Vanguard.Framework.Data.Resources;

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
        /// Finds entities accoording to the supplied find criteria.
        /// </summary>
        /// <param name="findCriteria">The find criteria.</param>
        /// <returns>A collection of entities.</returns>
        public IEnumerable<TEntity> Find(FindCriteria findCriteria = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (findCriteria == null)
            {
                return query.ToList();
            }

            Validate(findCriteria);

            if (!string.IsNullOrEmpty(findCriteria.Search))
            {
                query = query.Search(findCriteria.Search);
            }

            if (!string.IsNullOrEmpty(findCriteria.OrderBy) && findCriteria.SortOrder == SortOrder.Asc)
            {
                query = query.OrderBy(findCriteria.OrderBy);
            }
            else if (!string.IsNullOrEmpty(findCriteria.OrderBy) && findCriteria.SortOrder == SortOrder.Desc)
            {
                query = query.OrderByDescending(findCriteria.OrderBy);
            }

            query = query.GetPage(findCriteria.Page, findCriteria.PageSize);

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

        /// <summary>
        /// Gets the number of items in the database accoording to the supplied find criteria.
        /// </summary>
        /// <param name="findCriteria">The find criteria.</param>
        /// <returns>
        /// The total number of items in the database accoording to the supplied find criteria.
        /// </returns>
        public int GetCount(FindCriteria findCriteria)
        {
            IQueryable<TEntity> query = DbSet;

            if (findCriteria != null && !string.IsNullOrEmpty(findCriteria.Search))
            {
                query = query.Search(findCriteria.Search);
            }

            return query.Count();
        }

        /// <summary>
        /// Gets the number of items in the database accoording to the supplied filter.
        /// </summary>
        /// <param name="filter">The filter predicate.</param>
        /// <returns>
        /// The total number of items in the database accoording to the supplied filter.
        /// </returns>
        public int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
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

                properties.ForEach(property => _entityProperties.Add(property.Name));
            }

            return _entityProperties;
        }

        private void Validate(FindCriteria findCriteria)
        {
            if (!string.IsNullOrWhiteSpace(findCriteria.OrderBy))
            {
                ValidateOrderBy(findCriteria.OrderBy);
            }
        }

        private void ValidateOrderBy(string orderBy)
        {
            if (!EntityProperties.Contains(orderBy, StringComparer.InvariantCultureIgnoreCase))
            {
                string message = string.Format(ExceptionResource.CannotOrderBy, orderBy);
                throw new ValidationException(message, nameof(FindCriteria.OrderBy));
            }
        }

        private string CleanSelect(string select)
        {
            IEnumerable<string> items = select
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(item => item.Trim());
            items = items.Where(item => EntityProperties.Contains(item, StringComparer.InvariantCultureIgnoreCase));

            return string.Join(',', items);
        }
    }
}
