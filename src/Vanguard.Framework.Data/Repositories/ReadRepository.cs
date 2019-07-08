namespace Vanguard.Framework.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Vanguard.Framework.Core;
    using Vanguard.Framework.Core.Extensions;
    using Vanguard.Framework.Core.Repositories;
    using Vanguard.Framework.Data.Extensions;
    using Vanguard.Framework.Data.Resources;

    /// <summary>
    /// The read repository.
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
        public virtual bool Contains(object id)
        {
            Guard.ArgumentNotNull(id, nameof(id));
            return Contains(new[] { id });
        }

        /// <inheritdoc />
        public virtual bool Contains(object[] ids)
        {
            Guard.ArgumentNotNull(ids, nameof(ids));
            var pairs = GetIdValuePairs(ids);
            return DbSet.WhereEqual(pairs).Any();
        }

        /// <inheritdoc />
        public virtual async Task<bool> ContainsAsync(object id)
        {
            Guard.ArgumentNotNull(id, nameof(id));
            return await ContainsAsync(new[] { id });
        }

        /// <inheritdoc />
        public virtual async Task<bool> ContainsAsync(object[] ids)
        {
            Guard.ArgumentNotNull(ids, nameof(ids));
            var pairs = GetIdValuePairs(ids);
            return await DbSet.WhereEqual(pairs).AnyAsync();
        }

        /// <inheritdoc />
        [Obsolete("Use one of the following filters: AdvancedFilter, OrderByFilter, PagingFilter or SearchFilter")]
        public virtual IEnumerable<TEntity> Find(
            FilterQuery filterQuery,
            Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = DbSet;

            // Filter
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Filter(filterQuery).ToList();
        }

        /// <inheritdoc />
        [Obsolete("Use one of the following filters: AdvancedFilter, OrderByFilter, PagingFilter or SearchFilter")]
        public virtual async Task<IEnumerable<TEntity>> FindAsync(
            FilterQuery filterQuery,
            Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = DbSet;

            // Filter
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.Filter(filterQuery).ToListAsync();
        }

        /// <inheritdoc />
        public virtual IEnumerable<TEntity> Find(
            AdvancedFilter advancedFilter,
            Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = DbSet;

            // Filter
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Filter(advancedFilter).ToList();
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TEntity>> FindAsync(
            AdvancedFilter advancedFilter,
            Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = DbSet;

            // Filter
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.Filter(advancedFilter).ToListAsync();
        }

        /// <inheritdoc />
        public virtual IEnumerable<TEntity> Find(
            OrderByFilter orderByFilter,
            Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = DbSet;

            // Filter
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Filter(orderByFilter).ToList();
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TEntity>> FindAsync(
            OrderByFilter orderByFilter,
            Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = DbSet;

            // Filter
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.Filter(orderByFilter).ToListAsync();
        }

        /// <inheritdoc />
        public virtual IEnumerable<TEntity> Find(
            PagingFilter pagingFilter,
            Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = DbSet;

            // Filter
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Filter(pagingFilter).ToList();
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TEntity>> FindAsync(
            PagingFilter pagingFilter,
            Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = DbSet;

            // Filter
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.Filter(pagingFilter).ToListAsync();
        }

        /// <inheritdoc />
        public virtual IEnumerable<TEntity> Find(
            SearchFilter? searchFilter,
            Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = DbSet;

            // Filter
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Filter(searchFilter).ToList();
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TEntity>> FindAsync(
            SearchFilter? searchFilter,
            Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = DbSet;

            // Filter
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.Filter(searchFilter).ToListAsync();
        }

        /// <inheritdoc />
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            params string[] includeProperties)
        {
            var query = CompileQuery(filter, orderBy, includeProperties);
            return query.ToList();
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            params string[] includeProperties)
        {
            var query = CompileQuery(filter, orderBy, includeProperties);
            return await query.ToListAsync();
        }

        /// <inheritdoc />
        public virtual TEntity GetById(object id, params string[] includeProperties)
        {
            Guard.ArgumentNotNull(id, nameof(id));
            return GetById(new object[] { id }, includeProperties);
        }

        /// <inheritdoc />
        public virtual TEntity GetById(object[] ids, params string[] includeProperties)
        {
            Guard.ArgumentNotNull(ids, nameof(ids));
            var pairs = GetIdValuePairs(ids);

            var query = DbSet
                .Include(includeProperties)
                .WhereEqual(pairs);

            return query.SingleOrDefault();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> GetByIdAsync(object id, params string[] includeProperties)
        {
            Guard.ArgumentNotNull(id, nameof(id));
            return await GetByIdAsync(new object[] { id }, includeProperties);
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> GetByIdAsync(object[] id, params string[] includeProperties)
        {
            Guard.ArgumentNotNull(id, nameof(id));
            var pairs = GetIdValuePairs(id);

            var query = DbSet
                .Include(includeProperties)
                .WhereEqual(pairs);

            return await query.SingleOrDefaultAsync();
        }

        /// <inheritdoc />
        [Obsolete("Make use of the AdvancedFilter, SearchFilter, OrderByFilter or PagingFilter class.")]
        public virtual int GetCount(FilterQuery filterQuery)
        {
            IQueryable<TEntity> query = DbSet;
            if (!string.IsNullOrEmpty(filterQuery?.Search))
            {
                query = query.Search(filterQuery.Search);
            }

            if (!string.IsNullOrEmpty(filterQuery?.Filter))
            {
                query = query.Filter(filterQuery.Filter);
            }

            return query.Count();
        }

        /// <inheritdoc />
        [Obsolete("Make use of the AdvancedFilter, SearchFilter, OrderByFilter or PagingFilter class.")]
        public int GetCount(FilterQuery filterQuery, Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(filterQuery?.Search))
            {
                query = query.Search(filterQuery.Search);
            }

            if (!string.IsNullOrEmpty(filterQuery?.Filter))
            {
                query = query.Filter(filterQuery.Filter);
            }

            return query.Count();
        }

        /// <inheritdoc />
        public virtual int GetCount(Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
        }

        /// <inheritdoc />
        [Obsolete("Make use of the AdvancedFilter, SearchFilter, OrderByFilter or PagingFilter class.")]
        public async Task<int> GetCountAsync(FilterQuery filterQuery, Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(filterQuery?.Search))
            {
                query = query.Search(filterQuery.Search);
            }

            if (!string.IsNullOrEmpty(filterQuery?.Filter))
            {
                query = query.Filter(filterQuery.Filter);
            }

            return await query.CountAsync();
        }

        /// <inheritdoc />
        public virtual async Task<int> GetCountAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.CountAsync();
        }

        /// <inheritdoc />
        public int GetCount(AdvancedFilter advancedFilter, Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Filter(advancedFilter).Count();
        }

        /// <inheritdoc />
        public async Task<int> GetCountAsync(AdvancedFilter advancedFilter, Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.Filter(advancedFilter).CountAsync();
        }

        /// <inheritdoc />
        public int GetCount(OrderByFilter orderByFilter, Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Filter(orderByFilter).Count();
        }

        /// <inheritdoc />
        public async Task<int> GetCountAsync(OrderByFilter orderByFilter, Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.Filter(orderByFilter).CountAsync();
        }

        /// <inheritdoc />
        public int GetCount(PagingFilter pagingFilter, Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Filter(pagingFilter).Count();
        }

        /// <inheritdoc />
        public async Task<int> GetCountAsync(PagingFilter pagingFilter, Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.Filter(pagingFilter).CountAsync();
        }

        /// <inheritdoc />
        public int GetCount(SearchFilter? searchFilter, Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Filter(searchFilter).Count();
        }

        /// <inheritdoc />
        public async Task<int> GetCountAsync(SearchFilter? searchFilter, Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.Filter(searchFilter).CountAsync();
        }

        /// <inheritdoc />
        public virtual TEntity GetSingle(
            Expression<Func<TEntity, bool>> filter,
            params string[] includeProperties)
        {
            Guard.ArgumentNotNull(filter, nameof(filter));
            return DbSet
                .Include(includeProperties)
                .Where(filter)
                .SingleOrDefault();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> GetSingleAsync(
            Expression<Func<TEntity, bool>> filter,
            params string[] includeProperties)
        {
            Guard.ArgumentNotNull(filter, nameof(filter));
            return await DbSet
                .Include(includeProperties)
                .Where(filter)
                .SingleOrDefaultAsync();
        }

        /// <inheritdoc />
        [Obsolete("Make use of the AdvancedFilter, SearchFilter, OrderByFilter or PagingFilter class.")]
        public virtual async Task<int> GetCountAsync(FilterQuery filterQuery)
        {
            IQueryable<TEntity> query = DbSet;
            if (!string.IsNullOrEmpty(filterQuery?.Search))
            {
                query = query.Search(filterQuery.Search);
            }

            if (!string.IsNullOrEmpty(filterQuery?.Filter))
            {
                query = query.Filter(filterQuery.Filter);
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
                    .Where(prop => prop.PropertyType.IsPrimitive ||
                                   prop.PropertyType.IsEnum ||
                                   prop.PropertyType.IsString() ||
                                   prop.PropertyType.IsDateTime());

                properties.ForEach(property => _entityProperties.Add(property.Name));
            }

            return _entityProperties;
        }

        private IQueryable<TEntity> CompileQuery(
            Expression<Func<TEntity, bool>>? filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy,
            params string[] includeProperties)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                query = query.Include(includeProperties);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }

        private List<KeyValuePair<string, object>> GetIdValuePairs(object[] id)
        {
            IEntityType entityType = DbContext.Model.FindEntityType(typeof(TEntity));
            if (entityType == null)
            {
                var messge = string.Format(ExceptionResource.CannotFindEntityType, typeof(TEntity).FullName);
                throw new InvalidOperationException(messge);
            }

            IKey primaryKey = entityType.FindPrimaryKey();
            if (primaryKey == null)
            {
                var messge = string.Format(ExceptionResource.CannotFindPrimaryKey, typeof(TEntity).FullName);
                throw new InvalidOperationException(messge);
            }

            if (primaryKey.Properties.Count != id.Length)
            {
                var messge = string.Format(ExceptionResource.InvalidIdCount, primaryKey.Properties.Count, id.Length);
                throw new InvalidOperationException(messge);
            }

            var pairs = new List<KeyValuePair<string, object>>();
            for (int i = 0; i < primaryKey.Properties.Count; i++)
            {
                pairs.Add(new KeyValuePair<string, object>(primaryKey.Properties[i].Name, id[i]));
            }

            return pairs;
        }
    }
}
