namespace Vanguard.Framework.Core.Cqrs
{
    /// <summary>
    /// The get query class.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="Vanguard.Framework.Core.Cqrs.ICommand" />
    public class GetQuery<TModel> : IQuery<TModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuery{TModel}"/> class.
        /// </summary>
        /// <param name="id">The identifier of the entity that needs to be retrieved.</param>
        public GetQuery(object id)
        {
            Guard.ArgumentNotNull(id, nameof(id));
            Id = id;
        }

        /// <summary>
        /// Gets the identifier of the entity that needs to be retrieved.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public object Id { get; }
    }
}
