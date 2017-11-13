using System;
using Vanguard.Framework.Data.Entities;
using Vanguard.Framework.Data.Managers;

namespace ExampleData
{
    using ExampleData.Entities;
    using Microsoft.EntityFrameworkCore;
    using Vanguard.Framework.Core.DomainEvents;
    using Vanguard.Framework.Data;

    public class ExampleContext : DbContextBase
    {
        public ExampleContext(IEventDispatcher eventDispatcher)
            : base(eventDispatcher)
        {
        }

        public ExampleContext(IEventDispatcher eventDispatcher, DbContextOptions<ExampleContext> options)
            : base(eventDispatcher, options)
        {
        }

        public DbSet<AuditEntry> AuditEntries { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Garage> Garages { get; set; }

        public override int SaveChanges()
        {
            var auditManager = new AuditManager(this);
            auditManager.CreateAuditRecords("Test", DateTime.UtcNow);
            return base.SaveChanges();
        }
    }
}
