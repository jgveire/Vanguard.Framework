namespace Vanguard.Framework.Core
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// The predicate builder.
    /// </summary>
    public static class PredicateBuilder
    {
        /// <summary>
        /// Adds and AND expression to the supplied source expression.
        /// </summary>
        /// <typeparam name="T">The type to perform AND expression on.</typeparam>
        /// <param name="source">The source expression.</param>
        /// <param name="expression">The expression that should be added to the source expression with the AND condition.</param>
        /// <returns>An expression.</returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> source, Expression<Func<T, bool>> expression)
        {
            var invokedExpr = Expression.Invoke(expression, source.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(source.Body, invokedExpr), source.Parameters);
        }

        /// <summary>
        /// Gets an expression that evaluates to false.
        /// </summary>
        /// <typeparam name="T">The type to perform expression on.</typeparam>
        /// <returns>An expression that evaluates false.</returns>
        public static Expression<Func<T, bool>> False<T>() => f => false;

        /// <summary>
        /// Adds and OR expression to the supplied source expression.
        /// </summary>
        /// <typeparam name="T">The type to perform OR expression on.</typeparam>
        /// <param name="source">The source expression.</param>
        /// <param name="expression">The expression that should be added to the source expression with the OR condition.</param>
        /// <returns>An expression.</returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> source, Expression<Func<T, bool>> expression)
        {
            var invokedExpression = Expression.Invoke(expression, source.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(source.Body, invokedExpression), source.Parameters);
        }

        /// <summary>
        /// Gets an expression that evaluates to true.
        /// </summary>
        /// <typeparam name="T">The type to perform expression on.</typeparam>
        /// <returns>An expression that evaluates true.</returns>
        public static Expression<Func<T, bool>> True<T>() => f => true;
    }
}
