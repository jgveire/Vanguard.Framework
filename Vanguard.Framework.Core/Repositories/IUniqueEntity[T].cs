namespace Vanguard.Framework.Core.Repositories
{
    /// <summary>
    /// The unique entity interface.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <seealso cref="Vanguard.Framework.Core.Repositories.IEntity" />
    public interface IUniqueEntity<TIdentifier> : IEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        TIdentifier Id { get; set; }
    }
}
