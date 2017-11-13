namespace Vanguard.Framework.Core.Repositories
{
    /// <summary>
    /// The auditable interface make a data entity autditable.
    /// </summary>
    public interface IAuditable
    {
        /// <summary>
        /// Gets a string representation of the data entity identifier.
        /// </summary>
        /// <value>
        /// A string representation of the data entity identifier.
        /// </value>
        string EntityId { get; }
    }
}
