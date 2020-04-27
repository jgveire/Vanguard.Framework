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
        private readonly DbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditManager"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public AuditManager(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public void CreateAuditEntries(string? userId, ICollection<AuditRecord> auditRecords)
        {
            var utcChangeDate = DateTime.UtcNow;
            foreach (var record in auditRecords)
            {
                var auditEntry = new AuditEntry
                {
                    AuditType = record.AuditType,
                    EntityId = record.Entity.EntityId,
                    EntityName = record.Entity.GetType().Name,
                    UserId = userId ?? "Unknown",
                    UtcDate = utcChangeDate,
                    Entity = SerializeEntity(record.EntityEntry),
                };
                _dbContext.Add(auditEntry);
            }
        }

        /// <inheritdoc/>
        public ICollection<AuditRecord> GetAuditRecords()
        {
            return _dbContext
               .ChangeTracker
               .Entries<IAuditable>()
               .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
               .Select(e => new AuditRecord(e.State, e))
               .ToList();
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
