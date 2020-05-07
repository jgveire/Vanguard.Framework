namespace Vanguard.Framework.Data.Managers
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Vanguard.Framework.Data.Entities;

    /// <summary>
    /// The audit manager.
    /// </summary>
    public interface IAuditManager
    {
        /// <summary>
        /// Creates the audit entries and adds them to the database context.
        /// </summary>
        /// <param name="userId">The user identifier that is saved with the audit entry.</param>
        /// <param name="changeTracker">The change tracker.</param>
        /// <returns>A collection of audit entries.</returns>
        ICollection<AuditEntry> CreateAuditEntries(string? userId, ChangeTracker changeTracker);
    }
}