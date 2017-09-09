namespace Vanguard.Framework.Core.Repositories
{
    /// <summary>
    /// The repository interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<TEntity> : IReadRepository<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(TEntity entity);
    }
}
