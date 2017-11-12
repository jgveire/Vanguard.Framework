namespace Vanguard.Framework.Core
{
    /// <summary>
    /// The auditable interface make a data entity autditable.
    /// </summary>
    public interface IAuditable
    {
        /// <summary>
        /// Gets a string representation of the identifier of the data entity.
        /// </summary>
        /// <value>
        /// A string representation of the identifier of the data entity.
        /// </value>
        string AuditId { get; }
    }
}
