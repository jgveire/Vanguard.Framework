using System.Linq;
using Microsoft.EntityFrameworkCore;
using Vanguard.Framework.Core.DomainEvents;

namespace Vanguard.Framework.Data
{
    /// <inheritdoc />
    public class DbContextBase : DbContext
    {
        private readonly IEventDispatcher _eventDispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextBase"/> class.
        /// </summary>
        public DbContextBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextBase"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public DbContextBase(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextBase"/> class.
        /// </summary>
        /// <param name="eventDispatcher">The event dispatcher.</param>
        public DbContextBase(IEventDispatcher eventDispatcher)
        {
            Guard.ArgumentNotNull(eventDispatcher, nameof(eventDispatcher));
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
            _eventDispatcher = eventDispatcher;
        }

        /// <inheritdoc />
        public override int SaveChanges()
        {
            DispatchEvents();
            return base.SaveChanges();
        }

        private void DispatchEvents()
        {
            if (_eventDispatcher == null)
            {
                return;
            }

            var entities = ChangeTracker.Entries<IDomainEntity>()
                .Select(entry => entry.Entity)
                .Where(entry => entry.Events.Any())
                .ToArray();

            foreach (var entity in entities)
            {
                var events = entity.Events.ToArray();
                entity.Events.Clear();
                foreach (var domainEvent in events)
                {
                    _eventDispatcher.Dispatch(domainEvent);
                }
            }
        }
    }
}
