namespace Vanguard.Framework.Data
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
        private readonly IAuditManager _auditManager;
        private readonly ICurrentUser _currentUser;
        private readonly IEventDispatcher _eventDispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextBase"/> class.
        /// </summary>
        public DbContextBase()
        {
            _auditManager = new AuditManager(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextBase"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public DbContextBase(DbContextOptions options)
            : base(options)
        {
            _auditManager = new AuditManager(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextBase"/> class.
        /// </summary>
        /// <param name="eventDispatcher">The event dispatcher.</param>
        public DbContextBase(IEventDispatcher eventDispatcher)
        {
            Guard.ArgumentNotNull(eventDispatcher, nameof(eventDispatcher));
            _auditManager = new AuditManager(this);
            _eventDispatcher = eventDispatcher;
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
            _auditManager = new AuditManager(this);
            _eventDispatcher = eventDispatcher;
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
            _auditManager = new AuditManager(this);
            _eventDispatcher = eventDispatcher;
            _currentUser = currentUser;
        }

        /// <inheritdoc />
        public override int SaveChanges()
        {
            var events = GetAndClearEvents();
            CreateAuditRecords();
            var result = base.SaveChanges();
            DispatchEvents(events);
            return result;
        }

        private void CreateAuditRecords()
        {
            if (_currentUser == null)
            {
                return;
            }

            _auditManager.CreateAuditRecords(_currentUser.UserId, DateTime.UtcNow);
        }

        private void DispatchEvents(IEnumerable<IDomainEvent> events)
        {
            if (_eventDispatcher == null)
            {
                return;
            }

            foreach (var domainEvent in events)
            {
                _eventDispatcher.Dispatch(domainEvent);
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
