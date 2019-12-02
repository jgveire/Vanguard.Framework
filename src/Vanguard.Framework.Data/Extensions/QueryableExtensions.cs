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
    using Vanguard.Framework.Core.Collections;
    using Vanguard.Framework.Core.Exceptions;
    using Vanguard.Framework.Core.Extensions;
    using Vanguard.Framework.Core.Parsers;
    using Vanguard.Framework.Core.Repositories;
    using Vanguard.Framework.Data.Extensions;
    using Vanguard.Framework.Data.Resources;

    /// <summary>
    /// The queryable extension method class.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Applies the advanced filter to the queryable.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="source">The queryable.</param>
        /// <param name="advancedFilter">The advanced filter.</param>
        /// <returns>A collection of entities.</returns>
        public static IQueryable<TEntity> Filter<TEntity>(
            this IQueryable<TEntity> source,
            AdvancedFilter? advancedFilter)
            where TEntity : class, IDataEntity
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            else if (advancedFilter == null)
            {
                return source;
            }

            ValidateOrderBy<TEntity>(advancedFilter);
            ValidateInclude<TEntity>(advancedFilter);

            // Include
            if (advancedFilter.Include != null && !string.IsNullOrWhiteSpace(advancedFilter.Include))
            {
                var paths = advancedFilter.Include.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                paths = advancedFilter.PropertyMappings.MapProperties(paths).ToArray();
                source = source.Include(paths);
            }

            // Filter
            if (advancedFilter.Filter != null && !string.IsNullOrEmpty(advancedFilter.Filter))
            {
                source = source.Filter(advancedFilter.Filter, advancedFilter.PropertyMappings);
            }

            // Order by
            if (advancedFilter.OrderBy != null && !string.IsNullOrEmpty(advancedFilter.OrderBy))
            {
                var orderBy = advancedFilter.PropertyMappings.MapProperty(advancedFilter.OrderBy);
                source = source.OrderBy(orderBy, advancedFilter.SortOrder);
            }

            // Select
            if (advancedFilter.Select != null && !string.IsNullOrWhiteSpace(advancedFilter.Select))
            {
                var paths = advancedFilter.Select.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                paths = advancedFilter.PropertyMappings.MapProperties(paths).ToArray();
                string[] fields = GetEntityProperties<TEntity>(paths);
                source = source.Select(fields);
            }

            // Paging
            source = source.GetPage(advancedFilter.Page, advancedFilter.PageSize);

            return source;
        }

        /// <summary>
        /// Applies the order by filter to the queryable.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="source">The queryable.</param>
        /// <param name="orderByFilter">The order by filter.</param>
        /// <returns>A collection of entities.</returns>
        public static IQueryable<TEntity> Filter<TEntity>(
            this IQueryable<TEntity> source,
            OrderByFilter? orderByFilter)
            where TEntity : class, IDataEntity
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            else if (orderByFilter == null)
            {
                return source;
            }

            ValidateOrderBy<TEntity>(orderByFilter);

            // Order by
            if (orderByFilter.OrderBy != null && !string.IsNullOrEmpty(orderByFilter.OrderBy))
            {
                var orderBy = orderByFilter.PropertyMappings.MapProperty(orderByFilter.OrderBy);
                source = source.OrderBy(orderBy, orderByFilter.SortOrder);
            }

            // Paging
            source = source.GetPage(orderByFilter.Page, orderByFilter.PageSize);

            return source;
        }

        /// <summary>
        /// Applies the paging filter to the queryable.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="source">The queryable.</param>
        /// <param name="pagingFilter">The paging filter.</param>
        /// <returns>A collection of entities.</returns>
        public static IQueryable<TEntity> Filter<TEntity>(
            this IQueryable<TEntity> source,
            PagingFilter? pagingFilter)
            where TEntity : class, IDataEntity
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            else if (pagingFilter == null)
            {
                return source;
            }

            // Paging
            return source.GetPage(pagingFilter.Page, pagingFilter.PageSize);
        }

        /// <summary>
        /// Applies the search filter to the queryable.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="source">The queryable.</param>
        /// <param name="searchFilter">The search filter.</param>
        /// <returns>A collection of entities.</returns>
        public static IQueryable<TEntity> Filter<TEntity>(
            this IQueryable<TEntity> source,
            SearchFilter? searchFilter)
            where TEntity : class, IDataEntity
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            else if (searchFilter == null)
            {
                return source;
            }

            ValidateOrderBy<TEntity>(searchFilter);

            // Search
            if (searchFilter.Search != null && !string.IsNullOrEmpty(searchFilter.Search))
            {
                source = source.Search(searchFilter.Search);
            }

            // Order by
            if (searchFilter.OrderBy != null && !string.IsNullOrEmpty(searchFilter.OrderBy))
            {
                var orderBy = searchFilter.PropertyMappings.MapProperty(searchFilter.OrderBy);
                source = source.OrderBy(orderBy, searchFilter.SortOrder);
            }

            // Paging
            source = source.GetPage(searchFilter.Page, searchFilter.PageSize);

            return source;
        }

        /// <summary>
        /// Filters the collection.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="filter">The filter to apply.</param>
        /// <returns>A <see cref="IQueryable{T}"/> whose elements are filtered according to the specified filter.</returns>
        public static IQueryable<TEntity> Filter<TEntity>(this IQueryable<TEntity> source, string filter)
        {
            var propertyMapper = new PropertyMappings();
            return Filter(source, filter, propertyMapper);
        }

        /// <summary>
        /// Filters the collection.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="filter">The filter to apply.</param>
        /// <param name="propertyMapper">The property mapper.</param>
        /// <returns>A <see cref="IQueryable{T}" /> whose elements are filtered according to the specified filter.</returns>
        /// <exception cref="ValidationException">Thrown when the filter is not in the correct format.</exception>
        public static IQueryable<TEntity> Filter<TEntity>(this IQueryable<TEntity> source, string filter, IPropertyMapper propertyMapper)
        {
            Guard.ArgumentNotNullOrEmpty(filter, nameof(filter));
            var parser = new FilterParser<TEntity>(filter);

            try
            {
                var result = parser.ApplyFilter(source);
                return result;
            }
            catch (FormatException exception)
            {
                throw new ValidationException(exception.Message, exception);
            }
        }

        /// <summary>
        /// Gets the specified page of a sequence of elements.
        /// </summary>
        /// <typeparam name="TEntity">The type of the elements of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="page">The page to retrieve.</param>
        /// <param name="pageSize">Size of an page.</param>
        /// <returns>A <see cref="IQueryable{T}"/> whose elements are restricted to the specified page.</returns>
        public static IQueryable<TEntity> GetPage<TEntity>(this IQueryable<TEntity> source, int page, int pageSize)
        {
            int index = Math.Max(1, page) - 1;
            int skip = index * pageSize;
            int take = Math.Max(1, pageSize);

            var result = source;
            if (skip != 0)
            {
                result = result.Skip(skip);
            }

            if (take != int.MaxValue)
            {
                result = result.Take(take);
            }

            return result;
        }

        /// <summary>
        /// Gets the specified page of a sequence of elements.
        /// </summary>
        /// <typeparam name="TEntity">The type of the elements of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="navigationPropertyPaths">The navigation property paths to include in the query.</param>
        /// <returns>
        /// A <see cref="IQueryable{T}" /> whose elements are restricted to the specified page.
        /// </returns>
        public static IQueryable<TEntity> Include<TEntity>(this IQueryable<TEntity> source, string[] navigationPropertyPaths)
            where TEntity : class
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            else if (navigationPropertyPaths == null)
            {
                return source;
            }

            var query = source;
            foreach (string navigationPropertyPath in navigationPropertyPaths)
            {
                query = query.Include(navigationPropertyPath);
            }

            return query;
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TEntity">The type of the elements of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="memberPath">The ordering.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns>A <see cref="IQueryable{T}"/> whose elements are sorted according to the specified ordering.</returns>
        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string memberPath, SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.Desc)
            {
                return source.OrderByDescending(memberPath);
            }

            return source.OrderBy(memberPath);
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TEntity">The type of the elements of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="memberPath">The ordering.</param>
        /// <returns>A <see cref="IQueryable{T}"/> whose elements are sorted according to the specified ordering.</returns>
        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string memberPath)
        {
            return source.OrderByUsing(memberPath, "OrderBy");
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order according to a key.
        /// </summary>
        /// <typeparam name="TEntity">The type of the elements of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="memberPath">The ordering.</param>
        /// <returns>A <see cref="IQueryable{T}"/> whose elements are sorted according to the specified ordering.</returns>
        public static IOrderedQueryable<TEntity> OrderByDescending<TEntity>(this IQueryable<TEntity> source, string memberPath)
        {
            return source.OrderByUsing(memberPath, "OrderByDescending");
        }

        /// <summary>
        /// Searches for elements that have properties that contain the specified search string.
        /// </summary>
        /// <typeparam name="TEntity">The type of the elements of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="searchString">The search string.</param>
        /// <returns>A <see cref="IQueryable{T}"/> whose elements are filtered according to the specified search string.</returns>
        public static IQueryable<TEntity> Search<TEntity>(this IQueryable<TEntity> source, string searchString)
        {
            Guard.ArgumentNotNullOrEmpty(searchString, nameof(searchString));

            IEnumerable<PropertyInfo> properties = GetSearchableProperties(typeof(TEntity));
            if (!properties.Any())
            {
                return source;
            }

            Expression? expression = null;
            var parameter = Expression.Parameter(typeof(TEntity), "item");
            foreach (var property in properties)
            {
                Expression? searchExpression = GetSearchExpression(parameter, property, searchString);
                if (searchExpression == null)
                {
                    continue;
                }

                if (expression == null)
                {
                    expression = searchExpression;
                }
                else
                {
                    expression = Expression.Or(expression, searchExpression);
                }
            }

            var methodCall = Expression.Call(
                typeof(Queryable),
                "Where",
                new[] { parameter.Type },
                source.Expression,
                Expression.Lambda(expression, parameter));
            return (IQueryable<TEntity>)source.Provider.CreateQuery(methodCall);
        }

        /// <summary>
        /// Selects the specified fields, all other fields will have their
        /// default value.
        /// </summary>
        /// <typeparam name="TEntity">The type of the elements of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="fields">The fields to select.</param>
        /// <returns>A <see cref="IQueryable{T}"/>.</returns>
        public static IQueryable<TEntity> Select<TEntity>(this IQueryable<TEntity> source, params string[] fields)
        {
            if (fields == null || fields.Length == 0)
            {
                return source;
            }

            // Input parameter "item"
            var parameter = Expression.Parameter(typeof(TEntity), "item");

            // New statement "new Data()"
            var newExpression = Expression.New(typeof(TEntity));

            // Create initializers
            var bindings = fields
                .Select(item =>
                    {
                        // Property "Field1"
                        var propertyInfo = typeof(TEntity).GetProperty(item);

                        // Original value "item.Field1"
                        var property = Expression.Property(parameter, propertyInfo);

                        // Set value "Field1 = item.Field1"
                        return Expression.Bind(propertyInfo, property);
                    });

            // Initialization "new Data { Field1 = item.Field1, Field2 = item.Field2 }"
            var memberInit = Expression.MemberInit(newExpression, bindings);

            // Expression "item => new Data { Field1 = item.Field1, Field2 = item.Field2 }"
            var lambda = Expression.Lambda<Func<TEntity, TEntity>>(memberInit, parameter);

            // Call select method "source.Select(item => new Data { Field1 = item.Field1, Field2 = item.Field2 })"
            var methodCall = Expression.Call(
                typeof(Queryable),
                "Select",
                new[] { parameter.Type, parameter.Type },
                source.Expression,
                lambda);

            // Create the final source
            return (IQueryable<TEntity>)source.Provider.CreateQuery(methodCall);
        }

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TEntity">The type of the elements of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="memberPath">The member path.</param>
        /// <returns>A <see cref="IQueryable{T}"/> whose elements are sorted according to the specified ordering.</returns>
        public static IOrderedQueryable<TEntity> ThenBy<TEntity>(this IOrderedQueryable<TEntity> source, string memberPath)
        {
            return source.OrderByUsing(memberPath, "ThenBy");
        }

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in descending order according to a key.
        /// </summary>
        /// <typeparam name="TEntity">The type of the elements of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="memberPath">The member path.</param>
        /// <returns>A <see cref="IQueryable{T}"/> whose elements are sorted according to the specified ordering.</returns>
        public static IOrderedQueryable<TEntity> ThenByDescending<TEntity>(this IOrderedQueryable<TEntity> source, string memberPath)
        {
            return source.OrderByUsing(memberPath, "ThenByDescending");
        }

        /// <summary>
        /// Applies the order by filter to the queryable.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="source">The queryable.</param>
        /// <param name="orderByFilter">The order by filter.</param>
        /// <returns>A collection of entities.</returns>
        public static FilterResult<TEntity> ToFilteredResult<TEntity>(
            this IQueryable<TEntity> source,
            OrderByFilter orderByFilter)
            where TEntity : class, IDataEntity
        {
            if (source == null)
            {
                return new FilterResult<TEntity>();
            }
            else if (orderByFilter == null)
            {
                return new FilterResult<TEntity>(source.GetPage(1, 20).ToList(), source.Count());
            }

            ValidateOrderBy<TEntity>(orderByFilter);

            // Order by
            if (orderByFilter.OrderBy != null && !string.IsNullOrEmpty(orderByFilter.OrderBy))
            {
                var orderBy = orderByFilter.PropertyMappings.MapProperty(orderByFilter.OrderBy);
                source = source.OrderBy(orderBy, orderByFilter.SortOrder);
            }

            // Paging
            return new FilterResult<TEntity>(source.GetPage(orderByFilter.Page, orderByFilter.PageSize).ToList(), source.Count());
        }

        /// <summary>
        /// Applies the paging filter to the queryable.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="source">The queryable.</param>
        /// <param name="pagingFilter">The paging filter.</param>
        /// <returns>A collection of entities.</returns>
        public static FilterResult<TEntity> ToFilteredResult<TEntity>(
            this IQueryable<TEntity> source,
            PagingFilter pagingFilter)
            where TEntity : class, IDataEntity
        {
            if (source == null)
            {
                return new FilterResult<TEntity>();
            }
            else if (pagingFilter == null)
            {
                return new FilterResult<TEntity>(source.GetPage(1, 20).ToList(), source.Count());
            }

            return new FilterResult<TEntity>(source.GetPage(pagingFilter.Page, pagingFilter.PageSize).ToList(), source.Count());
        }

        /// <summary>
        /// Applies the search filter to the queryable.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="source">The queryable.</param>
        /// <param name="searchFilter">The search filter.</param>
        /// <returns>A collection of entities.</returns>
        public static FilterResult<TEntity> ToFilteredResult<TEntity>(
            this IQueryable<TEntity> source,
            SearchFilter? searchFilter)
            where TEntity : class, IDataEntity
        {
            if (source == null)
            {
                return new FilterResult<TEntity>();
            }
            else if (searchFilter == null)
            {
                return new FilterResult<TEntity>(source.GetPage(1, 20).ToList(), source.Count());
            }

            ValidateOrderBy<TEntity>(searchFilter);

            // Search
            if (searchFilter.Search != null && !string.IsNullOrEmpty(searchFilter.Search))
            {
                source = source.Search(searchFilter.Search);
            }

            // Order by
            if (searchFilter.OrderBy != null && !string.IsNullOrEmpty(searchFilter.OrderBy))
            {
                var orderBy = searchFilter.PropertyMappings.MapProperty(searchFilter.OrderBy);
                source = source.OrderBy(orderBy, searchFilter.SortOrder);
            }

            // Paging
            return new FilterResult<TEntity>(source.GetPage(searchFilter.Page, searchFilter.PageSize).ToList(), source.Count());
        }

        /// <summary>
        /// Applies the order by filter to the queryable.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="source">The queryable.</param>
        /// <param name="orderByFilter">The order by filter.</param>
        /// <returns>A collection of entities.</returns>
        public static async Task<FilterResult<TEntity>> ToFilteredResultAsync<TEntity>(
            this IQueryable<TEntity> source,
            OrderByFilter orderByFilter)
            where TEntity : class, IDataEntity
        {
            List<TEntity> items;
            int count;
            if (source == null)
            {
                return new FilterResult<TEntity>();
            }
            else if (orderByFilter == null)
            {
                items = await source.GetPage(1, 20).ToListAsync();
                count = await source.CountAsync();
                return new FilterResult<TEntity>(items, count);
            }

            ValidateOrderBy<TEntity>(orderByFilter);

            // Order by
            if (orderByFilter.OrderBy != null && !string.IsNullOrEmpty(orderByFilter.OrderBy))
            {
                var orderBy = orderByFilter.PropertyMappings.MapProperty(orderByFilter.OrderBy);
                source = source.OrderBy(orderBy, orderByFilter.SortOrder);
            }

            // Paging
            items = await source.GetPage(orderByFilter.Page, orderByFilter.PageSize).ToListAsync();
            count = await source.CountAsync();
            return new FilterResult<TEntity>(items, count);
        }

        /// <summary>
        /// Applies the paging filter to the queryable.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="source">The queryable.</param>
        /// <param name="pagingFilter">The paging filter.</param>
        /// <returns>A collection of entities.</returns>
        public static async Task<FilterResult<TEntity>> ToFilteredResultAsync<TEntity>(
            this IQueryable<TEntity> source,
            PagingFilter pagingFilter)
            where TEntity : class, IDataEntity
        {
            List<TEntity> items;
            int count;
            if (source == null)
            {
                return new FilterResult<TEntity>();
            }
            else if (pagingFilter == null)
            {
                items = await source.GetPage(1, 20).ToListAsync();
                count = await source.CountAsync();
                return new FilterResult<TEntity>(items, count);
            }

            items = await source.GetPage(pagingFilter.Page, pagingFilter.PageSize).ToListAsync();
            count = await source.CountAsync();
            return new FilterResult<TEntity>(items, count);
        }

        /// <summary>
        /// Applies the search filter to the queryable.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="source">The queryable.</param>
        /// <param name="searchFilter">The search filter.</param>
        /// <returns>A collection of entities.</returns>
        public static async Task<FilterResult<TEntity>> ToFilteredResultAsync<TEntity>(
            this IQueryable<TEntity> source,
            SearchFilter? searchFilter)
            where TEntity : class, IDataEntity
        {
            List<TEntity> items;
            int count;
            if (source == null)
            {
                return new FilterResult<TEntity>();
            }
            else if (searchFilter == null)
            {
                items = await source.GetPage(1, 20).ToListAsync();
                count = await source.CountAsync();
                return new FilterResult<TEntity>(items, count);
            }

            ValidateOrderBy<TEntity>(searchFilter);

            // Search
            if (searchFilter.Search != null && !string.IsNullOrEmpty(searchFilter.Search))
            {
                source = source.Search(searchFilter.Search);
            }

            // Order by
            if (searchFilter.OrderBy != null && !string.IsNullOrEmpty(searchFilter.OrderBy))
            {
                var orderBy = searchFilter.PropertyMappings.MapProperty(searchFilter.OrderBy);
                source = source.OrderBy(orderBy, searchFilter.SortOrder);
            }

            // Paging
            items = await source.GetPage(searchFilter.Page, searchFilter.PageSize).ToListAsync();
            count = await source.CountAsync();
            return new FilterResult<TEntity>(items, count);
        }

        /// <summary>
        /// Searches for elements that have the supplied values.
        /// </summary>
        /// <typeparam name="TEntity">The type of the elements of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="keyValuePairs">The property name and value pairs.</param>
        /// <returns>A <see cref="IQueryable{T}"/> whose elements are filtered according to the specified property name and value pairs.</returns>
        internal static IQueryable<TEntity> WhereEqual<TEntity>(this IQueryable<TEntity> source, IEnumerable<KeyValuePair<string, object>> keyValuePairs)
            where TEntity : class
        {
            Guard.ArgumentNotNull(source, nameof(source));
            Guard.ArgumentNotNull(keyValuePairs, nameof(keyValuePairs));

            Expression? expression = null;
            var parameter = Expression.Parameter(typeof(TEntity), "item");
            foreach (KeyValuePair<string, object> keyValuePair in keyValuePairs)
            {
                Expression searchExpression = GetEqualExpression(parameter, keyValuePair.Key, keyValuePair.Value);
                if (expression == null)
                {
                    expression = searchExpression;
                }
                else
                {
                    expression = Expression.And(expression, searchExpression);
                }
            }

            var methodCall = Expression.Call(
                typeof(Queryable),
                "Where",
                new[] { parameter.Type },
                source.Expression,
                Expression.Lambda(expression, parameter));
            return (IQueryable<TEntity>)source.Provider.CreateQuery(methodCall);
        }

        private static IEnumerable<string> GetEntityProperties<TEntity>()
        {
            var type = typeof(TEntity);
            var properties = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetField | BindingFlags.GetField)
                .Where(prop => prop.PropertyType.IsPrimitive ||
                               prop.PropertyType.IsEnum ||
                               prop.PropertyType.IsString() ||
                               prop.PropertyType.IsDateTime());
            return properties.Select(property => property.Name);
        }

        private static string[] GetEntityProperties<TEntity>(string[] navigationPropertyPaths)
        {
            IEnumerable<string> items = GetEntityProperties<TEntity>()
                .Where(item => navigationPropertyPaths.Contains(item, StringComparer.InvariantCultureIgnoreCase));

            return items.ToArray();
        }

        private static BinaryExpression GetEqualExpression(ParameterExpression parameter, PropertyInfo property, object value)
        {
            var memberExpression = Expression.PropertyOrField(parameter, property.Name);
            var valueExpression = Expression.Constant(value, value.GetType());
            var equalExpression = Expression.Equal(memberExpression, valueExpression);
            return equalExpression;
        }

        private static BinaryExpression GetEqualExpression(ParameterExpression parameter, string propertyName, object value)
        {
            var memberExpression = Expression.PropertyOrField(parameter, propertyName);
            var valueExpression = Expression.Constant(value, value.GetType());
            var equalExpression = Expression.Equal(memberExpression, valueExpression);
            return equalExpression;
        }

        private static BinaryExpression? GetIntegerExpression(ParameterExpression parameter, PropertyInfo property, string searchString)
        {
            if (int.TryParse(searchString, out int value))
            {
                return GetEqualExpression(parameter, property, value);
            }

            return null;
        }

        private static IEnumerable<PropertyInfo> GetSearchableProperties(Type type)
        {
            var supportedTypes = new[] { typeof(string), typeof(int) };
            return type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(prop => supportedTypes.Contains(prop.PropertyType) && prop.CanWrite);
        }

        private static Expression? GetSearchExpression(ParameterExpression parameter, PropertyInfo property, string searchString)
        {
            if (property.PropertyType == typeof(string))
            {
                return GetStringExpression(parameter, property, searchString);
            }
            else if (property.PropertyType == typeof(int))
            {
                return GetIntegerExpression(parameter, property, searchString);
            }

            throw new InvalidOperationException();
        }

        private static MethodCallExpression GetStringExpression(ParameterExpression parameter, PropertyInfo property, string searchString)
        {
            var memberExpression = Expression.PropertyOrField(parameter, property.Name);
            var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var valueExpression = Expression.Constant(searchString, typeof(string));
            var containsExpression = Expression.Call(memberExpression, methodInfo, valueExpression);
            return containsExpression;
        }

        private static IOrderedQueryable<T> OrderByUsing<T>(this IQueryable<T> source, string memberPath, string method)
        {
            var parameter = Expression.Parameter(typeof(T), "item");
            var member = memberPath.Split('.')
                .Aggregate((Expression)parameter, Expression.PropertyOrField);
            var keySelector = Expression.Lambda(member, parameter);
            var methodCall = Expression.Call(
                typeof(Queryable),
                method,
                new[] { parameter.Type, member.Type },
                source.Expression,
                Expression.Quote(keySelector));
            return (IOrderedQueryable<T>)source.Provider.CreateQuery(methodCall);
        }

        private static void ValidateInclude<TEntity>(AdvancedFilter advancedFilter)
        {
            if (advancedFilter.Include != null && !string.IsNullOrWhiteSpace(advancedFilter.Include))
            {
                var propertyNames = advancedFilter.Include.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                propertyNames = advancedFilter.PropertyMappings.MapProperties(propertyNames).ToArray();
                ValidateInclude<TEntity>(propertyNames);
            }
        }

        private static void ValidateInclude<TEntity>(string[] propertyNames)
        {
            foreach (string propertyName in propertyNames)
            {
                if (!GetEntityProperties<TEntity>().Contains(propertyName, StringComparer.InvariantCultureIgnoreCase))
                {
                    string message = string.Format(ExceptionResource.CannotInclude, propertyName);
                    throw new ValidationException(message, nameof(AdvancedFilter.Include));
                }
            }
        }

        private static void ValidateOrderBy<TEntity>(OrderByFilter orderByFilter)
        {
            if (orderByFilter.OrderBy != null && !string.IsNullOrWhiteSpace(orderByFilter.OrderBy))
            {
                var orderBy = orderByFilter.PropertyMappings.MapProperty(orderByFilter.OrderBy);
                ValidateOrderBy<TEntity>(orderBy);
            }
        }

        private static void ValidateOrderBy<TEntity>(string orderBy)
        {
            var type = typeof(TEntity);
            var fields = orderBy.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var field in fields)
            {
                var s = field.Capitalize();
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                PropertyInfo? property = properties.FirstOrDefault(e => e.Name == s) ??
                    properties.FirstOrDefault(e => string.Equals(e.Name, s, StringComparison.OrdinalIgnoreCase));

                if (property == null)
                {
                    string message = string.Format(ExceptionResource.CannotOrderBy, orderBy);
                    throw new ValidationException(message, nameof(AdvancedFilter.OrderBy));
                }

                type = property.PropertyType;
            }
        }
    }
}
