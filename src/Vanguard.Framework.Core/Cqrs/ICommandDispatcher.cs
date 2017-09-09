namespace Vanguard.Framework.Core.Cqrs
{
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
    }
}
