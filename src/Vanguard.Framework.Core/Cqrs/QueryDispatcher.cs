using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Vanguard.Framework.Core.Cqrs
{
    /// <summary>
    /// The query dispatcher class.
    /// </summary>
    /// <seealso cref="IQueryDispatcher" />
    public class QueryDispatcher : IQueryDispatcher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryDispatcher"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            Guard.ArgumentNotNull(serviceProvider, nameof(serviceProvider));
            ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Gets the service provider.
        /// </summary>
        /// <value>
        /// The service provider.
        /// </value>
        protected IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Dispatches the specified query.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="query">The query.</param>
        /// <returns>The query handler result.</returns>
        public TResult Dispatch<TResult>(IQuery<TResult> query)
        {
            Guard.ArgumentNotNull(query, nameof(query));

            // Retriever query handler.
            Type genericType = typeof(IQueryHandler<,>);
            Type[] typeArguments = { typeof(TResult), query.GetType() };
            Type queryHandlerType = genericType.MakeGenericType(typeArguments);
            var queryHandler = ServiceProvider.GetRequiredService(queryHandlerType);

            // Invoke retrieve method.
            MethodInfo retrieveMethod = queryHandlerType.GetMethod("Retrieve");
            var result = (TResult)retrieveMethod.Invoke(queryHandler, new object[] { query });
            return result;
        }

        /// <summary>
        /// Dispatches the specified query.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TQuery">The type of the query.</typeparam>
        /// <param name="query">The query.</param>
        /// <returns> The query handler result. </returns>
        public TResult Dispatch<TResult, TQuery>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            Guard.ArgumentNotNull(query, nameof(query));
            var queryHandler = ServiceProvider.GetRequiredService<IQueryHandler<TResult, TQuery>>();
            return queryHandler.Retrieve(query);
        }
    }
}
