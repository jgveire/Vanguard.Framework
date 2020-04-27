namespace Vanguard.Framework.Data.Managers
{
    using System.Collections.Generic;

    /// <summary>
    /// The audit manager.
    /// </summary>
    public interface IAuditManager
    {
        /// <summary>
        /// Creates the audit entries and adds them to the database context.
        /// </summary>
        /// <param name="userId">The user identifier that is saved with the audit entry.</param>
        /// <param name="auditRecords">The audit records.</param>
        void CreateAuditEntries(string? userId, ICollection<AuditRecord> auditRecords);

        /// <summary>
        /// Gets the audit records.
        /// </summary>
        /// <returns>A collection of audit records.</returns>
        ICollection<AuditRecord> GetAuditRecords();
    }
}