namespace Vanguard.Framework.Core.Cqrs
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The command dispatcher.
    /// </summary>
    /// <seealso cref="ICommandDispatcher" />
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly ILogger<CommandDispatcher>? _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandDispatcher"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            ServiceProvider = Guard.ArgumentNotNull(serviceProvider, nameof(serviceProvider));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandDispatcher" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="logger">The logger.</param>
        public CommandDispatcher(IServiceProvider serviceProvider, ILogger<CommandDispatcher> logger)
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
        public void Dispatch<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            Guard.ArgumentNotNull(command, nameof(command));
            var commandType = typeof(TCommand);
            _logger?.LogDebug($"Dispatch command: {commandType.Name}");

            var commandHandler = ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            commandHandler.Execute(command);
        }

        /// <inheritdoc />
        public Task DispatchAsync<TCommand>(TCommand command)
            where TCommand : IAsyncCommand
        {
            Guard.ArgumentNotNull(command, nameof(command));
            var commandType = typeof(TCommand);
            _logger?.LogDebug($"Dispatch command: {commandType.Name}");

            var commandHandler = ServiceProvider.GetRequiredService<IAsyncCommandHandler<TCommand>>();
            return commandHandler.ExecuteAsync(command);
        }
    }
}
