namespace Vanguard.Framework.Core.Models
{
    /// <summary>
    /// The unique entity interface.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    public interface IUniqueModel<TIdentifier>
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
