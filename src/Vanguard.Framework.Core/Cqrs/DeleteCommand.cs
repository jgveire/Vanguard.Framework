namespace Vanguard.Framework.Core.Cqrs
{
    /// <summary>
    /// The delete command class.
    /// </summary>
    /// <remarks>
    /// TModel is used to make the command unique so that
    /// the command dispatcher can find the appropiate command handler.
    /// </remarks>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="Vanguard.Framework.Core.Cqrs.ICommand" />
    public class DeleteCommand<TModel> : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCommand{TModel}"/> class.
        /// </summary>
        /// <param name="id">The identifier of the entity that needs to be deleted.</param>
        public DeleteCommand(object id)
        {
            Guard.ArgumentNotNull(id, nameof(id));
            Id = id;
        }

        /// <summary>
        /// Gets the identifier of the entity that needs to be deleted.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public object Id { get; }
    }
}
