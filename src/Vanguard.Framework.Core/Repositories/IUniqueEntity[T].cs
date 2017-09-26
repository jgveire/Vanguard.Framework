namespace Vanguard.Framework.Core.Repositories
{
    /// <summary>
    /// The unique entity interface.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <seealso cref="IDataEntity" />
    public interface IUniqueEntity<TIdentifier> : IDataEntity
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
