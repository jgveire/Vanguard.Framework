using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
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
        where TEntity : class, IDataEntity
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

        /// <inheritdoc />
        public IEnumerable<TEntity> Find(
            FindCriteria findCriteria = null,
            Expression<Func<TEntity, bool>> filter = null)
        {
            var query = CompileQuery(findCriteria, filter);
            return query.ToList();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> FindAsync(
            FindCriteria findCriteria,
            Expression<Func<TEntity, bool>> filter = null)
        {
            var query = CompileQuery(findCriteria, filter);
            return await query.ToListAsync();
        }

        /// <inheritdoc />
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params string[] includeProperties)
        {
            var query = CompileQuery(filter, orderBy, includeProperties);
            return query.ToList();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params string[] includeProperties)
        {
            var query = CompileQuery(filter, orderBy, includeProperties);
            return await query.ToListAsync();
        }

        /// <inheritdoc />
        public virtual TEntity GetById(params object[] id)
        {
            return DbSet.Find(id);
        }

        /// <inheritdoc />
        public async Task<TEntity> GetByIdAsync(params object[] id)
        {
            return await DbSet.FindAsync(id);
        }

        /// <inheritdoc />
        public int GetCount(FindCriteria findCriteria)
        {
            IQueryable<TEntity> query = DbSet;
            if (findCriteria != null && !string.IsNullOrEmpty(findCriteria.Search))
            {
                query = query.Search(findCriteria.Search);
            }

            return query.Count();
        }

        /// <inheritdoc />
        public int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
        }

        /// <inheritdoc />
        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.CountAsync();
        }

        /// <inheritdoc />
        public async Task<int> GetCountAsync(FindCriteria findCriteria)
        {
            IQueryable<TEntity> query = DbSet;
            if (findCriteria != null && !string.IsNullOrEmpty(findCriteria.Search))
            {
                query = query.Search(findCriteria.Search);
            }

            return await query.CountAsync();
        }

        private static ICollection<string> GetEntityProperties()
        {
            if (_entityProperties == null)
            {
                _entityProperties = new List<string>();

                Type type = typeof(TEntity);
                var properties = type
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(prop => prop.PropertyType.IsPrimitive || prop.PropertyType.IsEnum || prop.PropertyType == typeof(string));

                properties.ForEach(property => _entityProperties.Add(property.Name));
            }

            return _entityProperties;
        }

        private IQueryable<TEntity> CompileQuery(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
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
                query = orderBy(query);
            }

            return query;
        }

        private IQueryable<TEntity> CompileQuery(
            FindCriteria findCriteria,
            Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = DbSet;

            // Filter
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (findCriteria != null)
            {
                Validate(findCriteria);

                // Search
                if (!string.IsNullOrEmpty(findCriteria.Search))
                {
                    query = query.Search(findCriteria.Search);
                }

                // Order by
                if (!string.IsNullOrEmpty(findCriteria.OrderBy))
                {
                    query = query.OrderBy(findCriteria.OrderBy, findCriteria.SortOrder);
                }

                // Select
                if (!string.IsNullOrWhiteSpace(findCriteria.Select))
                {
                    string[] fields = GetEntityProperties(findCriteria.Select);
                    query = query.Select(fields);
                }

                // Paging
                query = query.GetPage(findCriteria.Page, findCriteria.PageSize);
            }

            return query;
        }

        private string[] GetEntityProperties(string select)
        {
            IEnumerable<string> items = select
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(item => item.Trim());
            items = items.Where(item => EntityProperties.Contains(item, StringComparer.InvariantCultureIgnoreCase));

            return items.ToArray();
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
    }
}
