using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Vanguard.Framework.Core.Repositories;
using Vanguard.Framework.Data.Entities;
using Vanguard.Framework.Data.Helpers;

namespace Vanguard.Framework.Data.Managers
{
    /// <inheritdoc />
    public class AuditManager : IAuditManager
    {
        private readonly DbContext _dbContext;
        private readonly CultureInfo _cultureInfo = CreateCulture();

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditManager"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public AuditManager(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public void CreateAuditRecords(string userId, DateTime utcChangeDate)
        {
            Guard.ArgumentNotNullOrEmpty(userId, nameof(userId));
            CreateInsertAuditRecords(userId, utcChangeDate);
            CreateUpdateAuditRecords(userId, utcChangeDate);
            CreateDeleteAuditRecords(userId, utcChangeDate);
        }

        /// <inheritdoc />
        public void CreateDeleteAuditRecords(string userId, DateTime utcChangeDate, List<EntityEntry<IAuditable>> entityEntries)
        {
            CreateAuditRecords(AuditType.Delete, userId, utcChangeDate, entityEntries);
        }

        /// <inheritdoc />
        public void CreateInsertAuditRecords(string userId, DateTime utcChangeDate, List<EntityEntry<IAuditable>> entityEntries)
        {
            CreateAuditRecords(AuditType.Insert, userId, utcChangeDate, entityEntries);
        }

        /// <inheritdoc />
        public void CreateUpdateAuditRecords(string userId, DateTime utcChangeDate, List<EntityEntry<IAuditable>> entityEntries)
        {
            CreateAuditRecords(AuditType.Update, userId, utcChangeDate, entityEntries);
        }

        private static CultureInfo CreateCulture()
        {
            var cultureInfo = new CultureInfo("en-US");
            return cultureInfo;
        }

        private void CreateAuditRecords(AuditType auditType, string userId, DateTime utcChangeDate, List<EntityEntry<IAuditable>> entityEntries)
        {
            foreach (EntityEntry<IAuditable> entityEntry in entityEntries)
            {
                var auditEntry = new AuditEntry
                {
                    AuditType = auditType,
                    EntityId = entityEntry.Entity.EntityId,
                    EntityName = entityEntry.Entity.GetType().Name,
                    UserId = userId,
                    UtcDate = utcChangeDate,
                    Entity = SerializeEntity(entityEntry)
                };
                _dbContext.Add(auditEntry);
            }
        }

        private void CreateDeleteAuditRecords(string userId, DateTime utcChangeDate)
        {
            var deletedEntries = _dbContext
                .ChangeTracker
                .Entries<IAuditable>()
                .Where(e => e.State == EntityState.Deleted)
                .ToList();
            CreateDeleteAuditRecords(userId, utcChangeDate, deletedEntries);
        }

        private void CreateInsertAuditRecords(string userId, DateTime utcChangeDate)
        {
            var addedEntries = _dbContext
                .ChangeTracker
                .Entries<IAuditable>()
                .Where(e => e.State == EntityState.Added)
                .ToList();
            CreateInsertAuditRecords(userId, utcChangeDate, addedEntries);
        }

        private void CreateUpdateAuditRecords(string userId, DateTime utcChangeDate)
        {
            var updatedEntries = _dbContext
                .ChangeTracker
                .Entries<IAuditable>()
                .Where(e => e.State == EntityState.Modified)
                .ToList();
            CreateUpdateAuditRecords(userId, utcChangeDate, updatedEntries);
        }

        private string SerializeEntity(EntityEntry<IAuditable> entry)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{");
            foreach (PropertyEntry propertyEntry in entry.Properties)
            {
                if (jsonBuilder.Length > 1)
                {
                    jsonBuilder.Append(",");
                }

                string name = propertyEntry.Metadata.Name;
                var value = ConvertValue(propertyEntry.Metadata.ClrType, propertyEntry.CurrentValue);
                jsonBuilder.Append($"\"{name}\":{value}");
            }

            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }

        private string ConvertValue(Type valueType, object value)
        {
            if (value == null)
            {
                return "null";
            }

            string stringValue = Convert.ToString(value, _cultureInfo);
            if (TypeHelper.IsNumeric(valueType))
            {
                return stringValue;
            }
            else if (TypeHelper.IsBoolean(valueType))
            {
                return stringValue.ToLower();
            }
            else if (TypeHelper.IsDateTime(valueType))
            {
                return string.Format("\"{0:O}\"", value);
            }

            return $"\"{stringValue}\"";
        }
    }
}
