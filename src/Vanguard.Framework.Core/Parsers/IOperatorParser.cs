using System.Linq.Expressions;

namespace Vanguard.Framework.Core.Parsers
{
    internal interface IOperatorParser
    {
        string PropertyName { get; }
        string Value { get; }

        Expression CreateExpression<TEntity>(ParameterExpression parameter);
    }
}