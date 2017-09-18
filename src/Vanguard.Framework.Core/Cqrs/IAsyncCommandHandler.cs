using System.Threading.Tasks;

namespace Vanguard.Framework.Core.Cqrs
{
    /// <summary>
    /// The asynchronous command handler interface.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    public interface IAsyncCommandHandler<in TCommand>
        where TCommand : ICommand
    {
        /// <summary>
        /// Executes the specified command asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Execute(TCommand command);
    }
}
