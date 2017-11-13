namespace Vanguard.Framework.Data.Entities
{
    /// <summary>
    /// The audit type enumeration.
    /// </summary>
    public enum AuditType
    {
        /// <summary>
        /// Represents an record that has been added.
        /// </summary>
        Insert = 0,

        /// <summary>
        /// Represents an record that has been updated.
        /// </summary>
        Update = 1,

        /// <summary>
        /// Represents an record that has been deleted.
        /// </summary>
        Delete = 2
    }
}