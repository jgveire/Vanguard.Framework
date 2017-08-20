namespace Vanguard.Framework.Core.Cqrs
{
    /// <summary>
    /// The command handler interface.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        void Execute(TCommand command);
    }
}
