namespace Vanguard.Framework.Core.Cqrs
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;

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

        /// <inheritdoc />
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

        /// <inheritdoc />
        public async Task<TResult> DispatchAsync<TResult>(IAsyncQuery<TResult> query)
        {
            Guard.ArgumentNotNull(query, nameof(query));

            // Retriever query handler.
            Type genericType = typeof(IAsyncQueryHandler<,>);
            Type[] typeArguments = { typeof(TResult), query.GetType() };
            Type queryHandlerType = genericType.MakeGenericType(typeArguments);
            var queryHandler = ServiceProvider.GetRequiredService(queryHandlerType);

            // Invoke retrieve method.
            MethodInfo retrieveMethod = queryHandlerType.GetMethod("RetrieveAsync");
            var result = (Task<TResult>)retrieveMethod.Invoke(queryHandler, new object[] { query });
            return await result;
        }

        /// <inheritdoc />
        public TResult Dispatch<TResult, TQuery>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            Guard.ArgumentNotNull(query, nameof(query));
            var queryHandler = ServiceProvider.GetRequiredService<IQueryHandler<TResult, TQuery>>();
            return queryHandler.Retrieve(query);
        }

        /// <inheritdoc />
        public async Task<TResult> DispatchAsync<TResult, TQuery>(TQuery query)
            where TQuery : IAsyncQuery<TResult>
        {
            Guard.ArgumentNotNull(query, nameof(query));
            var queryHandler = ServiceProvider.GetRequiredService<IAsyncQueryHandler<TResult, TQuery>>();
            return await queryHandler.RetrieveAsync(query);
        }
    }
}
