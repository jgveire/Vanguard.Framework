namespace Vanguard.Framework.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Vanguard.Framework.Core;
    using Vanguard.Framework.Core.Exceptions;
    using Vanguard.Framework.Core.Repositories;
    using Vanguard.Framework.Data.Extensions;
    using Vanguard.Framework.Data.Resources;

    /// <summary>
    /// The queryable extension method class.
    /// </summary>
    internal static class QueryableExtensions
    {
        /// <summary>
        /// Applies the find criteria to the querable.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="source">The queryable.</param>
        /// <param name="searchCriteria">The find criteria.</param>
        /// <returns>A collection of entities.</returns>
        public static IQueryable<TEntity> ApplySearch<TEntity>(
            this IQueryable<TEntity> source,
            SearchCriteria searchCriteria)
            where TEntity : class, IDataEntity
        {
            if (source == null || searchCriteria == null)
            {
                return source;
            }

            Validate<TEntity>(searchCriteria);

            // Search
            if (!string.IsNullOrEmpty(searchCriteria.Search))
            {
                source = source.Search(searchCriteria.Search);
            }

            // Order by
            if (!string.IsNullOrEmpty(searchCriteria.OrderBy))
            {
                source = source.OrderBy(searchCriteria.OrderBy, searchCriteria.SortOrder);
            }

            // Select
            if (!string.IsNullOrWhiteSpace(searchCriteria.Select))
            {
                string[] fields = GetEntityProperties<TEntity>(searchCriteria.Select);
                source = source.Select(fields);
            }

            // Paging
            source = source.GetPage(searchCriteria.Page, searchCriteria.PageSize);

            return source;
        }

        /// <summary>
        /// Gets the specified page of a sequence of elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="page">The page to retrieve.</param>
        /// <param name="pageSize">Size of an page.</param>
        /// <returns>A <see cref="IQueryable{TSource}"/> whose elements are restricted to the specified page.</returns>
        public static IQueryable<TSource> GetPage<TSource>(this IQueryable<TSource> source, int page, int pageSize)
        {
            int index = Math.Max(1, page) - 1;
            int skip = index * pageSize;
            return source.Skip(skip).Take(Math.Max(1, pageSize));
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="memberPath">The ordering.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns>A <see cref="IQueryable{TSource}"/> whose elements are sorted according to the specified ordering.</returns>
        public static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string memberPath, SortOrder sortOrder)
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
        /// <typeparam name="TSource">The type of the elements of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="memberPath">The ordering.</param>
        /// <returns>A <see cref="IQueryable{TSource}"/> whose elements are sorted according to the specified ordering.</returns>
        public static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string memberPath)
        {
            return source.OrderByUsing(memberPath, "OrderBy");
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order according to a key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="memberPath">The ordering.</param>
        /// <returns>A <see cref="IQueryable{TSource}"/> whose elements are sorted according to the specified ordering.</returns>
        public static IOrderedQueryable<TSource> OrderByDescending<TSource>(this IQueryable<TSource> source, string memberPath)
        {
            return source.OrderByUsing(memberPath, "OrderByDescending");
        }

        /// <summary>
        /// Searches for elements that have properties that contain the specified search string.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="searchString">The search string.</param>
        /// <returns>A <see cref="IQueryable{TSource}"/> whose elements are filtered according to the specified search string.</returns>
        public static IQueryable<TSource> Search<TSource>(this IQueryable<TSource> source, string searchString)
        {
            Guard.ArgumentNotNullOrEmpty(searchString, nameof(searchString));

            IEnumerable<PropertyInfo> properties = GetSearchableProperties(typeof(TSource));
            if (!properties.Any())
            {
                return source;
            }

            Expression expression = null;
            var parameter = Expression.Parameter(typeof(TSource), "item");
            foreach (var property in properties)
            {
                Expression searchExpression = GetSearchExpression(parameter, property, searchString);
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
            return (IQueryable<TSource>)source.Provider.CreateQuery(methodCall);
        }

        /// <summary>
        /// Selects the specified fields, all other fields will have their
        /// default value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="fields">The fields to select.</param>
        /// <returns>A <see cref="IQueryable{TSource}"/>.</returns>
        public static IQueryable<TSource> Select<TSource>(this IQueryable<TSource> source, params string[] fields)
        {
            if (fields == null || fields.Length == 0)
            {
                return source;
            }

            // Input parameter "item"
            var parameter = Expression.Parameter(typeof(TSource), "item");

            // New statement "new Data()"
            var newExpression = Expression.New(typeof(TSource));

            // Create initializers
            var bindings = fields
                .Select(item =>
                    {
                        // Property "Field1"
                        var propertyInfo = typeof(TSource).GetProperty(item);

                        // Original value "item.Field1"
                        var property = Expression.Property(parameter, propertyInfo);

                        // Set value "Field1 = item.Field1"
                        return Expression.Bind(propertyInfo, property);
                    });

            // Initialization "new Data { Field1 = item.Field1, Field2 = item.Field2 }"
            var memberInit = Expression.MemberInit(newExpression, bindings);

            // Expression "item => new Data { Field1 = item.Field1, Field2 = item.Field2 }"
            var lambda = Expression.Lambda<Func<TSource, TSource>>(memberInit, parameter);

            // Call select method "source.Select(item => new Data { Field1 = item.Field1, Field2 = item.Field2 })"
            var methodCall = Expression.Call(
                typeof(Queryable),
                "Select",
                new[] { parameter.Type, parameter.Type },
                source.Expression,
                lambda);

            // Create the final source
            return (IQueryable<TSource>)source.Provider.CreateQuery(methodCall);
        }

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="memberPath">The member path.</param>
        /// <returns>A <see cref="IQueryable{TSource}"/> whose elements are sorted according to the specified ordering.</returns>
        public static IOrderedQueryable<TSource> ThenBy<TSource>(this IOrderedQueryable<TSource> source, string memberPath)
        {
            return source.OrderByUsing(memberPath, "ThenBy");
        }

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in descending order according to a key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="memberPath">The member path.</param>
        /// <returns>A <see cref="IQueryable{TSource}"/> whose elements are sorted according to the specified ordering.</returns>
        public static IOrderedQueryable<TSource> ThenByDescending<TSource>(this IOrderedQueryable<TSource> source, string memberPath)
        {
            return source.OrderByUsing(memberPath, "ThenByDescending");
        }

        private static IEnumerable<string> GetEntityProperties<TEntity>()
        {
            var type = typeof(TEntity);
            var properties = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(prop => prop.PropertyType.IsPrimitive ||
                               prop.PropertyType.IsEnum ||
                               prop.PropertyType.IsString() ||
                               prop.PropertyType.IsDateTime());
            return properties.Select(property => property.Name);
        }

        private static string[] GetEntityProperties<TEntity>(string select)
        {
            IEnumerable<string> selectItems = select
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(item => item.Trim());
            IEnumerable<string> items = GetEntityProperties<TEntity>()
                .Where(item => selectItems.Contains(item, StringComparer.InvariantCultureIgnoreCase));

            return items.ToArray();
        }

        private static BinaryExpression GetEqualExpression(ParameterExpression parameter, PropertyInfo property, object value)
        {
            var memberExpression = Expression.PropertyOrField(parameter, property.Name);
            var valueExpression = Expression.Constant(value, value.GetType());
            var equalExpression = Expression.Equal(memberExpression, valueExpression);
            return equalExpression;
        }

        private static BinaryExpression GetIntegerExpression(ParameterExpression parameter, PropertyInfo property, string searchString)
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

        private static Expression GetSearchExpression(ParameterExpression parameter, PropertyInfo property, string searchString)
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

        private static void Validate<TEntity>(SearchCriteria searchCriteria)
        {
            if (!string.IsNullOrWhiteSpace(searchCriteria.OrderBy))
            {
                ValidateOrderBy<TEntity>(searchCriteria.OrderBy);
            }
        }

        private static void ValidateOrderBy<TEntity>(string orderBy)
        {
            if (!GetEntityProperties<TEntity>().Contains(orderBy, StringComparer.InvariantCultureIgnoreCase))
            {
                string message = string.Format(ExceptionResource.CannotOrderBy, orderBy);
                throw new ValidationException(message, nameof(SearchCriteria.OrderBy));
            }
        }
    }
}
