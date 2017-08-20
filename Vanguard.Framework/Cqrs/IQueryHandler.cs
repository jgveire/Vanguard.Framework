namespace Vanguard.Framework.Core.Cqrs
{
    /// <summary>
    /// The query handler interface.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    public interface IQueryHandler<out TResult, in TQuery>
        where TQuery : IQuery<TResult>
    {
        /// <summary>
        /// Retrieves the result for the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The query result.</returns>
        TResult Retrieve(TQuery query);
    }
}
