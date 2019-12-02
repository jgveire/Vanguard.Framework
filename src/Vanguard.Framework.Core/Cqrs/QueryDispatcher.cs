namespace Vanguard.Framework.Core.Cqrs
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The query dispatcher.
    /// </summary>
    /// <seealso cref="IQueryDispatcher" />
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly ILogger<QueryDispatcher>? _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryDispatcher"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            ServiceProvider = Guard.ArgumentNotNull(serviceProvider, nameof(serviceProvider));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryDispatcher" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="logger">The logger.</param>
        public QueryDispatcher(IServiceProvider serviceProvider, ILogger<QueryDispatcher> logger)
        {
            ServiceProvider = Guard.ArgumentNotNull(serviceProvider, nameof(serviceProvider));
            _logger = Guard.ArgumentNotNull(logger, nameof(logger));
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

            var queryType = query.GetType();
            _logger?.LogDebug($"Dispatch query: {queryType.Name}");

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

            var queryType = query.GetType();
            _logger?.LogDebug($"Dispatch query: {queryType.Name}");

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
            var queryType = typeof(TQuery);
            _logger?.LogDebug($"Dispatch query: {queryType.Name}");

            var queryHandler = ServiceProvider.GetRequiredService<IQueryHandler<TResult, TQuery>>();
            return queryHandler.Retrieve(query);
        }

        /// <inheritdoc />
        public async Task<TResult> DispatchAsync<TResult, TQuery>(TQuery query)
            where TQuery : IAsyncQuery<TResult>
        {
            Guard.ArgumentNotNull(query, nameof(query));
            var queryType = typeof(TQuery);
            _logger?.LogDebug($"Dispatch query: {queryType.Name}");

            var queryHandler = ServiceProvider.GetRequiredService<IAsyncQueryHandler<TResult, TQuery>>();
            return await queryHandler.RetrieveAsync(query);
        }
    }
}
