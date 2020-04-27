namespace Vanguard.Framework.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Vanguard.Framework.Core;
    using Vanguard.Framework.Core.DomainEvents;
    using Vanguard.Framework.Data.Managers;

    /// <inheritdoc />
    public class DbContextBase : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextBase"/> class.
        /// </summary>
        public DbContextBase()
        {
            AuditManager = new AuditManager(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextBase"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public DbContextBase(DbContextOptions options)
            : base(options)
        {
            AuditManager = new AuditManager(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextBase"/> class.
        /// </summary>
        /// <param name="eventDispatcher">The event dispatcher.</param>
        public DbContextBase(IEventDispatcher eventDispatcher)
        {
            EventDispatcher = Guard.ArgumentNotNull(eventDispatcher, nameof(eventDispatcher));
            AuditManager = new AuditManager(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextBase"/> class.
        /// </summary>
        /// <param name="eventDispatcher">The event dispatcher.</param>
        /// <param name="options">The options for this context.</param>
        public DbContextBase(IEventDispatcher eventDispatcher, DbContextOptions options)
            : base(options)
        {
            EventDispatcher = Guard.ArgumentNotNull(eventDispatcher, nameof(eventDispatcher));
            AuditManager = new AuditManager(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextBase"/> class.
        /// </summary>
        /// <param name="eventDispatcher">The event dispatcher.</param>
        /// <param name="currentUser">The current user.</param>
        /// <param name="options">The options for this context.</param>
        public DbContextBase(IEventDispatcher eventDispatcher, ICurrentUser currentUser, DbContextOptions options)
            : base(options)
        {
            EventDispatcher = Guard.ArgumentNotNull(eventDispatcher, nameof(eventDispatcher));
            CurrentUser = Guard.ArgumentNotNull(currentUser, nameof(currentUser));
            AuditManager = new AuditManager(this);
        }

        /// <summary>
        /// Gets the audit manager.
        /// </summary>
        /// <value>
        /// The audit manager.
        /// </value>
        protected IAuditManager? AuditManager { get; }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        protected ICurrentUser? CurrentUser { get; }

        /// <summary>
        /// Gets the event dispatcher.
        /// </summary>
        /// <value>
        /// The event dispatcher.
        /// </value>
        protected IEventDispatcher? EventDispatcher { get; }

        /// <inheritdoc />
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var events = GetAndClearEvents();
            var records = AuditManager?.GetAuditRecords() ?? new List<AuditRecord>();
            var result = base.SaveChanges(acceptAllChangesOnSuccess);

            if (records.Any())
            {
                AuditManager?.CreateAuditEntries(CurrentUser?.UserId, records);
                base.SaveChanges(acceptAllChangesOnSuccess);
            }

            DispatchEvents(events);
            return result;
        }

        /// <inheritdoc />
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var events = GetAndClearEvents();
            var records = AuditManager?.GetAuditRecords() ?? new List<AuditRecord>();
            var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            if (records.Any())
            {
                AuditManager?.CreateAuditEntries(CurrentUser?.UserId, records);
                await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }

            await DispatchEventsAsync(events).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var events = GetAndClearEvents();
            var records = AuditManager?.GetAuditRecords() ?? new List<AuditRecord>();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken).ConfigureAwait(false);

            if (records.Any())
            {
                AuditManager?.CreateAuditEntries(CurrentUser?.UserId, records);
                await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken).ConfigureAwait(false);
            }

            await DispatchEventsAsync(events).ConfigureAwait(false);
            return result;
        }

        private void DispatchEvents(IEnumerable<object> events)
        {
            if (EventDispatcher == null)
            {
                return;
            }

            foreach (var @event in events)
            {
                if (@event is IDomainEvent domainEvent)
                {
                    EventDispatcher.Dispatch(domainEvent);
                }
                else if (@event is IAsyncDomainEvent asyncDomainEvent)
                {
                    throw new InvalidOperationException("Asynchronous domain events cannot be dispatched in the SaveChanges method, please make use of the SaveChangesAsync method.");
                }
            }
        }

        private async Task DispatchEventsAsync(IEnumerable<object> events)
        {
            if (EventDispatcher == null)
            {
                return;
            }

            foreach (var @event in events)
            {
                if (@event is IDomainEvent domainEvent)
                {
                    EventDispatcher.Dispatch(domainEvent);
                }
                else if (@event is IAsyncDomainEvent asyncDomainEvent)
                {
                    await EventDispatcher.DispatchAsync(asyncDomainEvent).ConfigureAwait(false);
                }
            }
        }

        private List<object> GetAndClearEvents()
        {
            var events = new List<object>();

            var entities = GetChangedDomainEntities();
            foreach (var entity in entities)
            {
                events.AddRange(entity.Events);
                entity.Events.Clear();
            }

            var asyncEntities = GetChangedAsyncDomainEntities();
            foreach (var entity in asyncEntities)
            {
                events.AddRange(entity.Events);
                entity.Events.Clear();
            }

            return events;
        }

        private IDomainEntity[] GetChangedDomainEntities()
        {
            var entities = ChangeTracker.Entries<IDomainEntity>()
                .Select(entry => entry.Entity)
                .Where(entry => entry.Events.Any())
                .ToArray();
            return entities;
        }

        private IAsyncDomainEntity[] GetChangedAsyncDomainEntities()
        {
            var entities = ChangeTracker.Entries<IAsyncDomainEntity>()
                .Select(entry => entry.Entity)
                .Where(entry => entry.Events.Any())
                .ToArray();
            return entities;
        }
    }
}
