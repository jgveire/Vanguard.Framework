namespace Vanguard.Framework.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Vanguard.Framework.Core;
    using Vanguard.Framework.Core.Extensions;
    using Vanguard.Framework.Core.Repositories;

    /// <summary>
    /// The read repository class.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="IReadRepository{TEntity}" />
    public class ReadRepository<TEntity> : RepositoryBase<DbContext>, IReadRepository<TEntity>
        where TEntity : class, IDataEntity
    {
        private static ICollection<string> _entityProperties;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public ReadRepository(DbContext dbContext)
            : base(dbContext)
        {
            Guard.ArgumentNotNull(dbContext, nameof(dbContext));
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
        /// Gets the database set.
        /// </summary>
        /// <value>
        /// The database set.
        /// </value>
        protected DbSet<TEntity> DbSet { get; }

        /// <inheritdoc />
        public IEnumerable<TEntity> Find(
            SearchCriteria searchCriteria = null,
            Expression<Func<TEntity, bool>> filter = null)
        {
            var query = CompileQuery(searchCriteria, filter);
            return query.ToList();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> FindAsync(
            SearchCriteria searchCriteria,
            Expression<Func<TEntity, bool>> filter = null)
        {
            var query = CompileQuery(searchCriteria, filter);
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
        public int GetCount(SearchCriteria searchCriteria)
        {
            IQueryable<TEntity> query = DbSet;
            if (searchCriteria != null && !string.IsNullOrEmpty(searchCriteria.Search))
            {
                query = query.Search(searchCriteria.Search);
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
        public async Task<int> GetCountAsync(SearchCriteria searchCriteria)
        {
            IQueryable<TEntity> query = DbSet;
            if (searchCriteria != null && !string.IsNullOrEmpty(searchCriteria.Search))
            {
                query = query.Search(searchCriteria.Search);
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
            SearchCriteria searchCriteria,
            Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = DbSet;

            // Filter
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ApplySearch(searchCriteria);
        }
    }
}
