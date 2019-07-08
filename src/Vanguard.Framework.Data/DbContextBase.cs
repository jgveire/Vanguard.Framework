﻿namespace Vanguard.Framework.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            Guard.ArgumentNotNull(eventDispatcher, nameof(eventDispatcher));
            AuditManager = new AuditManager(this);
            EventDispatcher = eventDispatcher;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextBase"/> class.
        /// </summary>
        /// <param name="eventDispatcher">The event dispatcher.</param>
        /// <param name="options">The options for this context.</param>
        public DbContextBase(IEventDispatcher eventDispatcher, DbContextOptions options)
            : base(options)
        {
            Guard.ArgumentNotNull(eventDispatcher, nameof(eventDispatcher));
            AuditManager = new AuditManager(this);
            EventDispatcher = eventDispatcher;
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
            Guard.ArgumentNotNull(eventDispatcher, nameof(eventDispatcher));
            Guard.ArgumentNotNull(currentUser, nameof(currentUser));
            AuditManager = new AuditManager(this);
            EventDispatcher = eventDispatcher;
            CurrentUser = currentUser;
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
        public override int SaveChanges()
        {
            var events = GetAndClearEvents();
            CreateAuditRecords();
            var result = base.SaveChanges();
            DispatchEvents(events);
            return result;
        }

        /// <summary>
        /// Creates the audit records.
        /// </summary>
        protected virtual void CreateAuditRecords()
        {
            if (CurrentUser == null || AuditManager == null)
            {
                return;
            }

            AuditManager.CreateAuditRecords(CurrentUser.UserId, DateTime.UtcNow);
        }

        private void DispatchEvents(IEnumerable<IDomainEvent> events)
        {
            if (EventDispatcher == null)
            {
                return;
            }

            foreach (var domainEvent in events)
            {
                EventDispatcher.Dispatch(domainEvent);
            }
        }

        private List<IDomainEvent> GetAndClearEvents()
        {
            var events = new List<IDomainEvent>();

            var entities = GetChangedDomainEntities();
            foreach (var entity in entities)
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
    }
}
