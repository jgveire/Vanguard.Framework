using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Vanguard.Framework.Core.Repositories;

namespace Vanguard.Framework.Data.Managers
{
    /// <summary>
    /// The audit manager.
    /// </summary>
    public interface IAuditManager
    {
        /// <summary>
        /// Creates the audit records and adds them to the database context.
        /// </summary>
        /// <param name="userId">The user identifier that is saved with the audit entry.</param>
        /// <param name="utcChangeDate">A UTC date that is saved with the audit entry.</param>
        void CreateAuditRecords(string userId, DateTime utcChangeDate);

        /// <summary>
        /// Creates the delete audit records.
        /// </summary>
        /// <param name="userId">The user identifier that is saved with the audit entry.</param>
        /// <param name="utcChangeDate">A UTC date that is saved with the audit entry.</param>
        /// <param name="entityEntries">The entity entries for which a delete audit entry should be created.</param>
        void CreateDeleteAuditRecords(string userId, DateTime utcChangeDate, List<EntityEntry<IAuditable>> entityEntries);

        /// <summary>
        /// Creates the insert audit records.
        /// </summary>
        /// <param name="userId">The user identifier that is saved with the audit entry.</param>
        /// <param name="utcChangeDate">A UTC date that is saved with the audit entry.</param>
        /// <param name="entityEntries">The entity entries for which an insert audit entry should be created.</param>
        void CreateInsertAuditRecords(string userId, DateTime utcChangeDate, List<EntityEntry<IAuditable>> entityEntries);

        /// <summary>
        /// Creates the update audit records.
        /// </summary>
        /// <param name="userId">The user identifier that is saved with the audit entry.</param>
        /// <param name="utcChangeDate">A UTC date that is saved with the audit entry.</param>
        /// <param name="entityEntries">The entity entries for which an update audit entry should be created.</param>
        void CreateUpdateAuditRecords(string userId, DateTime utcChangeDate, List<EntityEntry<IAuditable>> entityEntries);
    }
}