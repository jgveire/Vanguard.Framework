namespace Vanguard.Framework.Data.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Vanguard.Framework.Core.Repositories;
    using Vanguard.Framework.Data.Builders;
    using Vanguard.Framework.Data.Entities;
    using Vanguard.Framework.Data.Helpers;

    /// <inheritdoc />
    public class AuditManager : IAuditManager
    {
        /// <inheritdoc/>
        public ICollection<AuditEntry> CreateAuditEntries(string? userId, ChangeTracker changeTracker)
        {
            var items = new List<AuditEntry>();
            var utcChangeDate = DateTime.UtcNow;
            var entries = changeTracker
               .Entries<IAuditable>()
               .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
               .ToList();

            foreach (var entry in entries)
            {
                var auditEntry = new AuditEntry
                {
                    AuditType = GetAuditType(entry.State),
                    EntityId = entry.Entity.EntityId,
                    EntityName = entry.Entity.GetType().Name,
                    UserId = userId ?? "Unknown",
                    UtcDate = utcChangeDate,
                    Entity = SerializeEntity(entry),
                };
                items.Add(auditEntry);
            }

            return items;
        }

        private AuditType GetAuditType(EntityState state)
        {
            switch (state)
            {
                case EntityState.Deleted:
                    return AuditType.Delete;
                case EntityState.Modified:
                    return AuditType.Update;
                case EntityState.Added:
                    return AuditType.Insert;
                default:
                    throw new NotSupportedException($"The state {state} is not supported.");
            }
        }

        private string SerializeEntity(EntityEntry<IAuditable> entry)
        {
            var jsonBuilder = new JsonBuilder();
            foreach (var propertyEntry in entry.Properties)
            {
                if (TypeHelper.IsByteArray(propertyEntry.Metadata.ClrType))
                {
                    continue;
                }

                var name = propertyEntry.Metadata.Name;
                jsonBuilder.AddProperty(name, propertyEntry.Metadata.ClrType, propertyEntry.CurrentValue);
            }

            return jsonBuilder.ToString();
        }
    }
}
