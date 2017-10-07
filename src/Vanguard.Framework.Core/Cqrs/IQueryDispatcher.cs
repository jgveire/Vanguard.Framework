using System.Threading.Tasks;

namespace Vanguard.Framework.Core.Cqrs
{
    /// <summary>
    /// The query dispatcher interface.
    /// </summary>
    public interface IQueryDispatcher
    {
        /// <summary>
        /// Dispatches the specified query.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="query">The query.</param>
        /// <returns>The query handler result.</returns>
        TResult Dispatch<TResult>(IQuery<TResult> query);

        /// <summary>
        /// Dispatches the specified query asynchronously.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="query">The query.</param>
        /// <returns>The query handler result asynchronously.</returns>
        Task<TResult> DispatchAsync<TResult>(IAsyncQuery<TResult> query);

        /// <summary>
        /// Dispatches the specified query.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TQuery">The type of the query.</typeparam>
        /// <param name="query">The query.</param>
        /// <returns>The query handler result.</returns>
        TResult Dispatch<TResult, TQuery>(TQuery query)
            where TQuery : IQuery<TResult>;

        /// <summary>
        /// Dispatches the specified query asynchronously.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TQuery">The type of the query.</typeparam>
        /// <param name="query">The query.</param>
        /// <returns>The query handler result asynchronously.</returns>
        Task<TResult> DispatchAsync<TResult, TQuery>(TQuery query)
            where TQuery : IAsyncQuery<TResult>;
    }
}
