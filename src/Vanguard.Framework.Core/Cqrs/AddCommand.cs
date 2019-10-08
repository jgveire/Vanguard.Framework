namespace Vanguard.Framework.Core.Cqrs
{
    /// <summary>
    /// The add command.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="Vanguard.Framework.Core.Cqrs.ICommand" />
    public class AddCommand<TModel> : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddCommand{TModel}"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public AddCommand(TModel model)
        {
            Model = Guard.ArgumentNotNull(model, nameof(model));
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public TModel Model { get; }
    }
}
