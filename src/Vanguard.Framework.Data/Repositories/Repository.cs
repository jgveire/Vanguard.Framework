using Microsoft.EntityFrameworkCore;
using Vanguard.Framework.Core.Repositories;

namespace Vanguard.Framework.Data.Repositories
{
    /// <summary>
    /// The repository class.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="ReadRepository{TEntity}" />
    /// <seealso cref="IRepository{TEntity}" />
    public class Repository<TEntity> : ReadRepository<TEntity>, IRepository<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public Repository(DbContext dbContext)
            : base(dbContext)
        {
            DbContext.ChangeTracker.AutoDetectChangesEnabled = true;
            DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        /// <summary>
        /// Saves all changes.
        /// </summary>
        public void Save()
        {
            DbContext.SaveChanges();
        }
    }
}
