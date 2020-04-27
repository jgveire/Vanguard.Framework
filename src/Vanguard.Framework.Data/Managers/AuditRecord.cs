namespace Vanguard.Framework.Data.Managers
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Vanguard.Framework.Core.Repositories;
    using Vanguard.Framework.Data.Entities;

    /// <summary>
    /// The audit record.
    /// </summary>
    public class AuditRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditRecord"/> class.
        /// </summary>
        /// <param name="state">The entity state.</param>
        /// <param name="entityEntry">The entity entry.</param>
        public AuditRecord(EntityState state, EntityEntry<IAuditable> entityEntry)
        {
            State = state;
            EntityEntry = entityEntry ?? throw new ArgumentNullException(nameof(entityEntry));
        }

        /// <summary>
        /// Gets the audit type.
        /// </summary>
        public AuditType AuditType => GetAuditType();

        /// <summary>
        /// Gets the entity.
        /// </summary>
        public IAuditable Entity => EntityEntry.Entity;

        /// <summary>
        /// Gets the entity entry.
        /// </summary>
        public EntityEntry<IAuditable> EntityEntry { get; private set; }

        /// <summary>
        /// Gets the entity state.
        /// </summary>
        public EntityState State { get; private set; }

        private AuditType GetAuditType()
        {
            switch (State)
            {
                case EntityState.Deleted:
                    return AuditType.Delete;
                case EntityState.Modified:
                    return AuditType.Update;
                case EntityState.Added:
                    return AuditType.Insert;
                default:
                    throw new NotSupportedException($"The state {State} is not supported.");
            }
        }
    }
}
