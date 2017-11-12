using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <param name="findCriteria">The find criteria.</param>
        /// <returns>A collection of entities.</returns>
        protected IQueryable<TEntity> ApplyFindCriteria<TEntity>(
            IQueryable<TEntity> query,
            FindCriteria findCriteria)
            where TEntity : class, IDataEntity
        {
            if (findCriteria == null)
            {
                return query;
            }

            Validate<TEntity>(findCriteria);

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
                string[] fields = GetEntityProperties<TEntity>(findCriteria.Select);
                query = query.Select(fields);
            }

            // Paging
            query = query.GetPage(findCriteria.Page, findCriteria.PageSize);

            return query;
        }

        private static IEnumerable<string> GetEntityProperties<TEntity>()
        {
            var type = typeof(TEntity);
            var properties = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(prop => prop.PropertyType.IsPrimitive || prop.PropertyType.IsEnum || prop.PropertyType == typeof(string));
            return properties.Select(property => property.Name);
        }

        private string[] GetEntityProperties<TEntity>(string select)
        {
            IEnumerable<string> selectItems = select
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(item => item.Trim());
            IEnumerable<string> items = GetEntityProperties<TEntity>()
                .Where(item => selectItems.Contains(item, StringComparer.InvariantCultureIgnoreCase));

            return items.ToArray();
        }

        private void Validate<TEntity>(FindCriteria findCriteria)
        {
            if (!string.IsNullOrWhiteSpace(findCriteria.OrderBy))
            {
                ValidateOrderBy<TEntity>(findCriteria.OrderBy);
            }
        }

        private void ValidateOrderBy<TEntity>(string orderBy)
        {
            if (!GetEntityProperties<TEntity>().Contains(orderBy, StringComparer.InvariantCultureIgnoreCase))
            {
                string message = string.Format(ExceptionResource.CannotOrderBy, orderBy);
                throw new ValidationException(message, nameof(FindCriteria.OrderBy));
            }
        }
    }
}
