namespace Vanguard.Framework.Core.Cqrs
{
    using System.Threading.Tasks;

    /// <summary>
    /// The command dispatcher interface.
    /// </summary>
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Dispatches the specified command.
        /// </summary>
        /// <typeparam name="TCommand">The type of the command.</typeparam>
        /// <param name="command">The command.</param>
        void Dispatch<TCommand>(TCommand command)
            where TCommand : ICommand;

        /// <summary>
        /// Dispatches the specified command asynchronously.
        /// </summary>
        /// <typeparam name="TCommand">The type of the command.</typeparam>
        /// <param name="command">The command.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DispatchAsync<TCommand>(TCommand command)
            where TCommand : IAsyncCommand;
    }
}
