namespace Vanguard.Framework.Core.Cqrs
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// The command dispatcher class.
    /// </summary>
    /// <seealso cref="ICommandDispatcher" />
    public class CommandDispatcher : ICommandDispatcher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandDispatcher"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public CommandDispatcher(IServiceProvider serviceProvider)
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
        public void Dispatch<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            Guard.ArgumentNotNull(command, nameof(command));
            var commandHandler = ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            commandHandler.Execute(command);
        }

        /// <inheritdoc />
        public async Task DispatchAsync<TCommand>(TCommand command)
            where TCommand : IAsyncCommand
        {
            Guard.ArgumentNotNull(command, nameof(command));
            var commandHandler = ServiceProvider.GetRequiredService<IAsyncCommandHandler<TCommand>>();
            await commandHandler.ExecuteAsync(command);
        }
    }
}
