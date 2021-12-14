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
            var genericType = typeof(IQueryHandler<,>);
            Type[] typeArguments = { typeof(TResult), query.GetType() };
            var queryHandlerType = genericType.MakeGenericType(typeArguments);
            var queryHandler = ServiceProvider.GetRequiredService(queryHandlerType);

            var queryType = query.GetType();
            _logger?.LogDebug($"Dispatch query: {queryType.Name}");

            // Invoke retrieve method.
            var retrieveMethod = queryHandlerType.GetMethod("Retrieve");
            if (retrieveMethod == null)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return default;
#pragma warning restore CS8603 // Possible null reference return.
            }

            var result = (TResult)retrieveMethod.Invoke(queryHandler, new object[] { query });
#pragma warning disable CS8603 // Possible null reference return.
            return result;
#pragma warning restore CS8603 // Possible null reference return.
        }

        /// <inheritdoc />
        public Task<TResult> DispatchAsync<TResult>(IAsyncQuery<TResult> query)
        {
            Guard.ArgumentNotNull(query, nameof(query));

            // Retriever query handler.
            var genericType = typeof(IAsyncQueryHandler<,>);
            Type[] typeArguments = { typeof(TResult), query.GetType() };
            var queryHandlerType = genericType.MakeGenericType(typeArguments);
            var queryHandler = ServiceProvider.GetRequiredService(queryHandlerType);

            var queryType = query.GetType();
            _logger?.LogDebug($"Dispatch query: {queryType.Name}");

            // Invoke retrieve method.
            var retrieveMethod = queryHandlerType.GetMethod("RetrieveAsync");
            if (retrieveMethod == null)
            {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                return Task.FromResult(default(TResult));
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
            }

            var result = retrieveMethod.Invoke(queryHandler, new object[] { query }) as Task<TResult>;
            if (result == null)
            {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                return Task.FromResult(default(TResult));
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
            }

            return result;
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
        public Task<TResult> DispatchAsync<TResult, TQuery>(TQuery query)
            where TQuery : IAsyncQuery<TResult>
        {
            Guard.ArgumentNotNull(query, nameof(query));
            var queryType = typeof(TQuery);
            _logger?.LogDebug($"Dispatch query: {queryType.Name}");

            var queryHandler = ServiceProvider.GetRequiredService<IAsyncQueryHandler<TResult, TQuery>>();
            return queryHandler.RetrieveAsync(query);
        }
    }
}
