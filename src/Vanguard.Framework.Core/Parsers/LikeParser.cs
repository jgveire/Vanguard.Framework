namespace Vanguard.Framework.Core.Parsers
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// The like operator parser.
    /// </summary>
    internal class LikeParser : IOperatorParser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LikeParser" /> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="ArgumentNullException">Thrown when propertyName or value is null.</exception>
        /// <exception cref="ArgumentException">Thrown when propertyName or value is an empty string.</exception>
        public LikeParser(string propertyName, string value)
        {
            PropertyName = Guard.ArgumentNotNullOrEmpty(propertyName, nameof(propertyName));
            Value = Guard.ArgumentNotNull(value, nameof(value));
        }

        /// <summary>
        /// Gets the operator the parser supports.
        /// </summary>
        /// <value>
        /// The operator the parser supports.
        /// </value>
        public static string Operator { get; } = "like";

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        public string PropertyName { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; }

        /// <summary>
        /// Parses the expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="parameter">The parameter to use in the expression.</param>
        /// <returns>
        /// A binary expression.
        /// </returns>
        /// <exception cref="FormatException">
        /// Thrown when the PropertyName does not exist or when the value could not be converted.
        /// </exception>
        public Expression CreateExpression<TEntity>(ParameterExpression parameter)
        {
            var property = GetProperty<TEntity>(PropertyName);
            var value = GetValue(property, Value);

            var memberExpression = Expression.PropertyOrField(parameter, property.Name);
            var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            if (methodInfo == null)
            {
                throw new InvalidOperationException("Could not find the Contains method.");
            }

            var valueExpression = Expression.Constant(value, typeof(string));
            var containsExpression = Expression.Call(memberExpression, methodInfo, valueExpression);
            return containsExpression;
        }

        private PropertyInfo GetProperty<TEntity>(string propertyName)
        {
            var type = typeof(TEntity);
            var property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
            {
                property = type
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .FirstOrDefault(p => p.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
            }

            if (property == null)
            {
                throw new FormatException($"The property {PropertyName} does not exist on the entity {typeof(TEntity).FullName}.");
            }

            return property;
        }

        private string? GetValue(PropertyInfo property, string value)
        {
            if (property.PropertyType != typeof(string))
            {
                throw new FormatException($"Cannot convert property {property.Name} because type {property.PropertyType} is not supported.");
            }

            return (string?)TypeConverters.Instance[property.PropertyType].Convert(value);
        }
    }
}