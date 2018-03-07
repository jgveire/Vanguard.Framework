namespace Vanguard.Framework.Core.Cqrs
{
    using System.Threading.Tasks;

    /// <summary>
    /// The asynchronous query handler interface.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    public interface IAsyncQueryHandler<TResult, in TQuery>
        where TQuery : IAsyncQuery<TResult>
    {
        /// <summary>
        /// Retrieves the result for the specified query asynchronously.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The query result asynchronously.</returns>
        Task<TResult> RetrieveAsync(TQuery query);
    }
}
