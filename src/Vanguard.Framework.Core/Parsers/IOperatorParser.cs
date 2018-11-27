namespace Vanguard.Framework.Core.Parsers
{
    using System.Linq.Expressions;

    /// <summary>
    /// The operator parser.
    /// </summary>
    internal interface IOperatorParser
    {
        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        string PropertyName { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        string Value { get; }

        /// <summary>
        /// Creates the expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="parameter">The parameter.</param>
        /// <returns>An expression.</returns>
        Expression CreateExpression<TEntity>(ParameterExpression parameter);
    }
}